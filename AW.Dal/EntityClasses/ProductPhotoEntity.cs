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
	/// <summary>Entity class which represents the entity 'ProductPhoto'.<br/><br/>MS_Description: Product images.<br/></summary>
	[Serializable]
	public partial class ProductPhotoEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<ProductProductPhotoEntity> _productProductPhotos;
		private EntityCollection<ProductEntity> _products;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static ProductPhotoEntityStaticMetaData _staticMetaData = new ProductPhotoEntityStaticMetaData();
		private static ProductPhotoRelations _relationsFactory = new ProductPhotoRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ProductProductPhotos</summary>
			public static readonly string ProductProductPhotos = "ProductProductPhotos";
			/// <summary>Member name Products</summary>
			public static readonly string Products = "Products";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class ProductPhotoEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public ProductPhotoEntityStaticMetaData()
			{
				SetEntityCoreInfo("ProductPhotoEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.ProductPhotoEntity, typeof(ProductPhotoEntity), typeof(ProductPhotoEntityFactory), false);
				AddNavigatorMetaData<ProductPhotoEntity, EntityCollection<ProductProductPhotoEntity>>("ProductProductPhotos", a => a._productProductPhotos, (a, b) => a._productProductPhotos = b, a => a.ProductProductPhotos, () => new ProductPhotoRelations().ProductProductPhotoEntityUsingProductPhotoID, typeof(ProductProductPhotoEntity), (int)AW.Dal.EntityType.ProductProductPhotoEntity);
				AddNavigatorMetaData<ProductPhotoEntity, EntityCollection<ProductEntity>>("Products", a => a._products, (a, b) => a._products = b, a => a.Products, () => new ProductPhotoRelations().ProductProductPhotoEntityUsingProductPhotoID, () => new ProductProductPhotoRelations().ProductEntityUsingProductID, "ProductPhotoEntity__", "ProductProductPhoto_", typeof(ProductEntity), (int)AW.Dal.EntityType.ProductEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static ProductPhotoEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public ProductPhotoEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ProductPhotoEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ProductPhotoEntity</param>
		public ProductPhotoEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="productPhotoID">PK value for ProductPhoto which data should be fetched into this ProductPhoto object</param>
		public ProductPhotoEntity(System.Int32 productPhotoID) : this(productPhotoID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="productPhotoID">PK value for ProductPhoto which data should be fetched into this ProductPhoto object</param>
		/// <param name="validator">The custom validator object for this ProductPhotoEntity</param>
		public ProductPhotoEntity(System.Int32 productPhotoID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.ProductPhotoID = productPhotoID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ProductPhotoEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'ProductProductPhoto' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductProductPhotos() { return CreateRelationInfoForNavigator("ProductProductPhotos"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProducts() { return CreateRelationInfoForNavigator("Products"); }
		
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
			_customProperties.Add("MS_Description", @"Product images.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Large image of the product.");
			_fieldsCustomProperties.Add("LargePhoto", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Large image file name.");
			_fieldsCustomProperties.Add("LargePhotoFileName", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("ProductPhotoID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Small image of the product.");
			_fieldsCustomProperties.Add("ThumbNailPhoto", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Small image file name.");
			_fieldsCustomProperties.Add("ThumbnailPhotoFileName", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ProductPhotoEntity</param>
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
		public static ProductPhotoRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'ProductProductPhoto' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductProductPhotos { get { return _staticMetaData.GetPrefetchPathElement("ProductProductPhotos", CommonEntityBase.CreateEntityCollection<ProductProductPhotoEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProducts { get { return _staticMetaData.GetPrefetchPathElement("Products", CommonEntityBase.CreateEntityCollection<ProductEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The LargePhoto property of the Entity ProductPhoto<br/><br/>MS_Description: Large image of the product.<br/>Large image of the product.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."LargePhoto".<br/>Table field type characteristics (type, precision, scale, length): VarBinary, 0, 0, 2147483647.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.Byte[] LargePhoto
		{
			get { return (System.Byte[])GetValue((int)ProductPhotoFieldIndex.LargePhoto, true); }
			set	{ SetValue((int)ProductPhotoFieldIndex.LargePhoto, value); }
		}

		/// <summary>The LargePhotoFileName property of the Entity ProductPhoto<br/><br/>MS_Description: Large image file name.<br/>Large image file name.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."LargePhotoFileName".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String LargePhotoFileName
		{
			get { return (System.String)GetValue((int)ProductPhotoFieldIndex.LargePhotoFileName, true); }
			set	{ SetValue((int)ProductPhotoFieldIndex.LargePhotoFileName, value); }
		}

		/// <summary>The ModifiedDate property of the Entity ProductPhoto<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)ProductPhotoFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)ProductPhotoFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The ProductPhotoID property of the Entity ProductPhoto<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."ProductPhotoID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 ProductPhotoID
		{
			get { return (System.Int32)GetValue((int)ProductPhotoFieldIndex.ProductPhotoID, true); }
			set { SetValue((int)ProductPhotoFieldIndex.ProductPhotoID, value); }		}

		/// <summary>The ThumbNailPhoto property of the Entity ProductPhoto<br/><br/>MS_Description: Small image of the product.<br/>Small image of the product.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."ThumbNailPhoto".<br/>Table field type characteristics (type, precision, scale, length): VarBinary, 0, 0, 2147483647.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.Byte[] ThumbNailPhoto
		{
			get { return (System.Byte[])GetValue((int)ProductPhotoFieldIndex.ThumbNailPhoto, true); }
			set	{ SetValue((int)ProductPhotoFieldIndex.ThumbNailPhoto, value); }
		}

		/// <summary>The ThumbnailPhotoFileName property of the Entity ProductPhoto<br/><br/>MS_Description: Small image file name.<br/>Small image file name.</summary>
		/// <remarks>Mapped on  table field: "ProductPhoto"."ThumbnailPhotoFileName".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String ThumbnailPhotoFileName
		{
			get { return (System.String)GetValue((int)ProductPhotoFieldIndex.ThumbnailPhotoFileName, true); }
			set	{ SetValue((int)ProductPhotoFieldIndex.ThumbnailPhotoFileName, value); }
		}

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductProductPhotoEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductProductPhotoEntity))]
		public virtual EntityCollection<ProductProductPhotoEntity> ProductProductPhotos { get { return GetOrCreateEntityCollection<ProductProductPhotoEntity, ProductProductPhotoEntityFactory>("ProductPhoto", true, false, ref _productProductPhotos); } }

		/// <summary>Gets the EntityCollection with the related entities of type 'ProductEntity' which are related to this entity via a relation of type 'm:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(ProductEntity))]
		public virtual EntityCollection<ProductEntity> Products { get { return GetOrCreateEntityCollection<ProductEntity, ProductEntityFactory>("Photos", false, true, ref _products); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum ProductPhotoFieldIndex
	{
		///<summary>LargePhoto. </summary>
		LargePhoto,
		///<summary>LargePhotoFileName. </summary>
		LargePhotoFileName,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>ProductPhotoID. </summary>
		ProductPhotoID,
		///<summary>ThumbNailPhoto. </summary>
		ThumbNailPhoto,
		///<summary>ThumbnailPhotoFileName. </summary>
		ThumbnailPhotoFileName,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ProductPhoto. </summary>
	public partial class ProductPhotoRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between ProductPhotoEntity and ProductProductPhotoEntity over the 1:n relation they have, using the relation between the fields: ProductPhoto.ProductPhotoID - ProductProductPhoto.ProductPhotoID</summary>
		public virtual IEntityRelation ProductProductPhotoEntityUsingProductPhotoID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "ProductProductPhotos", true, new[] { ProductPhotoFields.ProductPhotoID, ProductProductPhotoFields.ProductPhotoID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticProductPhotoRelations
	{
		internal static readonly IEntityRelation ProductProductPhotoEntityUsingProductPhotoIDStatic = new ProductPhotoRelations().ProductProductPhotoEntityUsingProductPhotoID;

		/// <summary>CTor</summary>
		static StaticProductPhotoRelations() { }
	}
}
