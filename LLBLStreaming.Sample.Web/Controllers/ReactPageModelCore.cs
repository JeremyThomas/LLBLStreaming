using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AW.Helper.Annotations;

namespace LLBLStreaming.Sample.Web.Controllers
{
  public class ReactPageModelCore
  {
    public string ViewTitle { get; set; }

    public string DefaultCulture { get; set; }

    public UrlDictionary Urls { get; set; }

    public ReactPageModelCore(string viewTitle = null)
    {
      ViewTitle = viewTitle;
    }

  }

  public interface IReactPageModelCore
  {
    string ViewTitle { get; set; }
    string DefaultCulture { get; set; }
    UrlDictionary Urls { get; set; }
  }

  public interface IReactPageModelLite : IReactPageModelCore
  {
    string SaveUrl { get; set; }
  }

  public interface IReactPageModelBase : IReactPageModelLite
  {
    string ReactComponentName { get; set; }
    string RefreshUrl { get; set; }
    string GetWorkflowUrl { get; set; }
    string GetQuicklinksUrl { get; set; }
    string ViewTitle { get; set; }
    string NavigationTitle { get; set; }
    bool HideSaveCancelButtons { get; set; }
    string CancelUrl { get; set; }
  }

  public class ReactLayoutModel
  {
    public ReactLayoutModel(string componentName, object model = null, string workflowUrl = null, string quicklinksUrl = null, object settingsPageModel = null, string workflowComponentName = null)
    {
      PrimaryComponentName = componentName;
      PrimaryComponentPageModel = model;
      WorkflowUrl = workflowUrl;
      QuicklinksUrl = quicklinksUrl;
      SettingsPageModel = settingsPageModel;
      // Not explicitly hidden and model.SaveUrl is not empty

      WorkflowComponentName = workflowComponentName;
    }

    public object SettingsPageModel { get; set; }



    public string ModelType => "ReactLayoutModel";
    public string PrimaryComponentName { get; set; }
    public object PrimaryComponentPageModel { get; set; }
    public string ComponentName => PrimaryComponentName;
    public object ComponentPageModel => PrimaryComponentPageModel;
    public string WorkflowComponentName { get; set; }
    public string WorkflowUrl { get; set; }
    public string QuicklinksUrl { get; set; }
    public bool ShowSaveCancelButtons { get; set; }
  }

  [Serializable]
  public class UrlDictionary : Dictionary<string, string>
  {
    readonly UrlHelper _urlHelper;

    public UrlDictionary()
    {
    }

    public UrlDictionary(IDictionary<string, string> dictionary) : base(dictionary)
    {
    }

    public UrlDictionary(UrlHelper urlHelper) : this()
    {
      _urlHelper = urlHelper;
    }

    public void Add(Enum key, string url)
    {
      Add(key.ToString(), url);
    }

    public void Add(UrlHelper url, [AspMvcAction] string actionName, [AspMvcController] string controllerName)
    {
      Add(actionName, url.Action(actionName, controllerName));
    }

    public void Add(UrlHelper url, [AspMvcAction] string actionName, [AspMvcController] string controllerName, object args)
    {
      Add(actionName, url.Action(actionName, controllerName, args));
    }
  }
}