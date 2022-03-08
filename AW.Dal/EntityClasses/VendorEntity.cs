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
	/// <summary>Entity class which represents the entity 'Vendor'.<br/><br/>MS_Description: Companies from whom Adventure Works Cycles purchases parts or other goods.<br/></summary>
	[Serializable]
	public partial class VendorEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<ProductVendorEntity> _productVendors;
		private EntityCollection<PurchaseOrderHeaderEntity> _purchaseOrderHeaders;
		private EntityCollection<VendorAddressEntity> _vendorAddresses;
		private EntityCollection<VendorContactEntity> _vendorContacts;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static VendorEntityStaticMetaData _staticMetaData = new VendorEntityStaticMetaData();
		private static VendorRelations _relationsFactory = new VendorRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ProductVendors</summary>
			public static readonly string ProductVendors = "ProductVendors";
			/// <summary>Member name PurchaseOrderHeaders</summary>
			public static readonly string PurchaseOrderHeaders = "PurchaseOrderHeaders";
			/// <summary>Member name VendorAddresses</summary>
			public static readonly string VendorAddresses = "VendorAddresses";
			/// <summary>Member name VendorContacts</summary>
			public static readonly string VendorContacts = "VendorContacts";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class VendorEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public VendorEntityStaticMetaData()
			{
				SetEntityCoreInfo("VendorEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.VendorEntity, typeof(VendorEntity), typeof(VendorEntityFactory), false);
				AddNavigatorMetaData<VendorEntity, EntityCollection<ProductVendorEntity>>("ProductVendors", a => a._productVendors, (a, b) => a._productVendors = b, a => a.ProductVendors, () => new VendorRelations().ProductVendorEntityUsingVendorID, typeof(ProductVendorEntity), (int)AW.Dal.EntityType.ProductVendorEntity);
				AddNavigatorMetaData<VendorEntity, EntityCollection<PurchaseOrderHeaderEntity>>("PurchaseOrderHeaders", a => a._purchaseOrderHeaders, (a, b) => a._purchaseOrderHeaders = b, a => a.PurchaseOrderHeaders, () => new VendorRelations().PurchaseOrderHeaderEntityUsingVendorID, typeof(PurchaseOrderHeaderEntity), (int)AW.Dal.EntityType.PurchaseOrderHeaderEntity);
				AddNavigatorMetaData<VendorEntity, EntityCollection<VendorAddressEntity>>("VendorAddresses", a => a._vendorAddresses, (a, b) => a._vendorAddresses = b, a => a.VendorAddresses, () => new VendorRelations().VendorAddressEntityUsingVendorID, typeof(VendorAddressEntity), (int)AW.Dal.EntityType.VendorAddressEntity);
				AddNavigatorMetaData<VendorEntity, EntityCollection<VendorContactEntity>>("VendorContacts", a => a._vendorContacts, (a, b) => a._vendorContacts = b, a => a.VendorContacts, () => new VendorRelations().VendorContactEntityUsingVendorID, typeof(VendorContactEntity), (int)AW.Dal.EntityType.VendorContactEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static VendorEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public VendorEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public VendorEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this VendorEntity</param>
		public VendorEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="vendorID">PK value for Vendor which data should be fetched into this Vendor object</param>
		public VendorEntity(System.Int32 vendorID) : this(vendorID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="vendorID">PK value for Vendor which data should be fetched into this Vendor object</param>
		/// <param name="validator">The custom validator object for this VendorEntity</param>
		public VendorEntity(System.Int32 vendorID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.VendorID = vendorID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected VendorEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductVendor' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductVendors() { return CreateRelationInfoForNavigator("ProductVendors"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'PurchaseOrderHeader' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoPurchaseOrderHeaders() { return CreateRelationInfoForNavigator("PurchaseOrderHeaders"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VendorAddress' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVendorAddresses() { return CreateRelationInfoForNavigator("VendorAddresses"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VendorContact' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVendorContacts() { return CreateRelationInfoForNavigator("VendorContacts"); }
		
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
			_customProperties.Add("MS_Description", @"Companies from whom Adventure Works Cycles purchases parts or other goods.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Vendor account (identification) number.");
			_fieldsCustomProperties.Add("AccountNumber", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"0 = Vendor no longer used. 1 = Vendor is actively used.");
			_fieldsCustomProperties.Add("ActiveFlag", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average");
			_fieldsCustomProperties.Add("CreditRating", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Company name.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.");
			_fieldsCustomProperties.Add("PreferredVendorStatus", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Vendor URL.");
			_fieldsCustomProperties.Add("PurchasingWebServiceUrl", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("VendorID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this VendorEntity</param>
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
		public static VendorRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductVendor' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductVendors { get { return _staticMetaData.GetPrefetchPathElement("ProductVendors", CommonEntityBase.CreateEntityCollection<ProductVendorEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'PurchaseOrderHeader' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathPurchaseOrderHeaders { get { return _staticMetaData.GetPrefetchPathElement("PurchaseOrderHeaders", CommonEntityBase.CreateEntityCollection<PurchaseOrderHeaderEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VendorAddress' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVendorAddresses { get { return _staticMetaData.GetPrefetchPathElement("VendorAddresses", CommonEntityBase.CreateEntityCollection<VendorAddressEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VendorContact' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVendorContacts { get { return _staticMetaData.GetPrefetchPathElement("VendorContacts", CommonEntityBase.CreateEntityCollection<VendorContactEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The AccountNumber property of the Entity Vendor<br/><br/>MS_Description: Vendor account (identification) number.<br/>Vendor account (identification) number.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."AccountNumber".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 15.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String AccountNumber
		{
			get { return (System.String)GetValue((int)VendorFieldIndex.AccountNumber, true); }
			set	{ SetValue((int)VendorFieldIndex.AccountNumber, value); }
		}

		/// <summary>The ActiveFlag property of the Entity Vendor<br/><br/>MS_Description: 0 = Vendor no longer used. 1 = Vendor is actively used.<br/>0 = Vendor no longer used. 1 = Vendor is actively used.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."ActiveFlag".<br/>Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean ActiveFlag
		{
			get { return (System.Boolean)GetValue((int)VendorFieldIndex.ActiveFlag, true); }
			set	{ SetValue((int)VendorFieldIndex.ActiveFlag, value); }
		}

		/// <summary>The CreditRating property of the Entity Vendor<br/><br/>MS_Description: 1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average<br/>1 = Superior, 2 = Excellent, 3 = Above average, 4 = Average, 5 = Below average</summary>
		/// <remarks>Mapped on  table field: "Vendor"."CreditRating".<br/>Table field type characteristics (type, precision, scale, length): TinyInt, 3, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual AW.Data.CreditRating CreditRating
		{
			get { return (AW.Data.CreditRating)GetValue((int)VendorFieldIndex.CreditRating, true); }
			set	{ SetValue((int)VendorFieldIndex.CreditRating, value); }
		}

		/// <summary>The ModifiedDate property of the Entity Vendor<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)VendorFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)VendorFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity Vendor<br/><br/>MS_Description: Company name.<br/>Company name.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)VendorFieldIndex.Name, true); }
			set	{ SetValue((int)VendorFieldIndex.Name, value); }
		}

		/// <summary>The PreferredVendorStatus property of the Entity Vendor<br/><br/>MS_Description: 0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.<br/>0 = Do not use if another vendor is available. 1 = Preferred over other vendors supplying the same product.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."PreferredVendorStatus".<br/>Table field type characteristics (type, precision, scale, length): Bit, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Boolean PreferredVendorStatus
		{
			get { return (System.Boolean)GetValue((int)VendorFieldIndex.PreferredVendorStatus, true); }
			set	{ SetValue((int)VendorFieldIndex.PreferredVendorStatus, value); }
		}

		/// <summary>The PurchasingWebServiceUrl property of the Entity Vendor<br/><br/>MS_Description: Vendor URL.<br/>Vendor URL.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."PurchasingWebServiceURL".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 1024.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		[DataType(DataType.Url)]
		public virtual System.String PurchasingWebServiceUrl
		{
			get { return (System.String)GetValue((int)VendorFieldIndex.PurchasingWebServiceUrl, true); }
			set	{ SetValue((int)VendorFieldIndex.PurchasingWebServiceUrl, value); }
		}

		/// <summary>The VendorID property of the Entity Vendor<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "Vendor"."VendorID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 VendorID
		{
			get { return (System.Int32)GetValue((int)VendorFieldIndex.VendorID, true); }
			set { SetValue((int)VendorFieldIndex.VendorID, value); }		}

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductVendorEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductVendorEntity))]
		public virtual EntityCollection<ProductVendorEntity> ProductVendors { get { return GetOrCreateEntityCollection<ProductVendorEntity, ProductVendorEntityFactory>("Vendor", true, false, ref _productVendors); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'PurchaseOrderHeaderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(PurchaseOrderHeaderEntity))]
		public virtual EntityCollection<PurchaseOrderHeaderEntity> PurchaseOrderHeaders { get { return GetOrCreateEntityCollection<PurchaseOrderHeaderEntity, PurchaseOrderHeaderEntityFactory>("Vendor", true, false, ref _purchaseOrderHeaders); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'VendorAddressEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VendorAddressEntity))]
		public virtual EntityCollection<VendorAddressEntity> VendorAddresses { get { return GetOrCreateEntityCollection<VendorAddressEntity, VendorAddressEntityFactory>("Vendor", true, false, ref _vendorAddresses); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'VendorContactEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VendorContactEntity))]
		public virtual EntityCollection<VendorContactEntity> VendorContacts { get { return GetOrCreateEntityCollection<VendorContactEntity, VendorContactEntityFactory>("Vendor", true, false, ref _vendorContacts); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum VendorFieldIndex
	{
		///<summary>AccountNumber. </summary>
		AccountNumber,
		///<summary>ActiveFlag. </summary>
		ActiveFlag,
		///<summary>CreditRating. </summary>
		CreditRating,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		///<summary>PreferredVendorStatus. </summary>
		PreferredVendorStatus,
		///<summary>PurchasingWebServiceUrl. </summary>
		PurchasingWebServiceUrl,
		///<summary>VendorID. </summary>
		VendorID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: Vendor. </summary>
	public partial class VendorRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between VendorEntity and ProductVendorEntity over the 1:n relation they have, using the relation between the fields: Vendor.VendorID - ProductVendor.VendorID</summary>
		public virtual IEntityRelation ProductVendorEntityUsingVendorID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "ProductVendors", true, new[] { VendorFields.VendorID, ProductVendorFields.VendorID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between VendorEntity and PurchaseOrderHeaderEntity over the 1:n relation they have, using the relation between the fields: Vendor.VendorID - PurchaseOrderHeader.VendorID</summary>
		public virtual IEntityRelation PurchaseOrderHeaderEntityUsingVendorID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "PurchaseOrderHeaders", true, new[] { VendorFields.VendorID, PurchaseOrderHeaderFields.VendorID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between VendorEntity and VendorAddressEntity over the 1:n relation they have, using the relation between the fields: Vendor.VendorID - VendorAddress.VendorID</summary>
		public virtual IEntityRelation VendorAddressEntityUsingVendorID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "VendorAddresses", true, new[] { VendorFields.VendorID, VendorAddressFields.VendorID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between VendorEntity and VendorContactEntity over the 1:n relation they have, using the relation between the fields: Vendor.VendorID - VendorContact.VendorID</summary>
		public virtual IEntityRelation VendorContactEntityUsingVendorID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "VendorContacts", true, new[] { VendorFields.VendorID, VendorContactFields.VendorID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticVendorRelations
	{
		internal static readonly IEntityRelation ProductVendorEntityUsingVendorIDStatic = new VendorRelations().ProductVendorEntityUsingVendorID;
		internal static readonly IEntityRelation PurchaseOrderHeaderEntityUsingVendorIDStatic = new VendorRelations().PurchaseOrderHeaderEntityUsingVendorID;
		internal static readonly IEntityRelation VendorAddressEntityUsingVendorIDStatic = new VendorRelations().VendorAddressEntityUsingVendorID;
		internal static readonly IEntityRelation VendorContactEntityUsingVendorIDStatic = new VendorRelations().VendorContactEntityUsingVendorID;

		/// <summary>CTor</summary>
		static StaticVendorRelations() { }
	}
}