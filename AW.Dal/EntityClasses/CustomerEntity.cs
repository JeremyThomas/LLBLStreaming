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
	/// <summary>Entity class which represents the entity 'Customer'.<br/><br/>MS_Description: Current customer information. Also see the Individual and Store tables.<br/></summary>
	[Serializable]
	public partial class CustomerEntity : CommonEntityBase, IMergable
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<CustomerAddressEntity> _customerAddresses;
		private EntityCollection<SalesOrderHeaderEntity> _salesOrderHeaders;
		private EntityCollection<AddressEntity> _addressCollectionViaCustomerAddress;
		private SalesTerritoryEntity _salesTerritory;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static CustomerEntityStaticMetaData _staticMetaData = new CustomerEntityStaticMetaData();
		private static CustomerRelations _relationsFactory = new CustomerRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name SalesTerritory</summary>
			public static readonly string SalesTerritory = "SalesTerritory";
			/// <summary>Member name CustomerAddresses</summary>
			public static readonly string CustomerAddresses = "CustomerAddresses";
			/// <summary>Member name SalesOrderHeaders</summary>
			public static readonly string SalesOrderHeaders = "SalesOrderHeaders";
			/// <summary>Member name AddressCollectionViaCustomerAddress</summary>
			public static readonly string AddressCollectionViaCustomerAddress = "AddressCollectionViaCustomerAddress";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class CustomerEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public CustomerEntityStaticMetaData()
			{
				SetEntityCoreInfo("CustomerEntity", InheritanceHierarchyType.TargetPerEntity, false, (int)AW.Dal.EntityType.CustomerEntity, typeof(CustomerEntity), typeof(CustomerEntityFactory), false);
				AddNavigatorMetaData<CustomerEntity, EntityCollection<CustomerAddressEntity>>("CustomerAddresses", a => a._customerAddresses, (a, b) => a._customerAddresses = b, a => a.CustomerAddresses, () => new CustomerRelations().CustomerAddressEntityUsingCustomerID, typeof(CustomerAddressEntity), (int)AW.Dal.EntityType.CustomerAddressEntity);
				AddNavigatorMetaData<CustomerEntity, EntityCollection<SalesOrderHeaderEntity>>("SalesOrderHeaders", a => a._salesOrderHeaders, (a, b) => a._salesOrderHeaders = b, a => a.SalesOrderHeaders, () => new CustomerRelations().SalesOrderHeaderEntityUsingCustomerID, typeof(SalesOrderHeaderEntity), (int)AW.Dal.EntityType.SalesOrderHeaderEntity);
				AddNavigatorMetaData<CustomerEntity, SalesTerritoryEntity>("SalesTerritory", "Customers", (a, b) => a._salesTerritory = b, a => a._salesTerritory, (a, b) => a.SalesTerritory = b, AW.Dal.RelationClasses.StaticCustomerRelations.SalesTerritoryEntityUsingTerritoryIDStatic, ()=>new CustomerRelations().SalesTerritoryEntityUsingTerritoryID, null, new int[] { (int)CustomerFieldIndex.TerritoryID }, null, true, (int)AW.Dal.EntityType.SalesTerritoryEntity);
				AddNavigatorMetaData<CustomerEntity, EntityCollection<AddressEntity>>("AddressCollectionViaCustomerAddress", a => a._addressCollectionViaCustomerAddress, (a, b) => a._addressCollectionViaCustomerAddress = b, a => a.AddressCollectionViaCustomerAddress, () => new CustomerRelations().CustomerAddressEntityUsingCustomerID, () => new CustomerAddressRelations().AddressEntityUsingAddressID, "CustomerEntity__", "CustomerAddress_", typeof(AddressEntity), (int)AW.Dal.EntityType.AddressEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static CustomerEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public CustomerEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public CustomerEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this CustomerEntity</param>
		public CustomerEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="customerID">PK value for Customer which data should be fetched into this Customer object</param>
		public CustomerEntity(System.Int32 customerID) : this(customerID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="customerID">PK value for Customer which data should be fetched into this Customer object</param>
		/// <param name="validator">The custom validator object for this CustomerEntity</param>
		public CustomerEntity(System.Int32 customerID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.CustomerID = customerID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected CustomerEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Gets a predicateexpression which filters on this entity. Only useful in entity fetches</summary>
		/// <param name="negate">Optional flag to produce a NOT filter, (true), or a normal filter (false, default). </param>
		/// <returns>ready to use predicateexpression</returns>
		public  static IPredicateExpression GetEntityTypeFilter(bool negate=false) { return ModelInfoProviderSingleton.GetInstance().GetEntityTypeFilter("CustomerEntity", negate); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'CustomerAddress' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoCustomerAddresses() { return CreateRelationInfoForNavigator("CustomerAddresses"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesOrderHeader' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesOrderHeaders() { return CreateRelationInfoForNavigator("SalesOrderHeaders"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Address' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoAddressCollectionViaCustomerAddress() { return CreateRelationInfoForNavigator("AddressCollectionViaCustomerAddress"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'SalesTerritory' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesTerritory() { return CreateRelationInfoForNavigator("SalesTerritory"); }
		
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
			_customProperties.Add("MS_Description", @"Current customer information. Also see the Individual and Store tables.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Unique nonclustered index.");
			_fieldsCustomProperties.Add("AccountNumber", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("CustomerID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Customer type: I = Individual, S = Store");
			_fieldsCustomProperties.Add("CustomerType", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Nonclustered index.");
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.");
			_fieldsCustomProperties.Add("TerritoryID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this CustomerEntity</param>
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
		public static CustomerRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'CustomerAddress' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathCustomerAddresses { get { return _staticMetaData.GetPrefetchPathElement("CustomerAddresses", CommonEntityBase.CreateEntityCollection<CustomerAddressEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesOrderHeader' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesOrderHeaders { get { return _staticMetaData.GetPrefetchPathElement("SalesOrderHeaders", CommonEntityBase.CreateEntityCollection<SalesOrderHeaderEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Address' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathAddressCollectionViaCustomerAddress { get { return _staticMetaData.GetPrefetchPathElement("AddressCollectionViaCustomerAddress", CommonEntityBase.CreateEntityCollection<AddressEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesTerritory' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesTerritory { get { return _staticMetaData.GetPrefetchPathElement("SalesTerritory", CommonEntityBase.CreateEntityCollection<SalesTerritoryEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The AccountNumber property of the Entity Customer<br/><br/>MS_Description: Unique nonclustered index.<br/>Unique nonclustered index.</summary>
		/// <remarks>Mapped on  table field: "Customer"."AccountNumber".<br/>Table field type characteristics (type, precision, scale, length): VarChar, 0, 0, 10.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String AccountNumber
		{
			get { return (System.String)GetValue((int)CustomerFieldIndex.AccountNumber, true); }
		}

		/// <summary>The CustomerID property of the Entity Customer<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "Customer"."CustomerID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 CustomerID
		{
			get { return (System.Int32)GetValue((int)CustomerFieldIndex.CustomerID, true); }
			set { SetValue((int)CustomerFieldIndex.CustomerID, value); }		}

		/// <summary>The CustomerType property of the Entity Customer<br/><br/>MS_Description: Customer type: I = Individual, S = Store<br/>Customer type: I = Individual, S = Store</summary>
		/// <remarks>Mapped on  table field: "Customer"."CustomerType".<br/>Table field type characteristics (type, precision, scale, length): NChar, 0, 0, 1.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String CustomerType
		{
			get { return (System.String)GetValue((int)CustomerFieldIndex.CustomerType, true); }
			set	{ SetValue((int)CustomerFieldIndex.CustomerType, value); }
		}

		/// <summary>The ModifiedDate property of the Entity Customer<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "Customer"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)CustomerFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)CustomerFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Rowguid property of the Entity Customer<br/><br/>MS_Description: Nonclustered index.<br/>Nonclustered index.</summary>
		/// <remarks>Mapped on  table field: "Customer"."rowguid".<br/>Table field type characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Guid Rowguid
		{
			get { return (System.Guid)GetValue((int)CustomerFieldIndex.Rowguid, true); }
			set	{ SetValue((int)CustomerFieldIndex.Rowguid, value); }
		}

		/// <summary>The TerritoryID property of the Entity Customer<br/><br/>MS_Description: ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.<br/>ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.</summary>
		/// <remarks>Mapped on  table field: "Customer"."TerritoryID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> TerritoryID
		{
			get { return (Nullable<System.Int32>)GetValue((int)CustomerFieldIndex.TerritoryID, false); }
			set	{ SetValue((int)CustomerFieldIndex.TerritoryID, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'CustomerAddressEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(CustomerAddressEntity))]
		public virtual EntityCollection<CustomerAddressEntity> CustomerAddresses { get { return GetOrCreateEntityCollection<CustomerAddressEntity, CustomerAddressEntityFactory>("Customer", true, false, ref _customerAddresses); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesOrderHeaderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesOrderHeaderEntity))]
		public virtual EntityCollection<SalesOrderHeaderEntity> SalesOrderHeaders { get { return GetOrCreateEntityCollection<SalesOrderHeaderEntity, SalesOrderHeaderEntityFactory>("Customer", true, false, ref _salesOrderHeaders); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'AddressEntity' which are related to this entity via a relation of type 'm:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(AddressEntity))]
		public virtual EntityCollection<AddressEntity> AddressCollectionViaCustomerAddress { get { return GetOrCreateEntityCollection<AddressEntity, AddressEntityFactory>("CustomerCollectionViaCustomerAddress", false, true, ref _addressCollectionViaCustomerAddress); } }

		/// <summary>Gets / sets related entity of type 'SalesTerritoryEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual SalesTerritoryEntity SalesTerritory
		{
			get { return _salesTerritory; }
			set { SetSingleRelatedEntityNavigator(value, "SalesTerritory"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum CustomerFieldIndex
	{
		///<summary>AccountNumber. </summary>
		AccountNumber,
		///<summary>CustomerID. </summary>
		CustomerID,
		///<summary>CustomerType. </summary>
		CustomerType,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Rowguid. </summary>
		Rowguid,
		///<summary>TerritoryID. </summary>
		TerritoryID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Customer. </summary>
	public partial class CustomerRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between CustomerEntity and CustomerAddressEntity over the 1:n relation they have, using the relation between the fields: Customer.CustomerID - CustomerAddress.CustomerID</summary>
		public virtual IEntityRelation CustomerAddressEntityUsingCustomerID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "CustomerAddresses", true, new[] { CustomerFields.CustomerID, CustomerAddressFields.CustomerID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between CustomerEntity and SalesOrderHeaderEntity over the 1:n relation they have, using the relation between the fields: Customer.CustomerID - SalesOrderHeader.CustomerID</summary>
		public virtual IEntityRelation SalesOrderHeaderEntityUsingCustomerID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesOrderHeaders", true, new[] { CustomerFields.CustomerID, SalesOrderHeaderFields.CustomerID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between CustomerEntity and SalesTerritoryEntity over the m:1 relation they have, using the relation between the fields: Customer.TerritoryID - SalesTerritory.TerritoryID</summary>
		public virtual IEntityRelation SalesTerritoryEntityUsingTerritoryID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "SalesTerritory", false, new[] { SalesTerritoryFields.TerritoryID, CustomerFields.TerritoryID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between CustomerEntity and IndividualEntity over the 1:1 relation they have, which is used to build a target per entity hierarchy</summary>		
		internal IEntityRelation RelationToSubTypeIndividualEntity
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateHierarchyRelation(true, new[] { CustomerFields.CustomerID, IndividualFields.CustomerID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between CustomerEntity and StoreEntity over the 1:1 relation they have, which is used to build a target per entity hierarchy</summary>		
		internal IEntityRelation RelationToSubTypeStoreEntity
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateHierarchyRelation(true, new[] { CustomerFields.CustomerID, StoreFields.CustomerID }); }
		}

		/// <inheritdoc/>
		public override IEntityRelation GetSubTypeRelation(string subTypeEntityName)
		{
			switch(subTypeEntityName)
			{
				case "IndividualEntity":
					return this.RelationToSubTypeIndividualEntity;
				case "StoreEntity":
					return this.RelationToSubTypeStoreEntity;
				default:
					return null;
			}
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticCustomerRelations
	{
		internal static readonly IEntityRelation CustomerAddressEntityUsingCustomerIDStatic = new CustomerRelations().CustomerAddressEntityUsingCustomerID;
		internal static readonly IEntityRelation SalesOrderHeaderEntityUsingCustomerIDStatic = new CustomerRelations().SalesOrderHeaderEntityUsingCustomerID;
		internal static readonly IEntityRelation SalesTerritoryEntityUsingTerritoryIDStatic = new CustomerRelations().SalesTerritoryEntityUsingTerritoryID;

		/// <summary>CTor</summary>
		static StaticCustomerRelations() { }
	}
}