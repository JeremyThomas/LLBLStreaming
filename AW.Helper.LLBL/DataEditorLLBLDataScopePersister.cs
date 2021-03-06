using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace AW.Helper.LLBL
{
  public class DataEditorLLBLDataScopePersister : DataEditorLLBLPersister, IDataEditorEventHandlers
  {
    public GeneralEntityCollectionDataScope GeneralEntityCollectionDataScope;

#pragma warning disable CS3002 // Return type is not CLS-compliant
    public static IDataEditorPersister DataEditorLLBLDataScopePersisterFactory(object data)
#pragma warning restore CS3002 // Return type is not CLS-compliant
    {
      var queryable = data as IQueryable<IEntityCore>;
      if (queryable == null)
      {
        var selfServcingData = data as IEnumerable<IEntity>;
        return selfServcingData == null ? null : new DataEditorLLBLSelfServicingPersister();
      }
      return new DataEditorLLBLDataScopePersister(queryable);
    }

    protected DataEditorLLBLDataScopePersister()
    {
    }

    public DataEditorLLBLDataScopePersister(IQueryable query)
    {
      GeneralEntityCollectionDataScope = new GeneralEntityCollectionDataScope(query);
    }

    public DataEditorLLBLDataScopePersister(GeneralEntityCollectionDataScope generalEntityCollectionDataScope)
    {
      GeneralEntityCollectionDataScope = generalEntityCollectionDataScope;
    }

    public DataEditorLLBLDataScopePersister(IEnumerable enumerable, ITransactionController transactionController)
      : this(new GeneralEntityCollectionDataScope(enumerable, transactionController))
    {
    }

    public DataEditorLLBLDataScopePersister(IContextAwareElement contextAwareElement, ITransactionController transactionController = null)
      : this(new GeneralEntityCollectionDataScope(contextAwareElement, transactionController))
    {
    }

    public event EventHandler EditingFinished
    {
      add { GeneralEntityCollectionDataScope.EditingFinished += value; }
      remove { GeneralEntityCollectionDataScope.EditingFinished -= value; }
    }

    /// <summary>
    ///   Raised when the data of an entity in the scope changed. Ignored during fetches. Sender is the entity which data was
    ///   changed
    /// </summary>
    public event EventHandler ContainedDataChanged
    {
      add { GeneralEntityCollectionDataScope.ContainedDataChanged += value; }
      remove { GeneralEntityCollectionDataScope.ContainedDataChanged -= value; }
    }

    /// <summary>
    ///   Raised when an entity has been added to the scope. Ignored during fetches. Sender is the entity which was added.
    /// </summary>
    public event EventHandler EntityAdded
    {
      add { GeneralEntityCollectionDataScope.EntityAdded += value; }
      remove { GeneralEntityCollectionDataScope.EntityAdded -= value; }
    }

    public event EventHandler EntityRemoved
    {
      add { GeneralEntityCollectionDataScope.EntityRemoved += value; }
      remove { GeneralEntityCollectionDataScope.EntityRemoved -= value; }
    }

    public override int Save(object dataToSave = null, bool cascadeDeletes = false)
    {
      return GeneralEntityCollectionDataScope.Save(dataToSave, cascadeDeletes);
    }

    public override int Delete(object dataToDelete, bool cascade = false)
    {
      return GeneralEntityCollectionDataScope.Delete(dataToDelete, cascade);
    }

    public override bool Undo(object dataToDelete)
    {
      GeneralEntityCollectionDataScope.Undo(dataToDelete);
      return true;
    }

    public override IDictionary<string, int> GetChildCounts(object entityThatMayHaveChildren)
    {
      return GeneralEntityCollectionDataScope.GetChildCounts(entityThatMayHaveChildren);
    }
  }

}