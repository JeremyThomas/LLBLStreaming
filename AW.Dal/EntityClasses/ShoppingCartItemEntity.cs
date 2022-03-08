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
	/// <summary>Entity class which represents the entity 'ShoppingCartItem'.<br/><br/>MS_Description: Contains online customer orders until the order is submitted or cancelled.<br/></summary>
	[Serializable]
	public partial class ShoppingCartItemEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private ProductEntity _product;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static ShoppingCartItemEntityStaticMetaData _staticMetaData = new ShoppingCartItemEntityStaticMetaData();
		private static ShoppingCartItemRelations _relationsFactory = new ShoppingCartItemRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Product</summary>
			public static readonly string Product = "Product";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class ShoppingCartItemEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public ShoppingCartItemEntityStaticMetaData()
			{
				SetEntityCoreInfo("ShoppingCartItemEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.ShoppingCartItemEntity, typeof(ShoppingCartItemEntity), typeof(ShoppingCartItemEntityFactory), false);
				AddNavigatorMetaData<ShoppingCartItemEntity, ProductEntity>("Product", "ShoppingCartItems", (a, b) => a._product = b, a => a._product, (a, b) => a.Product = b, AW.Dal.RelationClasses.StaticShoppingCartItemRelations.ProductEntityUsingProductIDStatic, ()=>new ShoppingCartItemRelations().ProductEntityUsingProductID, null, new int[] { (int)ShoppingCartItemFieldIndex.ProductID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static ShoppingCartItemEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public ShoppingCartItemEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ShoppingCartItemEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ShoppingCartItemEntity</param>
		public ShoppingCartItemEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="shoppingCartItemID">PK value for ShoppingCartItem which data should be fetched into this ShoppingCartItem object</param>
		public ShoppingCartItemEntity(System.Int32 shoppingCartItemID) : this(shoppingCartItemID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="shoppingCartItemID">PK value for ShoppingCartItem which data should be fetched into this ShoppingCartItem object</param>
		/// <param name="validator">The custom validator object for this ShoppingCartItemEntity</param>
		public ShoppingCartItemEntity(System.Int32 shoppingCartItemID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.ShoppingCartItemID = shoppingCartItemID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ShoppingCartItemEntity(SerializationInfo info, StreamingContext context) : base(info, context)
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
			_customProperties.Add("MS_Description", @"Contains online customer orders until the order is submitted or cancelled.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date the time the record was created.");
			_fieldsCustomProperties.Add("DateCreated", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Product ordered. Foreign key to Product.ProductID.");
			_fieldsCustomProperties.Add("ProductID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Product quantity ordered.");
			_fieldsCustomProperties.Add("Quantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Shopping cart identification number.");
			_fieldsCustomProperties.Add("ShoppingCartID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("ShoppingCartItemID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ShoppingCartItemEntity</param>
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
		public static ShoppingCartItemRelations Relations { get { return _relationsFactory; } }

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

		/// <summary>The DateCreated property of the Entity ShoppingCartItem<br/><br/>MS_Description: Date the time the record was created.<br/>Date the time the record was created.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."DateCreated".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime DateCreated
		{
			get { return (System.DateTime)GetValue((int)ShoppingCartItemFieldIndex.DateCreated, true); }
			set	{ SetValue((int)ShoppingCartItemFieldIndex.DateCreated, value); }
		}

		/// <summary>The ModifiedDate property of the Entity ShoppingCartItem<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)ShoppingCartItemFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)ShoppingCartItemFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The ProductID property of the Entity ShoppingCartItem<br/><br/>MS_Description: Product ordered. Foreign key to Product.ProductID.<br/>Product ordered. Foreign key to Product.ProductID.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."ProductID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ProductID
		{
			get { return (System.Int32)GetValue((int)ShoppingCartItemFieldIndex.ProductID, true); }
			set	{ SetValue((int)ShoppingCartItemFieldIndex.ProductID, value); }
		}

		/// <summary>The Quantity property of the Entity ShoppingCartItem<br/><br/>MS_Description: Product quantity ordered.<br/>Product quantity ordered.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."Quantity".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 Quantity
		{
			get { return (System.Int32)GetValue((int)ShoppingCartItemFieldIndex.Quantity, true); }
			set	{ SetValue((int)ShoppingCartItemFieldIndex.Quantity, value); }
		}

		/// <summary>The ShoppingCartID property of the Entity ShoppingCartItem<br/><br/>MS_Description: Shopping cart identification number.<br/>Shopping cart identification number.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."ShoppingCartID".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String ShoppingCartID
		{
			get { return (System.String)GetValue((int)ShoppingCartItemFieldIndex.ShoppingCartID, true); }
			set	{ SetValue((int)ShoppingCartItemFieldIndex.ShoppingCartID, value); }
		}

		/// <summary>The ShoppingCartItemID property of the Entity ShoppingCartItem<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "ShoppingCartItem"."ShoppingCartItemID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 ShoppingCartItemID
		{
			get { return (System.Int32)GetValue((int)ShoppingCartItemFieldIndex.ShoppingCartItemID, true); }
			set { SetValue((int)ShoppingCartItemFieldIndex.ShoppingCartItemID, value); }		}

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
	public enum ShoppingCartItemFieldIndex
	{
		///<summary>DateCreated. </summary>
		DateCreated,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>ProductID. </summary>
		ProductID,
		///<summary>Quantity. </summary>
		Quantity,
		///<summary>ShoppingCartID. </summary>
		ShoppingCartID,
		///<summary>ShoppingCartItemID. </summary>
		ShoppingCartItemID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ShoppingCartItem. </summary>
	public partial class ShoppingCartItemRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between ShoppingCartItemEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: ShoppingCartItem.ProductID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingProductID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Product", false, new[] { ProductFields.ProductID, ShoppingCartItemFields.ProductID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticShoppingCartItemRelations
	{
		internal static readonly IEntityRelation ProductEntityUsingProductIDStatic = new ShoppingCartItemRelations().ProductEntityUsingProductID;

		/// <summary>CTor</summary>
		static StaticShoppingCartItemRelations() { }
	}
}
