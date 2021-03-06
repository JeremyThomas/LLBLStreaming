using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fasterflect;
using Microsoft.CSharp.RuntimeBinder;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.QuerySpec;
using Expression = System.Linq.Expressions.Expression;

namespace AW.Helper.LLBL
{
  public static class EntityHelper
  {
    #region Linq

    /// <summary>
    ///   returns the DataSource to use in a Linq query for the entity type specified
    /// </summary>
    /// <typeparam name="T">the type of the entity to get the DataSource for</typeparam>
    /// <param name="entity">The entity.</param>
    /// <param name="linqMetaData">The Linq meta data.</param>
    /// <returns>the requested DataSource</returns>
    public static DataSourceBase<T> GetQueryableForEntity<T>(this ILinqMetaData linqMetaData, T entity)
      where T : class, IEntityCore
    {
      return linqMetaData.GetQueryableForEntity(entity.LLBLGenProEntityTypeValue) as DataSourceBase<T>;
    }

    /// <summary>returns the DataSource to use in a Linq query for the entity type specified</summary>
    /// <typeparam name="T">the type of the entity to get the DataSource for</typeparam>
    /// <returns>the requested DataSource</returns>
    public static DataSourceBase<T> GetQueryableForEntity<T>(this ILinqMetaData linqMetaData)
      where T : class, IEntityCore
    {
      try
      {
        return linqMetaData.GetQueryableForEntity(CreateEntity<T>());
      }
      catch (Exception)
      {
        try
        {
          return GetQueryableForEntitySafely(linqMetaData, typeof(T)) as DataSourceBase<T>;
        }
        catch (Exception)
        {
          return null;
        }
      }
    }

    public delegate IQueryable GetQueryableForEntityDelegate(ILinqMetaData linqMetaData, Type typeOfEntity);

    public static IQueryable GetQueryableForEntityIgnoreIfNull(ILinqMetaData linqMetaData, Type typeOfEntity)
    {
      IQueryable entityQueryable = null;
      if (typeOfEntity != null && linqMetaData != null)
      {
        var dataSource = linqMetaData.GetQueryableForEntity(typeOfEntity);
        entityQueryable = dataSource as IQueryable;
      }

      return entityQueryable;
    }

    public static IDataSource GetQueryableForEntity(this ILinqMetaData linqMetaData, Type typeOfEntity)
    {
      try
      {
        var entityTypeValueForType = GetEntityTypeValueForType(typeOfEntity);
        return linqMetaData.GetQueryableForEntity(entityTypeValueForType);
      }
      catch (Exception)
      {
        try
        {
          return GetQueryableForEntitySafely(linqMetaData, typeOfEntity);
        }
        catch (Exception)
        {
          return null;
        }
      }
    }

    private static IDataSource GetQueryableForEntitySafely(ILinqMetaData linqMetaData, Type typeOfEntity)
    {
      for (var entityTypeValueForType = 0; entityTypeValueForType < int.MaxValue; entityTypeValueForType++)
      {
        var queryableForEntity = linqMetaData.GetQueryableForEntity(entityTypeValueForType);
        if (queryableForEntity.ElementType == typeOfEntity)
          return queryableForEntity;
      }

      return null;
    }

    public static IQueryProvider GetProvider(ILinqMetaData linqMetaData)
    {
      return linqMetaData == null ? null : linqMetaData.GetQueryableForEntity(0).Provider;
    }

    public static IQueryable CreateLLBLGenProQueryFromEnumerableExpression(IQueryProvider provider,
      Expression expression)
    {
      var elementType = expression.Type;
      if (expression.Type.IsGenericType) elementType = expression.Type.GetGenericArguments()[0];
      var queryableType = typeof(IQueryable<>).MakeGenericType(elementType);
      if (queryableType.IsAssignableFrom(expression.Type)) provider.CreateQuery(expression);
      var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
      if (!enumerableType.IsAssignableFrom(expression.Type)) throw new ArgumentException("expression isn't enumerable");
      return (IQueryable)Activator.CreateInstance(typeof(LLBLGenProQuery<>).MakeGenericType(elementType), provider,
        expression);
    }

    public static Tuple<Expression, Expression, Expression> GetMethodCallExpressionParts(
      MethodCallExpression methodCallExpression)
    {
      return GetMethodCallExpressionParts(new Tuple<Expression, Expression, Expression>(null, null, null),
        methodCallExpression);
    }

    private static Tuple<Expression, Expression, Expression> GetMethodCallExpressionParts(
      Tuple<Expression, Expression, Expression> result
      , Expression expression)
    {
      if (expression is MethodCallExpression methodCallExpression)
      {
        if (methodCallExpression.Method.Name == "Where")
          result = new Tuple<Expression, Expression, Expression>(result.Item1, methodCallExpression.Arguments.Last(),
            result.Item3);
        if (methodCallExpression.Method.Name == "OrderBy")
          result = new Tuple<Expression, Expression, Expression>(result.Item1, result.Item2,
            methodCallExpression.Arguments.Last());
        result = methodCallExpression.Arguments.Aggregate(result, GetMethodCallExpressionParts);
      }
      else
      {
        if (expression.NodeType == ExpressionType.Constant)
          result = new Tuple<Expression, Expression, Expression>(expression, result.Item2, result.Item3);
      }

      return result;
    }

    #endregion Linq

    #region CreateEntity

    /// <summary>
    ///   Creates the entity from the .NET type specified.
    /// </summary>
    /// <param name="typeOfEntity">The type of entity.</param>
    /// <returns>An instance of the type.</returns>
    public static IEntityCore CreateEntity(Type typeOfEntity)
    {
      try
      {
        return Activator.CreateInstance(typeOfEntity) as IEntityCore;
      }
      catch (MissingMethodException)
      {
        var elementCreatorCore = CreateElementCreator(typeOfEntity);
        if (elementCreatorCore != null)
          return LinqUtils.CreateEntityInstanceFromEntityType(typeOfEntity, elementCreatorCore);
        throw;
      }
    }

    public static IElementCreatorCore CreateElementCreator(Type typeInTheSameAssemblyAsElementCreator)
    {
      return typeof(IElementCreatorCore).IsAssignableFrom(typeInTheSameAssemblyAsElementCreator)
        ? CreateElementCreatorFromType(typeInTheSameAssemblyAsElementCreator)
        : CreateElementCreator(typeInTheSameAssemblyAsElementCreator.Assembly.GetExportedTypes());
    }

    public static IElementCreatorCore CreateElementCreator(IEnumerable<Type> types)
    {
      var elementCreatorCoreType = typeof(IElementCreatorCore).GetAssignable(types).FirstOrDefault();
      return elementCreatorCoreType == null ? null : CreateElementCreatorFromType(elementCreatorCoreType);
    }

    private static IElementCreatorCore CreateElementCreatorFromType(Type elementCreatorCoreType)
    {
      return Activator.CreateInstance(elementCreatorCoreType) as IElementCreatorCore;
    }

    /// <summary>
    ///   Creates the entity from the .NET type specified.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <returns>An instance of the type.</returns>
    public static T CreateEntity<T>() where T : class, IEntityCore
    {
      return CreateEntity(typeof(T)) as T;
    }

    #endregion CreateEntity

    #region GetFactoryCore

    //public static IEntityFactory GetFactory<T>() where T : EntityBase
    //{
    //  return ((IEntity) CreateEntity<T>()).GetEntityFactory();
    //}

    public static IEntityFactoryCore GetFactoryCore<T>(IElementCreatorCore elementCreatorCore)
      where T : class, IEntityCore
    {
      return (elementCreatorCore.GetFactory(typeof(T)));
    }

    public static IEntityFactoryCore GetFactoryCore(IEntityCore entity)
    {
      if (!(entity is IEntity2 entity2))
        return ((IEntity)entity).GetEntityFactory();
      return entity2.GetEntityFactory();
    }

    /// <summary>
    ///   Gets the factory of the entity with the .NET type specified
    /// </summary>
    /// <remarks>See DataScope.GetEntityFactory</remarks>
    /// <typeparam name="T">The type of entity.</typeparam>
    /// <returns>factory to use or null if not found</returns>
    public static IEntityFactoryCore GetFactoryCore<T>() where T : class, IEntityCore
    {
      return GetFactoryCore(CreateEntity<T>());
    }

    public static IEntityFactoryCore GetFactoryCore(Type type)
    {
      return GetFactoryCore(CreateEntity(type));
    }

    private static IEntityFactoryCore GetFactoryCore<T>(IEnumerable<T> enumerable) where T : class, IEntityCore
    {
      return GetFactoryCore(enumerable, typeof(T));
    }

    private static IEntityFactoryCore GetFactoryCore(IEnumerable enumerable, Type type)
    {
      if (!enumerable.IsNullOrEmpty())
        if (enumerable.OfType<object>().FirstOrDefault(e => e.GetType() == type) is IEntityCore first)
          return GetFactoryCore(first);

      return GetFactoryCore(type);
    }

    #endregion GetFactoryCore

    #region GetEntitiesType

    /// <summary>
    ///   Gets the EntityType value as integer for the entity type passed in
    /// </summary>
    /// <param name="typeOfEntity">The type of entity.</param>
    /// <returns>the EntityType value as integer for the entity type passed in</returns>
    public static int GetEntityTypeValueForType(Type typeOfEntity)
    {
      var entity = CreateEntity(typeOfEntity);
      return entity == null ? 0 : entity.LLBLGenProEntityTypeValue;
    }

    public static int GetEntityTypeValueForType<T>() where T : class, IEntityCore
    {
      return GetEntityTypeValueForType(typeof(T));
    }

    public static IEnumerable<Type> GetEntitiesTypes()
    {
      return MetaDataHelper.GetAllLoadedDescendants(typeof(IEntityCore));
    }

    public static IEnumerable<Type> GetEntitiesTypes(Assembly entityAssembly)
    {
      return typeof(IEntityCore).GetAssignable(entityAssembly.GetExportedTypes());
    }

    public static IEnumerable<Type> GetEntitiesTypes(ILinqMetaData linqMetaData)
    {
      var entitiesTypes = GetEntitiesTypes(linqMetaData.GetType().Assembly);
      if (entitiesTypes.Any())
        return entitiesTypes;
      var topLevelProps =
        from prop in linqMetaData.GetType().GetProperties()
        where prop.PropertyType.IsAssignableTo(typeof(IDataSource)) && prop.PropertyType.IsGenericType
        let typeArgument = prop.PropertyType.GetGenericArguments()[0]
        where typeArgument != null
        select typeArgument;
      return topLevelProps;
    }

