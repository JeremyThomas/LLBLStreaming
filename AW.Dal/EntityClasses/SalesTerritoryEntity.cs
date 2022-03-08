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
	/// <summary>Entity class which represents the entity 'SalesTerritory'.<br/><br/>MS_Description: Sales territory lookup table.<br/></summary>
	[Serializable]
	public partial class SalesTerritoryEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<StateProvinceEntity> _stateProvinces;
		private EntityCollection<CustomerEntity> _customers;
		private EntityCollection<SalesOrderHeaderEntity> _salesOrderHeaders;
		private EntityCollection<SalesPersonEntity> _salesPeople;
		private EntityCollection<SalesTerritoryHistoryEntity> _salesTerritoryHistories;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static SalesTerritoryEntityStaticMetaData _staticMetaData = new SalesTerritoryEntityStaticMetaData();
		private static SalesTerritoryRelations _relationsFactory = new SalesTerritoryRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name StateProvinces</summary>
			public static readonly string StateProvinces = "StateProvinces";
			/// <summary>Member name Customers</summary>
			public static readonly string Customers = "Customers";
			/// <summary>Member name SalesOrderHeaders</summary>
			public static readonly string SalesOrderHeaders = "SalesOrderHeaders";
			/// <summary>Member name SalesPeople</summary>
			public static readonly string SalesPeople = "SalesPeople";
			/// <summary>Member name SalesTerritoryHistories</summary>
			public static readonly string SalesTerritoryHistories = "SalesTerritoryHistories";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SalesTerritoryEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SalesTerritoryEntityStaticMetaData()
			{
				SetEntityCoreInfo("SalesTerritoryEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.SalesTerritoryEntity, typeof(SalesTerritoryEntity), typeof(SalesTerritoryEntityFactory), false);
				AddNavigatorMetaData<SalesTerritoryEntity, EntityCollection<StateProvinceEntity>>("StateProvinces", a => a._stateProvinces, (a, b) => a._stateProvinces = b, a => a.StateProvinces, () => new SalesTerritoryRelations().StateProvinceEntityUsingTerritoryID, typeof(StateProvinceEntity), (int)AW.Dal.EntityType.StateProvinceEntity);
				AddNavigatorMetaData<SalesTerritoryEntity, EntityCollection<CustomerEntity>>("Customers", a => a._customers, (a, b) => a._customers = b, a => a.Customers, () => new SalesTerritoryRelations().CustomerEntityUsingTerritoryID, typeof(CustomerEntity), (int)AW.Dal.EntityType.CustomerEntity);
				AddNavigatorMetaData<SalesTerritoryEntity, EntityCollection<SalesOrderHeaderEntity>>("SalesOrderHeaders", a => a._salesOrderHeaders, (a, b) => a._salesOrderHeaders = b, a => a.SalesOrderHeaders, () => new SalesTerritoryRelations().SalesOrderHeaderEntityUsingTerritoryID, typeof(SalesOrderHeaderEntity), (int)AW.Dal.EntityType.SalesOrderHeaderEntity);
				AddNavigatorMetaData<SalesTerritoryEntity, EntityCollection<SalesPersonEntity>>("SalesPeople", a => a._salesPeople, (a, b) => a._salesPeople = b, a => a.SalesPeople, () => new SalesTerritoryRelations().SalesPersonEntityUsingTerritoryID, typeof(SalesPersonEntity), (int)AW.Dal.EntityType.SalesPersonEntity);
				AddNavigatorMetaData<SalesTerritoryEntity, EntityCollection<SalesTerritoryHistoryEntity>>("SalesTerritoryHistories", a => a._salesTerritoryHistories, (a, b) => a._salesTerritoryHistories = b, a => a.SalesTerritoryHistories, () => new SalesTerritoryRelations().SalesTerritoryHistoryEntityUsingTerritoryID, typeof(SalesTerritoryHistoryEntity), (int)AW.Dal.EntityType.SalesTerritoryHistoryEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static SalesTerritoryEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public SalesTerritoryEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SalesTerritoryEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SalesTerritoryEntity</param>
		public SalesTerritoryEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="territoryID">PK value for SalesTerritory which data should be fetched into this SalesTerritory object</param>
		public SalesTerritoryEntity(System.Int32 territoryID) : this(territoryID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="territoryID">PK value for SalesTerritory which data should be fetched into this SalesTerritory object</param>
		/// <param name="validator">The custom validator object for this SalesTerritoryEntity</param>
		public SalesTerritoryEntity(System.Int32 territoryID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.TerritoryID = territoryID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SalesTerritoryEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'StateProvince' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoStateProvinces() { return CreateRelationInfoForNavigator("StateProvinces"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Customer' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoCustomers() { return CreateRelationInfoForNavigator("Customers"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesOrderHeader' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesOrderHeaders() { return CreateRelationInfoForNavigator("SalesOrderHeaders"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesPerson' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesPeople() { return CreateRelationInfoForNavigator("SalesPeople"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesTerritoryHistory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesTerritoryHistories() { return CreateRelationInfoForNavigator("SalesTerritoryHistories"); }
		
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
			_customProperties.Add("MS_Description", @"Sales territory lookup table.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Business costs in the territory the previous year.");
			_fieldsCustomProperties.Add("CostLastYear", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Business costs in the territory year to date.");
			_fieldsCustomProperties.Add("CostYtd", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Unique nonclustered index. Used to support replication samples.");
			_fieldsCustomProperties.Add("CountryRegionCode", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Geographic area to which the sales territory belong.");
			_fieldsCustomProperties.Add("Group", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales territory description");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales in the territory the previous year.");
			_fieldsCustomProperties.Add("SalesLastYear", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales in the territory year to date.");
			_fieldsCustomProperties.Add("SalesYtd", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("TerritoryID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this SalesTerritoryEntity</param>
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
		public static SalesTerritoryRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'StateProvince' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathStateProvinces { get { return _staticMetaData.GetPrefetchPathElement("StateProvinces", CommonEntityBase.CreateEntityCollection<StateProvinceEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Customer' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathCustomers { get { return _staticMetaData.GetPrefetchPathElement("Customers", CommonEntityBase.CreateEntityCollection<CustomerEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesOrderHeader' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesOrderHeaders { get { return _staticMetaData.GetPrefetchPathElement("SalesOrderHeaders", CommonEntityBase.CreateEntityCollection<SalesOrderHeaderEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesPerson' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesPeople { get { return _staticMetaData.GetPrefetchPathElement("SalesPeople", CommonEntityBase.CreateEntityCollection<SalesPersonEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesTerritoryHistory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesTerritoryHistories { get { return _staticMetaData.GetPrefetchPathElement("SalesTerritoryHistories", CommonEntityBase.CreateEntityCollection<SalesTerritoryHistoryEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The CostLastYear property of the Entity SalesTerritory<br/><br/>MS_Description: Business costs in the territory the previous year.<br/>Business costs in the territory the previous year.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."CostLastYear".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal CostLastYear
		{
			get { return (System.Decimal)GetValue((int)SalesTerritoryFieldIndex.CostLastYear, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.CostLastYear, value); }
		}

		/// <summary>The CostYtd property of the Entity SalesTerritory<br/><br/>MS_Description: Business costs in the territory year to date.<br/>Business costs in the territory year to date.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."CostYTD".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal CostYtd
		{
			get { return (System.Decimal)GetValue((int)SalesTerritoryFieldIndex.CostYtd, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.CostYtd, value); }
		}

		/// <summary>The CountryRegionCode property of the Entity SalesTerritory<br/><br/>MS_Description: Unique nonclustered index. Used to support replication samples.<br/>Unique nonclustered index. Used to support replication samples.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."CountryRegionCode".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 3.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String CountryRegionCode
		{
			get { return (System.String)GetValue((int)SalesTerritoryFieldIndex.CountryRegionCode, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.CountryRegionCode, value); }
		}

		/// <summary>The Group property of the Entity SalesTerritory<br/><br/>MS_Description: Geographic area to which the sales territory belong.<br/>Geographic area to which the sales territory belong.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."Group".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Group
		{
			get { return (System.String)GetValue((int)SalesTerritoryFieldIndex.Group, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.Group, value); }
		}

		/// <summary>The ModifiedDate property of the Entity SalesTerritory<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)SalesTerritoryFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity SalesTerritory<br/><br/>MS_Description: Sales territory description<br/>Sales territory description</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SalesTerritoryFieldIndex.Name, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.Name, value); }
		}

		/// <summary>The Rowguid property of the Entity SalesTerritory<br/><br/>MS_Description: ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.<br/>ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."rowguid".<br/>Table field type characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Guid Rowguid
		{
			get { return (System.Guid)GetValue((int)SalesTerritoryFieldIndex.Rowguid, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.Rowguid, value); }
		}

		/// <summary>The SalesLastYear property of the Entity SalesTerritory<br/><br/>MS_Description: Sales in the territory the previous year.<br/>Sales in the territory the previous year.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."SalesLastYear".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal SalesLastYear
		{
			get { return (System.Decimal)GetValue((int)SalesTerritoryFieldIndex.SalesLastYear, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.SalesLastYear, value); }
		}

		/// <summary>The SalesYtd property of the Entity SalesTerritory<br/><br/>MS_Description: Sales in the territory year to date.<br/>Sales in the territory year to date.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."SalesYTD".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal SalesYtd
		{
			get { return (System.Decimal)GetValue((int)SalesTerritoryFieldIndex.SalesYtd, true); }
			set	{ SetValue((int)SalesTerritoryFieldIndex.SalesYtd, value); }
		}

		/// <summary>The TerritoryID property of the Entity SalesTerritory<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "SalesTerritory"."TerritoryID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 TerritoryID
		{
			get { return (System.Int32)GetValue((int)SalesTerritoryFieldIndex.TerritoryID, true); }
			set { SetValue((int)SalesTerritoryFieldIndex.TerritoryID, value); }		}

		/// <summary>Gets the EntityCollection with the related entities of type 'StateProvinceEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(StateProvinceEntity))]
		public virtual EntityCollection<StateProvinceEntity> StateProvinces { get { return GetOrCreateEntityCollection<StateProvinceEntity, StateProvinceEntityFactory>("SalesTerritory", true, false, ref _stateProvinces); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'CustomerEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(CustomerEntity))]
		public virtual EntityCollection<CustomerEntity> Customers { get { return GetOrCreateEntityCollection<CustomerEntity, CustomerEntityFactory>("SalesTerritory", true, false, ref _customers); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesOrderHeaderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesOrderHeaderEntity))]
		public virtual EntityCollection<SalesOrderHeaderEntity> SalesOrderHeaders { get { return GetOrCreateEntityCollection<SalesOrderHeaderEntity, SalesOrderHeaderEntityFactory>("SalesTerritory", true, false, ref _salesOrderHeaders); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesPersonEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesPersonEntity))]
		public virtual EntityCollection<SalesPersonEntity> SalesPeople { get { return GetOrCreateEntityCollection<SalesPersonEntity, SalesPersonEntityFactory>("SalesTerritory", true, false, ref _salesPeople); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesTerritoryHistoryEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesTerritoryHistoryEntity))]
		public virtual EntityCollection<SalesTerritoryHistoryEntity> SalesTerritoryHistories { get { return GetOrCreateEntityCollection<SalesTerritoryHistoryEntity, SalesTerritoryHistoryEntityFactory>("SalesTerritory", true, false, ref _salesTerritoryHistories); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum SalesTerritoryFieldIndex
	{
		///<summary>CostLastYear. </summary>
		CostLastYear,
		///<summary>CostYtd. </summary>
		CostYtd,
		///<summary>CountryRegionCode. </summary>
		CountryRegionCode,
		///<summary>Group. </summary>
		Group,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		///<summary>Rowguid. </summary>
		Rowguid,
		///<summary>SalesLastYear. </summary>
		SalesLastYear,
		///<summary>SalesYtd. </summary>
		SalesYtd,
		///<summary>TerritoryID. </summary>
		TerritoryID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: SalesTerritory. </summary>
	public partial class SalesTerritoryRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between SalesTerritoryEntity and StateProvinceEntity over the 1:n relation they have, using the relation between the fields: SalesTerritory.TerritoryID - StateProvince.TerritoryID</summary>
		public virtual IEntityRelation StateProvinceEntityUsingTerritoryID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "StateProvinces", true, new[] { SalesTerritoryFields.TerritoryID, StateProvinceFields.TerritoryID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between SalesTerritoryEntity and CustomerEntity over the 1:n relation they have, using the relation between the fields: SalesTerritory.TerritoryID - Customer.TerritoryID</summary>
		public virtual IEntityRelation CustomerEntityUsingTerritoryID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "Customers", true, new[] { SalesTerritoryFields.TerritoryID, CustomerFields.TerritoryID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between SalesTerritoryEntity and SalesOrderHeaderEntity over the 1:n relation they have, using the relation between the fields: SalesTerritory.TerritoryID - SalesOrderHeader.TerritoryID</summary>
		public virtual IEntityRelation SalesOrderHeaderEntityUsingTerritoryID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesOrderHeaders", true, new[] { SalesTerritoryFields.TerritoryID, SalesOrderHeaderFields.TerritoryID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between SalesTerritoryEntity and SalesPersonEntity over the 1:n relation they have, using the relation between the fields: SalesTerritory.TerritoryID - SalesPerson.TerritoryID</summary>
		public virtual IEntityRelation SalesPersonEntityUsingTerritoryID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesPeople", true, new[] { SalesTerritoryFields.TerritoryID, SalesPersonFields.TerritoryID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between SalesTerritoryEntity and SalesTerritoryHistoryEntity over the 1:n relation they have, using the relation between the fields: SalesTerritory.TerritoryID - SalesTerritoryHistory.TerritoryID</summary>
		public virtual IEntityRelation SalesTerritoryHistoryEntityUsingTerritoryID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesTerritoryHistories", true, new[] { SalesTerritoryFields.TerritoryID, SalesTerritoryHistoryFields.TerritoryID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSalesTerritoryRelations
	{
		internal static readonly IEntityRelation StateProvinceEntityUsingTerritoryIDStatic = new SalesTerritoryRelations().StateProvinceEntityUsingTerritoryID;
		internal static readonly IEntityRelation CustomerEntityUsingTerritoryIDStatic = new SalesTerritoryRelations().CustomerEntityUsingTerritoryID;
		internal static readonly IEntityRelation SalesOrderHeaderEntityUsingTerritoryIDStatic = new SalesTerritoryRelations().SalesOrderHeaderEntityUsingTerritoryID;
		internal static readonly IEntityRelation SalesPersonEntityUsingTerritoryIDStatic = new SalesTerritoryRelations().SalesPersonEntityUsingTerritoryID;
		internal static readonly IEntityRelation SalesTerritoryHistoryEntityUsingTerritoryIDStatic = new SalesTerritoryRelations().SalesTerritoryHistoryEntityUsingTerritoryID;

		/// <summary>CTor</summary>
		static StaticSalesTerritoryRelations() { }
	}
}