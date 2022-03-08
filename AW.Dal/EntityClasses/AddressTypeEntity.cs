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
	/// <summary>Entity class which represents the entity 'AddressType'.<br/><br/>MS_Description: Types of addresses stored in the Address table. <br/></summary>
	[Serializable]
	public partial class AddressTypeEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<VendorAddressEntity> _vendorAddresses;
		private EntityCollection<CustomerAddressEntity> _customerAddresses;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static AddressTypeEntityStaticMetaData _staticMetaData = new AddressTypeEntityStaticMetaData();
		private static AddressTypeRelations _relationsFactory = new AddressTypeRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name VendorAddresses</summary>
			public static readonly string VendorAddresses = "VendorAddresses";
			/// <summary>Member name CustomerAddresses</summary>
			public static readonly string CustomerAddresses = "CustomerAddresses";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class AddressTypeEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public AddressTypeEntityStaticMetaData()
			{
				SetEntityCoreInfo("AddressTypeEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.AddressTypeEntity, typeof(AddressTypeEntity), typeof(AddressTypeEntityFactory), false);
				AddNavigatorMetaData<AddressTypeEntity, EntityCollection<VendorAddressEntity>>("VendorAddresses", a => a._vendorAddresses, (a, b) => a._vendorAddresses = b, a => a.VendorAddresses, () => new AddressTypeRelations().VendorAddressEntityUsingAddressTypeID, typeof(VendorAddressEntity), (int)AW.Dal.EntityType.VendorAddressEntity);
				AddNavigatorMetaData<AddressTypeEntity, EntityCollection<CustomerAddressEntity>>("CustomerAddresses", a => a._customerAddresses, (a, b) => a._customerAddresses = b, a => a.CustomerAddresses, () => new AddressTypeRelations().CustomerAddressEntityUsingAddressTypeID, typeof(CustomerAddressEntity), (int)AW.Dal.EntityType.CustomerAddressEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static AddressTypeEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public AddressTypeEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public AddressTypeEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this AddressTypeEntity</param>
		public AddressTypeEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="addressTypeID">PK value for AddressType which data should be fetched into this AddressType object</param>
		public AddressTypeEntity(AW.Data.AddressType addressTypeID) : this(addressTypeID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="addressTypeID">PK value for AddressType which data should be fetched into this AddressType object</param>
		/// <param name="validator">The custom validator object for this AddressTypeEntity</param>
		public AddressTypeEntity(AW.Data.AddressType addressTypeID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.AddressTypeID = addressTypeID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected AddressTypeEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'VendorAddress' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoVendorAddresses() { return CreateRelationInfoForNavigator("VendorAddresses"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'CustomerAddress' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoCustomerAddresses() { return CreateRelationInfoForNavigator("CustomerAddresses"); }
		
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
			_customProperties.Add("MS_Description", @"Types of addresses stored in the Address table. ");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("AddressTypeID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Address type description. For example, Billing, Home, or Shipping.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Unique nonclustered index.");
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this AddressTypeEntity</param>
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
		public static AddressTypeRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'VendorAddress' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathVendorAddresses { get { return _staticMetaData.GetPrefetchPathElement("VendorAddresses", CommonEntityBase.CreateEntityCollection<VendorAddressEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'CustomerAddress' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathCustomerAddresses { get { return _staticMetaData.GetPrefetchPathElement("CustomerAddresses", CommonEntityBase.CreateEntityCollection<CustomerAddressEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The AddressTypeID property of the Entity AddressType<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "AddressType"."AddressTypeID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual AW.Data.AddressType AddressTypeID
		{
			get { return (AW.Data.AddressType)GetValue((int)AddressTypeFieldIndex.AddressTypeID, true); }
			set { SetValue((int)AddressTypeFieldIndex.AddressTypeID, value); }		}

		/// <summary>The ModifiedDate property of the Entity AddressType<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "AddressType"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)AddressTypeFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)AddressTypeFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity AddressType<br/><br/>MS_Description: Address type description. For example, Billing, Home, or Shipping.<br/>Address type description. For example, Billing, Home, or Shipping.</summary>
		/// <remarks>Mapped on  table field: "AddressType"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)AddressTypeFieldIndex.Name, true); }
			set	{ SetValue((int)AddressTypeFieldIndex.Name, value); }
		}

		/// <summary>The Rowguid property of the Entity AddressType<br/><br/>MS_Description: Unique nonclustered index.<br/>Unique nonclustered index.</summary>
		/// <remarks>Mapped on  table field: "AddressType"."rowguid".<br/>Table field type characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Guid Rowguid
		{
			get { return (System.Guid)GetValue((int)AddressTypeFieldIndex.Rowguid, true); }
			set	{ SetValue((int)AddressTypeFieldIndex.Rowguid, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'VendorAddressEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(VendorAddressEntity))]
		public virtual EntityCollection<VendorAddressEntity> VendorAddresses { get { return GetOrCreateEntityCollection<VendorAddressEntity, VendorAddressEntityFactory>("AddressType", true, false, ref _vendorAddresses); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'CustomerAddressEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(CustomerAddressEntity))]
		public virtual EntityCollection<CustomerAddressEntity> CustomerAddresses { get { return GetOrCreateEntityCollection<CustomerAddressEntity, CustomerAddressEntityFactory>("AddressType", true, false, ref _customerAddresses); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum AddressTypeFieldIndex
	{
		///<summary>AddressTypeID. </summary>
		AddressTypeID,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		///<summary>Rowguid. </summary>
		Rowguid,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: AddressType. </summary>
	public partial class AddressTypeRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between AddressTypeEntity and VendorAddressEntity over the 1:n relation they have, using the relation between the fields: AddressType.AddressTypeID - VendorAddress.AddressTypeID</summary>
		public virtual IEntityRelation VendorAddressEntityUsingAddressTypeID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "VendorAddresses", true, new[] { AddressTypeFields.AddressTypeID, VendorAddressFields.AddressTypeID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between AddressTypeEntity and CustomerAddressEntity over the 1:n relation they have, using the relation between the fields: AddressType.AddressTypeID - CustomerAddress.AddressTypeID</summary>
		public virtual IEntityRelation CustomerAddressEntityUsingAddressTypeID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "CustomerAddresses", true, new[] { AddressTypeFields.AddressTypeID, CustomerAddressFields.AddressTypeID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticAddressTypeRelations
	{
		internal static readonly IEntityRelation VendorAddressEntityUsingAddressTypeIDStatic = new AddressTypeRelations().VendorAddressEntityUsingAddressTypeID;
		internal static readonly IEntityRelation CustomerAddressEntityUsingAddressTypeIDStatic = new AddressTypeRelations().CustomerAddressEntityUsingAddressTypeID;

		/// <summary>CTor</summary>
		static StaticAddressTypeRelations() { }
	}
}