    public static IEnumerable<Type> GetEntitiesTypes(Type ancestorType, ILinqMetaData linqMetaData = null)
    {
      if (ancestorType != null)
        return MetaDataHelper.GetDescendants(ancestorType);
      return linqMetaData == null ? GetEntitiesTypes() : GetEntitiesTypes(linqMetaData);
    }

    #endregion GetEntitiesType

    public static IEntityCollectionCore ToEntityCollection(IEnumerable enumerable, Type itemType)
    {
      if (enumerable is IEntityCollectionCore entities)
        return entities;
      var llblQuery = enumerable as ILLBLGenProQuery;
      entities = ToEntityCollectionCore(llblQuery);
      if (entities == null)
      {
        var entityFactoryCore = GetFactoryCore(enumerable, itemType);
        entities = !(entityFactoryCore is IEntityFactory entityFactory)
          ? (IEntityCollectionCore)((IEntityFactory2)entityFactoryCore).CreateEntityCollection()
          : entityFactory.CreateEntityCollection();
        foreach (IEntityCore item in enumerable)
          entities.Add(item);
      }

      if (!entities.IsNullOrEmpty())
        entities.RemovedEntitiesTracker = entities.EntityFactoryToUse.CreateEntityCollection();
      return entities;
    }

    public static async Task<IEntityCollectionCore> ToEntityCollectionAsync(IEnumerable enumerable, Type itemType,
      CancellationToken cancellationToken)
    {
      if (enumerable is IEntityCollectionCore entities)
        return entities;
      var llblQuery = enumerable as ILLBLGenProQuery;
      entities = await ToEntityCollectionCoreAsync(llblQuery, cancellationToken);
      if (entities == null)
      {
        var entityFactoryCore = GetFactoryCore(enumerable, itemType);
        entities = !(entityFactoryCore is IEntityFactory entityFactory)
          ? (IEntityCollectionCore)((IEntityFactory2)entityFactoryCore).CreateEntityCollection()
          : entityFactory.CreateEntityCollection();
        foreach (IEntityCore item in enumerable)
          entities.Add(item);
      }

      if (!entities.IsNullOrEmpty())
        entities.RemovedEntitiesTracker = entities.EntityFactoryToUse.CreateEntityCollection();
      return entities;
    }

    public static IEntityCollectionCore ToEntityCollectionCore(ILLBLGenProQuery llblQuery)
    {
      if (llblQuery != null)
        return llblQuery.Execute<IEntityCollectionCore>();
      return null;
    }

    public static async Task<IEntityCollectionCore> ToEntityCollectionCoreAsync(ILLBLGenProQuery llblQuery,
      CancellationToken cancellationToken)
    {
      if (llblQuery != null)
        return await llblQuery.ExecuteAsync<IEntityCollectionCore>(cancellationToken);
      return null;
    }

    public static IBindingListView CreateEntityView(IEnumerable enumerable, Type itemType)
    {
      var entityCollectionCore = ToEntityCollection(enumerable, itemType);
      if (entityCollectionCore == null) return null;
      if (!(entityCollectionCore is IEntityCollection entityCollection))
      {
        var defaultView2 = ((IEntityCollection2)entityCollectionCore).DefaultView;
        defaultView2.DataChangeAction = PostCollectionChangeAction.NoAction;
        return defaultView2 as IBindingListView;
      }

      var defaultView = entityCollection.DefaultView;
      defaultView.DataChangeAction = PostCollectionChangeAction.NoAction;
      return defaultView as IBindingListView;
    }

    public static async Task<IBindingListView> CreateEntityViewAsync(IEnumerable enumerable, Type itemType,
      CancellationToken cancellationToken)
    {
      var entityCollectionCore = await ToEntityCollectionAsync(enumerable, itemType, cancellationToken);
      if (entityCollectionCore == null) return null;
      if (!(entityCollectionCore is IEntityCollection entityCollection))
      {
        var defaultView2 = ((IEntityCollection2)entityCollectionCore).DefaultView;
        defaultView2.DataChangeAction = PostCollectionChangeAction.NoAction;
        return defaultView2 as IBindingListView;
      }

      var defaultView = entityCollection.DefaultView;
      defaultView.DataChangeAction = PostCollectionChangeAction.NoAction;
      return defaultView as IBindingListView;
    }

    public static IEntityCollectionCore GetRelatedCollection(IBindingList entityView)
    {
      if (!(entityView is IEntityView view))
      {
        if (entityView is IEntityView2 view2)
          return view2.RelatedCollection;
      }
      else
      {
        return view.RelatedCollection;
      }

      return null;
    }

    public static IEnumerable<IEntityCore> AsEnumerable(this IEntityCollectionCore entityCollection)
    {
      return entityCollection.Cast<IEntityCore>();
    }

    public static IEnumerable<IEntityFieldCore> GetFields(this IEntityCore entityCore)
    {
      return entityCore.Fields;
    }

    public static IEnumerable<IEntityFieldCore> GetChangedFields(this IEntityCore entity)
    {
      return entity.IsDirty ? GetFields(entity).Where(f => f.IsChanged) : Enumerable.Empty<IEntityFieldCore>();
    }

    /// <summary>
    ///   Reverts the changes to database value.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public static void RevertChangesToDBValue(this IEntityCore entity)
    {
      foreach (var changedField in entity.GetChangedFields())
        changedField.ForcedCurrentValueWrite(changedField.DbValue, changedField.DbValue);
      ResetErrors(entity);
      entity.IsDirty = false;
    }

    /// <summary>
    ///   Resets the errors and remove any new entities.
    /// </summary>
    /// <param name="entityCollection">The entity collection.</param>
    private static void ResetErrorsAndRemoveNew(IEntityCollectionCore entityCollection)
    {
      foreach (var entity in entityCollection.AsEnumerable())
        ResetErrors(entity);
      var newEntities = entityCollection.AsEnumerable().Where(e => e.IsNew).ToList();
      foreach (var newEntity in newEntities)
      {
        newEntity.ActiveContext = null;
        entityCollection.Remove(newEntity);
      }
    }

    /// <summary>
    ///   Reverts the changes to database value and removes any new entities
    /// </summary>
    /// <param name="entityCollection">The entity collection.</param>
    public static void RevertChangesToDBValue(IEntityCollectionCore entityCollection)
    {
      if (!(entityCollection is IEntityCollection collection))
      {
        if (entityCollection is IEntityCollection2 entityCollection2)
          entityCollection2.RevertChangesToDBValue();
      }
      else
      {
        collection.RevertChangesToDBValue();
      }
    }

    /// <summary>
    ///   Reverts the changes to database value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities">The entities.</param>
    public static void RevertChangesToDBValue<T>(IEnumerable<T> entities) where T : class, IEntityCore
    {
      foreach (var dirtyEntity in WhereIsDirty(entities))
        dirtyEntity.RevertChangesToDBValue();
    }

    public static IEnumerable<T> WhereIsDirty<T>(this IEnumerable<T> entities) where T : class, IEntityCore
    {
      return entities.Where(e => e.IsDirty);
    }

    public static bool IsAnyDirty(this IEnumerable<IEntityCore> entities)
    {
      return entities.Any(e => e.IsDirty);
    }

    public static IEnumerable<IEntityCore> WhereNotDeleted(this IEnumerable<IEntityCore> entities)
    {
      return entities.Where(entity => entity.Fields.State != EntityState.Deleted);
    }

    public static IEnumerable<IEntityCore> WhereDeleted(this IEnumerable<IEntityCore> entities)
    {
      return entities.Where(entity => IsDeleted(entity));
    }

    public static bool IsDeleted(this IEntityCore entity)
    {
      return entity.Fields.State == EntityState.Deleted;
    }

    /// <summary>
    ///   Reverts the changes to database value, removes any new entities and restores any deleted ones from
    ///   RemovedEntitiesTracker.
    /// </summary>
    /// <param name="modifiedEntities">
    ///   The modified entities, which can be an
    ///   IEntityView,IEntityView2,IBindingListView,IEntityCollectionCore or IEnumerable[IEntityCore].
    /// </param>
    public static void RevertChangesToDBValue(IEnumerable modifiedEntities)
    {
      var postCollectionChangeActionNoAction = false;
      IEntityView2 view2 = null;
      var view = modifiedEntities as IEntityView;
      if (view == null)
        view2 = modifiedEntities as IEntityView2;
      if (view == null && view2 == null)
      {
        dynamic bindingListView = modifiedEntities as IBindingListView;
        if (bindingListView != null)
          try
          {
            modifiedEntities = bindingListView.List;
            view = modifiedEntities as IEntityView;
            view2 = modifiedEntities as IEntityView2;
          }
          catch (RuntimeBinderException)
          {
            //  MyProperty doesn't exist
          }
      }

      if (view != null)
      {
        modifiedEntities = ((IEntityView)modifiedEntities).RelatedCollection;
        postCollectionChangeActionNoAction = view.DataChangeAction == PostCollectionChangeAction.NoAction;
      }

      if (view2 != null)
      {
        modifiedEntities = ((IEntityView2)modifiedEntities).RelatedCollection;
        postCollectionChangeActionNoAction = view2.DataChangeAction == PostCollectionChangeAction.NoAction;
      }

      if (modifiedEntities is IEntityCollectionCore entities)
      {
        var entityCollection = entities;
        if (entityCollection.RemovedEntitiesTracker != null)
        {
          if (postCollectionChangeActionNoAction)
          {
            if (view != null) view.DataChangeAction = PostCollectionChangeAction.ReapplyFilterAndSorter;
            if (view2 != null) view2.DataChangeAction = PostCollectionChangeAction.ReapplyFilterAndSorter;
          }

          try
          {
            if (!(modifiedEntities is IEntityCollection collection))
            {
              if (modifiedEntities is IEntityCollection2 entityCollection2)
              {
                foreach (var entity in
                         WhereNotDeleted(entityCollection.RemovedEntitiesTracker.AsEnumerable())) //Hasn't been deleted
                  entityCollection2.Add(entity);
                entityCollection.RemovedEntitiesTracker.Clear();
              }
              //entityCollection2.AddRange((IEntityCollection2) entityCollection.RemovedEntitiesTracker);
            }
            else
            {
              collection.AddRange((IEntityCollection)entityCollection.RemovedEntitiesTracker);
            }
          }
          finally
          {
            if (postCollectionChangeActionNoAction)
            {
              if (view != null) view.DataChangeAction = PostCollectionChangeAction.NoAction;
              if (view2 != null) view2.DataChangeAction = PostCollectionChangeAction.NoAction;
            }
          }
        }

        RevertChangesToDBValue(entityCollection);
        if (entityCollection.RemovedEntitiesTracker != null)
          entityCollection.RemovedEntitiesTracker.Clear();
      }
      else
      {
        RevertChangesToDBValue(modifiedEntities.Cast<IEntityCore>());
      }
    }


