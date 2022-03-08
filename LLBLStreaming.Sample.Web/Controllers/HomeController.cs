using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using AW.Dal.EntityClasses;
using AW.Dal.Linq;
using AW.Dal.SqlServer;
using AW.Helper;
using AW.Helper.LLBL;
using AW.Services;
using LLBLStreaming.Sample.Web.Models;
using log4net;
using Newtonsoft.Json;
using SD.LLBLGen.Pro.LinqSupportClasses;

namespace LLBLStreaming.Sample.Web.Controllers
{
  /// <summary>
  ///   Sandbox controller - used for prototyping and research.
  ///   This controller will not be accessible on customer installations
  /// </summary>
  /// <see cref="http://medium.com/swlh/uploadig-large-files-as-chunks-using-reactjs-net-core-2e6e00e13875" />
  [SessionState(SessionStateBehavior.Disabled)]
  public class HomeController : Controller
  {

    protected internal const string ComponentName = "PrototypeLandingPage";
    const string ControllerName = "Home";
    static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

    static readonly string UploadFolder = Path.Combine(UploadedFileModelBinder.AttachmentsTemporyDirectoryPath, "Upload");

    public HomeController()
    {
    }

    protected JsonNetResult JsonNet(object data, string contentType = null, Encoding contentEncoding = null, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet)
    {
      return new()
      {
        Data = data,
        ContentType = contentType,
        ContentEncoding = contentEncoding,
        JsonRequestBehavior = behavior
      };
    }

    protected async Task<JsonNetResult> JsonNetAsync<T>(Task<T> task)
    {
      return JsonNet(await task.ConfigureAwait(GeneralHelper.ContinueOnCapturedContext));
    }

    protected NameValueCollection RequestParams => HttpContext.Request.RequestType == "POST" ? HttpContext.Request.Form : HttpContext.Request.QueryString;

    public ActionResult Component(ReactLayoutModel model)
    {
      if (Request.IsAjaxRequest())
      {
        if (!string.IsNullOrEmpty(RequestParams["issecondary"]))
          if (model.PrimaryComponentPageModel is IReactPageModelBase pageMOdel)
          {
            pageMOdel.ReactComponentName = model.PrimaryComponentName;
            return JsonNet(pageMOdel);
          }

        return JsonNet(model);
      }

      return View("ReactComponent", model);
    }

    // GET: Administration/Prototype
    public ActionResult Index()
    {
      Logger.DebugMethod();
      var chunkSizeMegaBytes = ConfigurationManager.AppSettings["ChunkSizeMegaBytes"];
      chunkSizeMegaBytes = StringHelper.Coalesce(chunkSizeMegaBytes, "3");
      var reactPageModelCore = new ReactPageModelCore("Prototype")
      {
        Urls = new UrlDictionary
        {
          { PrototypeUrls.UploadChunks, Url.Action(nameof(UploadChunksGuid), ControllerName) },
          { PrototypeUrls.UploadComplete, Url.Action(nameof(UploadCompleteUploadedFileAsync), ControllerName) },
          { PrototypeUrls.StreamJson, Url.Action(nameof(StreamJson), ControllerName) },
          { PrototypeUrls.StreamText, Url.Action(nameof(StreamInts), ControllerName) },
          { PrototypeUrls.StreamTextAsync, Url.Action(nameof(StreamIntsAsync), ControllerName) },
          // ReSharper disable once StringLiteralTypo
          { PrototypeUrls.StreamInts, Url.Action(nameof(StreamIntsBinary), ControllerName) },
          { PrototypeUrls.GetJson, Url.Action(nameof(GetJson), ControllerName) },
          { PrototypeUrls.GetAttachments, Url.Action(nameof(GetAttachmentsAsync), ControllerName) },
          { PrototypeUrls.DownloadAttachment, Url.Action(nameof(DownloadAttachmentAsync), ControllerName) },
          { PrototypeUrls.ChunkSizeMegaBytes, chunkSizeMegaBytes },
        }
      };
      return Component(new ReactLayoutModel(ComponentName, reactPageModelCore));
    }

    [HttpPost]
    public async Task<JsonResult> UploadChunks(string chunkNumber, string fileName)
    {
      Logger.DebugMethod(chunkNumber, fileName, UploadFolder);
      return JsonNet(await AttachmentHelper.UploadChunks(chunkNumber, fileName, UploadFolder, Request.InputStream).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext));
    }

