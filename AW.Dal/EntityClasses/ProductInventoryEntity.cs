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
	/// <summary>Entity class which represents the entity 'ProductInventory'.<br/><br/>MS_Description: Product inventory information.<br/></summary>
	[Serializable]
	public partial class ProductInventoryEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private LocationEntity _location;
		private ProductEntity _product;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static ProductInventoryEntityStaticMetaData _staticMetaData = new ProductInventoryEntityStaticMetaData();
		private static ProductInventoryRelations _relationsFactory = new ProductInventoryRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Location</summary>
			public static readonly string Location = "Location";
			/// <summary>Member name Product</summary>
			public static readonly string Product = "Product";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class ProductInventoryEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public ProductInventoryEntityStaticMetaData()
			{
				SetEntityCoreInfo("ProductInventoryEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.ProductInventoryEntity, typeof(ProductInventoryEntity), typeof(ProductInventoryEntityFactory), false);
				AddNavigatorMetaData<ProductInventoryEntity, LocationEntity>("Location", "ProductInventories", (a, b) => a._location = b, a => a._location, (a, b) => a.Location = b, AW.Dal.RelationClasses.StaticProductInventoryRelations.LocationEntityUsingLocationIDStatic, ()=>new ProductInventoryRelations().LocationEntityUsingLocationID, null, new int[] { (int)ProductInventoryFieldIndex.LocationID }, null, true, (int)AW.Dal.EntityType.LocationEntity);
				AddNavigatorMetaData<ProductInventoryEntity, ProductEntity>("Product", "ProductInventories", (a, b) => a._product = b, a => a._product, (a, b) => a.Product = b, AW.Dal.RelationClasses.StaticProductInventoryRelations.ProductEntityUsingProductIDStatic, ()=>new ProductInventoryRelations().ProductEntityUsingProductID, null, new int[] { (int)ProductInventoryFieldIndex.ProductID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static ProductInventoryEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public ProductInventoryEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ProductInventoryEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ProductInventoryEntity</param>
		public ProductInventoryEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="locationID">PK value for ProductInventory which data should be fetched into this ProductInventory object</param>
		/// <param name="productID">PK value for ProductInventory which data should be fetched into this ProductInventory object</param>
		public ProductInventoryEntity(System.Int16 locationID, System.Int32 productID) : this(locationID, productID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="locationID">PK value for ProductInventory which data should be fetched into this ProductInventory object</param>
		/// <param name="productID">PK value for ProductInventory which data should be fetched into this ProductInventory object</param>
		/// <param name="validator">The custom validator object for this ProductInventoryEntity</param>
		public ProductInventoryEntity(System.Int16 locationID, System.Int32 productID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.LocationID = locationID;
			this.ProductID = productID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ProductInventoryEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Location' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoLocation() { return CreateRelationInfoForNavigator("Location"); }

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
			_customProperties.Add("MS_Description", @"Product inventory information.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Storage container on a shelf in an inventory location.");
			_fieldsCustomProperties.Add("Bin", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Inventory location identification number. Foreign key to Location.LocationID. ");
			_fieldsCustomProperties.Add("LocationID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("ProductID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Quantity of products in the inventory location.");
			_fieldsCustomProperties.Add("Quantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");
			_fieldsCustomProperties.Add("Rowguid", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Storage compartment within an inventory location.");
			_fieldsCustomProperties.Add("Shelf", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ProductInventoryEntity</param>
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
		public static ProductInventoryRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Location' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathLocation { get { return _staticMetaData.GetPrefetchPathElement("Location", CommonEntityBase.CreateEntityCollection<LocationEntity>()); } }

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

		/// <summary>The Bin property of the Entity ProductInventory<br/><br/>MS_Description: Storage container on a shelf in an inventory location.<br/>Storage container on a shelf in an inventory location.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."Bin".<br/>Table field type characteristics (type, precision, scale, length): TinyInt, 3, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Byte Bin
		{
			get { return (System.Byte)GetValue((int)ProductInventoryFieldIndex.Bin, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.Bin, value); }
		}

		/// <summary>The LocationID property of the Entity ProductInventory<br/><br/>MS_Description: Inventory location identification number. Foreign key to Location.LocationID. <br/>Inventory location identification number. Foreign key to Location.LocationID. </summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."LocationID".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Int16 LocationID
		{
			get { return (System.Int16)GetValue((int)ProductInventoryFieldIndex.LocationID, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.LocationID, value); }
		}

		/// <summary>The ModifiedDate property of the Entity ProductInventory<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)ProductInventoryFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The ProductID property of the Entity ProductInventory<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."ProductID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, false</remarks>
		public virtual System.Int32 ProductID
		{
			get { return (System.Int32)GetValue((int)ProductInventoryFieldIndex.ProductID, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.ProductID, value); }
		}

		/// <summary>The Quantity property of the Entity ProductInventory<br/><br/>MS_Description: Quantity of products in the inventory location.<br/>Quantity of products in the inventory location.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."Quantity".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int16 Quantity
		{
			get { return (System.Int16)GetValue((int)ProductInventoryFieldIndex.Quantity, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.Quantity, value); }
		}

		/// <summary>The Rowguid property of the Entity ProductInventory<br/><br/>MS_Description: ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.<br/>ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."rowguid".<br/>Table field type characteristics (type, precision, scale, length): UniqueIdentifier, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Guid Rowguid
		{
			get { return (System.Guid)GetValue((int)ProductInventoryFieldIndex.Rowguid, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.Rowguid, value); }
		}

		/// <summary>The Shelf property of the Entity ProductInventory<br/><br/>MS_Description: Storage compartment within an inventory location.<br/>Storage compartment within an inventory location.</summary>
		/// <remarks>Mapped on  table field: "ProductInventory"."Shelf".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 10.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Shelf
		{
			get { return (System.String)GetValue((int)ProductInventoryFieldIndex.Shelf, true); }
			set	{ SetValue((int)ProductInventoryFieldIndex.Shelf, value); }
		}

		/// <summary>Gets / sets related entity of type 'LocationEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual LocationEntity Location
		{
			get { return _location; }
			set { SetSingleRelatedEntityNavigator(value, "Location"); }
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
	public enum ProductInventoryFieldIndex
	{
		///<summary>Bin. </summary>
		Bin,
		///<summary>LocationID. </summary>
		LocationID,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>ProductID. </summary>
		ProductID,
		///<summary>Quantity. </summary>
		Quantity,
		///<summary>Rowguid. </summary>
		Rowguid,
		///<summary>Shelf. </summary>
		Shelf,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ProductInventory. </summary>
	public partial class ProductInventoryRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between ProductInventoryEntity and LocationEntity over the m:1 relation they have, using the relation between the fields: ProductInventory.LocationID - Location.LocationID</summary>
		public virtual IEntityRelation LocationEntityUsingLocationID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Location", false, new[] { LocationFields.LocationID, ProductInventoryFields.LocationID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between ProductInventoryEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: ProductInventory.ProductID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingProductID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Product", false, new[] { ProductFields.ProductID, ProductInventoryFields.ProductID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticProductInventoryRelations
	{
		internal static readonly IEntityRelation LocationEntityUsingLocationIDStatic = new ProductInventoryRelations().LocationEntityUsingLocationID;
		internal static readonly IEntityRelation ProductEntityUsingProductIDStatic = new ProductInventoryRelations().ProductEntityUsingProductID;

		/// <summary>CTor</summary>
		static StaticProductInventoryRelations() { }
	}
}
