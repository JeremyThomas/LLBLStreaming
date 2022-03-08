using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AW.Helper;
using log4net;

namespace AW.Services
{
  /// <summary>
  ///   Contains information about an uploaded file, including fieldname, filename and location
  /// </summary>
  public class UploadedFile : IDisposable
  {
    static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    readonly string _tempFileName;

    string _fieldName;
    FileInfo _fileInfo;

    static string TrimFilePath(string fileName)
    {
      if (fileName.Contains("\\")) fileName = fileName.Split('\\').Last();
      if (fileName.Contains("/")) fileName = fileName.Split('/').Last();
      return fileName;
    }

    UploadedFile(string fileName, string fieldName = null)
    {
      Logger.DebugMethod(fileName, fieldName);
      FileName = TrimFilePath(fileName);
      _fieldName = fieldName;
      Logger.InfoFormat("FileName: {0}", FileName);
    }

    public UploadedFile(string fileName, Stream tempStream, string fieldName, string attachmentStagingDirectoryPath) : this(fileName, fieldName)
    {
      if (string.IsNullOrWhiteSpace(attachmentStagingDirectoryPath))
        attachmentStagingDirectoryPath = Path.GetTempPath();
      else if (!Directory.Exists(attachmentStagingDirectoryPath))
        Directory.CreateDirectory(attachmentStagingDirectoryPath);
      // Save to temporary folder
      var path = Path.Combine(attachmentStagingDirectoryPath, Guid.NewGuid() + ".tmp");

      using (var fileStream = File.Create(path))
      {
        tempStream.Seek(0, SeekOrigin.Begin);
        tempStream.CopyTo(fileStream);
        fileStream.Close();
      }

      _tempFileName = path;
    }

    public UploadedFile(string fileName, string tempFileName, string fieldName = null): this(fileName,  fieldName ?? fileName)
    {
      Logger.DebugMethod(fileName, tempFileName, fieldName);
      _tempFileName = tempFileName;
      Logger.InfoFormat("tempFileName: {0}", tempFileName);
    }

    FileInfo GetFileInfo()
    {
      if (_tempFileName == null)
        return null;
      return _fileInfo ??= new FileInfo(_tempFileName);
    }

    /// <summary>
    ///   Deletes the current temporary file from disk
    /// </summary>
    public virtual void Delete()
    {
      var fi = GetFileInfo();
      if (fi == null)
        return;
      if ((fi.Attributes & FileAttributes.ReadOnly) > 0) fi.Attributes ^= FileAttributes.ReadOnly;
      try
      {
        fi.Delete();
      }

      catch (Exception ex)
      {
        Logger.WarnFormat("Could not delete file {0}{1}{2}", fi.FullName, Environment.NewLine, ex.Message);
      }
    }

    /// <summary>
    ///   Deletes the current temporary file from disk
    /// </summary>
    /// <param name="o"></param>
    /// <param name="e"></param>
    public virtual void Delete(object o, EventArgs e)
    {
      var fi = GetFileInfo();
      if (fi == null)
        return;
      if ((fi.Attributes & FileAttributes.ReadOnly) > 0) fi.Attributes ^= FileAttributes.ReadOnly;
      try
      {
        fi.Delete();
      }
      catch (Exception ex)
      {
        Logger.WarnFormat("Could not delete file {0}{1}{2}", fi.FullName, Environment.NewLine, ex.Message);
      }
    }

    /// <summary>
    ///   Returns a stream for reading from the file
    /// </summary>
    /// <returns></returns>
    public virtual Stream Read()
    {
      return GetFileInfo().OpenRead();
    }

    /// <summary>
    ///   The size of the uploaded file
    /// </summary>
    public virtual long FileSize => GetFileInfo() != null ? GetFileInfo().Length : 0;

    /// <summary>
    ///   The file name of the uploaded file
    /// </summary>
    public virtual string FileName { get; } = "";

    /// <summary>
    ///   The field name of the uploaded file
    /// </summary>
    public virtual string FieldName
    {
      get => _fieldName;
      set => _fieldName = value;
    }

    #region IDisposable Members

    /// <summary>
    ///   Deletes the current temporary file
    /// </summary>
    public void Dispose()
    {
      Delete();
    }

    #endregion

  }


  public class ChunkedUploadedFile : UploadedFile
  {
    static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
    readonly MultiFileStream _multiStream;

    /// <inheritdoc />
    public ChunkedUploadedFile(string fileName, string fileGuid, string uploadFolder) : base(fileName, null)
    {
      Logger.DebugMethod(fileName, fileGuid, uploadFolder);
      _multiStream = new MultiFileStream(GetChunkedFilePaths(fileGuid, uploadFolder));
    }

    public static IOrderedEnumerable<string> GetChunkedFilePaths(string fileName, string uploadFolder)
    {
      return Directory.GetFiles(uploadFolder).Where(p => p.Contains(fileName)).OrderBy(p => int.Parse(p.Replace(fileName, "$").Split('$')[1]));
    }

    /// <inheritdoc />
    public override Stream Read()
    {
      return _multiStream;
    }

    /// <inheritdoc />
    public override long FileSize => _multiStream.Length;
  }
}