    [HttpPost]
    public async Task<JsonResult> UploadChunksGuid(CancellationToken cancellationToken, ushort? chunkNumber, Guid fileGuid)
    {
      Logger.DebugMethod(chunkNumber, fileGuid, UploadFolder);
      return JsonNet(await AttachmentHelper.UploadChunks(chunkNumber, fileGuid, UploadFolder, Request.InputStream).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext));
    }

    [HttpPost]
    public JsonResult UploadComplete(Guid fileGuid, string fileName)
    {
      Logger.DebugMethod(fileName, fileGuid, UploadFolder);
      return JsonNet(AttachmentHelper.OnChunkedUploadComplete(fileGuid, UploadFolder, fileName, UploadedFileModelBinder.AttachmentsTemporyDirectoryPath));
    }

    public async Task<ActionResult> UploadCompleteUploadedFileAsync(UploadedFile[] uploadedFiles, Guid? uid = null, string actionType = "Upload",
      string[] attachment = null, string description = null,
      bool? isSecure = null)
    {
      var uploadedFilesCount = uploadedFiles.Length;
      Logger.DebugMethod(uploadedFilesCount, uid, actionType, attachment, description, isSecure);
      Logger.InfoFormat("Uploading of {0} files", uploadedFilesCount);

      Response.ContentType = "text/plain";
      Response.BufferOutput = false;
      var cancellationToken = Response.ClientDisconnectedToken;
      using var streamWriter = new StreamWriter(Response.OutputStream) { AutoFlush = false };

      foreach (var uploadedFile in uploadedFiles)
        if (Response.IsClientConnected && !cancellationToken.IsCancellationRequested)
        {
          var fileSize = GeneralHelper.FormatToBytesKB(uploadedFile.FileSize);
          Logger.InfoFormat("Uploading: {0} {1}", uploadedFile.FileName, fileSize);
          var lastProgressReport = DateTime.MinValue;
          var minGap = new TimeSpan(0, 0, 0, 0, 200);
          long lastProgress = 0;
          // The Progress<T> constructor captures our UI context,
          //  so the lambda will be run on the UI thread.
          var progress = new Progress<long>(value =>
          {
            var now = DateTime.Now;
            if (Response.IsClientConnected && lastProgress != value && (now - lastProgressReport > minGap || value == uploadedFile.FileSize))
            {
              // ReSharper disable AccessToDisposedClosure
              streamWriter.WriteLine(Convert.ToString(value));
              streamWriter.Flush();
              Logger.InfoFormat("Upload Progress reported: {0}", GeneralHelper.FormatToBytesKB(value));
              lastProgressReport = now;
              lastProgress = value;
              // ReSharper restore AccessToDisposedClosure
            }
            else if (Logger.IsDebugEnabled)
              Logger.DebugFormat("Upload Progress: {0} {1} {2}", GeneralHelper.FormatToBytesKB(value), Response.IsClientConnected, cancellationToken.IsCancellationRequested);
          });

          await AttachmentHelper.StreamBlobToDataBase(new DataAccessAdapter(), cancellationToken, progress, uploadedFile)
            .ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
          Logger.InfoFormat("Uploaded: {0} {1}", uploadedFile.FileName, fileSize);
        }

      Logger.InfoFormat("Upload of {0} files complete", uploadedFilesCount);
      return JsonNet(new AjaxResponseMessage(true, "UploadComplete"));
    }

    public Task<JsonNetResult> GetAttachmentsAsync(CancellationToken cancellationToken)
    {
      var linqMetaData = new LinqMetaData(new DataAccessAdapter());
      var attachments = linqMetaData.ProductPhoto;
      return JsonNetAsync(attachments.ToEntityCollection2Async());
    }

    public async Task<FileStreamResult> DownloadAttachmentAsync(long attachmentID, bool useStream = true)
    {
      Logger.DebugMethod(attachmentID, useStream);
      var cancellationToken = Response.ClientDisconnectedToken;
      var dataAccessAdapter = new DataAccessAdapter();
      if (useStream)
      {
        await dataAccessAdapter.OpenConnectionAsync(cancellationToken);
        var dateReader = await AttachmentHelper.GetAttachmentReader(dataAccessAdapter, attachmentID, cancellationToken);
        if (dateReader is DbDataReader reader)
          if (await reader.ReadAsync(cancellationToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext))
          {
            long lastProgress = 0;
            var progress = new SynchronousProgress<long>(value =>
            {
              if (Response.IsClientConnected && lastProgress != value)
              {
                Logger.InfoFormat("Download Progress: {0}%", value);
                lastProgress = value;
              }
            });
            var (stream, size) = AttachmentHelper.GetAttachmentStream(reader);
            HttpContext.Response.AddHeader("Content-Disposition", "attachment");
            HttpContext.Response.AddHeader("Content-Length ", size.ToString());
            HttpContext.Response.Buffer = false;
            HttpContext.Response.BufferOutput = false;
            return new DisposingFileStreamResult(new StreamProgressWrapper(stream, progress, false, true, size), reader);
          }
        dateReader.Dispose();
      }

      var attachmentEntity = await new LinqMetaData(dataAccessAdapter).ProductPhoto.FirstAsync(a => a.ProductPhotoID == attachmentID, cancellationToken);
      return File(new MemoryStream(attachmentEntity.LargePhoto), "application/octet-stream", attachmentEntity.LargePhotoFileName);
    }

    /// <summary>
    ///   https://ittone.ma/ittone/net-downsides-of-streaming-large-json-or-html-content-to-a-browser-in-asp-net-mvc/
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult> StreamJson()
    {
      Logger.DebugMethod();
      var data = await GetDataAsync(Response.ClientDisconnectedToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
      //  var x = System.Web.HttpContext.Current.Request.ServerVariables;
      //  Request.Headers["Accept-Encoding"] = "";
      //   Response.Filter = new EmptyFilter(Response.Filter);
      Response.BufferOutput = false;
      Response.ContentType = "application/json";
      var serializer = JsonSerializer.Create();
      using (var sw = new StreamWriter(Response.OutputStream, Encoding.UTF8, 85000))
      {
        sw.AutoFlush = false;
        serializer.Serialize(sw, data);
        await sw.FlushAsync().ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
      }

      return new EmptyResult();
    }

    public async Task<ActionResult> GetJson()
    {
      Logger.DebugMethod();
      var data = await GetDataAsync(Response.ClientDisconnectedToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
      Response.BufferOutput = false;
      var jsonNetResult = JsonNet(data);
      return jsonNetResult;
    }

    static Task<List<ProductEntity>> GetDataAsync(CancellationToken cancellationToken)
    {
      var linqMetaData = new LinqMetaData(new DataAccessAdapter());
      return linqMetaData.Product.Take(5).ToListAsync(cancellationToken);
    }

    [HttpGet]
    public async Task<EmptyResult> StreamIntsAsync(int count = 10)
    {
      await StreamResponseAsync(RangeAsync(count: count, cancellationToken: Response.ClientDisconnectedToken), Response.ClientDisconnectedToken).ConfigureAwait(false);
      return new EmptyResult();
    }

    [HttpGet]
    public EmptyResult StreamInts(int count = 10)
    {
      StreamResponse(Range(count: count));
      return new EmptyResult();
    }

    [HttpGet]
    public EmptyResult StreamIntsBinary(int count = 10)
    {
      StreamResponseBinary(Range(count: count));
      return new EmptyResult();
    }

    async Task StreamResponseAsync<T>(IAsyncEnumerable<T> items, CancellationToken cancellationToken)
    {
      Response.ContentType = "text/plain";
      Response.BufferOutput = false;
      using var streamWriter = new StreamWriter(Response.OutputStream) { AutoFlush = false };
      await foreach (var item in items.WithCancellation(cancellationToken))
        if (Response.IsClientConnected && !cancellationToken.IsCancellationRequested)
        {
          await streamWriter.WriteLineAsync(Convert.ToString(item)).ConfigureAwait(false);
          await streamWriter.FlushAsync().ConfigureAwait(false);
        }
        else
          Logger.DebugFormat("StreamResponseAsync: {0} {1} {2}", item, Response.IsClientConnected, cancellationToken.IsCancellationRequested);
    }

    void StreamResponse<T>(IEnumerable<T> items)
    {
      Response.ContentType = "text/plain";
      Response.BufferOutput = false;
      using var streamWriter = new StreamWriter(Response.OutputStream) { AutoFlush = false };
      foreach (var item in items)
        if (Response.IsClientConnected && !Response.ClientDisconnectedToken.IsCancellationRequested)
        {
          streamWriter.WriteLine(Convert.ToString(item));
          streamWriter.Flush();
        }
        else
          Logger.DebugFormat("StreamResponse: {0} {1} {2}", item, Response.IsClientConnected, Response.ClientDisconnectedToken.IsCancellationRequested);
    }

    void StreamResponseBinary(IEnumerable<int> items)
    {
      Response.ContentType = "application/octet-stream";
      Response.BufferOutput = false;
      using var binaryWriter = new BinaryWriter(Response.OutputStream);
      foreach (var item in items)
        if (Response.IsClientConnected && !Response.ClientDisconnectedToken.IsCancellationRequested)
        {
          binaryWriter.Write(item);
          binaryWriter.Flush();
        }
        else
          Logger.DebugFormat("StreamResponseBinary: {0} {1} {2}", item, Response.IsClientConnected, Response.ClientDisconnectedToken.IsCancellationRequested);
    }


    static async IAsyncEnumerable<int> RangeAsync(int start = 0, int count = 10, int millisecondsDelay = 1000,
      CancellationToken cancellationToken = default)
    {
      for (var i = start; i < count; i++)
      {
        if (i > start)
          await Task.Delay(millisecondsDelay, cancellationToken).ConfigureAwait(false);
        var result = start + i;
        Logger.DebugFormat("Range: {0} {1}", result, cancellationToken.IsCancellationRequested);
        yield return result;
      }
    }

    static IEnumerable<int> Range(int start = 0, int count = 10, int millisecondsDelay = 1000)
    {
      for (var i = start; i < count; i++)
      {
        if (i > start)
          Thread.Sleep(millisecondsDelay);
        yield return start + i;
      }
    }
  }


  enum PrototypeUrls
  {
    UploadChunks,
    UploadComplete,
    StreamJson,
    StreamText,
    StreamTextAsync,
    StreamInts,
    GetJson,
    GetAttachments,
    DownloadAttachment,
    ChunkSizeMegaBytes
  }
}