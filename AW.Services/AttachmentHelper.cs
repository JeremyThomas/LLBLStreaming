using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AW.Dal;
using AW.Dal.EntityClasses;
using AW.Dal.FactoryClasses;
using AW.Dal.HelperClasses;
using AW.Dal.SqlServer;
using AW.Helper;
using log4net;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using SD.LLBLGen.Pro.QuerySpec.Adapter;

namespace AW.Services
{
  public static class AttachmentHelper
  {
    public const string HttpRequestParamNameUploadID = "UploadID";
    public const string HttpRequestParamNameFileName = "fileName";
    public const string HttpRequestParamNameFileGuid = "fileGuid";
    static readonly ILog Logger = LogManager.GetLogger(typeof(AttachmentHelper));

    public static Task<AjaxResponseMessage> UploadChunks(ushort? chunkNumber, Guid fileGuid, string uploadFolder,
      Stream inputStream)
    {
      return UploadChunks(Convert.ToString(chunkNumber), fileGuid.ToString(), uploadFolder, inputStream);
    }

    public static async Task<AjaxResponseMessage> UploadChunks(string chunkNumber, string fileName, string uploadFolder,
      Stream inputStream)
    {
      try
      {
        Directory.CreateDirectory(uploadFolder);
        var newpath = Path.Combine(uploadFolder, fileName + chunkNumber);
        using var fs = File.Create(newpath);
        await inputStream.CopyToAsync(fs).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
      }
      catch (Exception ex)
      {
        ex.LogException();
        throw;
      }

      return new AjaxResponseMessage(true, "UploadChunk");
    }

    public static AjaxResponseMessage OnChunkedUploadComplete(Guid fileGuid, string uploadFolder, string actualFileName,
      string destinationFolder)
    {
      return OnChunkedUploadComplete(fileGuid.ToString(), uploadFolder, actualFileName, destinationFolder);
    }

    public static AjaxResponseMessage OnChunkedUploadComplete(string fileName, string uploadFolder,
      string actualFileName, string destinationFolder)
    {
      try
      {
        File.Move(OnChunkedUploadComplete(fileName, uploadFolder), Path.Combine(destinationFolder, actualFileName));
      }
      catch (Exception ex)
      {
        ex.LogException();
        throw;
      }

      return new AjaxResponseMessage(true, "UploadComplete");
    }

    public static string OnChunkedUploadComplete(string fileName, string uploadFolder)
    {
      var filePaths = ChunkedUploadedFile.GetChunkedFilePaths(fileName, uploadFolder);
      var newPath = Path.Combine(uploadFolder, fileName);
      foreach (var filePath in filePaths)
        MergeChunks(newPath, filePath);
      return newPath;
    }

    static void MergeChunks(string chunk1, string chunk2)
    {
      FileStream fs1 = null;
      FileStream fs2 = null;
      try
      {
        fs1 = File.Open(chunk1, FileMode.Append);
        fs2 = File.Open(chunk2, FileMode.Open);
        var fs2Content = new byte[fs2.Length];
        fs2.Read(fs2Content, 0, (int)fs2.Length);
        fs1.Write(fs2Content, 0, (int)fs2.Length);
      }
      catch (Exception ex)
      {
        ex.LogException();
      }
      finally
      {
        if (fs1 != null) fs1.Close();
        if (fs2 != null) fs2.Close();
        File.Delete(chunk2);
      }
    }

