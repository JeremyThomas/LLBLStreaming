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
	/// <summary>Entity class which represents the entity 'JobCandidate'.<br/><br/>MS_Description: Résumés submitted to Human Resources by job applicants.<br/></summary>
	[Serializable]
	public partial class JobCandidateEntity : CommonEntityBase
		// __LLBLGENPRO_USER_CODE_REGION_START AdditionalInterfaces
		// __LLBLGENPRO_USER_CODE_REGION_END	
	{
		private EmployeeEntity _employee;

		// __LLBLGENPRO_USER_CODE_REGION_START PrivateMembers
		// __LLBLGENPRO_USER_CODE_REGION_END
		private static Dictionary<string, string>	_customProperties;
		private static Dictionary<string, Dictionary<string, string>>	_fieldsCustomProperties;
		private static JobCandidateEntityStaticMetaData _staticMetaData = new JobCandidateEntityStaticMetaData();
		private static JobCandidateRelations _relationsFactory = new JobCandidateRelations();

		/// <summary>All names of fields mapped onto a relation. Usable for in-memory filtering</summary>
		public static partial class MemberNames
		{
			/// <summary>Member name Employee</summary>
			public static readonly string Employee = "Employee";
		}

		/// <summary>Static meta-data storage for navigator related information</summary>
		protected class JobCandidateEntityStaticMetaData : EntityStaticMetaDataBase
		{
			public JobCandidateEntityStaticMetaData()
			{
				SetEntityCoreInfo("JobCandidateEntity", InheritanceHierarchyType.None, false, (int)AW.Dal.EntityType.JobCandidateEntity, typeof(JobCandidateEntity), typeof(JobCandidateEntityFactory), false);
				AddNavigatorMetaData<JobCandidateEntity, EmployeeEntity>("Employee", "JobCandidates", (a, b) => a._employee = b, a => a._employee, (a, b) => a.Employee = b, AW.Dal.RelationClasses.StaticJobCandidateRelations.EmployeeEntityUsingEmployeeIDStatic, ()=>new JobCandidateRelations().EmployeeEntityUsingEmployeeID, null, new int[] { (int)JobCandidateFieldIndex.EmployeeID }, null, true, (int)AW.Dal.EntityType.EmployeeEntity);
			}
		}

		/// <summary>Static ctor</summary>
		static JobCandidateEntity()
		{
			SetupCustomPropertyHashtables();
		}

		/// <summary> CTor</summary>
		public JobCandidateEntity()
		{
			InitClassEmpty(null, null);
		}

		/// <summary> CTor</summary>
		/// <param name="fields">Fields object to set as the fields for this entity.</param>
		public JobCandidateEntity(IEntityFields2 fields)
		{
			InitClassEmpty(null, fields);
		}

		/// <summary> CTor</summary>
		/// <param name="validator">The custom validator object for this JobCandidateEntity</param>
		public JobCandidateEntity(IValidator validator)
		{
			InitClassEmpty(validator, null);
		}

		/// <summary> CTor</summary>
		/// <param name="jobCandidateID">PK value for JobCandidate which data should be fetched into this JobCandidate object</param>
		public JobCandidateEntity(System.Int32 jobCandidateID) : this(jobCandidateID, null)
		{
		}

		/// <summary> CTor</summary>
		/// <param name="jobCandidateID">PK value for JobCandidate which data should be fetched into this JobCandidate object</param>
		/// <param name="validator">The custom validator object for this JobCandidateEntity</param>
		public JobCandidateEntity(System.Int32 jobCandidateID, IValidator validator)
		{
			InitClassEmpty(validator, null);
			this.JobCandidateID = jobCandidateID;
		}

		/// <summary>Private CTor for deserialization</summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected JobCandidateEntity(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			// __LLBLGENPRO_USER_CODE_REGION_START DeserializationConstructor
			// __LLBLGENPRO_USER_CODE_REGION_END
		}

		/// <summary>Creates a new IRelationPredicateBucket object which contains the predicate expression and relation collection to fetch the related entity of type 'Employee' to this entity.</summary>
		/// <returns></returns>
		public virtual IRelationPredicateBucket GetRelationInfoEmployee() { return CreateRelationInfoForNavigator("Employee"); }
		
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
			_customProperties.Add("MS_Description", @"Résumés submitted to Human Resources by job applicants.");
			_fieldsCustomProperties = new Dictionary<string, Dictionary<string, string>>();
			Dictionary<string, string> fieldHashtable;
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Employee identification number if applicant was hired. Foreign key to Employee.EmployeeID.");
			_fieldsCustomProperties.Add("EmployeeID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Clustered index created by a primary key constraint.");
			_fieldsCustomProperties.Add("JobCandidateID", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Date and time the record was last updated.");
			_fieldsCustomProperties.Add("ModifiedDate", fieldHashtable);
			fieldHashtable = new Dictionary<string, string>();
			fieldHashtable.Add("MS_Description", @"Résumé in XML format.");
			_fieldsCustomProperties.Add("Resume", fieldHashtable);
		}

		/// <summary>Initializes the class with empty data, as if it is a new Entity.</summary>
		/// <param name="validator">The validator object for this JobCandidateEntity</param>
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
		public static JobCandidateRelations Relations { get { return _relationsFactory; } }

		/// <summary>The custom properties for this entity type.</summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, string> CustomProperties { get { return _customProperties;} }

		/// <summary>Creates a new PrefetchPathElement2 object which contains all the information to prefetch the related entities of type 'Employee' for this entity.</summary>
		/// <returns>Ready to use IPrefetchPathElement2 implementation.</returns>
		public static IPrefetchPathElement2 PrefetchPathEmployee { get { return _staticMetaData.GetPrefetchPathElement("Employee", CommonEntityBase.CreateEntityCollection<EmployeeEntity>()); } }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, string> CustomPropertiesOfType { get { return CustomProperties;} }

		/// <summary>The custom properties for the fields of this entity type. The returned Hashtable contains per fieldname a hashtable of name-value pairs. </summary>
		/// <remarks>The data returned from this property should be considered read-only: it is not thread safe to alter this data at runtime.</remarks>
		public  static Dictionary<string, Dictionary<string, string>> FieldsCustomProperties { get { return _fieldsCustomProperties;} }

		/// <inheritdoc/>
		[Browsable(false), XmlIgnore]
		protected override Dictionary<string, Dictionary<string, string>> FieldsCustomPropertiesOfType { get { return FieldsCustomProperties;} }

		/// <summary>The EmployeeID property of the Entity JobCandidate<br/><br/>MS_Description: Employee identification number if applicant was hired. Foreign key to Employee.EmployeeID.<br/>Employee identification number if applicant was hired. Foreign key to Employee.EmployeeID.</summary>
		/// <remarks>Mapped on  table field: "JobCandidate"."EmployeeID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual Nullable<System.Int32> EmployeeID
		{
			get { return (Nullable<System.Int32>)GetValue((int)JobCandidateFieldIndex.EmployeeID, false); }
			set	{ SetValue((int)JobCandidateFieldIndex.EmployeeID, value); }
		}

		/// <summary>The JobCandidateID property of the Entity JobCandidate<br/><br/>MS_Description: Clustered index created by a primary key constraint.<br/>Clustered index created by a primary key constraint.</summary>
		/// <remarks>Mapped on  table field: "JobCandidate"."JobCandidateID".<br/>Table field type characteristics (type, precision, scale, length): Int, 10, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, true, true</remarks>
		public virtual System.Int32 JobCandidateID
		{
			get { return (System.Int32)GetValue((int)JobCandidateFieldIndex.JobCandidateID, true); }
			set { SetValue((int)JobCandidateFieldIndex.JobCandidateID, value); }		}

		/// <summary>The ModifiedDate property of the Entity JobCandidate<br/><br/>MS_Description: Date and time the record was last updated.<br/>Date and time the record was last updated.</summary>
		/// <remarks>Mapped on  table field: "JobCandidate"."ModifiedDate".<br/>Table field type characteristics (type, precision, scale, length): DateTime, 0, 0, 0.<br/>Table field behavior characteristics (is nullable, is PK, is identity): false, false, false</remarks>
		public virtual System.DateTime ModifiedDate
		{
			get { return (System.DateTime)GetValue((int)JobCandidateFieldIndex.ModifiedDate, true); }
			set	{ SetValue((int)JobCandidateFieldIndex.ModifiedDate, value); }
		}

		/// <summary>The Resume property of the Entity JobCandidate<br/><br/>MS_Description: Résumé in XML format.<br/>Résumé in XML format.</summary>
		/// <remarks>Mapped on  table field: "JobCandidate"."Resume".<br/>Table field type characteristics (type, precision, scale, length): Xml, 0, 0, 2147483647.<br/>Table field behavior characteristics (is nullable, is PK, is identity): true, false, false</remarks>
		public virtual System.String Resume
		{
			get { return (System.String)GetValue((int)JobCandidateFieldIndex.Resume, true); }
			set	{ SetValue((int)JobCandidateFieldIndex.Resume, value); }
		}

		/// <summary>Gets / sets related entity of type 'EmployeeEntity' which has to be set using a fetch action earlier. If no related entity is set for this property, null is returned..<br/><br/></summary>
		[Browsable(false)]
		public virtual EmployeeEntity Employee
		{
			get { return _employee; }
			set { SetSingleRelatedEntityNavigator(value, "Employee"); }
		}

		// __LLBLGENPRO_USER_CODE_REGION_START CustomEntityCode
		// __LLBLGENPRO_USER_CODE_REGION_END

	}
}

