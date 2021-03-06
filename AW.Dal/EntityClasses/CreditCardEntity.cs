//////////////////////////////////////////////////////////////
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
	/// <summary>Entity class which represents the entity 'CreditCard'.<br/><br/>MS_Description: Customer credit card information.<br/></summary>
	[Serializable]
	public partial class CreditCardEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<ContactCreditCardEntity> _contactCreditCards;
		private EntityCollection<SalesOrderHeaderEntity> _salesOrderHeaders;
		private EntityCollection<ContactEntity> _contactCollectionViaContactCreditCard;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static CreditCardEntityStaticMetaData _staticMetaData = new CreditCardEntityStaticMetaData();
		private static CreditCardRelations _relationsFactory = new CreditCardRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ContactCreditCards</summary>
			public static readonly string ContactCreditCards = "ContactCreditCards";
			/// <summary>Member name SalesOrderHeaders</summary>
			public static readonly string SalesOrderHeaders = "SalesOrderHeaders";
			/// <summary>Member name ContactCollectionViaContactCreditCard</summary>
			public static readonly string ContactCollectionViaContactCreditCard = "ContactCollectionViaContactCreditCard";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class CreditCardEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public CreditCardEntityStaticMetaData()
			{
				SetEntityCoreInfo("CreditCardEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.CreditCardEntity, typeof(CreditCardEntity), typeof(CreditCardEntityFactory), false);
				AddNavigatorMetaData<CreditCardEntity, EntityCollection<ContactCreditCardEntity>>("ContactCreditCards", a => a._contactCreditCards, (a, b) => a._contactCreditCards = b, a => a.ContactCreditCards, () => new CreditCardRelations().ContactCreditCardEntityUsingCreditCardID, typeof(ContactCreditCardEntity), (int)AW.Dal.EntityType.ContactCreditCardEntity);
				AddNavigatorMetaData<CreditCardEntity, EntityCollection<SalesOrderHeaderEntity>>("SalesOrderHeaders", a => a._salesOrderHeaders, (a, b) => a._salesOrderHeaders = b, a => a.SalesOrderHeaders, () => new CreditCardRelations().SalesOrderHeaderEntityUsingCreditCardID, typeof(SalesOrderHeaderEntity), (int)AW.Dal.EntityType.SalesOrderHeaderEntity);
				AddNavigatorMetaData<CreditCardEntity, EntityCollection<ContactEntity>>("ContactCollectionViaContactCreditCard", a => a._contactCollectionViaContactCreditCard, (a, b) => a._contactCollectionViaContactCreditCard = b, a => a.ContactCollectionViaContactCreditCard, () => new CreditCardRelations().ContactCreditCardEntityUsingCreditCardID, () => new ContactCreditCardRelations().ContactEntityUsingContactID, "CreditCardEntity__", "ContactCreditCard_", typeof(ContactEntity), (int)AW.Dal.EntityType.ContactEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static CreditCardEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public CreditCardEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public CreditCardEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this CreditCardEntity</param>
		public CreditCardEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="creditCardID">PK value for CreditCard which data should be fetched into this CreditCard object</param>
		public CreditCardEntity(System.Int32 creditCardID) : this(creditCardID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="creditCardID">PK value for CreditCard which data should be fetched into this CreditCard object</param>
		/// <param name="validator">The custom validator object for this CreditCardEntity</param>
		public CreditCardEntity(System.Int32 creditCardID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.CreditCardID = creditCardID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected CreditCardEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ContactCreditCard' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoContactCreditCards() { return CreateRelationInfoForNavigator("ContactCreditCards"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'SalesOrderHeader' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoSalesOrderHeaders() { return CreateRelationInfoForNavigator("SalesOrderHeaders"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Contact' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoContactCollectionViaContactCreditCard() { return CreateRelationInfoForNavigator("ContactCollectionViaContactCreditCard"); }
		
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
			_customProperties.Add("MS_Description", @"Customer credit card information.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Credit card number.");
			_fieldsCustomProperties.Add("CardNumber", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Credit card name.");
			_fieldsCustomProperties.Add("CardType", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("CreditCardID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Credit card expiration month.");
			_fieldsCustomProperties.Add("ExpMonth", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Credit card expiration year.");
			_fieldsCustomProperties.Add("ExpYear", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this CreditCardEntity</param>
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
		public static CreditCardRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ContactCreditCard' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathContactCreditCards { get { return _staticMetaData.GetPrefetchPathElement("ContactCreditCards", CommonEntityBase.CreateEntityCollection<ContactCreditCardEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'SalesOrderHeader' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathSalesOrderHeaders { get { return _staticMetaData.GetPrefetchPathElement("SalesOrderHeaders", CommonEntityBase.CreateEntityCollection<SalesOrderHeaderEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Contact' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathContactCollectionViaContactCreditCard { get { return _staticMetaData.GetPrefetchPathElement("ContactCollectionViaContactCreditCard", CommonEntityBase.CreateEntityCollection<ContactEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The CardNumber property of the Entity CreditCard<br/><br/>MS_Description: Credit card number.<br/>Credit card number.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."CardNumber".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 25.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String CardNumber
		{
			get { return (System.String)GetValue((int)CreditCardFieldIndex.CardNumber, true); }
			set	{ SetValue((int)CreditCardFieldIndex.CardNumber, value); }
		}

		/// <summary>The CardType property of the Entity CreditCard<br/><br/>MS_Description: Credit card name.<br/>Credit card name.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."CardType".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String CardType
		{
			get { return (System.String)GetValue((int)CreditCardFieldIndex.CardType, true); }
			set	{ SetValue((int)CreditCardFieldIndex.CardType, value); }
		}

		/// <summary>The CreditCardID property of the Entity CreditCard<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."CreditCardID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 CreditCardID
		{
			get { return (System.Int32)GetValue((int)CreditCardFieldIndex.CreditCardID, true); }
			set { SetValue((int)CreditCardFieldIndex.CreditCardID, value); }		}

		/// <summary>The ExpMonth property of the Entity CreditCard<br/><br/>MS_Description: Credit card expiration month.<br/>Credit card expiration month.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."ExpMonth".<br/>Table field type characteristics (type, precision, scale, length): TinyInt, 3, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Byte ExpMonth
		{
			get { return (System.Byte)GetValue((int)CreditCardFieldIndex.ExpMonth, true); }
			set	{ SetValue((int)CreditCardFieldIndex.ExpMonth, value); }
		}

		/// <summary>The ExpYear property of the Entity CreditCard<br/><br/>MS_Description: Credit card expiration year.<br/>Credit card expiration year.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."ExpYear".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int16 ExpYear
		{
			get { return (System.Int16)GetValue((int)CreditCardFieldIndex.ExpYear, true); }
			set	{ SetValue((int)CreditCardFieldIndex.ExpYear, value); }
		}

		/// <summary>The ModifiedDate property of the Entity CreditCard<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "CreditCard"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)CreditCardFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)CreditCardFieldIndex.ModifiedDate, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'ContactCreditCardEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ContactCreditCardEntity))]
		public virtual EntityCollection<ContactCreditCardEntity> ContactCreditCards { get { return GetOrCreateEntityCollection<ContactCreditCardEntity, ContactCreditCardEntityFactory>("CreditCard", true, false, ref _contactCreditCards); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'SalesOrderHeaderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(SalesOrderHeaderEntity))]
		public virtual EntityCollection<SalesOrderHeaderEntity> SalesOrderHeaders { get { return GetOrCreateEntityCollection<SalesOrderHeaderEntity, SalesOrderHeaderEntityFactory>("CreditCard", true, false, ref _salesOrderHeaders); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'ContactEntity' which are related to this entity via a relation of type 'm:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ContactEntity))]
		public virtual EntityCollection<ContactEntity> ContactCollectionViaContactCreditCard { get { return GetOrCreateEntityCollection<ContactEntity, ContactEntityFactory>("CreditCards", false, true, ref _contactCollectionViaContactCreditCard); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum CreditCardFieldIndex
	{
		///<summary>CardNumber. </summary>
		CardNumber,
		///<summary>CardType. </summary>
		CardType,
		///<summary>CreditCardID. </summary>
		CreditCardID,
		///<summary>ExpMonth. </summary>
		ExpMonth,
		///<summary>ExpYear. </summary>
		ExpYear,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: CreditCard. </summary>
	public partial class CreditCardRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between CreditCardEntity and ContactCreditCardEntity over the 1:n relation they have, using the relation between the fields: CreditCard.CreditCardID - ContactCreditCard.CreditCardID</summary>
		public virtual IEntityRelation ContactCreditCardEntityUsingCreditCardID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "ContactCreditCards", true, new[] { CreditCardFields.CreditCardID, ContactCreditCardFields.CreditCardID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between CreditCardEntity and SalesOrderHeaderEntity over the 1:n relation they have, using the relation between the fields: CreditCard.CreditCardID - SalesOrderHeader.CreditCardID</summary>
		public virtual IEntityRelation SalesOrderHeaderEntityUsingCreditCardID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "SalesOrderHeaders", true, new[] { CreditCardFields.CreditCardID, SalesOrderHeaderFields.CreditCardID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticCreditCardRelations
	{
		internal static readonly IEntityRelation ContactCreditCardEntityUsingCreditCardIDStatic = new CreditCardRelations().ContactCreditCardEntityUsingCreditCardID;
		internal static readonly IEntityRelation SalesOrderHeaderEntityUsingCreditCardIDStatic = new CreditCardRelations().SalesOrderHeaderEntityUsingCreditCardID;

		/// <summary>CTor</summary>
		static StaticCreditCardRelations() { }
	}
}