    /// <summary>
    ///   Reverts the changes to database values, removes any new entities and restores any deleted ones from any
    ///   RemovedEntitiesTracker.
    /// </summary>
    /// <param name="modifiedData">
    ///   The modified data, which can be an
    ///   IEntityCore,IUnitOfWorkCore,IEntityView,IEntityView2,IBindingListView,IEntityCollectionCore or
    ///   IEnumerable[IEntityCore].
    /// </param>
    public static void Undo(object modifiedData)
    {
      if (modifiedData is IUnitOfWorkCore unitOfWorkCore)
      {
        Undo(unitOfWorkCore);
        return;
      }

      var listItemType = MetaDataHelper.GetListItemType(modifiedData);
      if (IsEntityCore(listItemType))
      {
        if (!(modifiedData is IEnumerable enumerable))
        {
          if (modifiedData is IEntity entity)
            RevertChangesToDBValue(entity);
          else
            RevertChangesToDBValue((IEntity2)modifiedData);
        }
        else
        {
          RevertChangesToDBValue(enumerable);
        }
      }
    }

    private static void Undo(IUnitOfWorkCore unitOfWorkCore)
    {
      if (!(unitOfWorkCore is UnitOfWork unitOfWork))
      {
        if (unitOfWorkCore is UnitOfWork2 unitOfWork2)
        {
          foreach (var unitOfWorkCollectionElement2 in unitOfWork2.GetCollectionElementsToSave())
            Undo(unitOfWorkCollectionElement2.Collection);
          foreach (var unitOfWorkElement2 in unitOfWork2.GetEntityElementsToUpdate())
            Undo(unitOfWorkElement2.Entity);
          unitOfWork2.GetEntityElementsToDelete().Clear();
        }
      }
      else
      {
        foreach (var unitOfWorkCollectionElement in unitOfWork.GetCollectionElementsToSave())
          Undo(unitOfWorkCollectionElement.Collection);
        foreach (var unitOfWorkElement in unitOfWork.GetEntityElementsToUpdate()) Undo(unitOfWorkElement.Entity);
        unitOfWork.GetEntityElementsToDelete().Clear();
      }
    }

    public static bool IsDirty(IUnitOfWorkCore unitOfWorkCore)
    {
      if (!(unitOfWorkCore is UnitOfWork unitOfWork))
      {
        if (unitOfWorkCore is UnitOfWork2 unitOfWork2)
        {
          if (unitOfWork2.GetCollectionElementsToSave().Any())
            return true;
          if (unitOfWork2.GetEntityElementsToUpdate().Any(uowe => uowe.Entity.IsDirty))
            return true;
          if (unitOfWork2.GetEntityElementsToDelete().Any())
            return true;
        }
      }
      else
      {
        if (unitOfWork.GetCollectionElementsToSave().Any())
          return true;
        if (unitOfWork.GetEntityElementsToUpdate().Any(uowe => uowe.Entity.IsDirty))
          return true;
        if (unitOfWork.GetEntityElementsToDelete().Any())
          return true;
      }

      return false;
    }

    /// <summary>
    ///   Determines whether the specified data is dirty (Has a CUD) or has field errors from an attempted edit.
    /// </summary>
    /// <param name="data">The data, which can be an IEntityCore or IEntityCollectionCore.</param>
    /// <returns></returns>
    public static bool IsDirty(object data)
    {
      if (!(data is IEntityCore entity))
      {
        if (!(data is IEntityCollection entityCollection))
          return data is IEntityCollection2 entityCollection2 && (entityCollection2.ContainsDirtyContents
                                                                  || ContainsEntityFieldsErrors(entityCollection2)
                                                                  || (entityCollection2.RemovedEntitiesTracker !=
                                                                      null &&
                                                                      entityCollection2.RemovedEntitiesTracker.Count >
                                                                      0))
            //   || entityCollection2.AsEnumerable().Any(e=>e.IsNew)
            ;

        return entityCollection.ContainsDirtyContents || ContainsEntityFieldsErrors(entityCollection);
      }

      return entity.IsDirty || !string.IsNullOrWhiteSpace(GetEntityFieldsErrors(entity));
    }

    public static bool ContainsEntityFieldsErrors(IEntityCollectionCore entityCollection)
    {
      for (var i = 0; i < entityCollection.Count; i++)
        if (!string.IsNullOrWhiteSpace(GetEntityFieldsErrors(entityCollection[i])))
          return true;
      return false;
    }

    #region Self Servicing

    /// <summary>
    ///   Converts an entity enumeration to an entity collection. If the enumeration is a ILLBLGenProQuery then executes
    ///   the query this object represents and returns its results in its native container - an entity collection.
    /// </summary>
    /// <typeparam name="T">EntityBase2</typeparam>
    /// <param name="enumerable">The enumerable.</param>
    /// <returns></returns>
    public static EntityCollectionBase<T> ToEntityCollection<T>(this IEnumerable<T> enumerable) where T : EntityBase
    {
      if (enumerable is EntityCollectionBase<T> entityCollectionBase)
        return entityCollectionBase;
      if (enumerable is ILLBLGenProQuery llblQuery)
        return llblQuery.Execute<EntityCollectionBase<T>>();
      var entities = ((IEntityFactory)GetFactoryCore(enumerable)).CreateEntityCollection() as EntityCollectionBase<T>;
      if (entities != null)
        entities.AddRange(enumerable);
      return entities;
    }

    /// <summary>
    ///   Gets the entity field from the name of the field.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>the entity field</returns>
    public static IEntityField GetFieldFromFieldName(IEntity entity, string fieldName)
    {
      return entity.Fields[fieldName] ?? entity.Fields.AsEnumerable()
        .FirstOrDefault(ef => ef.Name.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));
    }

    /// <summary>
    ///   Gets a entity field enumeration from entity fields.
    /// </summary>
    /// <param name="entityFields">The entity fields.</param>
    /// <returns>entity field enumeration</returns>
    public static IEnumerable<IEntityField> AsEnumerable(this IEntityFields entityFields)
    {
      return entityFields.Cast<IEntityField>();
    }

    /// <summary>
    ///   Reverts the changes to database value  and removes any new entities.
    /// </summary>
    /// <param name="entityCollection">The entity collection.</param>
    public static void RevertChangesToDBValue(this IEntityCollection entityCollection)
    {
      foreach (var dirtyEntity in entityCollection.DirtyEntities)
        dirtyEntity.RevertChangesToDBValue();
      ResetErrorsAndRemoveNew(entityCollection);
    }

    /// <summary>
    ///   Gets the factory of the entity with the .NET type specified
    /// </summary>
    /// <param name="entitiesToDelete">The entities to delete.</param>
    /// <param name="cascadeDeletes">if set to <c>true</c> [cascade deletes].</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>
    ///   factory to use or null if not found
    /// </returns>
    public static int DeleteEntities(IEnumerable entitiesToDelete, bool cascadeDeletes = false,
      ITransaction transaction = null)
    {
      if (entitiesToDelete is EntityCollectionBase<EntityBase> entityCollectionBase)
        return DeleteEntities(entityCollectionBase, cascadeDeletes, transaction);
      if (transaction == null)
        return entitiesToDelete.Cast<EntityBase>().Count(entity => entity.Delete() || entity.IsNew);
      var numDeleted = 0;
      foreach (var entity in entitiesToDelete.Cast<EntityBase>())
      {
        numDeleted++;
        if (!entity.IsNew)
          transaction.Add(entity);
      }

      return numDeleted;
    }

    /// <summary>
    ///   Deletes the specified data from the DB.
    /// </summary>
    /// <param name="dataToDelete">The data to delete.</param>
    /// <param name="cascade">if set to <c>true</c> [cascadeDeletes].</param>
    /// <returns></returns>
    public static int Delete(object dataToDelete, bool cascade = false)
    {
      return Delete(dataToDelete, cascade, null);
    }

    public static int Delete(object dataToDelete, bool cascade, ITransaction transaction)
    {
      var listItemType = MetaDataHelper.GetListItemType(dataToDelete);
      if (typeof(IEntity).IsAssignableFrom(listItemType))
        return !(dataToDelete is IEnumerable enumerable)
          ? Convert.ToInt32(((IEntity)dataToDelete).Delete())
          : DeleteEntities(enumerable, cascade, transaction);

      return 0;
    }

    /// <summary>
    ///   Saves all dirty objects inside the enumeration passed to the persistent storage.
    /// </summary>
    /// <param name="entitiesToSave">The entities to save.</param>
    /// <param name="cascadeDeletes">if set to <c>true</c> [cascadeDeletes].</param>
    /// <param name="transaction">The transaction.</param>
    /// <returns>
    ///   the amount of persisted entities
    /// </returns>
    public static int SaveEntities(IEnumerable entitiesToSave, bool cascadeDeletes = false,
      ITransaction transaction = null)
    {
      if (entitiesToSave is IEntityView)
        entitiesToSave = ((IEntityView)entitiesToSave).RelatedCollection;
      if (entitiesToSave is IEntityCollection collection)
      {
        var entityCollection = collection;
        var modifyCount = 0;
        if (transaction == null)
        {
          if (!entityCollection.RemovedEntitiesTracker.IsNullOrEmpty())
          {
            modifyCount = DeleteEntities(entityCollection.RemovedEntitiesTracker, cascadeDeletes);
            entityCollection.RemovedEntitiesTracker.Clear();
          }

          return modifyCount + entityCollection.SaveMulti();
        }

        var unitOfWork = new UnitOfWork();
        if (!entityCollection.RemovedEntitiesTracker.IsNullOrEmpty())
          DeleteEntities(entityCollection.RemovedEntitiesTracker, unitOfWork);
        unitOfWork.AddCollectionForSave(entityCollection);
        modifyCount = unitOfWork.Commit(transaction);
        if (entityCollection.RemovedEntitiesTracker != null) entityCollection.RemovedEntitiesTracker.Clear();
        return modifyCount;
      }

      return entitiesToSave.Cast<EntityBase>().Count(entity => entity.IsDirty && entity.Save());
    }

