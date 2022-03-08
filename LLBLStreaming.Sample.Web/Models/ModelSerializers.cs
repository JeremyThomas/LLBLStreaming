using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web.Mvc;
using AW.Dal.EntityClasses;
using AW.Helper;
using AW.Helper.LLBL;
using Newtonsoft.Json;
using SD.LLBLGen.Pro.ORMSupportClasses;

namespace AQDPortal.Helpers.Models.ModelSerializer
{
  /// <summary>
  ///   Provides global access to the ModelSerializers used by the application
  /// </summary>
  public static class ModelSerializers
  {
    /// <summary>
    ///   id
    /// </summary>
    public const string IdKey = "id";

    /// <summary>
    ///   PkId
    /// </summary>
    public const string PkIdKey = "PkId";

    /// <summary>
    ///   EntityType
    /// </summary>
    public const string EntitytypeKey = "EntityType";

    /// <summary>
    ///   TypeGroup
    /// </summary>
    public const string TypeGroup = "TypeGroup";

    /// <summary>
    ///   WRType
    /// </summary>
    public const string WRType = "WRType";

    /// <summary>
    ///   formId
    /// </summary>
    public const string FormidKey = "formId";

    /// <summary>
    ///   .original
    /// </summary>
    public const string OriginalKeySuffix = ".original";

    #region Entity

    /// <summary>
    ///   Serializes the pk id. '/' aren't allowed in URL so must substitute with another character
    /// </summary>
    /// <param name="pkId">The pk id.</param>
    /// <returns></returns>
    public static string SerializePkId(string pkId)
    {
      return pkId;
    }

    private static string DeSerializePkId(string pkId)
    {
      return pkId;
    }

    #endregion

    public static ModelMetadata GetMetadataForType<T>()
    {
      return GetMetadataForType(typeof(T));
    }

    /// <summary>Gets metadata for the specified model accessor and model type.</summary>
    /// <returns>A <see cref="T:System.Web.Mvc.ModelMetadata" /> object for the specified model accessor and model type.</returns>
    /// <param name="modelType">The type of the model.</param>
    public static ModelMetadata GetMetadataForType(Type modelType)
    {
      return ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
    }

    public static ModelMetadata GetMetadataForType(object model)
    {
      return ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
    }

    public static IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadataForType, ControllerContext controllerContext)
    {
      if (controllerContext == null)
        controllerContext = new ControllerContext();
      var modelValidators = metadataForType.GetValidators(controllerContext);
      return modelValidators.SelectMany(v => v.GetClientValidationRules());
    }
    
  }
}