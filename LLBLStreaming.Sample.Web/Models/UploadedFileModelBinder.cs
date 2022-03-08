using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AW.Helper;
using AW.Services;
using log4net;

namespace LLBLStreaming.Sample.Web.Models
{
  public class UploadedFileModelBinder : IModelBinder
  {
    static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    /// <summary>
    ///   TempAttachments
    /// </summary>
    public const string DefaultAttachmentsTemporyDirectory = "TempAttachments";

    /// <summary>
    ///   AttachmentTempPath
    /// </summary>
    public const string AttachmentsTemporyDirectoryAppSettings = "AttachmentTempPath";

    public static string AttachmentsTemporyDirectory => ConfigurationManager.AppSettings[AttachmentsTemporyDirectoryAppSettings] ?? DefaultAttachmentsTemporyDirectory;

    static string _attachmentsTemporyDirectoryPath;

    public static string AttachmentsTemporyDirectoryPath => _attachmentsTemporyDirectoryPath ??= HostingEnvironment.MapPath(@"~/" + AttachmentsTemporyDirectory);
    static readonly string UploadFolder = Path.Combine(AttachmentsTemporyDirectoryPath, "Upload");

    #region IModelBinder Members

    /// <summary>
    ///   Attempts to bind UploadedFile models
    /// </summary>
    /// <param name="controllerContext">The current controller context</param>
    /// <param name="bindingContext">The binding context</param>
    /// <returns>The model</returns>
    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      // First, work out what type the method is expecting:
      if (bindingContext.ModelType == typeof(UploadedFile))
        LLBLGenModelBinder.AssignModel(bindingContext, BindSingleFile(controllerContext, bindingContext));
      if (bindingContext.ModelType == typeof(UploadedFile[]))
        LLBLGenModelBinder.AssignModel(bindingContext, BindMultipleFiles(controllerContext, bindingContext).ToArray());
      if (bindingContext.ModelType == typeof(List<UploadedFile>))
        LLBLGenModelBinder.AssignModel(bindingContext, BindMultipleFiles(controllerContext, bindingContext).ToList());
      if (bindingContext.ModelType == typeof(UploadedFileCollection))
        LLBLGenModelBinder.AssignModel(bindingContext, BindMultipleFiles(controllerContext, bindingContext));
      return bindingContext.Model;
    }

    static UploadedFile BindSingleFile(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var files = BindMultipleFiles(controllerContext, bindingContext);
      var file = files.LastOrDefault();
      return file;
    }

    static UploadedFileCollection BindMultipleFiles(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      Logger.DebugMethod();
      return GetUploadedFiles(controllerContext.HttpContext, bindingContext.ModelName);
    }

    public static UploadedFileCollection GetUploadedFiles(HttpContextBase http, string modelName)
    {
      Logger.DebugMethod();
      return UploadedFileCollectionGetUploadedFiles(http);
    }

    public static UploadedFileCollection UploadedFileCollectionGetUploadedFiles(HttpContextBase http)
    {
      var fileCollection = new UploadedFileCollection();
      var uploadId = http.Request[AttachmentHelper.HttpRequestParamNameUploadID];
      Logger.DebugMethod(http.Request.CurrentExecutionFilePath);
      if (uploadId == null)
      {
        var fileGuid = http.Request[AttachmentHelper.HttpRequestParamNameFileGuid];
        var fileName = http.Request[AttachmentHelper.HttpRequestParamNameFileName];
        if (fileGuid == null || fileName == null)
          return fileCollection;

        var uploadedFile = new ChunkedUploadedFile(fileName, fileGuid, UploadFolder);
        fileCollection.Add(uploadedFile);
      }
      else
        fileCollection.AddRange(from key in http.Request.Files.AllKeys
          let requestFile = http.Request.Files[key]
          where requestFile != null
          select new UploadedFile(requestFile.FileName, requestFile.InputStream, key, AttachmentsTemporyDirectoryPath));
      // Get Uploaded Files

      return fileCollection;
    }

    #endregion
  }
}