    private static int DeleteEntities(IEntityCollection collectionToDelete, bool cascadeDeletes = false,
      ITransaction transaction = null)
    {
      if (cascadeDeletes)
      {
        if (transaction == null)
          transaction = CreateTransaction(collectionToDelete);
        var unitOfWork = new UnitOfWork();
        DeleteEntities(collectionToDelete, unitOfWork);
        return unitOfWork.Commit(transaction);
      }

      if (transaction == null)
        return collectionToDelete.DeleteMulti();
      transaction.Add(collectionToDelete as ITransactionalElement);
      return collectionToDelete.Count;
    }

    private static void DeleteEntities(IEntityCollection collectionToDelete, UnitOfWork unitOfWork,
      bool cascadeDeletes = true)
    {
      unitOfWork.AddCollectionForDelete(collectionToDelete);
      if (cascadeDeletes)
      {
        MakeDirectDeletesPerformBeforeEntityDeletes(unitOfWork);
        foreach (var entityToDelete in collectionToDelete.OfType<IEntity>())
          MakeCascadeDeletesForAllChildren(unitOfWork, entityToDelete);
      }
    }

    private static ITransaction CreateTransaction(IEntityCollection collectionToDelete)
    {
      var constructed = collectionToDelete.GetType();
      var createTransactionMethod =
        constructed.GetMethod("CreateTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
      if (createTransactionMethod != null)
        return createTransactionMethod.Invoke(collectionToDelete, new object[] { IsolationLevel.ReadCommitted, "UOW" })
          as
          ITransaction;


      return null;
    }

    /// <summary>
    ///   Makes the cascade deletes for all children of the rooty entity.
    ///   Other entities that reference the root entity will remain unless onlyDeleteComponents is false
    /// </summary>
    /// <param name="uow">The uow.</param>
    /// <param name="entity">The root entity.</param>
    /// <param name="deleteDirectly">if set to <c>true</c> [delete directly].</param>
    /// <param name="onlyDeleteComponents">if set to <c>true</c> only delete components i.e children.</param>
    /// <param name="entityTypesToExclude">The entity types to exclude.</param>
    public static void MakeCascadeDeletesForAllChildren(UnitOfWork uow, IEntity entity, bool deleteDirectly = false,
      bool onlyDeleteComponents = true, params string[] entityTypesToExclude)
    {
      // Delete children of Entity
      var allFkEntityFieldCoreObjectList =
        GetAllFkEntityFieldCoreObjectsWhereStartEntityIsPkSide(entity, onlyDeleteComponents);
      foreach (var allFkEntityFieldCoreObjects in allFkEntityFieldCoreObjectList)
        AddDeleteRelatedEntitiesDirectlyCall(uow, entity, allFkEntityFieldCoreObjects);
      if (deleteDirectly)
        ChangeDeleteToDirect(uow, entity);
    }

    private static void ChangeDeleteToDirect(UnitOfWork uow, IEntity entity)
    {
      // Delete Entity directly
      uow.AddDeleteMultiCall(entity.GetEntityFactory().CreateEntityCollection(), CreatePKPredicateExpression(entity));
      // Remove Entity as it now being delete directly
      uow.RemoveFromUoW(entity);
    }

    private static IPredicateExpression CreatePKPredicateExpression(IEntity entity)
    {
      IPredicateExpression predicateExpression = new PredicateExpression();
      foreach (var primaryKeyField in entity.PrimaryKeyFields.OfType<EntityField>())
        predicateExpression.Add(primaryKeyField == primaryKeyField.CurrentValue);
      return predicateExpression;
    }

    /// <summary>
    ///   DeleteMulti() and overloads are not supported for entities which are in a hierarchy of type TargetPerEntity.
    ///   This is by design, as the delete action isn't possible in one go with proper checks due to referential integrity
    ///   issues.
    /// </summary>
    /// <remarks>
    ///   https://www.llblgen.com/documentation/5.0/LLBLGen%20Pro%20RTF/Using%20the%20generated%20code/SelfServicing/gencode_usingcollectionclasses.htm
    ///   http://www.llblgen.com/TinyForum/Messages.aspx?ThreadID=15400
    ///   http://www.llblgen.com/TinyForum/Messages.aspx?ThreadID=4589 DeleteEntityDirectly with a hierarchy of type
    ///   TargetPerEntity isn't supported.
    /// </remarks>
    /// <param name="uow"></param>
    /// <param name="entity"></param>
    /// <param name="allFkEntityFieldCoreObjects"></param>
    private static void AddDeleteRelatedEntitiesDirectlyCall(UnitOfWork uow, IEntity entity,
      IEnumerable<IEntityFieldCore> allFkEntityFieldCoreObjects)
    {
      var relationPredicateBucket = CreateRelationPredicateBucket(allFkEntityFieldCoreObjects, entity.PrimaryKeyFields);
      var relatedType = GetRelatedType(entity, relationPredicateBucket.Item1);
      if (relatedType != null)
      {
        var entityFactoryCore = GetFactoryCore(relatedType);
        var entityCore = entityFactoryCore.Create();
        if (entityCore.LLBLGenProIsInHierarchyOfType != InheritanceHierarchyType.TargetPerEntity)
          uow.AddDeleteMultiCall((IEntityCollection)entityFactoryCore.CreateEntityCollection(),
            relationPredicateBucket.Item2.PredicateExpression);
      }
    }

    private static Type GetRelatedType(object entity, string entityName)
    {
      var type = entity.GetType();
      var relatedType = type.Assembly.GetType(type.Namespace + "." + entityName);
      return relatedType;
    }

    /// <summary>
    ///   Makes the deletes perform first.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public static void MakeDirectDeletesPerformBeforeEntityDeletes(UnitOfWork unitOfWork)
    {
      var unitOfWorkDeletesBlockType = unitOfWork.CommitOrder.Single(bt => bt == UnitOfWorkBlockType.Deletes);
      unitOfWork.CommitOrder.Remove(unitOfWorkDeletesBlockType);
      unitOfWork.CommitOrder.Add(unitOfWorkDeletesBlockType);
    }

    public static int Save(object dataToSave, bool cascadeDeletes = false)
    {
      return Save(dataToSave, (ITransaction)null, cascadeDeletes);
    }

    /// <summary>
    ///   Saves any changes to the specified data to the DB.
    /// </summary>
    /// <param name="dataToSave">The data to save, must be a CommonEntityBase or a list of CommonEntityBase's.</param>
    /// <param name="transaction">The transaction.</param>
    /// <param name="cascadeDeletes">if set to <c>true</c> [cascade deletes].</param>
    /// <returns>
    ///   The number of persisted entities.
    /// </returns>
    public static int Save(object dataToSave, ITransaction transaction, bool cascadeDeletes = false)
    {
      var listItemType = MetaDataHelper.GetListItemType(dataToSave);
      if (typeof(IEntity).IsAssignableFrom(listItemType))
        return !(dataToSave is IEnumerable enumerable)
          ? Convert.ToInt32(((IEntity)dataToSave).Save())
          : SaveEntities(enumerable, cascadeDeletes, transaction);

      return 0;
    }

    public static Type GetDaoBaseImplementation(Assembly assembly)
    {
      return assembly.GetConcretePublicImplementations(typeof(DaoBase)).FirstOrDefault();
      //  return assembly.GetTypes().SingleOrDefault(t => t.Name.Contains("CommonDaoBase") && t.IsClass);
    }

    /// <summary>
    ///   Sets the selfservicing connection string. Note the catalog in the connection string is ignored unless
    ///   sqlServerCatalogNameOverwrites sets it to blank.
    /// </summary>
    /// <remarks>
    ///   https://www.llblgen.com/tinyforum/Messages.aspx?ThreadID=15107 OverrideCatalogs
    ///   http://www.llblgen.com/TinyForum/Messages.aspx?ThreadID=15875
    /// </remarks>
    /// <param name="daoBaseImplementationType">Type of the DAO base implementation.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static void SetSelfservicingConnectionString(Type daoBaseImplementationType, string connectionString)
    {
      if (daoBaseImplementationType != null)
        if (!string.IsNullOrEmpty(connectionString))
          daoBaseImplementationType.StaticMembers().ActualConnectionString = connectionString;
    }

    #endregion Self Servicing

    #region Adapter

    /// <summary>
    ///   Reverts the changes to database value  and removes any new entities.
    /// </summary>
    /// <param name="entityCollection">The entity collection.</param>
    public static void RevertChangesToDBValue(this IEntityCollection2 entityCollection)
    {
      foreach (var dirtyEntity in entityCollection.DirtyEntities)
        dirtyEntity.RevertChangesToDBValue();
      ResetErrorsAndRemoveNew(entityCollection);
    }

    /// <summary>
    ///   Gets a entity field enumeration from entity fields.
    /// </summary>
    /// <param name="entityFields">The entity fields.</param>
    /// <returns>entity field enumeration</returns>
    public static IEnumerable<IEntityField2> AsEnumerable(this IEntityFields2 entityFields)
    {
      return entityFields.Cast<IEntityField2>();
    }

    /// <summary>
    ///   Saves all dirty objects inside the enumeration passed to the persistent storage.
    /// </summary>
    /// <param name="entitiesToSave">The entities to save.</param>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="cascade">if set to <c>true</c> [cascadeDeletes].</param>
    /// <returns>
    ///   the amount of persisted entities
    /// </returns>
    public static int SaveEntities(IEnumerable entitiesToSave, IDataAccessAdapter dataAccessAdapter,
      bool cascade = false)
    {
      if (entitiesToSave is IEntityView2)
        entitiesToSave = ((IEntityView2)entitiesToSave).RelatedCollection;
      if (entitiesToSave is IEntityCollection2 collectionToSave)
      {
        var entityCollection = collectionToSave;
        var modifyCount = 0;
        if (entityCollection.RemovedEntitiesTracker != null)
        {
          foreach (IEntityCore entity in entityCollection.RemovedEntitiesTracker)
            if (!entity.MarkedForDeletion)
              entityCollection.RemovedEntitiesTracker.Remove(entity);
          modifyCount = DeleteEntities(entityCollection.RemovedEntitiesTracker, dataAccessAdapter, cascade);
          entityCollection.RemovedEntitiesTracker.Clear();
        }

        return modifyCount + dataAccessAdapter.SaveEntityCollection(collectionToSave, false, true);
      }

      return SaveEntities(entitiesToSave.Cast<IEntity2>(), dataAccessAdapter, cascade);
    }

    /// <summary>
    ///   Saves all dirty objects inside the enumeration passed to the persistent storage.
    /// </summary>
    /// <param name="entitiesToSave">The entities to save.</param>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="cascade">if set to <c>true</c>Deletes cascade non-recursively to children of the selected entity.</param>
    /// <returns>
    ///   the amount of persisted entities
    /// </returns>
    public static int SaveEntities(IEnumerable<IEntity2> entitiesToSave, IDataAccessAdapter dataAccessAdapter,
      bool cascade = false)
    {
      return entitiesToSave.Count(entity => entity.IsDirty && dataAccessAdapter.SaveEntity(entity));
    }

    /// <summary>
    ///   Saves any changes to the specified data to the DB.
    /// </summary>
    /// <param name="dataToSave">The data to save, must be a CommonEntityBase or a list of CommonEntityBase's.</param>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="cascadeDeletes">if set to <c>true</c> [cascade deletes].</param>
    /// <returns>
    ///   The number of persisted entities.
    /// </returns>
    public static int Save(object dataToSave, IDataAccessAdapter dataAccessAdapter, bool cascadeDeletes = false)
    {
      var listItemType = MetaDataHelper.GetListItemType(dataToSave);
      if (typeof(IEntity2).IsAssignableFrom(listItemType))
        return !(dataToSave is IEnumerable enumerable)
          ? Convert.ToInt32(dataAccessAdapter.SaveEntity((IEntity2)dataToSave))
          : SaveEntities(enumerable, dataAccessAdapter, cascadeDeletes);

      return 0;
    }

    /// <summary>
    ///   Deletes all entities inside the enumeration passed from the DB.
    /// </summary>
    /// <param name="entitiesToDelete">The entities to delete.</param>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="cascade">if set to <c>true</c>Deletes cascade non-recursively to children of the selected entity.</param>
    /// <returns>
    ///   the amount of persisted entities
    /// </returns>
    private static int DeleteEntities(IEnumerable<IEntity2> entitiesToDelete, IDataAccessAdapter dataAccessAdapter,
      bool cascade = false)
    {
      if (cascade)
      {
        var unitOfWork2 = new UnitOfWork2();
        MakeDirectDeletesPerformBeforeEntityDeletes(unitOfWork2);
        foreach (var entityToDelete in entitiesToDelete)
        {
          unitOfWork2.AddForDelete(entityToDelete);
          MakeCascadeDeletesForAllChildren(unitOfWork2, entityToDelete);
        }

        return unitOfWork2.Commit(dataAccessAdapter);
      }

      return entitiesToDelete.Count(dataAccessAdapter.DeleteEntity);
    }

    /// <summary>
    ///   Makes the deletes perform first.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    public static void MakeDirectDeletesPerformBeforeEntityDeletes(UnitOfWork2 unitOfWork)
    {
      var unitOfWorkDeletesBlockType = unitOfWork.CommitOrder.Single(bt => bt == UnitOfWorkBlockType.Deletes);
      unitOfWork.CommitOrder.Remove(unitOfWorkDeletesBlockType);
      unitOfWork.CommitOrder.Add(unitOfWorkDeletesBlockType);
    }

    /// <summary>
    ///   Deletes the entities.
    /// </summary>
    /// <param name="entitiesToDelete">The entities to delete.</param>
    /// <param name="dataAccessAdapter">The data access adapter.</param>
    /// <param name="cascade">if set to <c>true</c> [cascadeDeletes].</param>
    /// <returns></returns>
    public static int DeleteEntities(IEnumerable entitiesToDelete, IDataAccessAdapter dataAccessAdapter,
      bool cascade = false)
    {
      if (entitiesToDelete is IEntityCollection2 collectionToDelete)
      {
        if (cascade)
        {
          var unitOfWork2 = new UnitOfWork2();
          MakeDirectDeletesPerformBeforeEntityDeletes(unitOfWork2);
          unitOfWork2.AddCollectionForDelete(collectionToDelete);
          foreach (var entityToDelete in collectionToDelete.OfType<IEntity2>())
            MakeCascadeDeletesForAllChildren(unitOfWork2, entityToDelete);
          return unitOfWork2.Commit(dataAccessAdapter);
        }

        return dataAccessAdapter.DeleteEntityCollection(collectionToDelete);
      }

      return DeleteEntities(entitiesToDelete.Cast<IEntity2>(), dataAccessAdapter, cascade);
    }

    public static int Delete(object dataToDelete, IDataAccessAdapter dataAccessAdapter, bool cascade = false)
    {
      var listItemType = MetaDataHelper.GetListItemType(dataToDelete);
      if (typeof(IEntity2).IsAssignableFrom(listItemType))
        return !(dataToDelete is IEnumerable enumerable)
          ? Convert.ToInt32(DeleteEntity((IEntity2)dataToDelete, dataAccessAdapter, cascade))
          : DeleteEntities(enumerable, dataAccessAdapter, cascade);

      return 0;
    }

    private static bool DeleteEntity(IEntity2 entityToDelete, IDataAccessAdapter dataAccessAdapter,
      bool cascade = false)
    {
      if (cascade)
      {
        var unitOfWork2 = new UnitOfWork2();
        unitOfWork2.AddForDelete(entityToDelete);
        MakeCascadeDeletesForAllChildren(unitOfWork2, entityToDelete);
        return Convert.ToBoolean(unitOfWork2.Commit(dataAccessAdapter));
      }

      return dataAccessAdapter.DeleteEntity(entityToDelete);
    }

    /// <summary>
    ///   Makes the cascade deletes for all children of the rooty entity.
    ///   Other entities that reference the root entity will remain unless onlyDeleteComponents is false
    /// </summary>
    /// <param name="uow">The uow.</param>
    /// <param name="entity">The root entity.</param>
    /// <param name="deleteDirectly">if set to <c>true</c> [delete directly].</param>
    /// <param name="onlyDeleteComponents">if set to <c>true</c> only delete components i.e children.</param>
    /// <param name="entityTypesToExclude">The entity types to exclude.</param>
    public static void MakeCascadeDeletesForAllChildren(UnitOfWork2 uow, IEntity2 entity, bool deleteDirectly = false,
      bool onlyDeleteComponents = true, params string[] entityTypesToExclude)
    {
      // Delete children of Entity
      var allFkEntityFieldCoreObjectList =
        GetAllFkEntityFieldCoreObjectsWhereStartEntityIsPkSide(entity, onlyDeleteComponents);
      foreach (var allFkEntityFieldCoreObjects in allFkEntityFieldCoreObjectList)
        AddDeleteRelatedEntitiesDirectlyCall(uow, entity, allFkEntityFieldCoreObjects);
      if (deleteDirectly)
        ChangeDeleteToDirect(uow, entity);
    }

    private static void AddDeleteRelatedEntitiesDirectlyCall(UnitOfWork2 uow, IEntity2 entity,
      IEnumerable<IEntityFieldCore> allFkEntityFieldCoreObjects)
    {
      var relationPredicateBucket = CreateRelationPredicateBucket(allFkEntityFieldCoreObjects, entity.PrimaryKeyFields);
      var relatedType = GetRelatedType(entity, relationPredicateBucket.Item1);
      if (relatedType == null)
      {
        uow.AddDeleteEntitiesDirectlyCall(relationPredicateBucket.Item1, relationPredicateBucket.Item2);
      }
      else
      {
        var entityFactoryCore = GetFactoryCore(relatedType);
        var entityCore = entityFactoryCore.Create();
        if (entityCore.LLBLGenProIsInHierarchyOfType != InheritanceHierarchyType.TargetPerEntity)
          uow.AddDeleteEntitiesDirectlyCall(relationPredicateBucket.Item1, relationPredicateBucket.Item2);
      }
    }

    private static void MakeCascadeDeletesForSpecifiedChildren(UnitOfWork2 uow, IEntity2 entity,
      IEnumerable<string> entityTypesToDelete, bool deleteDirectly = true)
    {
      // Delete children of Entity

      foreach (var relationPredicateBucket in GetAllRelationsWhereStartEntityIsPkSide(entity)
                 .Select(entityRelation => CreateRelationPredicateBucket(entityRelation, entity.PrimaryKeyFields))
                 .Where(relationPredicateBucket => entityTypesToDelete.Contains(relationPredicateBucket.Item1)))
        uow.AddDeleteEntitiesDirectlyCall(relationPredicateBucket.Item1, relationPredicateBucket.Item2);

      if (deleteDirectly)
        ChangeDeleteToDirect(uow, entity);
    }

    private static void ChangeDeleteToDirect(UnitOfWork2 uow, IEntity2 entity)
    {
      // Delete Entity directly
      uow.AddDeleteEntitiesDirectlyCall(entity.LLBLGenProEntityName, CreatePKRelationPredicateBucket(entity));

      // Remove Entity as it now being delete directly
      uow.RemoveFromUoW(entity);
    }

    private static IRelationPredicateBucket CreatePKRelationPredicateBucket(IEntity2 entity)
    {
      return new RelationPredicateBucket(CreatePKPredicateExpression(entity));
    }

    private static IPredicateExpression CreatePKPredicateExpression(IEntity2 entity)
    {
      IPredicateExpression predicateExpression = new PredicateExpression();
      foreach (var primaryKeyField in entity.PrimaryKeyFields.OfType<EntityField2>())
        predicateExpression.Add(primaryKeyField == primaryKeyField.CurrentValue);
      return predicateExpression;
    }

    public static Dictionary<string, int> GetExistingChildCounts(IDataAccessAdapter dataAccessAdapter,
      EntityBase2 entity, params string[] entityTypesToExclude)
    {
      return GetExistingChildCounts(dataAccessAdapter, entity, (IEnumerable<string>)entityTypesToExclude);
    }

    public static Dictionary<string, int> GetExistingChildCounts(IDao dao, EntityBase entity,
      params string[] entityTypesToExclude)
    {
      return GetExistingChildCounts(dao, entity, (IEnumerable<string>)entityTypesToExclude);
    }

    public static Dictionary<string, int> GetExistingChildCounts(IDataAccessAdapter dataAccessAdapter,
      EntityBase2 entity, IEnumerable<string> entityTypesToExclude)
    {
      return GetChildCounts(dataAccessAdapter, entity, entityTypesToExclude).Where(childCount => childCount.Item2 > 0)
        .GroupBy(kvp => kvp.Item1)
        .ToDictionary(grp => grp.Key, grp => grp.Sum(kvp => kvp.Item2));
    }

    public static Dictionary<string, int> GetExistingChildCounts(IDao dao, EntityBase entity,
      IEnumerable<string> entityTypesToExclude)
    {
      return GetChildCounts(dao, entity, entityTypesToExclude).Where(childCount => childCount.Item2 > 0)
        .GroupBy(kvp => kvp.Item1)
        .ToDictionary(grp => grp.Key, grp => grp.Sum(kvp => kvp.Item2));
    }

    public static IEnumerable<Tuple<string, int>> GetChildCounts(IDataAccessAdapter dataAccessAdapter,
      EntityBase2 entity, params string[] entityTypesToExclude)
    {
      return GetChildCounts(dataAccessAdapter, entity, (IEnumerable<string>)entityTypesToExclude);
    }

    public static IEnumerable<Tuple<string, int>> GetChildCounts(IDataAccessAdapter dataAccessAdapter,
      EntityBase2 entity, IEnumerable<string> entityTypesToExclude)
    {
      var allRelationsWhereStartEntityIsPkSide = GetAllRelationsWhereStartEntityIsPkSide(entity);
      return allRelationsWhereStartEntityIsPkSide
        .Select(entityRelation => GetChildCount(dataAccessAdapter, entity, entityRelation, entityTypesToExclude))
        .Where(childCount => childCount != null);
    }

    public static IEnumerable<Tuple<string, int>> GetChildCounts(IDao dao, EntityBase entity,
      IEnumerable<string> entityTypesToExclude)
    {
      var allRelationsWhereStartEntityIsPkSide = GetAllRelationsWhereStartEntityIsPkSide(entity);
      return allRelationsWhereStartEntityIsPkSide
        .Select(entityRelation => GetChildCount(dao, entity, entityRelation, entityTypesToExclude))
        .Where(childCount => childCount != null);
    }

    /// <summary>
    ///   Gets all foreign key entity field core objects where start entity is the primary key side.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="onlyComponentsRelationShips">
    ///   if set to <c>true</c> Only include components relation ships i.e. where the
    ///   referencing entity can't exist without the start Entity.
    /// </param>
    /// <returns></returns>
    private static IEnumerable<List<IEntityFieldCore>> GetAllFkEntityFieldCoreObjectsWhereStartEntityIsPkSide(
      IEntityCore entity, bool onlyComponentsRelationShips = false)
    {
      var enumerable = from entityRelation in GetAllRelationsWhereStartEntityIsPkSide(entity)
        select entityRelation.GetAllFKEntityFieldCoreObjects();
      return from allFkEntityFieldCoreObjects in enumerable
        let allRequired = allFkEntityFieldCoreObjects.All(f => !f.IsNullable)
        where !onlyComponentsRelationShips || allRequired
        select allFkEntityFieldCoreObjects;
    }

    public static IEnumerable<IEntityRelation> GetAllRelationsWhereStartEntityIsPkSide(IEntityCore entity)
    {
      return entity.GetAllRelations().Where(entityRelation => entityRelation.StartEntityIsPkSide);
    }

    private static Tuple<string, int> GetChildCount(IDataAccessAdapter dataAccessAdapter, IEntityCore entity,
      IEntityRelation entityRelation, IEnumerable<string> entityTypesToExclude)
    {
      var allFkEntityFieldCoreObjects = entityRelation.GetAllFKEntityFieldCoreObjects();
      return GetChildCount(dataAccessAdapter, entity, allFkEntityFieldCoreObjects, entityTypesToExclude);
    }

    private static Tuple<string, int> GetChildCount(IDataAccessAdapter dataAccessAdapter, IEntityCore entity,
      List<IEntityFieldCore> allFkEntityFieldCoreObjects,
      IEnumerable<string> entityTypesToExclude)
    {
      var entityTypeRelationPredicateBucketTuple =
        CreateRelationPredicateBucket(allFkEntityFieldCoreObjects, entity.PrimaryKeyFields);
      if (entityTypeRelationPredicateBucketTuple != null &&
          !entityTypesToExclude.Contains(entityTypeRelationPredicateBucketTuple.Item1))
      {
        var entityFields2 = new EntityFields2(allFkEntityFieldCoreObjects.ToArray(), null, null);
        var count = dataAccessAdapter.GetDbCount(entityFields2, entityTypeRelationPredicateBucketTuple.Item2);
        return new Tuple<string, int>(entityTypeRelationPredicateBucketTuple.Item1, count);
      }

      return null;
    }

    public static Tuple<string, int> GetChildCount(IDao dao, EntityBase entity, IEntityRelation entityRelation,
      IEnumerable<string> entityTypesToExclude)
    {
      var entityTypeRelationPredicateBucketTuple =
        CreateRelationPredicateBucket(entityRelation, ((IEntity)entity).PrimaryKeyFields);
      if (entityTypeRelationPredicateBucketTuple != null &&
          !entityTypesToExclude.Contains(entityTypeRelationPredicateBucketTuple.Item1))
      {
        var entityFields2 = new EntityFields(entityRelation.GetAllFKEntityFieldCoreObjects().ToArray(), null, null);
        if (dao == null)
        {
          //dao = entity.CreateDAOInstance();
          var createDaoInstanceMethod =
            typeof(EntityBase).GetMethod("CreateDAOInstance", BindingFlags.NonPublic | BindingFlags.Instance);
          if (createDaoInstanceMethod != null)
            dao = createDaoInstanceMethod.Invoke(entity, null) as IDao;
        }

        if (dao != null)
        {
          var count = dao.GetDbCount(entityFields2, null,
            entityTypeRelationPredicateBucketTuple.Item2.PredicateExpression,
            entityTypeRelationPredicateBucketTuple.Item2.Relations, null);
          return new Tuple<string, int>(entityTypeRelationPredicateBucketTuple.Item1, count);
        }
      }

      return null;
    }

    public static Tuple<string, IRelationPredicateBucket> CreateRelationPredicateBucket(IEntityRelation entityRelation,
      IList foreignKeyFieldValues, bool useActualName = true)
    {
      var allFkEntityFieldCoreObjects = entityRelation.GetAllFKEntityFieldCoreObjects();
      return CreateRelationPredicateBucket(allFkEntityFieldCoreObjects, foreignKeyFieldValues, useActualName);
    }

    private static Tuple<string, IRelationPredicateBucket> CreateRelationPredicateBucket(
      IEnumerable<IEntityFieldCore> allFkEntityFieldCoreObjects, IList foreignKeyFieldValues,
      bool useActualName = true)
    {
      string typeOfEntity = null;
      var bucket = new RelationPredicateBucket();
      var index = 0;

      foreach (var foreignKeyField in allFkEntityFieldCoreObjects)
      {
        if (string.IsNullOrEmpty(typeOfEntity))
          typeOfEntity = useActualName
            ? foreignKeyField.ActualContainingObjectName
            : foreignKeyField.ContainingObjectName;
        var fkValue = foreignKeyFieldValues[index];
        index++;
        if (fkValue == null) return null;
        if (fkValue is IEntityField2)
          fkValue = ((IEntityField2)fkValue).CurrentValue;
        else if (fkValue is IEntityField)
          fkValue = ((IEntityField)fkValue).CurrentValue;
        var fieldCompareValuePredicate = foreignKeyField.Equal(fkValue);
        bucket.PredicateExpression.Add(fieldCompareValuePredicate);
      }

      return new Tuple<string, IRelationPredicateBucket>(typeOfEntity, bucket);
    }

    public static IEnumerable<Tuple<string, int>> GetRelatedCounts(IDataAccessAdapter dataAccessAdapter,
      EntityBase2 entity, params string[] entityTypesToExclude)
    {
      return GetRelatedCounts(dataAccessAdapter, entity, (IEnumerable<string>)entityTypesToExclude);
    }

    public static IEnumerable<Tuple<string, int>> GetRelatedCounts(IDataAccessAdapter dataAccessAdapter,
      IEntityCore entity, IEnumerable<string> entityTypesToExclude)
    {
      var allRelationsWhereStartEntityIsPkSide = entity.GetAllRelations();
      return allRelationsWhereStartEntityIsPkSide
        .Select(entityRelation => GetChildCount(dataAccessAdapter, entity, entityRelation, entityTypesToExclude))
        .Where(childCount => childCount != null);
    }

    #region GetDataAccessAdapter

    /// <summary>
    ///   Gets the data access adapter from a DataSource2.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public static IDataAccessAdapter GetDataAccessAdapter<T>(DataSource2<T> query) where T : EntityBase2
    {
      return query == null ? null : GetDataAccessAdapter(query.Provider);
    }

    /// <summary>
    ///   Gets the data access adapter from a query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public static IDataAccessAdapter GetDataAccessAdapter(IQueryable query)
    {
      return GetDataAccessAdapter(query.Provider);
    }

    public static IDataAccessAdapter GetDataAccessAdapter(IEnumerable enumerable)
    {
      return !(enumerable is IQueryable queryable) ? null : GetDataAccessAdapter(queryable);
    }

    /// <summary>
    ///   Gets the data access adapter from a LLBLGenProProvider2.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public static IDataAccessAdapter GetDataAccessAdapter(IQueryProvider provider)
    {
      return !(provider is LLBLGenProProvider2 llblGenProProvider2) ? null : llblGenProProvider2.AdapterToUse;
    }

    /// <summary>
    ///   Gets the IDataAccessAdapter from a ILinqMetaData Via the provider.
    /// </summary>
    /// <param name="linqMetaData">The ILinqMetaData.</param>
    /// <returns></returns>
    public static IDataAccessAdapter GetDataAccessAdapter(ILinqMetaData linqMetaData)
    {
      return GetDataAccessAdapter(GetProvider(linqMetaData));
    }

    /// <summary>
    ///   Gets the adapter from the ILinqMetaData.
    /// </summary>
    /// <param name="linqMetaData">The linq meta data.</param>
    /// <returns></returns>
    public static DataAccessAdapterBase GetDataAccessAdapterViaReflection(ILinqMetaData linqMetaData)
    {
      var adapterToUseProperty = linqMetaData.GetType().GetProperty("AdapterToUse");
      if (adapterToUseProperty != null)
        return adapterToUseProperty.GetValue(linqMetaData, null) as DataAccessAdapterBase;
      return null;
    }

    public static IDataAccessAdapter GetDataAccessAdapter(ILinqMetaData linqMetaData, bool isAdapter)
    {
      IDataAccessAdapter adapter = null;
      if (isAdapter)
      {
        dynamic dlinqMetaData = linqMetaData;
        if (dlinqMetaData != null) adapter = dlinqMetaData.AdapterToUse;
      }

      return adapter;
    }

    public static DataAccessAdapterBase CreateDataAccessAdapterInstance(Type dataAccessAdapterType,
      string connectionString
      , Func<bool> isSqlServer = null, Func<bool> isOracle = null)
    {
      object adapter;
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        adapter = Activator.CreateInstance(dataAccessAdapterType);
      }
      else
      {
        if (isSqlServer != null && isSqlServer())
        {
          var newCatalog = DataHelper.GetSqlDatabaseName(connectionString);
          adapter = string.IsNullOrWhiteSpace(newCatalog)
            ? Activator.CreateInstance(dataAccessAdapterType, connectionString)
            : Activator.CreateInstance(dataAccessAdapterType, connectionString, true, CatalogNameUsage.Clear,
              newCatalog);
          // <param name="connectionString">The connection string to use when connecting to the database.</param>
          // <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
          // <param name="catalogNameUsageSetting"> Configures this data access adapter object how to threat catalog names in persistence information.</param>
          // <param name="catalogNameToUse"> The name to use if catalogNameUsageSetting is set to ForceName. Ignored otherwise.</param>
        }
        else
        {
          if (isOracle != null && isOracle())
            try
            {
              adapter = Activator.CreateInstance(dataAccessAdapterType, connectionString, true, SchemaNameUsage.Default,
                null);
            }
            //catch (TypeInitializationException e)
            //{ return null; }
            //catch (TargetInvocationException e)
            //{ return null; }
            catch (Exception e)
            {
              var baseException = e.GetBaseException();
              if (baseException is ORMGeneralOperationException)
              {
                var oracleManagedDataAccessFactory = DataHelper.GetOracleManagedDataAccessFactoryRegisterIfNotAlready(
                  Path.GetDirectoryName(dataAccessAdapterType.Assembly.Location));

                if (oracleManagedDataAccessFactory == null)
                  return null;
                adapter = Activator.CreateInstance(dataAccessAdapterType, connectionString, true,
                  SchemaNameUsage.Default, null);
              }
              else
              {
                throw;
              }
            }

          // <param name="connectionString">The connection string to use when connecting to the database.</param>
          // <param name="keepConnectionOpen">when true, the DataAccessAdapter will not close an opened connection. Use this for multi action usage.</param>
          // <param name="schemaNameUsageSetting">Configures this data access adapter object how to threat schema names in persistence information.</param>
          // <param name="schemaNameToUse">Oracle specific. The name to use if schemaNameUsageSetting is set to ForceName. Ignored otherwise.</param>
          else
            adapter = Activator.CreateInstance(dataAccessAdapterType, connectionString);
        }
      }

      return adapter as DataAccessAdapterBase;
    }

