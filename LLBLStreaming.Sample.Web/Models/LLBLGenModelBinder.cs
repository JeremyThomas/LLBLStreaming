/* Usage documentation at the bottom of this file */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using AQDPortal.Helpers.Models.ModelSerializer;
using AW.Helper;
using AW.Helper.LLBL;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace LLBLStreaming.Sample.Web.Models
{
  public class LLBLGenModelBinder : DefaultModelBinder
  {
    private class LLBLValueProvider : IValueProvider
    {
      private readonly IValueProvider _provider;
      private readonly List<string> _filteredProperties;

      public LLBLValueProvider(IValueProvider valueProvider, params string[] filteredPropertyArr)
      {
        _provider = valueProvider;
        _filteredProperties = new List<string>(filteredPropertyArr);
      }

      #region IValueProvider Members

      public bool ContainsPrefix(string prefix)
      {
        return _provider.ContainsPrefix(prefix);
      }

      public ValueProviderResult GetValue(string key)
      {
        if (!_filteredProperties.Contains(key))
        {
          var valueProviderResult = _provider.GetValue(key);
          // If new value isn't present return old so validation works
          if (valueProviderResult == null && !key.Contains(OriginalNameSufixWithSeperator)) 
            return _provider.GetValue(key + OriginalNameSufixWithSeperator);
          return valueProviderResult;
        }
        return null;
      }

      #endregion
    }

    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var valueProvider = bindingContext.ValueProvider;
      bindingContext.ValueProvider = new LLBLValueProvider(valueProvider, bindingContext.ModelName);
      return base.BindModel(controllerContext, bindingContext);
    }

    public const string OriginalNameSufix = "original";
    public const string OriginalNameSufixWithSeperator = "." + OriginalNameSufix;

    public static readonly List<string> Entity2Exclude =
      new List<string>(new[]
      {
        "CustomPropertiesOfType", "FieldsCustomPropertiesOfType", "Validator", "AuthorizerToUse", "AuditorToUse", "Fields", "Transaction", "ConcurrencyPredicateFactoryToUse",
        "TypeDefaultValueProviderToUse", "PrimaryKeyFields",
        "LLBLGenProEntityTypeValue", "LLBLGenProEntityName", "ActiveContext", "IsDirty", "IsNew", "ObjectID", "ParticipatesInTransaction", "IsDeserializing", 
        "EntityTypesToCascadeDelete", "FieldsThatHaveADefault ", "Identity ", "EntityName ", "FixedEntityName ", "EntityNameAndValue ", "EndUserId ", "PkId ", "IsInError "
      });

    public static readonly List<string> _entityExclude =
      new List<string>(new[]
      {
        "CustomPropertiesOfType", "FieldsCustomPropertiesOfType", "Validator", "AuthorizerToUse", "AuditorToUse", "Fields", "Transaction", "ConcurrencyPredicateFactoryToUse",
        "TypeDefaultValueProviderToUse", "PrimaryKeyFields",
        "LLBLGenProEntityTypeValue", "LLBLGenProEntityName", "ActiveContext", "IsDirty", "IsNew", "ObjectID", "ParticipatesInTransaction", "IsDeserializing", "IsSerializing"
      });

    private static readonly Dictionary<Type, PropertyDescriptorCollection> EntityPropertyDescriptorCache = new Dictionary<Type, PropertyDescriptorCollection>();

    protected override PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var entity = bindingContext.Model as IEntityCore;
      if (entity == null) return base.GetModelProperties(controllerContext, bindingContext);

      PropertyDescriptorCollection result;
      lock (EntityPropertyDescriptorCache)
      {
        if (!EntityPropertyDescriptorCache.TryGetValue(entity.GetType(), out result))
        {
          var excludelist = entity is IEntity2 ? Entity2Exclude : _entityExclude;
          var properties = GetTypeDescriptor(controllerContext, bindingContext).GetProperties();
          result =
            new PropertyDescriptorCollection(
              properties.Cast<PropertyDescriptor>()
                .Where(
                  property =>
                    ((!excludelist.Contains(property.Name) && !property.Name.StartsWith("AlwaysFetch")) && !property.Name.StartsWith("AlreadyFetched")) &&
                    !property.Name.EndsWith("ReturnsNewIfNotFound"))
                .
                ToArray());
          EntityPropertyDescriptorCache.Add(entity.GetType(), result);
        }
      }
      return result;
    }

    protected override bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var result = base.OnModelUpdating(controllerContext, bindingContext);
      if (result)
      {
        var entity = bindingContext.Model as IEntityCore;
        if (entity != null)
        {
          foreach (var field in entity.Fields)
          {
            var value = bindingContext.ValueProvider.GetValue((bindingContext.ModelName + "." + field.Name + OriginalNameSufixWithSeperator).Trim('.', ' '));
            if (value == null && bindingContext.FallbackToEmptyPrefix)
              value = bindingContext.ValueProvider.GetValue(field.Name + OriginalNameSufixWithSeperator);
            if (value != null)
            {
              if (!entity.CustomPropertiesOfType.ContainsKey("UsingModelState"))
                entity.CustomPropertiesOfType.Add("UsingModelState", "true");
              //field.ForcedCurrentValueWrite(field.CurrentValue, value.ConvertTo(MetaDataHelper.GetUnderlyingTypeIfNullable(field.DataType)));
              field.ForcedCurrentValueWrite(field.CurrentValue, value.ConvertTo(field.DataType));
              field.IsChanged = false;
              //bindingContext.ValueProvider.Remove(originalField.Key);
            }
          }

          entity.IsDeserializing = true;
        }
      }
      return result;
    }

    protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
    {
      // Validate using DataAnnotations (not required in MVC2)

      foreach (var attr in MetaDataHelper.GetValidationAttributes(bindingContext.ModelType, propertyDescriptor.Name).Where(attr => !attr.IsValid(value)))
        bindingContext.ModelState.AddModelError(propertyDescriptor.Name, attr.FormatErrorMessage(propertyDescriptor.DisplayName));
      return base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
    }

    protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
    {
      var entity = bindingContext.Model as IEntityCore;
      if (entity == null)
      {
        base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        return;
      }

      var pk = entity.PrimaryKeyFields.OfType<EntityFieldCore>().FirstOrDefault(field => field.Name == propertyDescriptor.Name);
      var fieldkey = StringHelper.Join(".", bindingContext.ModelName, propertyDescriptor.Name);
      var fieldValue = bindingContext.ValueProvider.GetValue(fieldkey);
      // If it's a string and the value exists in the value provider, we want to set the value to empty string, not null
      // If we're setting PkId, only do so if we have something to set (ie: not an empty string)
      if (fieldValue != null && propertyDescriptor.PropertyType == typeof (string) && propertyDescriptor.Name != "PkId")
        value = value ?? fieldValue.AttemptedValue;

      if (pk == null && value != null && !(propertyDescriptor.Name == "PkId" && value == String.Empty))
      {
        base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        // Find original
        var originalFieldKey = StringHelper.Join(".", bindingContext.ModelName, propertyDescriptor.Name, OriginalNameSufix);
        var originalValue = bindingContext.ValueProvider.GetValue(originalFieldKey);
        if (originalValue != null)
        {
          var field = entity.GetFieldByName(propertyDescriptor.Name);
          if (field != null)
          {
            field.ForcedCurrentValueWrite(field.CurrentValue, originalValue.ConvertTo(field.DataType));
            if (GeneralHelper.IsEqualivalent(field.CurrentValue, field.DbValue))
              field.IsChanged = false;
            else
            {
              ;
            }
          }
        }
      }
      else if (EntityHelper.ValidKeyValue(value))
      {
        pk.ForcedCurrentValueWrite(value, value);
        //pk.CurrentValue = value;
        if (entity.IsNew)
          entity.IsNew = !EntityHelper.PrimaryKeyFieldsAreValid(((List<IEntityField2>) entity.PrimaryKeyFields));
      }
      /*
      var field = entity.Fields.OfType<EntityFieldCore>().FirstOrDefault(efc => efc.Name == propertyDescriptor.Name);
      if (field == null)
      {
        base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
      }
      else if (!field.IsPrimaryKey)
      {
        base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
        if (value == null && !field.IsChanged)
        {
          field.IsChanged = true;
          entity.IsDirty = true;
        }
      }
      else if (ValidKeyValue(value))
      {
        field.ForcedCurrentValueWrite(value);
        entity.IsNew = false;
      }
       * */
    }

    protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      var entity = bindingContext.Model as IEntity2;
      if (entity != null)
      {
        entity.IsDeserializing = false;
        if (entity.IsNew)
          foreach (var primaryKeyField in entity.PrimaryKeyFields)
            primaryKeyField.IsChanged = true;
        else
        {
          if (entity.CustomPropertiesOfType.ContainsKey("UsingModelState") && entity.CustomPropertiesOfType["UsingModelState"] == "true")
          {
            var changedFields = entity.GetChangedFields();
            if (changedFields != null)
              foreach (var field in changedFields.Where(field => GeneralHelper.IsEqualivalent(field.CurrentValue, field.DbValue)))
                field.IsChanged = false;

            if (entity.IsDirty && entity.AuditorToUse != null)
              foreach (var changedField in entity.GetChangedFields())
                entity.AuditorToUse.AuditEntityFieldSet(entity, changedField.FieldIndex, changedField.DbValue);
          }
        }
      }

      base.OnModelUpdated(controllerContext, bindingContext);
    }

    protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
    {
      var overrideModelType = bindingContext.ValueProvider.GetValue(CreateSubPropertyName(bindingContext.ModelName, "OverrideModelType"));
      if (overrideModelType != null)
      {
        modelType = Type.GetType(overrideModelType.AttemptedValue);
        bindingContext.ModelMetadata = ModelSerializers.GetMetadataForType(modelType); //in theory a call to CreateModel, the model would always be null
      }

      return MarkAsFetched(base.CreateModel(controllerContext, bindingContext, modelType));
    }

    protected static object MarkAsFetched(object model)
    {
      var entity2 = model as IEntity2;
      if (entity2 != null) //&& !entity2.IsNew
        entity2.Fields.MarkAsFetched();
      return model;
    }

    /// <summary>
    ///   Makes the model errors from data error info in a model.
    /// </summary>
    /// <param name="controllerContext">The controller context.</param>
    /// <param name="bindingContext">The binding context.</param>
    /// <remarks>
    ///   Used for testing only
    /// </remarks>
    public void MakeModelErrorsFromDataErrorInfo(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
      Debug.Assert(bindingContext.Model != null);
      var properties = GetModelProperties(null, bindingContext);
      foreach (PropertyDescriptor propertyDescriptor in properties)
        OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, null);
      OnModelUpdated(controllerContext, bindingContext);
    }

    /// <summary>
    ///   Assigns the model. as can no longer do bindingContext.Model = model
    /// </summary>
    /// <param name="bindingContext">The binding context.</param>
    /// <param name="model">The model.</param>
    public static void AssignModel(ModelBindingContext bindingContext, object model)
    {
      if (model != null)
      {
        AssignModel(bindingContext, model.GetType(), model);
      }
    }

    internal static void AssignModel(ModelBindingContext bindingContext, Type modelType, object model)
    {
      AssignModelType(bindingContext, modelType);
      bindingContext.ModelMetadata.Model = model;
    }

    internal static void AssignModelType(ModelBindingContext bindingContext, Type modelType)
    {
      if (modelType != null)
        bindingContext.ModelMetadata = ModelSerializers.GetMetadataForType(modelType);
    }

    protected static bool ValidKeyValue(object o)
    {
      return !((o == null) ||
               (o is Int32 && (int) o <= 0) ||
               (o is string && string.IsNullOrEmpty((string) o)) ||
               (o is Guid && (Guid) o == Guid.Empty) ||
               (o == DBNull.Value) ||
               (o is Int16 && (short) o <= 0) ||
               (o is Int64 && (long) o <= 0) ||
               (o is DateTime && (DateTime) o == DateTime.MinValue) ||
               (o is Double && (double) o <= 0) ||
               (o is Decimal && (decimal) o <= 0) ||
               (o is Single && (float) o <= 0));
    }
  }
}

