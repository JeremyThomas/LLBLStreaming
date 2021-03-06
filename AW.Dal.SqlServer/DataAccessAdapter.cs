using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AW.Helper;
using ExposedObject;
using Fasterflect;
using SD.LLBLGen.Pro.DQE.SqlServer;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace AW.Dal.SqlServer
{
  // __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
  // __LLBLGENPRO_USER_CODE_REGION_END
  /// <summary>Data access adapter class, which controls the complete database interaction with the database for all objects.</summary>
  /// <remarks>
  ///   Use a DataAccessAdapter object solely per thread, and per connection. A DataAccessAdapter object contains 1 active
  ///   connection
  ///   and no thread-access scheduling code. This means that you need to create a new DataAccessAdapter object if you want
  ///   to utilize
  ///   in another thread a new connection and a new transaction or want to open a new connection.
  /// </remarks>
  public class DataAccessAdapter : DataAccessAdapterBase
  {
    /// <summary>The name of the key in the *.config file of the executing application which contains the connection string.</summary>
    /// <remarks>Default: the value set in the LLBLGen Pro project properties</remarks>
    public static string ConnectionStringKeyName = "Main.ConnectionString";

    /// <summary>CTor</summary>
    public DataAccessAdapter() : this(ReadConnectionStringFromConfig(), false, null, null)
    {
    }

    /// <summary>CTor</summary>
    /// <param name="keepConnectionOpen">
    ///   when true, the DataAccessAdapter will not close an opened connection. Use this for
    ///   multi action usage.
    /// </param>
    public DataAccessAdapter(bool keepConnectionOpen) : this(ReadConnectionStringFromConfig(), keepConnectionOpen, null, null)
    {
    }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    public DataAccessAdapter(string connectionString) : this(connectionString, false, null, null)
    {
    }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">
    ///   when true, the DataAccessAdapter will not close an opened connection. Use this for
    ///   multi action usage.
    /// </param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen) : this(connectionString, keepConnectionOpen, null, null)
    {
    }

    /// <summary>CTor.</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">
    ///   when true, the DataAccessAdapter will not close an opened connection. Use this for
    ///   multi action usage.
    /// </param>
    /// <param name="catalogNameUsageSetting">
    ///   Configures this data access adapter object how to threat catalog names in
    ///   persistence information.
    /// </param>
    /// <param name="catalogNameToUse"> The name to use if catalogNameUsageSetting is set to ForceName. Ignored otherwise.</param>
    /// <remarks>For backwards compatibility.</remarks>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, CatalogNameUsage catalogNameUsageSetting, string catalogNameToUse)
      : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, catalogNameUsageSetting, SchemaNameUsage.Default, catalogNameToUse, string.Empty, null, null);
    }

    /// <summary>CTor</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">
    ///   when true, the DataAccessAdapter will not close an opened connection. Use this for
    ///   multi action usage.
    /// </param>
    /// <param name="schemaNameUsageSetting">
    ///   Configures this data access adapter object how to threat schema names in
    ///   persistence information.
    /// </param>
    /// <param name="schemaNameToUse">
    ///   Oracle specific. The name to use if schemaNameUsageSetting is set to ForceName. Ignored
    ///   otherwise.
    /// </param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, SchemaNameUsage schemaNameUsageSetting, string schemaNameToUse)
      : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, schemaNameUsageSetting, string.Empty, schemaNameToUse, null, null);
    }

    /// <summary>CTor.</summary>
    /// <param name="connectionString">The connection string to use when connecting to the database.</param>
    /// <param name="keepConnectionOpen">
    ///   when true, the DataAccessAdapter will not close an opened connection. Use this for
    ///   multi action usage.
    /// </param>
    /// <param name="catalogNameOverwrites">
    ///   The from-to name value pairs and setting for the overwriting of catalog names. Can
    ///   be null.
    /// </param>
    /// <param name="schemaNameOverwrites">
    ///   The from-to name value pairs and setting for the overwriting of schema names. Can
    ///   be null.
    /// </param>
    public DataAccessAdapter(string connectionString, bool keepConnectionOpen, CatalogNameOverwriteHashtable catalogNameOverwrites, SchemaNameOverwriteHashtable schemaNameOverwrites)
      : base(PersistenceInfoProviderSingleton.GetInstance())
    {
      InitClassPhase2(connectionString, keepConnectionOpen, CatalogNameUsage.Default, SchemaNameUsage.Default, string.Empty, string.Empty, catalogNameOverwrites, schemaNameOverwrites);
    }

    /// <summary>
    ///   Sets the flag to signal the SqlServer DQE to generate SET ARITHABORT ON statements prior to INSERT, DELETE and UPDATE
    ///   Queries.
    ///   Keep this flag to false in normal usage, but set it to true if you need to write into a table which is part of an
    ///   indexed view.
    ///   It will not affect normal inserts/updates that much, leaving it on is not harmful. See Books online for details on
    ///   SET ARITHABORT ON.
    ///   After each statement the setting is turned off if it has been turned on prior to that statement.
    /// </summary>
    /// <remarks>Setting this flag is a global change.</remarks>
    public static void SetArithAbortFlag(bool value)
    {
      DynamicQueryEngine.ArithAbortOn = value;
    }

    /// <summary>
    ///   Sets the default compatibility level used by the DQE. Default is SqlServer2005. This is a global setting.
    ///   Compatibility level influences the query generated for paging, sequence name (@@IDENTITY/SCOPE_IDENTITY()), and usage
    ///   of newsequenceid() in inserts.
    ///   It also influences the ado.net provider to use. This way you can switch between SqlServer server client 'SqlClient'
    ///   and SqlServer CE Desktop.
    /// </summary>
    /// <remarks>
    ///   Setting this property will overrule a similar setting in the .config file. Don't set this property when queries are
    ///   executed as
    ///   it might switch factories for ADO.NET elements which could result in undefined behavior so set this property at
    ///   startup of your application
    /// </remarks>
    public static void SetSqlServerCompatibilityLevel(SqlServerCompatibilityLevel compatibilityLevel)
    {
      DynamicQueryEngine.DefaultCompatibilityLevel = compatibilityLevel;
    }

    /// <summary>Creates a new Dynamic Query engine object and passes in the defined catalog/schema overwrite hashtables.</summary>
    protected override DynamicQueryEngineBase CreateDynamicQueryEngine()
    {
      return PostProcessNewDynamicQueryEngine(new DynamicQueryEngine());
    }

    /// <summary>
    ///   Reads the value of the setting with the key ConnectionStringKeyName from the *.config file and stores that
    ///   value as the active connection string to use for this object.
    /// </summary>
    /// <returns>connection string read</returns>
    static string ReadConnectionStringFromConfig()
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
      if (_compatibilityLevel.HasValue) ((DynamicQueryEngine)dqe).CompatibilityLevel = _compatibilityLevel.Value;
    }

    SqlServerCompatibilityLevel? _compatibilityLevel;


    /// <summary>
    ///   The per-instance compatibility level used by this DQE instance. Default is the one set globally, which is by default
    ///   SqlServer2005 (for 2005+).
    ///   Compatibility level influences the query generated for paging, sequence name (@@IDENTITY/SCOPE_IDENTITY()), and usage
    ///   of newsequenceid() in inserts.
    ///   It also influences the ado.net provider to use. This way you can switch between SqlServer server client 'SqlClient'
    ///   and SqlServer CE Desktop.
    /// </summary>
    public SqlServerCompatibilityLevel? CompatibilityLevel
    {
      get => _compatibilityLevel;
      set => _compatibilityLevel = value;
    }

    // __LLBLGENPRO_USER_CODE_REGION_START CustomDataAccessAdapterCode

    static readonly Type PersistenceCoreType = typeof(PersistenceCore);
    static readonly dynamic ExposedPersistenceCore = Exposed.From(PersistenceCoreType);
    static readonly Type EntityBase2Type = typeof(EntityBase2);
    static readonly Type ExcludedFieldsBatchQueryParametersType = typeof(IEntityCollection2).Assembly.GetType("SD.LLBLGen.Pro.ORMSupportClasses.ExcludedFieldsBatchQueryParameters`1");
    static readonly Type ExcludedFieldsBatchQueryParametersSpecificType = ExcludedFieldsBatchQueryParametersType.MakeGenericType(EntityBase2Type);
    static readonly MemberGetter GetNumberOfBatches = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "NumberOfBatches");
    static readonly MemberGetter GetDummy = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "Dummy");
    static readonly MemberGetter GetBatchSize = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "BatchSize");
    static readonly MemberGetter GetNumberOfPkFields = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "NumberOfPkFields");
    static readonly MemberGetter MemberGetter = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "Filter");

    static DataAccessAdapter()
    {
    }

    public async Task<IDataReader> FetchExcludedFieldsAsStreamsAsync(IEntity2 entity, ExcludeIncludeFieldsList excludedIncludedFields, CancellationToken cancellationToken)
    {
      var dataReader = await FetchDataReaderAsync(entity, excludedIncludedFields, cancellationToken);
      if (dataReader is DbDataReader reader && await reader.ReadAsync(cancellationToken).ConfigureAwait(GeneralHelper.ContinueOnCapturedContext))
        foreach (var field in excludedIncludedFields)
        {
          var stream = DataHelper.GetStream(reader, field.FieldIndex);
          entity.Fields.SetCurrentValue(field.FieldIndex, stream);
        }

      return dataReader;
    }

    public Task<IDataReader> FetchDataReaderAsync(IEntity2 entity, ExcludeIncludeFieldsList excludedIncludedFields, CancellationToken cancellationToken)
    {
      var retrievalQuery = CreateSelectQueryToFetch(entity, excludedIncludedFields);
      return FetchDataReaderAsync(retrievalQuery, CommandBehavior.SequentialAccess, cancellationToken);
    }

    public IRetrievalQuery CreateSelectQueryToFetch(IEntity2 entityToFetch, ExcludeIncludeFieldsList excludedIncludedFields)
    {
      TraceHelper.WriteLineIf(TraceHelper.PersistenceExecutionSwitch.TraceInfo, "DataAccessAdapterBase.FetchEntity(4)", "Method Enter");

      var filter = new RelationPredicateBucket();
      var pkFilter = CreatePrimaryKeyFilter(entityToFetch.Fields.PrimaryKeyFields);
      if (pkFilter == null)
        throw new ORMQueryConstructionException("The entity '" + entityToFetch.LLBLGenProEntityName +
                                                "' doesn't have a PK defined, and FetchEntity therefore can't create a query to fetch it. Please define a PK on the entity");
      filter.PredicateExpression.Add(pkFilter);

      var fetchResult = CreateSelectQueryUsingFilter(entityToFetch, filter, excludedIncludedFields);
      TraceHelper.WriteLineIf(TraceHelper.PersistenceExecutionSwitch.TraceInfo, "DataAccessAdapterBase.FetchEntity(4)", "Method Exit");
      return fetchResult;
    }

    IRetrievalQuery CreateSelectQueryUsingFilter(IEntity2 entityToFetch, IRelationPredicateBucket filter, ExcludeIncludeFieldsList excludedIncludedFields)
    {
      if (filter == null) throw new ORMGeneralOperationException("Filter can't be null.");
      if (entityToFetch == null)
      {
        TraceHelper.WriteLineIf(TraceHelper.PersistenceExecutionSwitch.TraceInfo, "DataAccessAdapterBase.FetchEntityUsingFilter(5): entity is null.", "Method Exit");
        return null;
      }

      var queryCreationManager = CreateQueryCreationManager(PersistenceInfoProviderSingleton.GetInstance());
      var exposedQueryCreationManager = Exposed.From(queryCreationManager);
      var discriminatorFieldIndex = -1;
      var entityToFetchAsEntityBase2 = (EntityBase2)entityToFetch;
      var entityToFetchAsEntityBase2Exposed = Exposed.From(entityToFetchAsEntityBase2);
      var hierarchyType = (InheritanceHierarchyType)entityToFetchAsEntityBase2Exposed.GetInheritanceHierarchyType();
      var inheritanceInfoProvider = entityToFetchAsEntityBase2Exposed.GetInheritanceInfoProvider() as IInheritanceInfoProvider;
      ExposedPersistenceCore.AddInheritanceRelatedElementsToQueryElementsForEntities(hierarchyType, filter.PredicateExpression, filter.Relations,
        inheritanceInfoProvider, entityToFetch.LLBLGenProEntityName, ref discriminatorFieldIndex);
      ExposedPersistenceCore.PreprocessDerivedTablesForInheritanceInfo(filter.Relations.GetAllDerivedTables(), inheritanceInfoProvider);

      KeepConnectionOpen = true;
      IFieldPersistenceInfo[] persistenceInfos = exposedQueryCreationManager.GetFieldPersistenceInfos(entityToFetch.Fields);
      // remove the persistence infos from the persistentInfos array (set the slots to null) for the excluded fields in excludedFields.
      ExposedPersistenceCore.ExcludeFieldsFromPersistenceInfos(entityToFetch.Fields, persistenceInfos, excludedIncludedFields, hierarchyType, discriminatorFieldIndex);
      var fetchResult = CreateSelectQueryUsingFilter(entityToFetch.Fields, persistenceInfos, filter);

      TraceHelper.WriteLineIf(TraceHelper.PersistenceExecutionSwitch.TraceInfo, "DataAccessAdapterBase.FetchEntityUsingFilter(5)", "Method Exit");
      return fetchResult;
    }

    IRetrievalQuery CreateSelectQueryUsingFilter(IEntityFields2 fieldsToFetch, IFieldPersistenceInfo[] persistenceInfos, IRelationPredicateBucket filter)
    {
      TraceHelper.WriteLineIf(TraceHelper.PersistenceExecutionSwitch.TraceInfo, "DataAccessAdapterBase.FetchEntityUsingFilter(3)", "Method Enter");

      fieldsToFetch.State = EntityState.OutOfSync;
      IPredicateExpression predicateExpressionToUse = null;
      var relationsPresent = false;

      var queryCreationManager = CreateQueryCreationManager(PersistenceInfoProviderSingleton.GetInstance());
      var exposedQueryCreationManager = Exposed.From(queryCreationManager);
      exposedQueryCreationManager.InterpretFilterBucket(filter, ref relationsPresent, ref predicateExpressionToUse);

      foreach (var expressionField in fieldsToFetch.FieldsWithExpressionOrAggregate)
        exposedQueryCreationManager.InsertPersistenceInfoObjects(expressionField as IEntityField2);

      IRetrievalQuery selectQuery = exposedQueryCreationManager.CreateSelectDQ(new QueryParameters(0, 0, 0)
      {
        FilterToUse = predicateExpressionToUse,
        RelationsToUse = filter.Relations,
        AllowDuplicates = true,
        FieldsForQuery = fieldsToFetch,
        FieldPersistenceInfosForQuery = persistenceInfos,
        IsLocalCopy = true
      });
      return selectQuery;
    }

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
    public async Task FetchExcludedFieldsAsStreamsAsync(IEntity2 entity, ExcludeIncludeFieldsList excludedIncludedFields,
      Action<IEntityCore> doSomethingWithStreams, CancellationToken cancellationToken)
    {
      if (excludedIncludedFields == null || excludedIncludedFields.Count <= 0 || entity == null) return;

      var collection = new EntityCollectionNonGeneric(entity.GetEntityFactory()) { (EntityBase2)entity };
      await FetchExcludedFieldsAsStreamsAsync(collection, excludedIncludedFields, doSomethingWithStreams, cancellationToken).ConfigureAwait(false);
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
    public virtual async Task FetchExcludedFieldsAsStreamsAsync(IEntityCollection2 entities, ExcludeIncludeFieldsList excludedIncludedFields, Action<IEntityCore> doSomethingWithStreams,
      CancellationToken cancellationToken)
    {
      if (entities == null || entities.Count <= 0) return;
      if (entities.EntityFactoryToUse == null) throw new ORMGeneralOperationException("The entity factory of the passed in entities collection is null.");


      var parameters = Activator.CreateInstance(ExcludedFieldsBatchQueryParametersSpecificType);

      var produceElementsForExcludedFieldBatchFetchesMethod = PersistenceCoreType.GetMethod("ProduceElementsForExcludedFieldBatchFetches", BindingFlags.NonPublic | BindingFlags.Static);

      // Binding the method info to generic arguments
      Type[] genericArguments = { EntityBase2Type };
      if (produceElementsForExcludedFieldBatchFetchesMethod != null)
      {
        var produceElementsForExcludedFieldBatchFetchesMethodInfo = produceElementsForExcludedFieldBatchFetchesMethod.MakeGenericMethod(genericArguments);

        var result = produceElementsForExcludedFieldBatchFetchesMethodInfo.Invoke(null, new[]
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
          var parametersExposed = Exposed.From(parameters);

          var ExcludedFieldsBatchQueryParametersType = parameters.GetType();
          var propertyInfo = ExcludedFieldsBatchQueryParametersSpecificType.GetRuntimeProperties().Last();
          var x = ExcludedFieldsBatchQueryParametersType.GetProperty("ResultFields", BindingFlags.NonPublic);
          var resultFields = propertyInfo.GetValue(parameters) as IEntityFieldsCore;
          // var resultFields = parameters.ResultFields;
          var fieldPersistenceInfos = exposedQueryCreationManager.GetFieldPersistenceInfos(resultFields) as IFieldPersistenceInfo[];

          var fetchExcludedFieldsBatchesAsyncMethod = PersistenceCoreType.GetMethod("FetchExcludedFieldsBatchesAsync", BindingFlags.NonPublic | BindingFlags.Static);
          var fetchExcludedFieldsBatchesAsyncMethodInfo = fetchExcludedFieldsBatchesAsyncMethod.MakeGenericMethod(genericArguments);

          //var reader = await ((IDataAccessCore)this).FetchExcludedFieldBatchAsync(parameters.ResultFields, parameters.Filter, parameters.BatchSize, cancellationToken)
          //  .ConfigureAwait(false);

          //var aResult = (Task)FetchExcludedFieldsBatchesAsyncMethodInfo.Invoke(null, new object[] {this, entities, parameters, fieldPersistenceInfos, cancellationToken });
          //await aResult;

          var entityHashes = new Dictionary<int, List<IEntityCore>>();
          ExposedPersistenceCore.CreateEntityHashes(entityHashes, entities);
          IEntityFieldsCore hashProducer = entities[0].Fields.Clone();

          var currentIndex = 0;
          var dummy = GetDummy(parameters) as IEntityCore;
          var batchSize = (int)GetBatchSize(parameters);
          var numberOfPkFields = (int)GetNumberOfPkFields(parameters);
          var getFilter = MemberGetter;
          var filter = getFilter(parameters) as IRelationPredicateBucket;
          var getExcludedFieldsToUse = Reflect.Getter(ExcludedFieldsBatchQueryParametersSpecificType, "ExcludedFieldsToUse");
          var excludedFieldsToUse = getExcludedFieldsToUse(parameters) as List<IEntityFieldCore>;
          var numberOfBatches = GetNumberOfBatches(parameters) as int?;
          for (var i = 0; i < numberOfBatches; i++)
          {
            List<IEntityFieldCore> pkFieldsToPass = ExposedPersistenceCore.PrepareExcludedFieldsBatchFetchElements(entities, dummy, batchSize,
              numberOfPkFields, filter, currentIndex);
            // fetch batch using a datareader.
            IDataReader reader = null;
            try
            {
              reader = await FetchDataReaderAsync(CommandBehavior.SequentialAccess,
                new QueryParameters(0, 0, batchSize, filter)
                {
                  SorterToUse = null,
                  GroupByToUse = null,
                  AllowDuplicates = true,
                  FieldsForQuery = resultFields as IEntityFields2,
                  IsLocalCopy = true
                }, cancellationToken).ConfigureAwait(false);
              ConsumeExcludedFieldsValueBatch(ExposedPersistenceCore, dummy, numberOfPkFields, excludedFieldsToUse, resultFields, fieldPersistenceInfos,
                entityHashes, hashProducer, pkFieldsToPass, reader, doSomethingWithStreams);
            }
            finally
            {
              var cleanupDataReaders = PersistenceCoreType.GetRuntimeMethods().Where(m => m.Name.Contains("CleanupDataReader"));
              var methodInfo = cleanupDataReaders.First();
              methodInfo.Invoke(null, new[] { reader, null });
              //var y = Reflect.Method(persistenceCoreType, "CleanupDataReader");
              //y.Invoke(null, reader, null, true);
              //exposedPersistenceCore.CleanupDataReader(reader, null);
            }

            currentIndex += batchSize;
          }
        }
        finally
        {
          KeepConnectionOpen = keepConnectionOpenSave;
          CloseConnectionIfPossible();
        }
      }
    }

    /// <summary>
    ///   Consumes the excluded fields value batch.
    /// </summary>
    /// <param name="dummy">The dummy.</param>
    /// <param name="numberOfPkFields">The number of pk fields.</param>
    /// <param name="excludedFieldsToUse">The excluded fields to use.</param>
    /// <param name="resultFields">The result fields.</param>
    /// <param name="persistenceInfos">The persistence infos.</param>
    /// <param name="entityHashes">The entity hashes.</param>
    /// <param name="hashProducer">The hash producer.</param>
    /// <param name="pkFieldsToPass">The pk fields to pass.</param>
    /// <param name="reader">The reader.</param>
    static void ConsumeExcludedFieldsValueBatch(dynamic exposedPersistenceCore, IEntityCore dummy, int numberOfPkFields, IList excludedFieldsToUse, IEntityFieldsCore resultFields,
      IFieldPersistenceInfo[] persistenceInfos, Dictionary<int, List<IEntityCore>> entityHashes,
      IEntityFieldsCore hashProducer, List<IEntityFieldCore> pkFieldsToPass, IDataReader reader, Action<IEntityCore> doSomethingWithStreams)
    {
      while (reader.Read())
      {
        // calculate hash from pk fields in row. Do this by setting the pk fields in hashProducer and grab the hashvalue. 
        var isConverted = false;
        for (var j = 0; j < numberOfPkFields; j++)
        {
          // pk fields are at the front. 
          var index = ((IEntityFieldCore)dummy.Fields.PrimaryKeyFields[j]).FieldIndex;
          var fieldInfo = hashProducer.GetFieldInfo(index);
          var val = reader.GetValue(j);
          var value = FieldUtilities.DetermineValueToSet(fieldInfo, persistenceInfos[j], val, out isConverted);
          hashProducer.SetCurrentValue(index, value);
        }

        var childHash = hashProducer.GetHashCode();
        IEntityCore currentEntity = exposedPersistenceCore.FindPkObject(entityHashes, childHash, hashProducer.PrimaryKeyFields, pkFieldsToPass);
        if (currentEntity == null) continue;
        for (var j = 0; j < excludedFieldsToUse.Count; j++)
        {
          var excludedField = (IEntityFieldCore)excludedFieldsToUse[j];
          var index = currentEntity.Fields.GetFieldIndex(excludedField.Name);
          if (index < 0)
            // unknown
            continue;
          if (reader is DbDataReader dbDataReader)
          {
            var val = dbDataReader.GetStream(numberOfPkFields + j);
            currentEntity.Fields.SetCurrentValue(index, val);
          }
        }

        doSomethingWithStreams(currentEntity);
      }
    }

    /// <summary> Closes the connection if possible (i.e. when keepConnectionOpen is false and isTransactionInProgress is false </summary>
    void CloseConnectionIfPossible()
    {
      if (!(KeepConnectionOpen || IsTransactionInProgress)) CloseConnection();
    }

    /// <summary>
    ///   Creates a new Insert Query object which is ready to use.
    /// </summary>
    /// <returns>IActionQuery Instance which is ready to be used.</returns>
    /// <remarks>Generic version.</remarks>
    /// <exception cref="T:System.ArgumentNullException">When fields is null or fieldsPersistenceInfo is null</exception>
    /// <exception cref="T:System.ArgumentException">
    ///   When fields contains no EntityFieldCore instances or fieldsPersistenceInfo
    ///   is empty.
    /// </exception>
    /// <exception cref="T:SD.LLBLGen.Pro.ORMSupportClasses.ORMQueryConstructionException">
    ///   When there are no fields to insert in the fields list. This exception is to prevent
    ///   INSERT INTO table () VALUES () style queries.
    /// </exception>
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