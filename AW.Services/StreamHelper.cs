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
using AW.Dal.Linq;
using AW.Dal.SqlServer;
using AW.Helper;
using log4net;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using SD.LLBLGen.Pro.QuerySpec.Adapter;

namespace AW.Services
{
  public static class StreamHelper
  {
    public const string HttpRequestParamNameUploadID = "UploadID";
    public const string HttpRequestParamNameFileName = "fileName";
    public const string HttpRequestParamNameFileGuid = "fileGuid";
    static readonly ILog Logger = LogManager.GetLogger(typeof(StreamHelper));

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
    ///   Inserts an Entity in the database asynchronously, with the specified field streamed.
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
      entity.Fields.SetCurrentValue(indexOfFieldtoBeStreamed, stream);
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

    public static async Task<long> StreamLargePhotoToFileWithExcludedFieldsAsync(DataAccessAdapter dataAccessAdapter, long productPhotoID, string filePath, CancellationToken cancellationToken)
    {
      var linqMetaData = new LinqMetaData(dataAccessAdapter);
      var productPhotoEntity = linqMetaData.ProductPhoto.ExcludeFields(e => e.LargePhoto).First(p => p.ProductPhotoID == productPhotoID);

      var entityFieldCore = ProductPhotoFields.LargePhoto;
      var excludedFields = new ExcludeIncludeFieldsList { entityFieldCore };
      long length = 0;
      await dataAccessAdapter.FetchExcludedFieldsAsStreamsAsync(productPhotoEntity, excludedFields, WriteLargePhotoToFile, cancellationToken);
      return length;

      void WriteLargePhotoToFile(IEntityCore entityCore)
      {
        var currentValue = entityCore.Fields.GetCurrentValue(entityFieldCore.FieldIndex);
        using var fileStream = File.Create(filePath);
        if (currentValue is Stream stream)
        {
          stream.CopyTo(fileStream);
          length = fileStream.Length;
        }
        else if (currentValue is byte[] { Length: > 0 } blob)
        {
          fileStream.Write(blob, 0, blob.Length);
          length = fileStream.Length;
        }
      }
    }

    public static async Task<long> StreamLargePhotoToFileWithExcludedFieldsAsync2(DataAccessAdapter dataAccessAdapter, int productPhotoID, string filePath, CancellationToken cancellationToken)
    {
      var productPhotoEntity = new ProductPhotoEntity(productPhotoID);
      var entityFieldCore = ProductPhotoFields.LargePhoto;
      using var dataReader = await dataAccessAdapter.FetchExcludedFieldsAsStreamsAsync(productPhotoEntity, new IncludeFieldsList { entityFieldCore }, cancellationToken);
      long length = 0;
      var currentValue = productPhotoEntity.Fields.GetCurrentValue(entityFieldCore.FieldIndex);
      using var fileStream = File.Create(filePath);
      if (currentValue is Stream stream)
      {
        await stream.CopyToAsync(fileStream);
        length = fileStream.Length;
      }
      else if (currentValue is byte[] { Length: > 0 } blob)
      {
        await fileStream.WriteAsync(blob, 0, blob.Length, cancellationToken);
        length = fileStream.Length;
      }

      return length;
    }


    public static async Task<long> StreamLargePhotoToFileAsync(IDataAccessAdapter dataAccessAdapter, long productPhotoID, string filePath, CancellationToken cancellationToken,
      IProgress<long> progress)
    {
      using var dateReader = await GetProductLargePhotoReader(dataAccessAdapter, productPhotoID, cancellationToken);
      if (dateReader is DbDataReader reader && await reader.ReadAsync(cancellationToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext))
      {
        var stream = DataHelper.GetStream(reader, 1);
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

    public static Task<IDataReader> GetProductLargePhotoReader(IDataAccessAdapter dataAccessAdapter, long productPhotoID, CancellationToken cancellationToken)
    {
      Logger.DebugMethod(dataAccessAdapter.ConnectionString, productPhotoID);
      var qf = new QueryFactory();
      var q = qf.Create()
        .Select(ProductPhotoFields.ProductPhotoID, ProductPhotoFields.LargePhoto, ProductPhotoFields.LargePhotoFileName)
        .Where(ProductPhotoFields.ProductPhotoID == productPhotoID);
      return dataAccessAdapter.FetchAsDataReaderAsync(q, CommandBehavior.SequentialAccess, cancellationToken);
    }
  }
}