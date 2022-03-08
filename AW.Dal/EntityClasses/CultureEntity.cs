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
	/// <summary>Entity class which represents the entity 'Culture'.<br/><br/>MS_Description: Lookup table containing the languages in which some AdventureWorks data is stored.<br/></summary>
	[Serializable]
	public partial class CultureEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<ProductModelProductDescriptionCultureEntity> _productModelProductDescriptionCultures;
		private EntityCollection<ProductDescriptionEntity> _productDescriptions;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static CultureEntityStaticMetaData _staticMetaData = new CultureEntityStaticMetaData();
		private static CultureRelations _relationsFactory = new CultureRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ProductModelProductDescriptionCultures</summary>
			public static readonly string ProductModelProductDescriptionCultures = "ProductModelProductDescriptionCultures";
			/// <summary>Member name ProductDescriptions</summary>
			public static readonly string ProductDescriptions = "ProductDescriptions";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class CultureEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public CultureEntityStaticMetaData()
			{
				SetEntityCoreInfo("CultureEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.CultureEntity, typeof(CultureEntity), typeof(CultureEntityFactory), false);
				AddNavigatorMetaData<CultureEntity, EntityCollection<ProductModelProductDescriptionCultureEntity>>("ProductModelProductDescriptionCultures", a => a._productModelProductDescriptionCultures, (a, b) => a._productModelProductDescriptionCultures = b, a => a.ProductModelProductDescriptionCultures, () => new CultureRelations().ProductModelProductDescriptionCultureEntityUsingCultureID, typeof(ProductModelProductDescriptionCultureEntity), (int)AW.Dal.EntityType.ProductModelProductDescriptionCultureEntity);
				AddNavigatorMetaData<CultureEntity, EntityCollection<ProductDescriptionEntity>>("ProductDescriptions", a => a._productDescriptions, (a, b) => a._productDescriptions = b, a => a.ProductDescriptions, () => new CultureRelations().ProductModelProductDescriptionCultureEntityUsingCultureID, () => new ProductModelProductDescriptionCultureRelations().ProductDescriptionEntityUsingProductDescriptionID, "CultureEntity__", "ProductModelProductDescriptionCulture_", typeof(ProductDescriptionEntity), (int)AW.Dal.EntityType.ProductDescriptionEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static CultureEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public CultureEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public CultureEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this CultureEntity</param>
		public CultureEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="cultureID">PK value for Culture which data should be fetched into this Culture object</param>
		public CultureEntity(System.String cultureID) : this(cultureID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="cultureID">PK value for Culture which data should be fetched into this Culture object</param>
		/// <param name="validator">The custom validator object for this CultureEntity</param>
		public CultureEntity(System.String cultureID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.CultureID = cultureID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected CultureEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductModelProductDescriptionCulture' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductModelProductDescriptionCultures() { return CreateRelationInfoForNavigator("ProductModelProductDescriptionCultures"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductDescription' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductDescriptions() { return CreateRelationInfoForNavigator("ProductDescriptions"); }
		
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
			_customProperties.Add("MS_Description", @"Lookup table containing the languages in which some AdventureWorks data is stored.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("CultureID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Culture description.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this CultureEntity</param>
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
		public static CultureRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductModelProductDescriptionCulture' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductModelProductDescriptionCultures { get { return _staticMetaData.GetPrefetchPathElement("ProductModelProductDescriptionCultures", CommonEntityBase.CreateEntityCollection<ProductModelProductDescriptionCultureEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductDescription' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductDescriptions { get { return _staticMetaData.GetPrefetchPathElement("ProductDescriptions", CommonEntityBase.CreateEntityCollection<ProductDescriptionEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The CultureID property of the Entity Culture<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "Culture"."CultureID".<br/>Table field type characteristics (type, precision, scale, length): NChar, 0, 0, 6.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.String CultureID
		{
			get { return (System.String)GetValue((int)CultureFieldIndex.CultureID, true); }
			set	{ SetValue((int)CultureFieldIndex.CultureID, value); }
		}

		/// <summary>The ModifiedDate property of the Entity Culture<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "Culture"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)CultureFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)CultureFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity Culture<br/><br/>MS_Description: Culture description.<br/>Culture description.</summary>
		/// <remarks>Mapped on  table field: "Culture"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)CultureFieldIndex.Name, true); }
			set	{ SetValue((int)CultureFieldIndex.Name, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductModelProductDescriptionCultureEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductModelProductDescriptionCultureEntity))]
		public virtual EntityCollection<ProductModelProductDescriptionCultureEntity> ProductModelProductDescriptionCultures { get { return GetOrCreateEntityCollection<ProductModelProductDescriptionCultureEntity, ProductModelProductDescriptionCultureEntityFactory>("Culture", true, false, ref _productModelProductDescriptionCultures); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductDescriptionEntity' which are related to this entity via a relation of type 'm:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductDescriptionEntity))]
		public virtual EntityCollection<ProductDescriptionEntity> ProductDescriptions { get { return GetOrCreateEntityCollection<ProductDescriptionEntity, ProductDescriptionEntityFactory>("CultureCollectionViaProductModelProductDescriptionCulture", false, true, ref _productDescriptions); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum CultureFieldIndex
	{
		///<summary>CultureID. </summary>
		CultureID,
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
	/// <summary>Implements the relations factory for the entity: Culture. </summary>
	public partial class CultureRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between CultureEntity and ProductModelProductDescriptionCultureEntity over the 1:n relation they have, using the relation between the fields: Culture.CultureID - ProductModelProductDescriptionCulture.CultureID</summary>
		public virtual IEntityRelation ProductModelProductDescriptionCultureEntityUsingCultureID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "ProductModelProductDescriptionCultures", true, new[] { CultureFields.CultureID, ProductModelProductDescriptionCultureFields.CultureID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticCultureRelations
	{
		internal static readonly IEntityRelation ProductModelProductDescriptionCultureEntityUsingCultureIDStatic = new CultureRelations().ProductModelProductDescriptionCultureEntityUsingCultureID;

		/// <summary>CTor</summary>
		static StaticCultureRelations() { }
	}
}
