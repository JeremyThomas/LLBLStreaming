﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.9.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AW.Helper;
using ExposedObject;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.DQE.SqlServer;

namespace AW.Dal.SqlServer
{	
  // __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
  // __LLBLGENPRO_USER_CODE_REGION_END
  /// <summary>Data access adapter class, which controls the complete database interaction with the database for all objects.</summary>
  /// <remarks>Use a DataAccessAdapter object solely per thread, and per connection. A DataAccessAdapter object contains 1 active connection 
  /// and no thread-access scheduling code. This means that you need to create a new DataAccessAdapter object if you want to utilize
  /// in another thread a new connection and a new transaction or want to open a new connection.</remarks>
  public partial class DataAccessAdapter : DataAccessAdapterBase
  {
    /// <summary>The name of the key in the *.config file of the executing application which contains the connection string.</summary>
    /// <remarks>Default: the value set in the LLBLGen Pro project properties</remarks>
    public static string ConnectionStringKeyName="Main.ConnectionString";

    /// <summary>CTor</summary>
    public DataAccessAdapter() : this(ReadConnectionStringFromConfig(), false, null, null) { }

    /// <summary>CTor</summary>
    /// <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
    public DataAccessAdapter(bool keepConnectionOpen) : this(ReadConnectionStringFromConfig(), keepConnectionOpen, null, null) { }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    public DataAccessAdapter(string connectionString) : this(connectionString, false, null, null) { }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen) : this(connectionString, keepConnectionOpen, null, null) { }
    