    public static Task<long> StreamProductPhotoToDataBase(DataAccessAdapter dataAccessAdapter,
      CancellationToken cancellationToken, IProgress<long> progress,
      UploadedFile file, bool incrementalProgress = false)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, cancellationToken, null, file, incrementalProgress);
      return StreamProductPhotoToDataBase(dataAccessAdapter, cancellationToken, progress, file.Read(), file.FileName, incrementalProgress);
    }

    static async Task<long> StreamProductPhotoToDataBase(IDataAccessAdapter dataAccessAdapter, CancellationToken cancellationToken,
      IProgress<long> progress, Stream stream,
      string fileName, bool incrementalProgress = false)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, cancellationToken, null, stream.GetType(), fileName, incrementalProgress);
      var productPhotoEntity = new ProductPhotoEntity
      {
        LargePhotoFileName = fileName, ModifiedDate = DateTime.Now
      };

      using var streamProgressWrapper = new StreamProgressWrapper(stream, progress, incrementalProgress);
      productPhotoEntity.Fields[(int)ProductPhotoFieldIndex.LargePhoto].CurrentValue = streamProgressWrapper;
      await dataAccessAdapter.SaveEntityAsync(productPhotoEntity, cancellationToken);
      return productPhotoEntity.ProductPhotoID;
    }

    static Task<long> StreamProductPhotoToDataBaseOld(DataAccessAdapter dataAccessAdapter, CancellationToken cancellationToken,
      IProgress<long> progress, Stream stream,
      string fileName, bool incrementalProgress = false)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, cancellationToken, null, stream.GetType(), fileName, incrementalProgress);
      return InsertEntityWithOneFieldStreamedAsync(dataAccessAdapter, cancellationToken, progress, stream,
        new ProductPhotoEntity
        {
          LargePhotoFileName = fileName, ModifiedDate = DateTime.Now, LargePhoto = Array.Empty<byte>()
        }, (int)ProductPhotoFieldIndex.LargePhoto, stream.Length, incrementalProgress);
    }

    static async Task<long> InsertEntityWithOneFieldStreamedAsync(DataAccessAdapter dataAccessAdapter, CancellationToken cancellationToken,
      IProgress<long> progress, Stream stream, EntityBase2 entity, int blobbFieldIndex, long? length = null, bool incrementalProgress = false)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, cancellationToken, null, stream.GetType(), entity, blobbFieldIndex, length, incrementalProgress);
      using var streamProgressWrapper = new StreamProgressWrapper(stream, progress, incrementalProgress);
      return await InsertEntityWithOneFieldStreamedAsync(dataAccessAdapter, entity, blobbFieldIndex, streamProgressWrapper, cancellationToken);
    }

    /// <summary>
    /// Inserts an Entity in the database asynchronously, with the specified field streamed.
    /// </summary>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="entity">The entity to be inserted.</param>
    /// <param name="indexOfFieldtoBeStreamed">Index of the field in the entity to be streamed.</param>
    /// <param name="stream">The stream.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of rows affected or the integer primary key of inserted row (if applicable)</returns>
    static async Task<long> InsertEntityWithOneFieldStreamedAsync(DataAccessAdapter dataAccessAdapter, EntityBase2 entity, int indexOfFieldtoBeStreamed, Stream stream,
      CancellationToken cancellationToken)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, entity, indexOfFieldtoBeStreamed, stream.GetType(), cancellationToken);
      entity.Fields[indexOfFieldtoBeStreamed].CurrentValue = stream;
      var actionQuery = dataAccessAdapter.CreateInsertDQ(entity);
      var numRowsAffected = await dataAccessAdapter.ExecuteActionQueryAsync(actionQuery, cancellationToken);
      var id = actionQuery.ParameterFieldRelations.FirstOrDefault()?.Field.CurrentValue;
      return id switch
      {
        int intID => intID,
        long longID => longID,
        _ => numRowsAffected
      };
    }

    public static async Task<long> StreamLargePhotoToFileAsync(IDataAccessAdapter portalAdapterToUse, long productPhotoID, string filePath, CancellationToken cancellationToken,
      IProgress<long> progress)
    {
      using var dateReader = await GetProductLargePhotoReader(portalAdapterToUse, productPhotoID, cancellationToken);
      if (dateReader is DbDataReader reader && await reader.ReadAsync(cancellationToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext))
      {
        var stream = GetStream(reader, 1);
        if (stream.CanRead)
        {
          using var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
          using var streamProgressWrapper = new StreamProgressWrapper(stream, progress);
          await streamProgressWrapper.CopyToAsync(file).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
          return file.Length;
        }
      }

      return 0;
    }

    public static Task<IDataReader> GetProductLargePhotoReader(IDataAccessAdapter portalAdapterToUse, long productPhotoID, CancellationToken cancellationToken)
    {
      Logger.DebugMethod(portalAdapterToUse.ConnectionString, productPhotoID);
      var qf = new QueryFactory();
      var q = qf.Create()
        .Select(ProductPhotoFields.ProductPhotoID, ProductPhotoFields.LargePhoto, ProductPhotoFields.LargePhotoFileName)
        .Where(ProductPhotoFields.ProductPhotoID == productPhotoID);
      return portalAdapterToUse.FetchAsDataReaderAsync(q, CommandBehavior.SequentialAccess, cancellationToken);
    }

    /// <summary>
    ///   Gets the attachment stream. Should be System.Data.SqlClient.SqlSequentialStream
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <returns>Stream and its Length</returns>
    public static (Stream stream, long size) GetStream(DbDataReader reader)
    {
      Logger.DebugMethod();
      var size = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
      return (GetStream(reader, 1), size);
    }

    /// <summary>
    ///   Gets the stream. Should be System.Data.SqlClient.SqlSequentialStream
    /// </summary>
    /// <param name="reader">The reader.</param>
    /// <param name="ordinal"></param>
    /// <returns>The stream</returns>
    public static Stream GetStream(DbDataReader reader, int ordinal)
    {
      var readerType = reader.GetType();
      Logger.DebugMethod(readerType);
      if (reader.IsDBNull(ordinal))
        return null;
      var stream = reader.GetStream(ordinal);
      Logger.DebugFormat("{0} returned {1} which has CanRead: {2}", readerType, stream.GetType(), stream.CanRead);
      return stream;
    }
  }
}