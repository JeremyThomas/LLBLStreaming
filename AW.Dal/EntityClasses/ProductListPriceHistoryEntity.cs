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
	/// <summary>Entity class which represents the entity 'ProductListPriceHistory'.<br/><br/>MS_Description: Changes in the list price of a product over time.<br/></summary>
	[Serializable]
	public partial class ProductListPriceHistoryEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private ProductEntity _product;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static ProductListPriceHistoryEntityStaticMetaData _staticMetaData = new ProductListPriceHistoryEntityStaticMetaData();
		private static ProductListPriceHistoryRelations _relationsFactory = new ProductListPriceHistoryRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Product</summary>
			public static readonly string Product = "Product";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class ProductListPriceHistoryEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public ProductListPriceHistoryEntityStaticMetaData()
			{
				SetEntityCoreInfo("ProductListPriceHistoryEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.ProductListPriceHistoryEntity, typeof(ProductListPriceHistoryEntity), typeof(ProductListPriceHistoryEntityFactory), false);
				AddNavigatorMetaData<ProductListPriceHistoryEntity, ProductEntity>("Product", "ProductListPriceHistories", (a, b) => a._product = b, a => a._product, (a, b) => a.Product = b, AW.Dal.RelationClasses.StaticProductListPriceHistoryRelations.ProductEntityUsingProductIDStatic, ()=>new ProductListPriceHistoryRelations().ProductEntityUsingProductID, null, new int[] { (int)ProductListPriceHistoryFieldIndex.ProductID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static ProductListPriceHistoryEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public ProductListPriceHistoryEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ProductListPriceHistoryEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ProductListPriceHistoryEntity</param>
		public ProductListPriceHistoryEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="productID">PK value for ProductListPriceHistory which data should be fetched into this ProductListPriceHistory object</param>
		/// <param name="startDate">PK value for ProductListPriceHistory which data should be fetched into this ProductListPriceHistory object</param>
		public ProductListPriceHistoryEntity(System.Int32 productID, System.DateTime startDate) : this(productID, startDate, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="productID">PK value for ProductListPriceHistory which data should be fetched into this ProductListPriceHistory object</param>
		/// <param name="startDate">PK value for ProductListPriceHistory which data should be fetched into this ProductListPriceHistory object</param>
		/// <param name="validator">The custom validator object for this ProductListPriceHistoryEntity</param>
		public ProductListPriceHistoryEntity(System.Int32 productID, System.DateTime startDate, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.ProductID = productID;
			this.StartDate = startDate;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ProductListPriceHistoryEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProduct() { return CreateRelationInfoForNavigator("Product"); }
		
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
			_customProperties.Add("MS_Description", @"Changes in the list price of a product over time.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"List price end date");
			_fieldsCustomProperties.Add("EndDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Product list price.");
			_fieldsCustomProperties.Add("ListPrice", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("ProductID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"List price start date.");
			_fieldsCustomProperties.Add("StartDate", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ProductListPriceHistoryEntity</param>
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
		public static ProductListPriceHistoryRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProduct { get { return _staticMetaData.GetPrefetchPathElement("Product", CommonEntityBase.CreateEntityCollection<ProductEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The EndDate property of the Entity ProductListPriceHistory<br/><br/>MS_Description: List price end date<br/>List price end date</summary>
		/// <remarks>Mapped on  table field: "ProductListPriceHistory"."EndDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> EndDate
		{
			get { return (Nullable<System.DateTime>)GetValue((int)ProductListPriceHistoryFieldIndex.EndDate, false); }
			set	{ SetValue((int)ProductListPriceHistoryFieldIndex.EndDate, value); }
		}

		/// <summary>The ListPrice property of the Entity ProductListPriceHistory<br/><br/>MS_Description: Product list price.<br/>Product list price.</summary>
		/// <remarks>Mapped on  table field: "ProductListPriceHistory"."ListPrice".<br/>Table field type characteristics (type, precision, scale, length): Money, 19, 4, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal ListPrice
		{
			get { return (System.Decimal)GetValue((int)ProductListPriceHistoryFieldIndex.ListPrice, true); }
			set	{ SetValue((int)ProductListPriceHistoryFieldIndex.ListPrice, value); }
		}

		/// <summary>The ModifiedDate property of the Entity ProductListPriceHistory<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "ProductListPriceHistory"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)ProductListPriceHistoryFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)ProductListPriceHistoryFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The ProductID property of the Entity ProductListPriceHistory<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "ProductListPriceHistory"."ProductID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Int32 ProductID
		{
			get { return (System.Int32)GetValue((int)ProductListPriceHistoryFieldIndex.ProductID, true); }
			set	{ SetValue((int)ProductListPriceHistoryFieldIndex.ProductID, value); }
		}

		/// <summary>The StartDate property of the Entity ProductListPriceHistory<br/><br/>MS_Description: List price start date.<br/>List price start date.</summary>
		/// <remarks>Mapped on  table field: "ProductListPriceHistory"."StartDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.DateTime StartDate
		{
			get { return (System.DateTime)GetValue((int)ProductListPriceHistoryFieldIndex.StartDate, true); }
			set	{ SetValue((int)ProductListPriceHistoryFieldIndex.StartDate, value); }
		}

		/// <summary>Gets / sets related entity of type 'ProductEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual ProductEntity Product
		{
			get { return _product; }
			set { SetSingleRelatedEntityNavigator(value, "Product"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum ProductListPriceHistoryFieldIndex
	{
		///<summary>EndDate. </summary>
		EndDate,
		///<summary>ListPrice. </summary>
		ListPrice,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>ProductID. </summary>
		ProductID,
		///<summary>StartDate. </summary>
		StartDate,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ProductListPriceHistory. </summary>
	public partial class ProductListPriceHistoryRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between ProductListPriceHistoryEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: ProductListPriceHistory.ProductID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingProductID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Product", false, new[] { ProductFields.ProductID, ProductListPriceHistoryFields.ProductID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticProductListPriceHistoryRelations
	{
		internal static readonly IEntityRelation ProductEntityUsingProductIDStatic = new ProductListPriceHistoryRelations().ProductEntityUsingProductID;

		/// <summary>CTor</summary>
		static StaticProductListPriceHistoryRelations() { }
	}
}