/*
This is a ModelBinder to use with the new ASP.NET MVC 2 framework. This will automatically create and populate LLBLGen objects from form post, query string and route data. Works with both SelfServicing and Adapter.

An overview of Model Binding in ASP.NET MVC can be found here: http://weblogs.asp.net/scottgu/archive/2008/09/02/asp-net-mvc-preview-5-and-form-posting-scenarios.aspx and here: http://weblogs.asp.net/scottgu/archive/2008/10/16/asp-net-mvc-beta-released.aspx#three

This implementation inherits from the provided DefaultModelBinder and defers most of the work to it. For non LLBLGen entities, they just pass through to the default implementation. This code is to help set the primary keys and stop infinite recursive Parent-Child-Parent calls.

Requirements
============
- LLBLGen Pro v2.6
- .NET 3.5 SP1
- ASP.NET MVC 2

Usage
=====
Add the LLBLGenModelBinder.cs to your project

In the Global.asax.cs, add this to the Application_Start():
ModelBinders.Binders.DefaultBinder = new LLBLGenModelBinder();

You will be able to write code like this and have the entities automatically populated from the request

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(CompanyEntity company)
        {
            if (ModelState.IsValid)
            {
                company.Save(true);
                return RedirectToAction("Edit", new { id = company.CompanyId });
            }
            else
            {
                return View(company);
            }
        }


How it works
============
In order for the Model Binding to find the correct fields, they need to be named correctly. 

For the top level object, the take the form parameterName.Field so: <%= Html.TextBox("company.CompanyId", ViewData.Model.CompanyId)%>

For 1-to-1 objects, they look like <%= Html.TextBox("company.NormalAddress.Address", ViewData.Model.NormalAddress.Address)%>

For 1-to-many it gets a little more complex:
The release behaviour changed from the beta. 
    The beta looked for a .index property and looped through them to fill the collection 
    the release method ONLY processes for sequential indexes starting with 0. 

I have implemented the beta behaviour if an index field exists, otherwise it passes through to the release method.
    If you do not want this behaviour at all, remove IndexModelBinder and change LLBLGenModelBinder to inherit from DefaultModelBinder
  NOTE: This will not automatically fall back to a blank model name for top level parameters, use a BindAttribute to specify a blank name
 
The release behaviour: Name your fields company.Contacts[0].FieldName, counting up from 0
    
    <% for (int i=0;i<Model.Contacts.Count;i++) 
        ContactEntity contact = Model.Contacts[i]; {%>
    <tr>
        <td>Contacts.ContactId</td>
        <td><%= Html.TextBox("company.Contacts[" + i + "].ContactId", contact.ContactId)%></td>
    </tr>
    <tr>
        <td>Contacts.FirstName</td>
        <td><%= Html.TextBox("company.Contacts[" + i + "].FirstName", contact.FirstName)%></td>
    </tr>
    <tr>
        <td>Contacts.LastName</td>
        <td><%= Html.TextBox("company.Contacts[" + i + "].LastName", contact.LastName)%></td>
    </tr>
    <% } %>
 
To add, you need to add a new set of fields numbered one higher.
 
Beta behaviour: looks for a field (.index) that holds the index values to iterate:
    
    <% foreach (ContactEntity contact in Model.Contacts) {%>
    <tr>
        <td>Contacts.index</td>
        <td><%= Html.TextBox("company.Contacts.index", contact.ContactId)%></td>
    </tr>
    <tr>
        <td>Contacts.ContactId</td>
        <td><%= Html.TextBox("company.Contacts[" + contact.ContactId + "].ContactId", contact.ContactId)%></td>
    </tr>
    <tr>
        <td>Contacts.FirstName</td>
        <td><%= Html.TextBox("company.Contacts[" + contact.ContactId + "].FirstName", contact.FirstName)%></td>
    </tr>
    <tr>
        <td>Contacts.LastName</td>
        <td><%= Html.TextBox("company.Contacts[" + contact.ContactId + "].LastName", contact.LastName)%></td>
    </tr>
    <% } %>

if you want to add a new object, create any unique index:
<%= Html.TextBox("company.Contacts.index", -1)%>
<%= Html.TextBox("company.Contacts[-1].ContactId", 0)%>
<%= Html.TextBox("company.Contacts[-1].FirstName")%>

NOTES
=====
To override the type created, output a property named OverrideModelType (i.e. contact.OverrideModelType and set the value to a valid type name, fully qualified -- EntityClass.GetType().AssemblyQualifiedName OR typeof(EntityClass).AssemblyQualifiedName).

The Model Binder sets IsNew if a primary key field is set to a meaningful value (for ints > 0, strings not blank, etc.). You can change this behaviour by providing your own form field (i.e. company.IsNew) AND removing the "IsNew" from the list of excluded fields in the LLBLGenModelBinder

This process "creates" child objects, it does not "syncronize" child objects. It can syncronize objects, but you need to use the UpdateModel method to manually walk the child elements of 1-to-many. In this case, DO NOT output the .index form element because they will be in the child collection twice.

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit2(int id)
        {
            CompanyEntity company = new CompanyEntity(id);
            UpdateModel(company, "company");
            foreach (ContactEntity contact in company.Contacts)
            {
                UpdateModel(contact, "company.Contacts[" + contact.ContactId + "]");
            }

            if (ModelState.IsValid)
            {
                company.Save(true);
                return RedirectToAction("Edit", new { id = company.CompanyId });
            }
            else
            {
                return View(company);
            }
        }
*/