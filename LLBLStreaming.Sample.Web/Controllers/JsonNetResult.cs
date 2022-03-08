using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LLBLStreaming.Sample.Web.Controllers
{
  /// <summary>
  ///   JsonNetResult for ASP.NET MVC - correct formatting of dates and camel cased properties Got most of the code from Stack Overflow.
  /// </summary>
  /// <Remarks>
  ///   https://gist.github.com/DavidDeSloovere/5689824
  ///   http://james.newtonking.com/archive/2008/10/16/asp-net-mvc-and-json-net
  ///   https://wingkaiwan.wordpress.com/2012/12/28/replacing-mvc-javascriptserializer-with-json-net-jsonserializer/
  /// </Remarks>
  public class JsonNetResult : JsonResult
  {
    static readonly JsonSerializerSettings JsonSerializerSettings;

    /// <summary>
    ///   Initializes the <see cref="JsonNetResult" /> class.
    /// </summary>
    static JsonNetResult()
    {
      JsonSerializerSettings = new JsonSerializerSettings
      {
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        ContractResolver = new DefaultContractResolver //CamelCasePropertyNamesContractResolver
        {
          IgnoreSerializableInterface = true,
          IgnoreSerializableAttribute = true
        },
        //DateFormatString = "yyyy-MM-ddTHH:mm:ss"
      };
      ////https://stackoverflow.com/questions/7427909/how-to-tell-json-net-globally-to-apply-the-stringenumconverter-to-all-enums
      //JsonSerializerSettings.Converters.Add(new StringEnumConverter());
    }

    public override void ExecuteResult(ControllerContext context)
    {
      if (context == null) throw new ArgumentNullException(nameof(context));

      var response = context.HttpContext.Response;
      if (!response.HeadersWritten)
        response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

      if (ContentEncoding != null)
        response.ContentEncoding = ContentEncoding;

      if (Data == null) return;

      var serializedObject = SerializedObject(Data, HttpContext.Current is { IsDebuggingEnabled: true } ? Formatting.Indented : Formatting.None);
      response.Write(serializedObject);
    }

    public static string SerializedObject(object value, Formatting formatting = Formatting.None)
    {
      return JsonConvert.SerializeObject(value, formatting, JsonSerializerSettings);
    }
  }
}