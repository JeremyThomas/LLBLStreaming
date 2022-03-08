﻿//////////////////////////////////////////////////////////////
// <auto-generated>This code was generated by LLBLGen Pro 5.9.</auto-generated>
//////////////////////////////////////////////////////////////
// Code is generated on: 
// Code is generated using templates: SD.TemplateBindings.SharedTemplates
// Templates vendor: Solutions Design.
//////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using AW.Dal.HelperClasses;
using AW.Dal.FactoryClasses;
using AW.Dal.RelationClasses;
using System.ComponentModel.DataAnnotations;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace AW.Dal.EntityClasses
{
	// __LLBLGENPRO_USER_CODE_REGION_START AdditionalNamespaces
	// __LLBLGENPRO_USER_CODE_REGION_END
	/// <summary>Entity class which represents the entity 'WorkOrder'.<br/><br/>MS_Description: Manufacturing work orders.<br/></summary>
	[Serializable]
	public partial class WorkOrderEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<WorkOrderRoutingEntity> _workOrderRoutings;
		private ProductEntity _product;
		private ScrapReasonEntity _scrapReason;
		private WorkOrderHistoryEntity _workOrderHistory;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static WorkOrderEntityStaticMetaData _staticMetaData = new WorkOrderEntityStaticMetaData();
		private static WorkOrderRelations _relationsFactory = new WorkOrderRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Product</summary>
			public static readonly string Product = "Product";
			/// <summary>Member name ScrapReason</summary>
			public static readonly string ScrapReason = "ScrapReason";
			/// <summary>Member name WorkOrderRoutings</summary>
			public static readonly string WorkOrderRoutings = "WorkOrderRoutings";
			/// <summary>Member name WorkOrderHistory</summary>
			public static readonly string WorkOrderHistory = "WorkOrderHistory";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class WorkOrderEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public WorkOrderEntityStaticMetaData()
			{
				SetEntityCoreInfo("WorkOrderEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.WorkOrderEntity, typeof(WorkOrderEntity), typeof(WorkOrderEntityFactory), false);
				AddNavigatorMetaData<WorkOrderEntity, EntityCollection<WorkOrderRoutingEntity>>("WorkOrderRoutings", a => a._workOrderRoutings, (a, b) => a._workOrderRoutings = b, a => a.WorkOrderRoutings, () => new WorkOrderRelations().WorkOrderRoutingEntityUsingWorkOrderID, typeof(WorkOrderRoutingEntity), (int)AW.Dal.EntityType.WorkOrderRoutingEntity);
				AddNavigatorMetaData<WorkOrderEntity, ProductEntity>("Product", "WorkOrders", (a, b) => a._product = b, a => a._product, (a, b) => a.Product = b, AW.Dal.RelationClasses.StaticWorkOrderRelations.ProductEntityUsingProductIDStatic, ()=>new WorkOrderRelations().ProductEntityUsingProductID, null, new int[] { (int)WorkOrderFieldIndex.ProductID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
				AddNavigatorMetaData<WorkOrderEntity, ScrapReasonEntity>("ScrapReason", "WorkOrders", (a, b) => a._scrapReason = b, a => a._scrapReason, (a, b) => a.ScrapReason = b, AW.Dal.RelationClasses.StaticWorkOrderRelations.ScrapReasonEntityUsingScrapReasonIDStatic, ()=>new WorkOrderRelations().ScrapReasonEntityUsingScrapReasonID, null, new int[] { (int)WorkOrderFieldIndex.ScrapReasonID }, null, true, (int)AW.Dal.EntityType.ScrapReasonEntity);
				AddNavigatorMetaData<WorkOrderEntity, WorkOrderHistoryEntity>("WorkOrderHistory", "WorkOrder", (a, b) => a._workOrderHistory = b, a => a._workOrderHistory, (a, b) => a.WorkOrderHistory = b, AW.Dal.RelationClasses.StaticWorkOrderRelations.WorkOrderHistoryEntityUsingReferenceOrderIDStatic, ()=>new WorkOrderRelations().WorkOrderHistoryEntityUsingReferenceOrderID, null, null, null, true, (int)AW.Dal.EntityType.WorkOrderHistoryEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static WorkOrderEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public WorkOrderEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public WorkOrderEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this WorkOrderEntity</param>
		public WorkOrderEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="workOrderID">PK value for WorkOrder which data should be fetched into this WorkOrder object</param>
		public WorkOrderEntity(System.Int32 workOrderID) : this(workOrderID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="workOrderID">PK value for WorkOrder which data should be fetched into this WorkOrder object</param>
		/// <param name="validator">The custom validator object for this WorkOrderEntity</param>
		public WorkOrderEntity(System.Int32 workOrderID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.WorkOrderID = workOrderID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected WorkOrderEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'WorkOrderRouting' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoWorkOrderRoutings() { return CreateRelationInfoForNavigator("WorkOrderRoutings"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProduct() { return CreateRelationInfoForNavigator("Product"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'ScrapReason' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoScrapReason() { return CreateRelationInfoForNavigator("ScrapReason"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'WorkOrderHistory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoWorkOrderHistory() { return CreateRelationInfoForNavigator("WorkOrderHistory"); }
		
		/// <inheritdoc/>
		protected override EntityStaticMetaDataBase GetEntityStaticMetaData() {	return _staticMetaData; }

		/// <summary>Initializes the class members</summary>
		private void InitClassMembers()
		{
			PerformDependencyInjection();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassMembers
			// __LLBLGENPRO_USER_CODE_REGION_END
			OnInitClassMembersComplete();
		}

		/// <summary>Initializes the hashtables for the entity type and entity field custom properties. </summary>
		private static void SetupCustomPropertyHashtables()
		{
			_customProperties = new Dictionary<string, string>();
			_customProperties.Add("MS_Description", @"Manufacturing work orders.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Work order due date.");
			_fieldsCustomProperties.Add("DueDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Work order end date.");
			_fieldsCustomProperties.Add("EndDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Nonclustered index.");
			_fieldsCustomProperties.Add("OrderQuantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Product identification number. Foreign key to Product.ProductID.");
			_fieldsCustomProperties.Add("ProductID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Quantity that failed inspection.");
			_fieldsCustomProperties.Add("ScrappedQuantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Reason for inspection failure.");
			_fieldsCustomProperties.Add("ScrapReasonID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Work order start date.");
			_fieldsCustomProperties.Add("StartDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Quantity built and put in inventory.");
			_fieldsCustomProperties.Add("StockedQuantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("WorkOrderID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this WorkOrderEntity</param>
		/// <param name="fields">Fields of this entity</param>
		private void InitClassEmpty(IValidator validator, IEntityFields2 fields)
		{
			OnInitializing();
			this.Fields = fields ?? CreateFields();
			this.Validator = validator;
			InitClassMembers();
			// __LLBLGENPRO_USER_CODE_REGION_START InitClassEmpty
			// __LLBLGENPRO_USER_CODE_REGION_END

			OnInitialized();
		}

		/// <summary>The relations object holding all relations of this entity with other entity classes.</summary>
		public static WorkOrderRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'WorkOrderRouting' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathWorkOrderRoutings { get { return _staticMetaData.GetPrefetchPathElement("WorkOrderRoutings", CommonEntityBase.CreateEntityCollection<WorkOrderRoutingEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProduct { get { return _staticMetaData.GetPrefetchPathElement("Product", CommonEntityBase.CreateEntityCollection<ProductEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ScrapReason' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathScrapReason { get { return _staticMetaData.GetPrefetchPathElement("ScrapReason", CommonEntityBase.CreateEntityCollection<ScrapReasonEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'WorkOrderHistory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathWorkOrderHistory { get { return _staticMetaData.GetPrefetchPathElement("WorkOrderHistory", CommonEntityBase.CreateEntityCollection<WorkOrderHistoryEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The DueDate property of the Entity WorkOrder<br/><br/>MS_Description: Work order due date.<br/>Work order due date.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."DueDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime DueDate
		{
			get { return (System.DateTime)GetValue((int)WorkOrderFieldIndex.DueDate, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.DueDate, value); }
		}

		/// <summary>The EndDate property of the Entity WorkOrder<br/><br/>MS_Description: Work order end date.<br/>Work order end date.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."EndDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> EndDate
		{
			get { return (Nullable<System.DateTime>)GetValue((int)WorkOrderFieldIndex.EndDate, false); }
			set	{ SetValue((int)WorkOrderFieldIndex.EndDate, value); }
		}

		/// <summary>The ModifiedDate property of the Entity WorkOrder<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)WorkOrderFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The OrderQuantity property of the Entity WorkOrder<br/><br/>MS_Description: Nonclustered index.<br/>Nonclustered index.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."OrderQty".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 OrderQuantity
		{
			get { return (System.Int32)GetValue((int)WorkOrderFieldIndex.OrderQuantity, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.OrderQuantity, value); }
		}

		/// <summary>The ProductID property of the Entity WorkOrder<br/><br/>MS_Description: Product identification number. Foreign key to Product.ProductID.<br/>Product identification number. Foreign key to Product.ProductID.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."ProductID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ProductID
		{
			get { return (System.Int32)GetValue((int)WorkOrderFieldIndex.ProductID, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.ProductID, value); }
		}

		/// <summary>The ScrappedQuantity property of the Entity WorkOrder<br/><br/>MS_Description: Quantity that failed inspection.<br/>Quantity that failed inspection.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."ScrappedQty".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int16 ScrappedQuantity
		{
			get { return (System.Int16)GetValue((int)WorkOrderFieldIndex.ScrappedQuantity, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.ScrappedQuantity, value); }
		}

		/// <summary>The ScrapReasonID property of the Entity WorkOrder<br/><br/>MS_Description: Reason for inspection failure.<br/>Reason for inspection failure.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."ScrapReasonID".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int16> ScrapReasonID
		{
			get { return (Nullable<System.Int16>)GetValue((int)WorkOrderFieldIndex.ScrapReasonID, false); }
			set	{ SetValue((int)WorkOrderFieldIndex.ScrapReasonID, value); }
		}

		/// <summary>The StartDate property of the Entity WorkOrder<br/><br/>MS_Description: Work order start date.<br/>Work order start date.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."StartDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime StartDate
		{
			get { return (System.DateTime)GetValue((int)WorkOrderFieldIndex.StartDate, true); }
			set	{ SetValue((int)WorkOrderFieldIndex.StartDate, value); }
		}

		/// <summary>The StockedQuantity property of the Entity WorkOrder<br/><br/>MS_Description: Quantity built and put in inventory.<br/>Quantity built and put in inventory.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."StockedQty".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 StockedQuantity
		{
			get { return (System.Int32)GetValue((int)WorkOrderFieldIndex.StockedQuantity, true); }
		}

		/// <summary>The WorkOrderID property of the Entity WorkOrder<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "WorkOrder"."WorkOrderID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 WorkOrderID
		{
			get { return (System.Int32)GetValue((int)WorkOrderFieldIndex.WorkOrderID, true); }
			set { SetValue((int)WorkOrderFieldIndex.WorkOrderID, value); }		}

		/// <summary>Gets the EntityCollection with the related entities of type 'WorkOrderRoutingEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(WorkOrderRoutingEntity))]
		public virtual EntityCollection<WorkOrderRoutingEntity> WorkOrderRoutings { get { return GetOrCreateEntityCollection<WorkOrderRoutingEntity, WorkOrderRoutingEntityFactory>("WorkOrder", true, false, ref _workOrderRoutings); } }

		/// <summary>Gets / sets related entity of type 'ProductEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual ProductEntity Product
		{
			get { return _product; }
			set { SetSingleRelatedEntityNavigator(value, "Product"); }
		}

		/// <summary>Gets / sets related entity of type 'ScrapReasonEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual ScrapReasonEntity ScrapReason
		{
			get { return _scrapReason; }
			set { SetSingleRelatedEntityNavigator(value, "ScrapReason"); }
		}

		/// <summary>Gets / sets related entity of type 'WorkOrderHistoryEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned.<br/><br/></summary>
		[Browsable(false)]
		public virtual WorkOrderHistoryEntity WorkOrderHistory
		{
			get { return _workOrderHistory; }
			set { SetSingleRelatedEntityNavigator(value, "WorkOrderHistory"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum WorkOrderFieldIndex
	{
		///<summary>DueDate. </summary>
		DueDate,
		///<summary>EndDate. </summary>
		EndDate,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>OrderQuantity. </summary>
		OrderQuantity,
		///<summary>ProductID. </summary>
		ProductID,
		///<summary>ScrappedQuantity. </summary>
		ScrappedQuantity,
		///<summary>ScrapReasonID. </summary>
		ScrapReasonID,
		///<summary>StartDate. </summary>
		StartDate,
		///<summary>StockedQuantity. </summary>
		StockedQuantity,
		///<summary>WorkOrderID. </summary>
		WorkOrderID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: WorkOrder. </summary>
	public partial class WorkOrderRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between WorkOrderEntity and WorkOrderRoutingEntity over the 1:n relation they have, using the relation between the fields: WorkOrder.WorkOrderID - WorkOrderRouting.WorkOrderID</summary>
		public virtual IEntityRelation WorkOrderRoutingEntityUsingWorkOrderID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "WorkOrderRoutings", true, new[] { WorkOrderFields.WorkOrderID, WorkOrderRoutingFields.WorkOrderID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between WorkOrderEntity and WorkOrderHistoryEntity over the 1:1 relation they have, using the relation between the fields: WorkOrder.WorkOrderID - WorkOrderHistory.ReferenceOrderID</summary>
		public virtual IEntityRelation WorkOrderHistoryEntityUsingReferenceOrderID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToOne, "WorkOrderHistory", true, new[] { WorkOrderFields.WorkOrderID, WorkOrderHistoryFields.ReferenceOrderID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between WorkOrderEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: WorkOrder.ProductID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingProductID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Product", false, new[] { ProductFields.ProductID, WorkOrderFields.ProductID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between WorkOrderEntity and ScrapReasonEntity over the m:1 relation they have, using the relation between the fields: WorkOrder.ScrapReasonID - ScrapReason.ScrapReasonID</summary>
		public virtual IEntityRelation ScrapReasonEntityUsingScrapReasonID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "ScrapReason", false, new[] { ScrapReasonFields.ScrapReasonID, WorkOrderFields.ScrapReasonID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticWorkOrderRelations
	{
		internal static readonly IEntityRelation WorkOrderRoutingEntityUsingWorkOrderIDStatic = new WorkOrderRelations().WorkOrderRoutingEntityUsingWorkOrderID;
		internal static readonly IEntityRelation WorkOrderHistoryEntityUsingReferenceOrderIDStatic = new WorkOrderRelations().WorkOrderHistoryEntityUsingReferenceOrderID;
		internal static readonly IEntityRelation ProductEntityUsingProductIDStatic = new WorkOrderRelations().ProductEntityUsingProductID;
		internal static readonly IEntityRelation ScrapReasonEntityUsingScrapReasonIDStatic = new WorkOrderRelations().ScrapReasonEntityUsingScrapReasonID;

		/// <summary>CTor</summary>
		static StaticWorkOrderRelations() { }
	}
}
