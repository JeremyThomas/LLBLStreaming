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
	/// <summary>Entity class which represents the entity 'ScrapReason'.<br/><br/>MS_Description: Manufacturing failure reasons lookup table.<br/></summary>
	[Serializable]
	public partial class ScrapReasonEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EntityCollection<WorkOrderEntity> _workOrders;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static ScrapReasonEntityStaticMetaData _staticMetaData = new ScrapReasonEntityStaticMetaData();
		private static ScrapReasonRelations _relationsFactory = new ScrapReasonRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name WorkOrders</summary>
			public static readonly string WorkOrders = "WorkOrders";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class ScrapReasonEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public ScrapReasonEntityStaticMetaData()
			{
				SetEntityCoreInfo("ScrapReasonEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.ScrapReasonEntity, typeof(ScrapReasonEntity), typeof(ScrapReasonEntityFactory), false);
				AddNavigatorMetaData<ScrapReasonEntity, EntityCollection<WorkOrderEntity>>("WorkOrders", a => a._workOrders, (a, b) => a._workOrders = b, a => a.WorkOrders, () => new ScrapReasonRelations().WorkOrderEntityUsingScrapReasonID, typeof(WorkOrderEntity), (int)AW.Dal.EntityType.WorkOrderEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static ScrapReasonEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public ScrapReasonEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public ScrapReasonEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this ScrapReasonEntity</param>
		public ScrapReasonEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="scrapReasonID">PK value for ScrapReason which data should be fetched into this ScrapReason object</param>
		public ScrapReasonEntity(System.Int16 scrapReasonID) : this(scrapReasonID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="scrapReasonID">PK value for ScrapReason which data should be fetched into this ScrapReason object</param>
		/// <param name="validator">The custom validator object for this ScrapReasonEntity</param>
		public ScrapReasonEntity(System.Int16 scrapReasonID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.ScrapReasonID = scrapReasonID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected ScrapReasonEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entities of type 'WorkOrder' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoWorkOrders() { return CreateRelationInfoForNavigator("WorkOrders"); }
		
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
			_customProperties.Add("MS_Description", @"Manufacturing failure reasons lookup table.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Failure description.");
			_fieldsCustomProperties.Add("Name", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("ScrapReasonID", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this ScrapReasonEntity</param>
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
		public static ScrapReasonRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'WorkOrder' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathWorkOrders { get { return _staticMetaData.GetPrefetchPathElement("WorkOrders", CommonEntityBase.CreateEntityCollection<WorkOrderEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The ModifiedDate property of the Entity ScrapReason<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "ScrapReason"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)ScrapReasonFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)ScrapReasonFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Name property of the Entity ScrapReason<br/><br/>MS_Description: Failure description.<br/>Failure description.</summary>
		/// <remarks>Mapped on  table field: "ScrapReason"."Name".<br/>Table field type characteristics (type, precision, scale, length): NVarChar, 0, 0, 50.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.String Name
		{
			get { return (System.String)GetValue((int)ScrapReasonFieldIndex.Name, true); }
			set	{ SetValue((int)ScrapReasonFieldIndex.Name, value); }
		}

		/// <summary>The ScrapReasonID property of the Entity ScrapReason<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "ScrapReason"."ScrapReasonID".<br/>Table field type characteristics (type, precision, scale, length): SmallInt, 5, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int16 ScrapReasonID
		{
			get { return (System.Int16)GetValue((int)ScrapReasonFieldIndex.ScrapReasonID, true); }
			set { SetValue((int)ScrapReasonFieldIndex.ScrapReasonID, value); }		}

		/// <summary>Gets the EntityCollection with the related entities of type 'WorkOrderEntity' which are related to this entity via a relation of type '1:n'. If the EntityCollection hasn't been fetched yet, the collection returned will be empty.<br/><br/></summary>
		[TypeContainedAttribute(typeof(WorkOrderEntity))]
		public virtual EntityCollection<WorkOrderEntity> WorkOrders { get { return GetOrCreateEntityCollection<WorkOrderEntity, WorkOrderEntityFactory>("ScrapReason", true, false, ref _workOrders); } }

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum ScrapReasonFieldIndex
	{
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Name. </summary>
		Name,
		///<summary>ScrapReasonID. </summary>
		ScrapReasonID,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: ScrapReason. </summary>
	public partial class ScrapReasonRelations: RelationFactory
	{
		/// <summary>Returns a new IEntityRelation object, between ScrapReasonEntity and WorkOrderEntity over the 1:n relation they have, using the relation between the fields: ScrapReason.ScrapReasonID - WorkOrder.ScrapReasonID</summary>
		public virtual IEntityRelation WorkOrderEntityUsingScrapReasonID
		{
			get { return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.OneToMany, "WorkOrders", true, new[] { ScrapReasonFields.ScrapReasonID, WorkOrderFields.ScrapReasonID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticScrapReasonRelations
	{
		internal static readonly IEntityRelation WorkOrderEntityUsingScrapReasonIDStatic = new ScrapReasonRelations().WorkOrderEntityUsingScrapReasonID;

		/// <summary>CTor</summary>
		static StaticScrapReasonRelations() { }
	}
}