    #endregion GetDataAccessAdapter

    #region ToEntityCollection2

    /// <summary>
    ///   Converts an entity enumeration to an entity collection. If the enumeration is a ILLBLGenProQuery then executes
    ///   the query this object represents and returns its results in its native container - an entity collection.
    /// </summary>
    /// <typeparam name="T">EntityBase2</typeparam>
    /// <param name="enumerable">The enumerable.</param>
    /// <returns>
    ///   Results of the query in an entity collection.
    /// </returns>
    public static EntityCollectionBase2<T> ToEntityCollection2<T>(this IEnumerable<T> enumerable) where T : EntityBase2
    {
      if (enumerable is EntityCollectionBase2<T> entityCollection)
        return entityCollection;
      return !(enumerable is ILLBLGenProQuery llblQuery)
        ? ToEntityCollection(enumerable, (IEntityFactory2)GetFactoryCore(enumerable))
        : llblQuery.Execute<EntityCollectionBase2<T>>();
    }

    private static EntityCollectionBase2<T> ToEntityCollection<T>(IEnumerable<T> enumerable,
      IEntityFactory2 entityFactory) where T : EntityBase2
    {
      if (entityFactory.CreateEntityCollection() is EntityCollectionBase2<T> entityCollection)
      {
        entityCollection.AddRange(enumerable);
        return entityCollection;
      }

      return null;
    }

