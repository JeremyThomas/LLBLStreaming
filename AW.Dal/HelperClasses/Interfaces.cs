namespace AW.Dal.HelperClasses
{
  public interface IModifiedTracking
  {
    System.DateTime ModifiedDate { get; set; }
  }

  public interface IMergable : IModifiedTracking
  {
    System.Guid Rowguid { get; set; }
  }

}