namespace AW.Dal
{
	public enum JobCandidateFieldIndex
	{
		///<summary>EmployeeID. </summary>
		EmployeeID,
		///<summary>JobCandidateID. </summary>
		JobCandidateID,
		///<summary>ModifiedDate. </summary>
		ModifiedDate,
		///<summary>Resume. </summary>
		Resume,
		/// <summary></summary>
		AmountOfFields
	}
}

namespace AW.Dal.RelationClasses
{
	/// <summary>Implements the relations factory for the entity: JobCandidate. </summary>
	public partial class JobCandidateRelations: RelationFactory
	{

		/// <summary>Returns a new IEntityRelation object, between JobCandidateEntity and EmployeeEntity over the m:1 relation they have, using the relation between the fields: JobCandidate.EmployeeID - Employee.EmployeeID</summary>
		public virtual IEntityRelation EmployeeEntityUsingEmployeeID
		{
			get	{ return ModelInfoProviderSingleton.GetInstance().CreateRelation(RelationType.ManyToOne, "Employee", false, new[] { EmployeeFields.EmployeeID, JobCandidateFields.EmployeeID }); }
		}

	}
	
	/// <summary>Static class which is used for providing relationship instances which are re-used internally for syncing</summary>
	internal static class StaticJobCandidateRelations
	{
		internal static readonly IEntityRelation EmployeeEntityUsingEmployeeIDStatic = new JobCandidateRelations().EmployeeEntityUsingEmployeeID;

		/// <summary>CTor</summary>
		static StaticJobCandidateRelations() { }
	}
}
