using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using AW.Helper;
using log4net;

namespace LLBLStreaming.Sample.Web.Controllers
{
  /// <summary>
  ///   DisposingFileStreamResult
  /// </summary>
  /// <remarks>https://stackoverflow.com/questions/58552126/streaming-data-from-the-database-asp-net-core-sqldatareader-getstream</remarks>
  /// <seealso cref="FileStreamResult" />
  /// <seealso cref="System.Web.WebPages.HttpContextExtensions.RegisterForDispose" />
  public class DisposingFileStreamResult : FileStreamResult
  {
    protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    readonly IStreamableDisposible _streamableDisposible;

    public DisposingFileStreamResult(IStreamableDisposible streamableDisposible, string contentType)
      : base(streamableDisposible.Stream, contentType)
    {
      Logger.DebugMethod(streamableDisposible.GetType(), contentType);
      _streamableDisposible = streamableDisposible;
    }

    public DisposingFileStreamResult(Stream stream, IDisposable toDisposeOf) : this(new StreamableDisposible(stream, toDisposeOf), "application/octet-stream")
    {
    }

    public override void ExecuteResult(ControllerContext context)
    {
      Logger.DebugMethod(context.HttpContext.Request.CurrentExecutionFilePath);
      base.ExecuteResult(context);
      Logger.DebugFormat("ExecuteResult finished, IsClientConnected: {0}", context.HttpContext.Response.IsClientConnected);
      _streamableDisposible.Dispose();
    }

    /// <summary>Writes the file to the response.</summary>
    /// <param name="response">The response.</param>
    protected override void WriteFile(HttpResponseBase response)
    {
      Logger.DebugMethod(response.IsClientConnected);
      var outputStream = response.OutputStream;
      using (FileStream)
      {
        var buffer = new byte[4096];
        while (response.IsClientConnected)
        {
          var count = FileStream.Read(buffer, 0, 4096);
          if (count != 0 && response.IsClientConnected)
            try
            {
              outputStream.Write(buffer, 0, count);
            }
            catch (Exception e)
            {
              e.LogException();
              if (response.IsClientConnected)
                break;
              throw;
            }
          else
            break;
        }
      }
    }
  }

  public interface IStreamableDisposible : IDisposable
  {
    public Stream Stream { get; }
  }

  /// <summary>
  ///   StreamableDisposible
  /// </summary>
  /// <remarks>https://stackoverflow.com/questions/58552126/streaming-data-from-the-database-asp-net-core-sqldatareader-getstream</remarks>
  /// <seealso cref="IStreamableDisposible" />
  public class StreamableDisposible : IStreamableDisposible
  {
    readonly IDisposable _toDisposeOf;

    public StreamableDisposible(Stream stream, IDisposable toDisposeOf)
    {
      Stream = stream ?? throw new ArgumentNullException(nameof(stream));
      _toDisposeOf = toDisposeOf;
    }

    public Stream Stream { get; set; }

    public void Dispose()
    {
      _toDisposeOf?.Dispose();
    }
  }
}