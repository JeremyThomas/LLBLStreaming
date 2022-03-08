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
	/// <summary>Entity class which represents the entity 'Location'.<br/><br/>MS_Description: Product inventory and manufacturing locations.<br/></summary>
	[Serializable]
	public partial class LocationEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<ProductInventoryEntity> _productInventories;
		private EntityCollection<WorkOrderRoutingEntity> _workOrderRoutings;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static LocationEntityStaticMetaData _staticMetaData = new LocationEntityStaticMetaData();
		private static LocationRelations _relationsFactory = new LocationRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ProductInventories</summary>
			public static readonly string ProductInventories = "ProductInventories";
			/// <summary>Member name WorkOrderRoutings</summary>
			public static readonly string WorkOrderRoutings = "WorkOrderRoutings";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class LocationEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public LocationEntityStaticMetaData()
			{
				SetEntityCoreInfo("LocationEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.LocationEntity, typeof(LocationEntity), typeof(LocationEntityFactory), false);
				AddNavigatorMetaData<LocationEntity, EntityCollection<ProductInventoryEntity>>("ProductInventories", a => a._productInventories, (a, b) => a._productInventories = b, a => a.ProductInventories, () => new LocationRelations().ProductInventoryEntityUsingLocationID, typeof(ProductInventoryEntity), (int)AW.Dal.EntityType.ProductInventoryEntity);
				AddNavigatorMetaData<LocationEntity, EntityCollection<WorkOrderRoutingEntity>>("WorkOrderRoutings", a => a._workOrderRoutings, (a, b) => a._workOrderRoutings = b, a => a.WorkOrderRoutings, () => new LocationRelations().WorkOrderRoutingEntityUsingLocationID, typeof(WorkOrderRoutingEntity), (int)AW.Dal.EntityType.WorkOrderRoutingEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static LocationEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public LocationEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public LocationEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this LocationEntity</param>
		public LocationEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="locationID">PK value for Location which data should be fetched into this Location object</param>
		public LocationEntity(System.Int16 locationID) : this(locationID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="locationID">PK value for Location which data should be fetched into this Location object</param>
		/// <param name="validator">The custom validator object for this LocationEntity</param>
		public LocationEntity(System.Int16 locationID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.LocationID = locationID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected LocationEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductInventory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductInventories() { return CreateRelationInfoForNavigator("ProductInventories"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'WorkOrderRouting' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoWorkOrderRoutings() { return CreateRelationInfoForNavigator("WorkOrderRoutings"); }
		
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
			_customProperties.Add("MS_Description", @"Product inventory and manufacturing locations.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Work capacity (in hours) of the manufacturing location.");
			_fieldsCustomProperties.Add("Availability", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Standard hourly cost of the manufacturing location.");
			_fieldsCustomProperties.Add("CostRate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("LocationID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Location description.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this LocationEntity</param>
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
		public static LocationRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductInventory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductInventories { get { return _staticMetaData.GetPrefetchPathElement("ProductInventories", CommonEntityBase.CreateEntityCollection<ProductInventoryEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'WorkOrderRouting' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathWorkOrderRoutings { get { return _staticMetaData.GetPrefetchPathElement("WorkOrderRoutings", CommonEntityBase.CreateEntityCollection<WorkOrderRoutingEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The Availability property of the Entity Location<br/><br/>MS_Description: Work capacity (in hours) of the manufacturing location.<br/>Work capacity (in hours) of the manufacturing location.</summary>
		/// <remarks>Mapped on  table field: "Location"."Availability".<br/>Table field type characteristics (type, precision, scale, length): Decimal, 8, 2, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal Availability
		{
			get { return (System.Decimal)GetValue((int)LocationFieldIndex.Availability, true); }
			set	{ SetValue((int)LocationFieldIndex.Availability, value); }
		}

		/// <summary>The CostRate property of the Entity Location<br/><br/>MS_Description: Standard hourly cost of the manufacturing location.<br/>Standard hourly cost of the manufacturing location.</summary>
		/// <remarks>Mapped on  table field: "Location"."CostRate".<br/>Table field type characteristics (type, precision, scale, length): SmallMoney, 10, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal CostRate
		{
			get { return (System.Decimal)GetValue((int)LocationFieldIndex.CostRate, true); }
			set	{ SetValue((int)LocationFieldIndex.CostRate, value); }
		}

		/// <summary>The LocationID property of the Entity Location<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "Location"."LocationID".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int16 LocationID
		{
			get { return (System.Int16)GetValue((int)LocationFieldIndex.LocationID, true); }
			set { SetValue((int)LocationFieldIndex.LocationID, value); }		}

		/// <summary>The ModifiedDate property of the Entity Location<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "Location"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)LocationFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)LocationFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity Location<br/><br/>MS_Description: Location description.<br/>Location description.</summary>
		/// <remarks>Mapped on  table field: "Location"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)LocationFieldIndex.Name, true); }
			set	{ SetValue((int)LocationFieldIndex.Name, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductInventoryEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductInventoryEntity))]
		public virtual EntityCollection<ProductInventoryEntity> ProductInventories { get { return GetOrCreateEntityCollection<ProductInventoryEntity, ProductInventoryEntityFactory>("Location", true, false, ref _productInventories); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'WorkOrderRoutingEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(WorkOrderRoutingEntity))]
		public virtual EntityCollection<WorkOrderRoutingEntity> WorkOrderRoutings { get { return GetOrCreateEntityCollection<WorkOrderRoutingEntity, WorkOrderRoutingEntityFactory>("Location", true, false, ref _workOrderRoutings); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum LocationFieldIndex
	{
		///<summary>Availability. </summary>
		Availability,
		///<summary>CostRate. </summary>
		CostRate,
		///<summary>LocationID. </summary>
		LocationID,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Location. </summary>
	public partial class LocationRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between LocationEntity and ProductInventoryEntity over the 1:n relation they have, using the relation between the fields: Location.LocationID - ProductInventory.LocationID</summary>
		public virtual IEntityRelation ProductInventoryEntityUsingLocationID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "ProductInventories", true, new[] { LocationFields.LocationID, ProductInventoryFields.LocationID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between LocationEntity and WorkOrderRoutingEntity over the 1:n relation they have, using the relation between the fields: Location.LocationID - WorkOrderRouting.LocationID</summary>
		public virtual IEntityRelation WorkOrderRoutingEntityUsingLocationID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "WorkOrderRoutings", true, new[] { LocationFields.LocationID, WorkOrderRoutingFields.LocationID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticLocationRelations
	{
		internal static readonly IEntityRelation ProductInventoryEntityUsingLocationIDStatic = new LocationRelations().ProductInventoryEntityUsingLocationID;
		internal static readonly IEntityRelation WorkOrderRoutingEntityUsingLocationIDStatic = new LocationRelations().WorkOrderRoutingEntityUsingLocationID;

		/// <summary>CTor</summary>
		static StaticLocationRelations() { }
	}
}