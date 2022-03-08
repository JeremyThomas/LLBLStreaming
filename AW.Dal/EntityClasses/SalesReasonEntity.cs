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
	/// <summary>Entity class which represents the entity 'SalesReason'.<br/><br/>MS_Description: Lookup table of customer purchase reasons.<br/></summary>
	[Serializable]
	public partial class SalesReasonEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<SalesOrderHeaderSalesReasonEntity> _salesOrderHeaderSalesReasons;
		private EntityCollection<SalesOrderHeaderEntity> _salesOrders;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static SalesReasonEntityStaticMetaData _staticMetaData = new SalesReasonEntityStaticMetaData();
		private static SalesReasonRelations _relationsFactory = new SalesReasonRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name SalesOrderHeaderSalesReasons</summary>
			public static readonly string SalesOrderHeaderSalesReasons = "SalesOrderHeaderSalesReasons";
			/// <summary>Member name SalesOrders</summary>
			public static readonly string SalesOrders = "SalesOrders";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class SalesReasonEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public SalesReasonEntityStaticMetaData()
			{
				SetEntityCoreInfo("SalesReasonEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.SalesReasonEntity, typeof(SalesReasonEntity), typeof(SalesReasonEntityFactory), false);
				AddNavigatorMetaData<SalesReasonEntity, EntityCollection<SalesOrderHeaderSalesReasonEntity>>("SalesOrderHeaderSalesReasons", a => a._salesOrderHeaderSalesReasons, (a, b) => a._salesOrderHeaderSalesReasons = b, a => a.SalesOrderHeaderSalesReasons, () => new SalesReasonRelations().SalesOrderHeaderSalesReasonEntityUsingSalesReasonID, typeof(SalesOrderHeaderSalesReasonEntity), (int)AW.Dal.EntityType.SalesOrderHeaderSalesReasonEntity);
				AddNavigatorMetaData<SalesReasonEntity, EntityCollection<SalesOrderHeaderEntity>>("SalesOrders", a => a._salesOrders, (a, b) => a._salesOrders = b, a => a.SalesOrders, () => new SalesReasonRelations().SalesOrderHeaderSalesReasonEntityUsingSalesReasonID, () => new SalesOrderHeaderSalesReasonRelations().SalesOrderHeaderEntityUsingSalesOrderID, "SalesReasonEntity__", "SalesOrderHeaderSalesReason_", typeof(SalesOrderHeaderEntity), (int)AW.Dal.EntityType.SalesOrderHeaderEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static SalesReasonEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public SalesReasonEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public SalesReasonEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this SalesReasonEntity</param>
		public SalesReasonEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="salesReasonID">PK value for SalesReason which data should be fetched into this SalesReason object</param>
		public SalesReasonEntity(System.Int32 salesReasonID) : this(salesReasonID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="salesReasonID">PK value for SalesReason which data should be fetched into this SalesReason object</param>
		/// <param name="validator">The custom validator object for this SalesReasonEntity</param>
		public SalesReasonEntity(System.Int32 salesReasonID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.SalesReasonID = salesReasonID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected SalesReasonEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesOrderHeaderSalesReason' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesOrderHeaderSalesReasons() { return CreateRelationInfoForNavigator("SalesOrderHeaderSalesReasons"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesOrderHeader' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesOrders() { return CreateRelationInfoForNavigator("SalesOrders"); }
		
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
			_customProperties.Add("MS_Description", @"Lookup table of customer purchase reasons.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Sales reason description.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Category the sales reason belongs to.");
			_fieldsCustomProperties.Add("ReasonType", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("SalesReasonID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this SalesReasonEntity</param>
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
		public static SalesReasonRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesOrderHeaderSalesReason' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesOrderHeaderSalesReasons { get { return _staticMetaData.GetPrefetchPathElement("SalesOrderHeaderSalesReasons", CommonEntityBase.CreateEntityCollection<SalesOrderHeaderSalesReasonEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesOrderHeader' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesOrders { get { return _staticMetaData.GetPrefetchPathElement("SalesOrders", CommonEntityBase.CreateEntityCollection<SalesOrderHeaderEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The ModifiedDate property of the Entity SalesReason<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "SalesReason"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)SalesReasonFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)SalesReasonFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity SalesReason<br/><br/>MS_Description: Sales reason description.<br/>Sales reason description.</summary>
		/// <remarks>Mapped on  table field: "SalesReason"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)SalesReasonFieldIndex.Name, true); }
			set	{ SetValue((int)SalesReasonFieldIndex.Name, value); }
		}

		/// <summary>The ReasonType property of the Entity SalesReason<br/><br/>MS_Description: Category the sales reason belongs to.<br/>Category the sales reason belongs to.</summary>
		/// <remarks>Mapped on  table field: "SalesReason"."ReasonType".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String ReasonType
		{
			get { return (System.String)GetValue((int)SalesReasonFieldIndex.ReasonType, true); }
			set	{ SetValue((int)SalesReasonFieldIndex.ReasonType, value); }
		}

		/// <summary>The SalesReasonID property of the Entity SalesReason<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "SalesReason"."SalesReasonID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 SalesReasonID
		{
			get { return (System.Int32)GetValue((int)SalesReasonFieldIndex.SalesReasonID, true); }
			set { SetValue((int)SalesReasonFieldIndex.SalesReasonID, value); }		}

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesOrderHeaderSalesReasonEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesOrderHeaderSalesReasonEntity))]
		public virtual EntityCollection<SalesOrderHeaderSalesReasonEntity> SalesOrderHeaderSalesReasons { get { return GetOrCreateEntityCollection<SalesOrderHeaderSalesReasonEntity, SalesOrderHeaderSalesReasonEntityFactory>("SalesReason", true, false, ref _salesOrderHeaderSalesReasons); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesOrderHeaderEntity' which are related to this entity via a relation of type 'm:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesOrderHeaderEntity))]
		public virtual EntityCollection<SalesOrderHeaderEntity> SalesOrders { get { return GetOrCreateEntityCollection<SalesOrderHeaderEntity, SalesOrderHeaderEntityFactory>("SalesReasons", false, true, ref _salesOrders); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum SalesReasonFieldIndex
	{
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		///<summary>ReasonType. </summary>
		ReasonType,
		///<summary>SalesReasonID. </summary>
		SalesReasonID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: SalesReason. </summary>
	public partial class SalesReasonRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between SalesReasonEntity and SalesOrderHeaderSalesReasonEntity over the 1:n relation they have, using the relation between the fields: SalesReason.SalesReasonID - SalesOrderHeaderSalesReason.SalesReasonID</summary>
		public virtual IEntityRelation SalesOrderHeaderSalesReasonEntityUsingSalesReasonID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesOrderHeaderSalesReasons", true, new[] { SalesReasonFields.SalesReasonID, SalesOrderHeaderSalesReasonFields.SalesReasonID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticSalesReasonRelations
	{
		internal static readonly IEntityRelation SalesOrderHeaderSalesReasonEntityUsingSalesReasonIDStatic = new SalesReasonRelations().SalesOrderHeaderSalesReasonEntityUsingSalesReasonID;

		/// <summary>CTor</summary>
		static StaticSalesReasonRelations() { }
	}
}