    /// <summary>CTor.</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
    /// <param name="catalogNameUsageSetting"> Configures this data access adapter object how to threat catalog names in persistence information.</param>
    /// <param name="catalogNameToUse"> The name to use if catalogNameUsageSetting is set to ForceName. Ignored otherwise.</param>
    /// <remarks>For backwards compatibility.</remarks>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, CatalogNameUsage catalogNameUsageSetting, string catalogNameToUse) 
        : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, catalogNameUsageSetting, SchemaNameUsage.Default, catalogNameToUse, string.Empty, null, null);
    }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
    /// <param name="schemaNameUsageSetting">Configures this data access adapter object how to threat schema names in persistence information.</param>
    /// <param name="schemaNameToUse">Oracle specific. The name to use if schemaNameUsageSetting is set to ForceName. Ignored otherwise.</param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, SchemaNameUsage schemaNameUsageSetting, string schemaNameToUse) 
        : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, schemaNameUsageSetting, string.Empty, schemaNameToUse, null, null);
    }

    /// <summary>CTor.</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
    /// <param name="catalogNameOverwrites"> The from-to name value pairs and setting for the overwriting of catalog names. Can be null.</param>
    /// <param name="schemaNameOverwrites"> The from-to name value pairs and setting for the overwriting of schema names. Can be null.</param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, CatalogNameOverwriteHashtable catalogNameOverwrites, SchemaNameOverwriteHashtable schemaNameOverwrites) 
        : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, SchemaNameUsage.Default, string.Empty, string.Empty, catalogNameOverwrites, schemaNameOverwrites);
    }

    /// <summary>Sets the flag to signal the SqlServer DQE to generate SET ARITHABORT ON statements prior to INSERT, DELETE and UPDATE Queries.
    /// Keep this flag to false in normal usage, but set it to true if you need to write into a table which is part of an indexed view.
    /// It will not affect normal inserts/updates that much, leaving it on is not harmful. See Books online for details on SET ARITHABORT ON.
    /// After each statement the setting is turned off if it has been turned on prior to that statement.</summary>
    /// <remarks>Setting this flag is a global change.</remarks>
    public static void SetArithAbortFlag(bool value)
    {
      DynamicQueryEngine.ArithAbortOn = value;
    }

    /// <summary>Sets the default compatibility level used by the DQE. Default is SqlServer2005. This is a global setting.
    /// Compatibility level influences the query generated for paging, sequence name (@@IDENTITY/SCOPE_IDENTITY()), and usage of newsequenceid() in inserts. 
    /// It also influences the ado.net provider to use. This way you can switch between SqlServer server client 'SqlClient' and SqlServer CE Desktop.</summary>
    /// <remarks>Setting this property will overrule a similar setting in the .config file. Don't set this property when queries are executed as
    /// it might switch factories for ADO.NET elements which could result in undefined behavior so set this property at startup of your application</remarks>
    public static void SetSqlServerCompatibilityLevel(SqlServerCompatibilityLevel compatibilityLevel)
    {
      DynamicQueryEngine.DefaultCompatibilityLevel = compatibilityLevel;
    }

    /// <summary>Creates a new Dynamic Query engine object and passes in the defined catalog/schema overwrite hashtables.</summary>
    protected override DynamicQueryEngineBase CreateDynamicQueryEngine()
    {
      return this.PostProcessNewDynamicQueryEngine(new DynamicQueryEngine());
    }

    /// <summary>Reads the value of the setting with the key ConnectionStringKeyName from the *.config file and stores that value as the active connection string to use for this object.</summary>
    /// <returns>connection string read</returns>
    private static string ReadConnectionStringFromConfig()
    {
#if NETSTANDARD || NETCOREAPP
      return RuntimeConfiguration.GetConnectionString(ConnectionStringKeyName);
#else
      return ConfigFileHelper.ReadConnectionStringFromConfig(ConnectionStringKeyName);
#endif
    }
    
    /// <summary>Sets the per instance compatibility level on the dqe instance specified.</summary>
    /// <param name="dqe">The dqe.</param>
    protected override void SetPerInstanceCompatibilityLevel(DynamicQueryEngineBase dqe)
    {
      if(_compatibilityLevel.HasValue)
      {
        ((DynamicQueryEngine)dqe).CompatibilityLevel = _compatibilityLevel.Value;
      }
    }

    private Nullable<SqlServerCompatibilityLevel> _compatibilityLevel = null;
    
    /// <summary>The per-instance compatibility level used by this DQE instance. Default is the one set globally, which is by default SqlServer2005 (for 2005+). 
    /// Compatibility level influences the query generated for paging, sequence name (@@IDENTITY/SCOPE_IDENTITY()), and usage of newsequenceid() in inserts. 
    /// It also influences the ado.net provider to use. This way you can switch between SqlServer server client 'SqlClient' and SqlServer CE Desktop.</summary>
    public Nullable<SqlServerCompatibilityLevel> CompatibilityLevel
    {
      get { return _compatibilityLevel; }
      set { _compatibilityLevel = value; }
    }

    // __LLBLGENPRO_USER_CODE_REGION_START CustomDataAccessAdapterCode

      /// <summary>
    ///   Async variant of <see cref="FetchExcludedFields(IEntity2, ExcludeIncludeFieldsList)" />.
    ///   Loads the data for the excluded fields specified in the list of excluded fields into the entity passed in.
    /// </summary>
    /// <param name="entity">The entity to load the excluded field data into.</param>
    /// <param name="excludedIncludedFields">
    ///   The excludedIncludedFields object as it is used when fetching the passed in entity. If you used
    ///   the excludedIncludedFields object to fetch only the fields in that list (i.e.
    ///   excludedIncludedFields.ExcludeContainedFields==false), the routine
    ///   will fetch all other fields in the resultset for the entities in the collection excluding the fields in
    ///   excludedIncludedFields.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <remarks>
    ///   The field data is set like a normal field value set, so authorization is applied to it.
    /// </remarks>
    public async Task FetchExcludedFieldsAsStreamsAsync(IEntity2 entity, ExcludeIncludeFieldsList excludedIncludedFields, CancellationToken cancellationToken)
    {
      if (excludedIncludedFields == null || excludedIncludedFields.Count <= 0 || entity == null) return;

      var collection = new EntityCollectionNonGeneric(entity.GetEntityFactory()) { (EntityBase2)entity };
      await FetchExcludedFieldsAsStreamsAsync(collection, excludedIncludedFields, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    ///   Async variant of <see cref="FetchExcludedFields(IEntityCollection2, ExcludeIncludeFieldsList)" />.
    ///   Loads the data for the excluded fields specified in the list of excluded fields into all the entities in the entities
    ///   collection passed in.
    /// </summary>
    /// <param name="entities">
    ///   The entities to load the excluded field data into. The entities have to be either of the same type or have to be
    ///   in the same inheritance hierarchy as the entity which factory is set in the collection.
    /// </param>
    /// <param name="excludedIncludedFields">
    ///   The excludedIncludedFields object as it is used when fetching the passed in collection. If you used
    ///   the excludedIncludedFields object to fetch only the fields in that list (i.e.
    ///   excludedIncludedFields.ExcludeContainedFields==false), the routine
    ///   will fetch all other fields in the resultset for the entities in the collection excluding the fields in
    ///   excludedIncludedFields.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="ORMGeneralOperationException">The entity factory of the passed in entities collection is null.</exception>
    /// <remarks>
    ///   The field data is set like a normal field value set, so authorization is applied to it.
    ///   This routine batches fetches to have at most 5*ParameterizedThreshold of parameters per fetch. Keep in mind that most
    ///   databases have a limit
    ///   on the # of parameters per query.
    /// </remarks>
    public virtual async Task FetchExcludedFieldsAsStreamsAsync(IEntityCollection2 entities, ExcludeIncludeFieldsList excludedIncludedFields, CancellationToken cancellationToken)
    {
      if (entities == null || entities.Count <= 0) return;
      if (entities.EntityFactoryToUse == null) throw new ORMGeneralOperationException("The entity factory of the passed in entities collection is null.");

      var persistenceCoreType = typeof(PersistenceCore);
      var exposedPersistenceCore = Exposed.From(persistenceCoreType);
      var assembly = typeof(IEntityCollection2).Assembly;
      var excludedFieldsBatchQueryParametersType = assembly.GetType("SD.LLBLGen.Pro.ORMSupportClasses.ExcludedFieldsBatchQueryParameters`1");
      var entityBase2Type = typeof(EntityBase2);
      dynamic parameters = MetaDataHelper.CreateGeneric(excludedFieldsBatchQueryParametersType, entityBase2Type);

      var produceElementsForExcludedFieldBatchFetchesMethod = persistenceCoreType.GetMethod("ProduceElementsForExcludedFieldBatchFetches", BindingFlags.NonPublic | BindingFlags.Static);

      // Binding the method info to generic arguments
      Type[] genericArguments = { entityBase2Type };
      if (produceElementsForExcludedFieldBatchFetchesMethod != null)
      {
        var produceElementsForExcludedFieldBatchFetchesMethodInfo = produceElementsForExcludedFieldBatchFetchesMethod.MakeGenericMethod(genericArguments);

        var result = produceElementsForExcludedFieldBatchFetchesMethodInfo.Invoke(null, new object[]
        {
          entities, excludedIncludedFields, parameters, new Func<int, IEntityFieldsCore>(a => new EntityFields2(a)),
          ParameterisedPrefetchPathThreshold
        });
        //if (!exposedPersistenceCore.ProduceElementsForExcludedFieldBatchFetches(entities, excludedIncludedFields, parameters, new Func<int, IEntityFieldsCore>(a => new EntityFields2(a)),
        //      ParameterisedPrefetchPathThreshold))
        var b = result as bool?;
        if (!b.GetValueOrDefault())
          // nothing to do further
          return;
        // Grab the field persistence infos for type converters
        var keepConnectionOpenSave = KeepConnectionOpen;
        try
        {
          KeepConnectionOpen = true;
          var queryCreationManager = CreateQueryCreationManager(PersistenceInfoProviderSingleton.GetInstance());
          var exposedQueryCreationManager = Exposed.From(queryCreationManager);
          var resultFields = parameters.ResultFields;
          var fieldPersistenceInfos = exposedQueryCreationManager.GetFieldPersistenceInfos(resultFields);
          await exposedPersistenceCore.FetchExcludedFieldsBatchesAsync(this, entities, parameters, fieldPersistenceInfos, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
          KeepConnectionOpen = keepConnectionOpenSave;
          CloseConnectionIfPossible();
        }
      }
    }

    /// <summary> Closes the connection if possible (i.e. when keepConnectionOpen is false and isTransactionInProgress is false </summary>
    private void CloseConnectionIfPossible()
    {
      if (!(KeepConnectionOpen || IsTransactionInProgress)) CloseConnection();
    }

    /// <summary>
    /// Creates a new Insert Query object which is ready to use.
    /// </summary>
    /// <returns>IActionQuery Instance which is ready to be used.</returns>
    /// <remarks>Generic version.</remarks>
    /// <exception cref="T:System.ArgumentNullException">When fields is null or fieldsPersistenceInfo is null</exception>
    /// <exception cref="T:System.ArgumentException">When fields contains no EntityFieldCore instances or fieldsPersistenceInfo is empty.</exception>
    /// <exception cref="T:SD.LLBLGen.Pro.ORMSupportClasses.ORMQueryConstructionException">When there are no fields to insert in the fields list. This exception is to prevent
    /// INSERT INTO table () VALUES () style queries.</exception>
    public IActionQuery CreateInsertDQ(IEntity2 entity)
    {
      return CreateDynamicQueryEngine().CreateInsertDQ(entity.Fields.GetAsEntityFieldCoreArray(), GetAllFieldPersistenceInfos(entity), GetActiveConnection());
    }

    /// <summary>
    ///   Gets all field persistence infos.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    IFieldPersistenceInfo[] GetAllFieldPersistenceInfos(IEntity2 entity)
    {
      return PersistenceInfoProviderSingleton.GetInstance().GetAllFieldPersistenceInfos(entity);
    }

    // __LLBLGENPRO_USER_CODE_REGION_END

  }
}