    public static async Task<EntityCollectionBase2<T>> ToEntityCollection2Async<T>(this IEnumerable<T> enumerable,
      bool checkAdapterFirst = false) where T : EntityBase2
    {
      if (enumerable is EntityCollectionBase2<T> entityCollection)
        return entityCollection;
      if ((IEntityFactory2)GetFactoryCore(enumerable).CreateEntityCollection() is EntityCollectionBase2<T>
          entityCollection2)
      {
        if (enumerable is ILLBLGenProQuery llblQuery)
        {
          if (checkAdapterFirst && GetDataAccessAdapter(llblQuery) == null)
            return entityCollection2;
          return await llblQuery.ExecuteAsync<EntityCollectionBase2<T>>()
            .ConfigureAwait(GeneralHelper.ContinueOnCapturedContext);
        }


        entityCollection2.AddRange(enumerable);
        return entityCollection2;
      }

      return null;
    }

    #endregion ToEntityCollection2

    public static IRelationPredicateBucket GetRelationInfo(IEntity2 entity, string fieldName,
      IEnumerable<string> primaryKeyColumnNames)
    {
      var bucket = new RelationPredicateBucket();
      bucket.Relations.AddRange(entity.GetRelationsForFieldOfType(fieldName));
      foreach (var entityField2 in primaryKeyColumnNames.Select(primaryKeyColumn => entity.Fields[primaryKeyColumn]))
        bucket.PredicateExpression.Add(new FieldCompareValuePredicate(entityField2, null, ComparisonOperator.Equal,
          entityField2.CurrentValue, entity.LLBLGenProEntityName + "__"));
      return bucket;
    }

