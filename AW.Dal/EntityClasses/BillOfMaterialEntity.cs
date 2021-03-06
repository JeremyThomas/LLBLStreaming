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
	/// <summary>Entity class which represents the entity 'BillOfMaterial'.<br/><br/>MS_Description: Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components.<br/></summary>
	[Serializable]
	public partial class BillOfMaterialEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private ProductEntity _productAssembly;
		private ProductEntity _productComponent;
		private UnitMeasureEntity _unitMeasure;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static BillOfMaterialEntityStaticMetaData _staticMetaData = new BillOfMaterialEntityStaticMetaData();
		private static BillOfMaterialRelations _relationsFactory = new BillOfMaterialRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name ProductAssembly</summary>
			public static readonly string ProductAssembly = "ProductAssembly";
			/// <summary>Member name ProductComponent</summary>
			public static readonly string ProductComponent = "ProductComponent";
			/// <summary>Member name UnitMeasure</summary>
			public static readonly string UnitMeasure = "UnitMeasure";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class BillOfMaterialEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public BillOfMaterialEntityStaticMetaData()
			{
				SetEntityCoreInfo("BillOfMaterialEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.BillOfMaterialEntity, typeof(BillOfMaterialEntity), typeof(BillOfMaterialEntityFactory), false);
				AddNavigatorMetaData<BillOfMaterialEntity, ProductEntity>("ProductAssembly", "BillOfAssemblyMaterials", (a, b) => a._productAssembly = b, a => a._productAssembly, (a, b) => a.ProductAssembly = b, AW.Dal.RelationClasses.StaticBillOfMaterialRelations.ProductEntityUsingProductAssemblyIDStatic, ()=>new BillOfMaterialRelations().ProductEntityUsingProductAssemblyID, null, new int[] { (int)BillOfMaterialFieldIndex.ProductAssemblyID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
				AddNavigatorMetaData<BillOfMaterialEntity, ProductEntity>("ProductComponent", "BillOfComponentMaterials", (a, b) => a._productComponent = b, a => a._productComponent, (a, b) => a.ProductComponent = b, AW.Dal.RelationClasses.StaticBillOfMaterialRelations.ProductEntityUsingComponentIDStatic, ()=>new BillOfMaterialRelations().ProductEntityUsingComponentID, null, new int[] { (int)BillOfMaterialFieldIndex.ComponentID }, null, true, (int)AW.Dal.EntityType.ProductEntity);
				AddNavigatorMetaData<BillOfMaterialEntity, UnitMeasureEntity>("UnitMeasure", "BillOfMaterials", (a, b) => a._unitMeasure = b, a => a._unitMeasure, (a, b) => a.UnitMeasure = b, AW.Dal.RelationClasses.StaticBillOfMaterialRelations.UnitMeasureEntityUsingUnitMeasureCodeStatic, ()=>new BillOfMaterialRelations().UnitMeasureEntityUsingUnitMeasureCode, null, new int[] { (int)BillOfMaterialFieldIndex.UnitMeasureCode }, null, true, (int)AW.Dal.EntityType.UnitMeasureEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static BillOfMaterialEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public BillOfMaterialEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public BillOfMaterialEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this BillOfMaterialEntity</param>
		public BillOfMaterialEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="billOfMaterialsID">PK value for BillOfMaterial which data should be fetched into this BillOfMaterial object</param>
		public BillOfMaterialEntity(System.Int32 billOfMaterialsID) : this(billOfMaterialsID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="billOfMaterialsID">PK value for BillOfMaterial which data should be fetched into this BillOfMaterial object</param>
		/// <param name="validator">The custom validator object for this BillOfMaterialEntity</param>
		public BillOfMaterialEntity(System.Int32 billOfMaterialsID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.BillOfMaterialsID = billOfMaterialsID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected BillOfMaterialEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductAssembly() { return CreateRelationInfoForNavigator("ProductAssembly"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Product' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoProductComponent() { return CreateRelationInfoForNavigator("ProductComponent"); }

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'UnitMeasure' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoUnitMeasure() { return CreateRelationInfoForNavigator("UnitMeasure"); }
		
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
			_customProperties.Add("MS_Description", @"Items required to make bicycles and bicycle subassemblies. It identifies the heirarchical relationship between a parent product and its components.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index.");
			_fieldsCustomProperties.Add("BillOfMaterialsID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Indicates the depth the component is from its parent (AssemblyID).");
			_fieldsCustomProperties.Add("Bomlevel", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Nonclustered index.");
			_fieldsCustomProperties.Add("ComponentID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date the component stopped being used in the assembly item.");
			_fieldsCustomProperties.Add("EndDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Quantity of the component needed to create the assembly.");
			_fieldsCustomProperties.Add("PerAssemblyQuantity", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Parent product identification number. Foreign key to Product.ProductID.");
			_fieldsCustomProperties.Add("ProductAssemblyID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date the component started being used in the assembly item.");
			_fieldsCustomProperties.Add("StartDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Standard code identifying the unit of measure for the quantity.");
			_fieldsCustomProperties.Add("UnitMeasureCode", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this BillOfMaterialEntity</param>
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
		public static BillOfMaterialRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductAssembly { get { return _staticMetaData.GetPrefetchPathElement("ProductAssembly", CommonEntityBase.CreateEntityCollection<ProductEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Product' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathProductComponent { get { return _staticMetaData.GetPrefetchPathElement("ProductComponent", CommonEntityBase.CreateEntityCollection<ProductEntity>()); } }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'UnitMeasure' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathUnitMeasure { get { return _staticMetaData.GetPrefetchPathElement("UnitMeasure", CommonEntityBase.CreateEntityCollection<UnitMeasureEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The BillOfMaterialsID property of the Entity BillOfMaterial<br/><br/>MS_Description: Clustered index.<br/>Clustered index.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."BillOfMaterialsID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 BillOfMaterialsID
		{
			get { return (System.Int32)GetValue((int)BillOfMaterialFieldIndex.BillOfMaterialsID, true); }
			set { SetValue((int)BillOfMaterialFieldIndex.BillOfMaterialsID, value); }		}

		/// <summary>The Bomlevel property of the Entity BillOfMaterial<br/><br/>MS_Description: Indicates the depth the component is from its parent (AssemblyID).<br/>Indicates the depth the component is from its parent (AssemblyID).</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."BOMLevel".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int16 Bomlevel
		{
			get { return (System.Int16)GetValue((int)BillOfMaterialFieldIndex.Bomlevel, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.Bomlevel, value); }
		}

		/// <summary>The ComponentID property of the Entity BillOfMaterial<br/><br/>MS_Description: Nonclustered index.<br/>Nonclustered index.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."ComponentID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Int32 ComponentID
		{
			get { return (System.Int32)GetValue((int)BillOfMaterialFieldIndex.ComponentID, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.ComponentID, value); }
		}

		/// <summary>The EndDate property of the Entity BillOfMaterial<br/><br/>MS_Description: Date the component stopped being used in the assembly item.<br/>Date the component stopped being used in the assembly item.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."EndDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.DateTime> EndDate
		{
			get { return (Nullable<System.DateTime>)GetValue((int)BillOfMaterialFieldIndex.EndDate, false); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.EndDate, value); }
		}

		/// <summary>The ModifiedDate property of the Entity BillOfMaterial<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)BillOfMaterialFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The PerAssemblyQuantity property of the Entity BillOfMaterial<br/><br/>MS_Description: Quantity of the component needed to create the assembly.<br/>Quantity of the component needed to create the assembly.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."PerAssemblyQty".<br/>Table field type characteristics (type, precision, scale, length): Decimal, 8, 2, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.Decimal PerAssemblyQuantity
		{
			get { return (System.Decimal)GetValue((int)BillOfMaterialFieldIndex.PerAssemblyQuantity, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.PerAssemblyQuantity, value); }
		}

		/// <summary>The ProductAssemblyID property of the Entity BillOfMaterial<br/><br/>MS_Description: Parent product identification number. Foreign key to Product.ProductID.<br/>Parent product identification number. Foreign key to Product.ProductID.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."ProductAssemblyID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> ProductAssemblyID
		{
			get { return (Nullable<System.Int32>)GetValue((int)BillOfMaterialFieldIndex.ProductAssemblyID, false); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.ProductAssemblyID, value); }
		}

		/// <summary>The StartDate property of the Entity BillOfMaterial<br/><br/>MS_Description: Date the component started being used in the assembly item.<br/>Date the component started being used in the assembly item.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."StartDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime StartDate
		{
			get { return (System.DateTime)GetValue((int)BillOfMaterialFieldIndex.StartDate, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.StartDate, value); }
		}

		/// <summary>The UnitMeasureCode property of the Entity BillOfMaterial<br/><br/>MS_Description: Standard code identifying the unit of measure for the quantity.<br/>Standard code identifying the unit of measure for the quantity.</summary>
		/// <remarks>Mapped on  table field: "BillOfMaterials"."UnitMeasureCode".<br/>Table field type characteristics (type, precision, scale, length): NChar, 0, 0, 3.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String UnitMeasureCode
		{
			get { return (System.String)GetValue((int)BillOfMaterialFieldIndex.UnitMeasureCode, true); }
			set	{ SetValue((int)BillOfMaterialFieldIndex.UnitMeasureCode, value); }
		}

		/// <summary>Gets / sets related entity of type 'ProductEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual ProductEntity ProductAssembly
		{
			get { return _productAssembly; }
			set { SetSingleRelatedEntityNavigator(value, "ProductAssembly"); }
		}

		/// <summary>Gets / sets related entity of type 'ProductEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual ProductEntity ProductComponent
		{
			get { return _productComponent; }
			set { SetSingleRelatedEntityNavigator(value, "ProductComponent"); }
		}

		/// <summary>Gets / sets related entity of type 'UnitMeasureEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual UnitMeasureEntity UnitMeasure
		{
			get { return _unitMeasure; }
			set { SetSingleRelatedEntityNavigator(value, "UnitMeasure"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum BillOfMaterialFieldIndex
	{
		///<summary>BillOfMaterialsID. </summary>
		BillOfMaterialsID,
		///<summary>Bomlevel. </summary>
		Bomlevel,
		///<summary>ComponentID. </summary>
		ComponentID,
		///<summary>EndDate. </summary>
		EndDate,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>PerAssemblyQuantity. </summary>
		PerAssemblyQuantity,
		///<summary>ProductAssemblyID. </summary>
		ProductAssemblyID,
		///<summary>StartDate. </summary>
		StartDate,
		///<summary>UnitMeasureCode. </summary>
		UnitMeasureCode,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: BillOfMaterial. </summary>
	public partial class BillOfMaterialRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between BillOfMaterialEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: BillOfMaterial.ProductAssemblyID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingProductAssemblyID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "ProductAssembly", false, new[] { ProductFields.ProductID, BillOfMaterialFields.ProductAssemblyID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between BillOfMaterialEntity and ProductEntity over the m:1 relation they have, using the relation between the fields: BillOfMaterial.ComponentID - Product.ProductID</summary>
		public virtual IEntityRelation ProductEntityUsingComponentID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "ProductComponent", false, new[] { ProductFields.ProductID, BillOfMaterialFields.ComponentID }); }
		}

		/// <summary>Returns a new IEntityRelation object, between BillOfMaterialEntity and UnitMeasureEntity over the m:1 relation they have, using the relation between the fields: BillOfMaterial.UnitMeasureCode - UnitMeasure.UnitMeasureCode</summary>
		public virtual IEntityRelation UnitMeasureEntityUsingUnitMeasureCode
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "UnitMeasure", false, new[] { UnitMeasureFields.UnitMeasureCode, BillOfMaterialFields.UnitMeasureCode }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticBillOfMaterialRelations
	{
		internal static readonly IEntityRelation ProductEntityUsingProductAssemblyIDStatic = new BillOfMaterialRelations().ProductEntityUsingProductAssemblyID;
		internal static readonly IEntityRelation ProductEntityUsingComponentIDStatic = new BillOfMaterialRelations().ProductEntityUsingComponentID;
		internal static readonly IEntityRelation UnitMeasureEntityUsingUnitMeasureCodeStatic = new BillOfMaterialRelations().UnitMeasureEntityUsingUnitMeasureCode;

		/// <summary>CTor</summary>
		static StaticBillOfMaterialRelations() { }
	}
}
