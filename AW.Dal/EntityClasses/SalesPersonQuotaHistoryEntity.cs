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
	/// <summary>Entity class which represents the entity 'SalesPersonQuotaHistory'.<br/><br/>MS_Description: Sales performance tracking.<br/></summary>
	[Serializable]
	public partial class SalesPersonQuotaHistoryEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private SalesPersonEntity _salesPerson;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static SalesPersonQuotaHistoryEntityStaticMetaData _staticMetaData = new SalesPersonQuotaHistoryEntityStaticMetaData();
		private static SalesPersonQuotaHistoryRelations _relationsFactory = new SalesPersonQuotaHistoryRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name SalesPerson</summary>
			public static readonly string SalesPerson = "SalesPerson";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SalesPersonQuotaHistoryEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SalesPersonQuotaHistoryEntityStaticMetaData()
			{
				SetEntityCoreInfo("SalesPersonQuotaHistoryEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.SalesPersonQuotaHistoryEntity, typeof(SalesPersonQuotaHistoryEntity), typeof(SalesPersonQuotaHistoryEntityFactory), false);
				AddNavigatorMetaData<SalesPersonQuotaHistoryEntity, SalesPersonEntity>("SalesPerson", "SalesPersonQuotaHistories", (a, b) => a._salesPerson = b, a => a._salesPerson, (a, b) => a.SalesPerson = b, AW.Dal.RelationClasses.StaticSalesPersonQuotaHistoryRelations.SalesPersonEntityUsingSalesPersonIDStatic, ()=>new SalesPersonQuotaHistoryRelations().SalesPersonEntityUsingSalesPersonID, null, new int[] { (int)SalesPersonQuotaHistoryFieldIndex.SalesPersonID }, null, true, (int)AW.Dal.EntityType.SalesPersonEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static SalesPersonQuotaHistoryEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public SalesPersonQuotaHistoryEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SalesPersonQuotaHistoryEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SalesPersonQuotaHistoryEntity</param>
		public SalesPersonQuotaHistoryEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="quotaDate">PK value for SalesPersonQuotaHistory which data should be fetched into this SalesPersonQuotaHistory object</param>
		/// <param name="salesPersonID">PK value for SalesPersonQuotaHistory which data should be fetched into this SalesPersonQuotaHistory object</param>
		public SalesPersonQuotaHistoryEntity(System.DateTime quotaDate, System.Int32 salesPersonID) : this(quotaDate, salesPersonID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="quotaDate">PK value for SalesPersonQuotaHistory which data should be fetched into this SalesPersonQuotaHistory object</param>
		/// <param name="salesPersonID">PK value for SalesPersonQuotaHistory which data should be fetched into this SalesPersonQuotaHistory object</param>
		/// <param name="validator">The custom validator object for this SalesPersonQuotaHistoryEntity</param>
		public SalesPersonQuotaHistoryEntity(System.DateTime quotaDate, System.Int32 salesPersonID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.QuotaDate = quotaDate;
			this.SalesPersonID = salesPersonID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SalesPersonQuotaHistoryEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'SalesPerson' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesPerson() { return CreateRelationInfoForNavigator("SalesPerson"); }
		
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
			_customProperties.Add("MS_Description", @"Sales performance tracking.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales quota date.");
			_fieldsCustomProperties.Add("QuotaDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("SalesPersonID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales quota amount.");
			_fieldsCustomProperties.Add("SalesQuota", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this SalesPersonQuotaHistoryEntity</param>
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
		public static SalesPersonQuotaHistoryRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesPerson' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesPerson { get { return _staticMetaData.GetPrefetchPathElement("SalesPerson", CommonEntityBase.CreateEntityCollection<SalesPersonEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The ModifiedDate property of the Entity SalesPersonQuotaHistory<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "SalesPersonQuotaHistory"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)SalesPersonQuotaHistoryFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)SalesPersonQuotaHistoryFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The QuotaDate property of the Entity SalesPersonQuotaHistory<br/><br/>MS_Description: Sales quota date.<br/>Sales quota date.</summary>
		/// <remarks>Mapped on  table field: "SalesPersonQuotaHistory"."QuotaDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.DateTime QuotaDate
		{
			get { return (System.DateTime)GetValue((int)SalesPersonQuotaHistoryFieldIndex.QuotaDate, true); }
			set	{ SetValue((int)SalesPersonQuotaHistoryFieldIndex.QuotaDate, value); }
		}

		/// <summary>The Rowguid property of the Entity SalesPersonQuotaHistory<br/><br/>MS_Description: ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.<br/>ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.</summary>
		/// <remarks>Mapped on  table field: "SalesPersonQuotaHistory"."rowguid".<br/>Table field type characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Guid Rowguid
		{
			get { return (System.Guid)GetValue((int)SalesPersonQuotaHistoryFieldIndex.Rowguid, true); }
			set	{ SetValue((int)SalesPersonQuotaHistoryFieldIndex.Rowguid, value); }
		}

		/// <summary>The SalesPersonID property of the Entity SalesPersonQuotaHistory<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "SalesPersonQuotaHistory"."SalesPersonID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Int32 SalesPersonID
		{
			get { return (System.Int32)GetValue((int)SalesPersonQuotaHistoryFieldIndex.SalesPersonID, true); }
			set	{ SetValue((int)SalesPersonQuotaHistoryFieldIndex.SalesPersonID, value); }
		}

		/// <summary>The SalesQuota property of the Entity SalesPersonQuotaHistory<br/><br/>MS_Description: Sales quota amount.<br/>Sales quota amount.</summary>
		/// <remarks>Mapped on  table field: "SalesPersonQuotaHistory"."SalesQuota".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal SalesQuota
		{
			get { return (System.Decimal)GetValue((int)SalesPersonQuotaHistoryFieldIndex.SalesQuota, true); }
			set	{ SetValue((int)SalesPersonQuotaHistoryFieldIndex.SalesQuota, value); }
		}

		/// <summary>Gets / sets related entity of type 'SalesPersonEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual SalesPersonEntity SalesPerson
		{
			get { return _salesPerson; }
			set { SetSingleRelatedEntityNavigator(value, "SalesPerson"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum SalesPersonQuotaHistoryFieldIndex
	{
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>QuotaDate. </summary>
		QuotaDate,
		///<summary>Rowguid. </summary>
		Rowguid,
		///<summary>SalesPersonID. </summary>
		SalesPersonID,
		///<summary>SalesQuota. </summary>
		SalesQuota,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: SalesPersonQuotaHistory. </summary>
	public partial class SalesPersonQuotaHistoryRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between SalesPersonQuotaHistoryEntity and SalesPersonEntity over the m:1 relation they have, using the relation between the fields: SalesPersonQuotaHistory.SalesPersonID - SalesPerson.EmployeeID</summary>
		public virtual IEntityRelation SalesPersonEntityUsingSalesPersonID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "SalesPerson", false, new[] { SalesPersonFields.EmployeeID, SalesPersonQuotaHistoryFields.SalesPersonID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSalesPersonQuotaHistoryRelations
	{
		internal static readonly IEntityRelation SalesPersonEntityUsingSalesPersonIDStatic = new SalesPersonQuotaHistoryRelations().SalesPersonEntityUsingSalesPersonID;

		/// <summary>CTor</summary>
		static StaticSalesPersonQuotaHistoryRelations() { }
	}
}