    public static IRelationPredicateBucket GetRelationInfo(IEntity2 entity, string fieldName)
    {
      var bucket = new RelationPredicateBucket();
      bucket.Relations.AddRange(entity.GetRelationsForFieldOfType(fieldName));
      foreach (var entityField2 in entity.PrimaryKeyFields)
        bucket.PredicateExpression.Add(new FieldCompareValuePredicate(entityField2, null, ComparisonOperator.Equal,
          entityField2.CurrentValue, entity.LLBLGenProEntityName + "__"));
      return bucket;
    }

    #endregion Adapter

    #region GetTransactionController

    /// <summary>
    ///   Gets the transaction controller from a query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public static ITransactionController GetTransactionController(IQueryable query)
    {
      return GetDataAccessAdapter(query.Provider);
    }

    public static ITransactionController GetTransactionController(IEnumerable enumerable)
    {
      return !(enumerable is IQueryable queryable) ? null : GetDataAccessAdapter(queryable);
    }

    /// <summary>
    ///   Gets the transaction controller from a Query Provider.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public static ITransactionController GetTransactionController(IQueryProvider provider)
    {
      if (!(provider is LLBLGenProProvider2 llblGenProProvider2))
      {
        if (!(provider is LLBLGenProProvider llblGenProProvider))
          return null;
        return llblGenProProvider.TransactionToUse; //Usually null
      }

      return llblGenProProvider2.AdapterToUse;
    }

    /// <summary>
    ///   Gets the ITransactionController (IDataAccessAdapter or ITransaction) from a ILinqMetaData Via the provider.
    /// </summary>
    /// <param name="linqMetaData">The ILinqMetaData.</param>
    /// <returns></returns>
    public static ITransactionController GetTransactionController(ILinqMetaData linqMetaData)
    {
      return GetTransactionController(GetProvider(linqMetaData));
    }

    #endregion GetTransactionController

    /// <summary>
    ///   Gets the entity fields errors.
    /// </summary>
    /// <returns>
    ///   A separator-by-semicolon errors in string representation of the
    ///   error (if exist).
    ///   This could be useful if you want to obtain the errors list at some GUI.
    /// </returns>
    public static string GetEntityFieldsErrors(IEntityCore entity)
    {
      // variables to construct the message
      var sbErrors = new StringBuilder();
      var toReturn = string.Empty;

      // iterate over fields and get their errorInfo
      foreach (var field in entity.Fields.AsEnumerable())
        // IEntity implements IDataErrorInfo, and it contains a collections of field errors already set.
        // For more info read the docs (LLBLGen Pro Help -> Using generated code -> Validation per field or per entity -> IDataErrorInfo implementation).
        if (!string.IsNullOrEmpty(((IDataErrorInfo)entity)[field.Name]))
          sbErrors.Append(((IDataErrorInfo)entity)[field.Name] + ";");

      // determine if there was errors and cut off the extra ';'
      if (sbErrors.ToString() != string.Empty)
      {
        toReturn = sbErrors.ToString();
        toReturn = toReturn.Substring(0, toReturn.Length - 2);
      }

      return toReturn;
    }

    public static void ResetErrors(IEntityCore entity)
    {
      // reset the field errors
      foreach (var field in entity.Fields.AsEnumerable())
        entity.SetEntityFieldError(field.Name, string.Empty, false);

      // reset entity error
      entity.SetEntityError(string.Empty);
    }

    /// <summary>
    ///   Called right at the beginning of SetValue(), which is called from an entity field property setter
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="fieldIndex">Index of the field to set.</param>
    /// <param name="valueToSet">The value to set.</param>
    /// <remarks>
    ///   This code fixes the flaw of the IDataErrorInfo + Refresh field value in controls.
    ///   For more explanation on this issue, please visit this forum's post:
    ///   http://www.llblgen.com/TinyForum/Messages.aspx?ThreadID=12166
    /// </remarks>
    public static void SetEntityFieldErrorIfNeeded(IEntityCore entity, int fieldIndex, object valueToSet)
    {
      var entityField = entity.Fields[fieldIndex];
      if (entityField.CurrentValue != null)
        if (entityField.CurrentValue.Equals(valueToSet)
            && !string.IsNullOrEmpty(((IDataErrorInfo)entity)[entityField.Name]))
          entity.SetEntityFieldError(entityField.Name, string.Empty, false);
    }

    /// <summary>
    ///   Gets the properties of type IEntityCore since sometimes these properties are not browseable so they need to be
    ///   handled as
    ///   a special case.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="includeGenericParameters">if set to <c>true</c> [include generic].</param>
    /// <returns></returns>
    public static IEnumerable<PropertyDescriptor> GetPropertiesOfTypeEntity(Type type,
      bool includeGenericParameters = false)
    {
      return MetaDataHelper.GetPropertyDescriptors(type).FilterByIsEntityCore(true, includeGenericParameters);
    }

    /// <summary>
    ///   Filters the property descriptor to IEntityCore.
    /// </summary>
    /// <param name="propertyDescriptors">The property descriptors.</param>
    /// <param name="isEntityCore">The is entity core.</param>
    /// <param name="includeGenericParameters">if set to <c>true</c> [include generic parameters].</param>
    /// <returns></returns>
    public static IEnumerable<PropertyDescriptor> FilterByIsEntityCore(
      this IEnumerable<PropertyDescriptor> propertyDescriptors, bool? isEntityCore = true,
      bool includeGenericParameters = false)
    {
      return isEntityCore.HasValue
        ? propertyDescriptors.Where(propertyDescriptor =>
          IsEntityCore(propertyDescriptor, includeGenericParameters) == isEntityCore.Value)
        : propertyDescriptors;
    }

    /// <summary>
    ///   Determines whether the specified property descriptor is IEntityCore.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="includeGenericParameters">if set to <c>true</c> [include generic parameters].</param>
    /// <returns></returns>
    public static bool IsEntityCore(PropertyDescriptor propertyDescriptor, bool includeGenericParameters = false)
    {
      return IsEntityCore(propertyDescriptor.PropertyType, includeGenericParameters);
    }

    public static bool IsMemberOfEntityCore(PropertyDescriptor propertyDescriptor)
    {
      return IsEntityCore(propertyDescriptor.ComponentType);
    }

    /// <summary>
    ///   Determines whether the specified type is IEntityCore.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="includeGenericParameters">if set to <c>true</c> [include generic parameters].</param>
    /// <returns></returns>
    public static bool IsEntityCore(Type type, bool includeGenericParameters = false)
    {
      var isEntityCore = typeof(IEntityCore).IsAssignableFrom(type);
      if (isEntityCore || !includeGenericParameters) return isEntityCore;
      var typeParametersOfGenericType = MetaDataHelper.GetTypeParametersOfGenericType(type);
      return typeParametersOfGenericType != null && typeParametersOfGenericType.Any(t => IsEntityCore(t));
    }

    public static IEntityFields GetFieldsFromType(Type type)
    {
      //			return ef.Create().Fields;
      return !(CreateEntity(type) is IEntity ef) ? null : ef.Fields;
    }

    public static IEntityFields2 GetFieldsFromType2(Type type)
    {
      //			return ef.Create().Fields;
      return !(CreateEntity(type) is IEntity2 ef) ? null : ef.Fields;
    }

    public static IEnumerable<PropertyDescriptor> GetNavigatorProperties(IEntityCore entityCore)
    {
      return MetaDataHelper.GetPropertyDescriptors(entityCore.GetType()).FilterByIsNavigator(entityCore);
    }

    public static IEnumerable<PropertyDescriptor> FilterByIsNavigator(
      this IEnumerable<PropertyDescriptor> propertyDescriptors, IEntityCore entityCore)
    {
      return propertyDescriptors.Where(propertyDescriptor =>
        FieldIsNavigator(entityCore, propertyDescriptor.Name) && IsMemberOfEntityCore(propertyDescriptor));
    }

    public static IEnumerable<PropertyDescriptor> FilterByIsField(
      this IEnumerable<PropertyDescriptor> propertyDescriptors, IEntityCore entityCore)
    {
      var propertyNames = entityCore.Fields.Select(ef => ef.Name);
      return propertyDescriptors.Where(propertyDescriptor => propertyNames.Contains(propertyDescriptor.Name));
    }

    public static bool FieldIsNavigator(IEntityCore entityCore, string fieldName)
    {
      var relations = entityCore.GetRelationsForFieldOfType(fieldName);
      return relations.Count > 0;
    }

    public static string GetNameFromEntity(IEntityCore entity)
    {
      return GetNameFromEntityName(entity.LLBLGenProEntityName);
    }

    public static string GetNameFromEntityEnum(Enum entity)
    {
      return GetNameFromEntityName(entity.ToString());
    }

    public static string GetNameFromEntityName(string llblGenProEntityName)
    {
      return llblGenProEntityName.Replace("Entity", "");
    }

    #region GetFieldPersistenceInfo

    public static IFieldPersistenceInfo GetFieldPersistenceInfo(IDataAccessAdapter dataAccessAdapter,
      IEntityField2 field)
    {
      var type = dataAccessAdapter.GetType();
      var fullListQueryMethod = type.GetMethod("GetFieldPersistenceInfo",
        BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(IEntityField2) }, null);
      if (fullListQueryMethod is null)
      {
        var fieldValue = dataAccessAdapter.GetFieldValue("_queryCreationManager");
        if (fieldValue?.GetFieldValue("_persistenceInfoProvider") is PersistenceInfoProviderBase
            persistenceInfoProvider)
          return persistenceInfoProvider.GetFieldPersistenceInfo(field.ContainingObjectName, field.Name);

        return null;
      }

      return fullListQueryMethod.Invoke(dataAccessAdapter, new[] { field }) as IFieldPersistenceInfo;
    }

    public static IFieldPersistenceInfo GetFieldPersistenceInfoSafely(IDataAccessAdapter dataAccessAdapter,
      IEntityField2 field)
    {
      IFieldPersistenceInfo fieldPersistenceInfo = null;
      try
      {
        fieldPersistenceInfo = GetFieldPersistenceInfo(dataAccessAdapter, field);
      }
      catch (Exception e)
      {
        e.TraceOut();
      }

      return fieldPersistenceInfo;
    }

    public static IFieldPersistenceInfo GetFieldPersistenceInfo(IEntityFieldCore field,
      IDataAccessAdapter adapter = null)
    {
      if (field is IEntityField entityField)
        return entityField;
      return adapter == null ? null : GetFieldPersistenceInfo(adapter, (IEntityField2)field);
    }

    public static IFieldPersistenceInfo GetFieldPersistenceInfoSafely(IEntityFieldCore field,
      IDataAccessAdapter adapter = null)
    {
      if (field is IEntityField entityField)
        return entityField;
      return adapter == null ? null : GetFieldPersistenceInfoSafely(adapter, (IEntityField2)field);
    }

    public static IFieldPersistenceInfo GetFieldPersistenceInfoSafely(IEntityCore entity,
      IDataAccessAdapter adapter = null)
    {
      return GetFieldPersistenceInfoSafely(entity.Fields.First(), adapter);
    }

    #endregion GetFieldPersistenceInfo

    public static IEnumerable<string> GetFieldsCustomProperties(IEntityCore entity, string fieldName)
    {
      return entity.FieldsCustomPropertiesOfType.ContainsKey(fieldName)
        ? entity.FieldsCustomPropertiesOfType[fieldName].Values
        : Enumerable.Empty<string>();
    }

    /// <summary>
    ///   Gets the navigator name(s) for a foreign key field.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public static IEnumerable<string> GetNavigatorNames(IEntityCore entity, string fieldName)
    {
      return from entityRelation in entity.GetAllRelations().Where(r => !r.StartEntityIsPkSide)
        from fkEntityFieldCoreObject in entityRelation.GetAllFKEntityFieldCoreObjects()
        where fkEntityFieldCoreObject.Name.Equals(fieldName)
        select entityRelation.MappedFieldName;
    }

    public static string GetPkIdStringFromFields(List<IEntityField2> primaryKeyFields)
    {
      if (primaryKeyFields.Count == 1 && primaryKeyFields[0].CurrentValue == null)
        return null;

      return string.Join("/", primaryKeyFields.Select(pk => (pk.CurrentValue ?? "").ToString()).ToArray());
    }

    public static string GetPkIdStringFromEntity(EntityBase2 entity)
    {
      return GetPkIdStringFromFields(((IEntity2)entity).PrimaryKeyFields);
    }

    public static Context GetContextToUse(object potentialContextAwareElement)
    {
      if (potentialContextAwareElement != null)
      {
        var contextAwareElement = potentialContextAwareElement as IContextAwareElement;
        if (contextAwareElement == null)
          if (potentialContextAwareElement is IQueryable queryable)
            contextAwareElement = queryable.Provider as IContextAwareElement;

        if (contextAwareElement == null)
        {
          var contextToUse = ((dynamic)potentialContextAwareElement).ContextToUse as Context;
          return contextToUse;
        }

        return contextAwareElement.ContextToUse;
      }

      return null;
    }

    /// <summary>
    ///   Tests whether the Primary key fields are valid.
    /// </summary>
    /// <param name="primaryKeyFields">The primary key fields.</param>
    /// <returns></returns>
    public static bool PrimaryKeyFieldsAreValid<T>(IEnumerable<T> primaryKeyFields) where T : IEntityFieldCore
    {
      return primaryKeyFields.All(primaryKeyField => ValidKeyValue(primaryKeyField.CurrentValue));
    }

    /// <summary>
    ///   Tests wether the Primary key value is valid.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns></returns>
    public static bool ValidKeyValue(object o)
    {
      return !(MetaDataHelper.HasDefaultValue(o) ||
               o is int && (int)o <= 0 ||
               o is string && string.IsNullOrEmpty((string)o) ||
               o is Guid && (Guid)o == Guid.Empty ||
               o == DBNull.Value ||
               o is short && (short)o <= 0 ||
               o is long && (long)o <= 0 ||
               o is DateTime && (DateTime)o == DateTime.MinValue ||
               o is double && (double)o <= 0 ||
               o is decimal && (decimal)o <= 0 ||
               o is float && (float)o <= 0);
    }
  }

  public enum LLBLQueryType
  {
    Native,
    QuerySpec,
    Linq
  }
}