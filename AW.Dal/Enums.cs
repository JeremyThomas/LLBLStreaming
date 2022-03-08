using System.ComponentModel;

namespace AW.Data
{
  public enum ContactType : byte
    {
      AccountingManager = 1,
      AssistantSalesAgent = 2,
      AssistantSalesRepresentative = 3,
      CoordinatorForeignMarkets = 4,
      ExportAdministrator = 5,
      InternationalMarketingManager = 6,
      MarketingAssistant = 7,
      MarketingManager = 8,
      MarketingRepresentative = 9,
      OrderAdministrator = 10,
      Owner = 11,
      OwnerMarketingAssistant = 12,
      ProductManager = 13,
      PurchasingAgent = 14,
      PurchasingManager = 15,
      RegionalAccountRepresentative = 16,
      SalesAgent = 17,
      SalesAssociate = 18,
      SalesManager = 19,
      SalesRepresentative = 20,
    }

    [TypeConverter(typeof (AW.Helper.TypeConverters.HumanizedEnumConverter))]
    public enum AddressType : byte
    {
      Billing = 1,
      Home = 2,
      MainOffice = 3,
      Primary = 4,
      Shipping = 5,
      Archive = 6
    };

    public enum OrderStatus : byte
    {
      InProcess = 1,
      Approved,
      Backordered,
      Rejected,
      Shipped,
      Cancelled
    }

    public enum OrderPlacesBy : byte
    {
      SalesPerson,
      Customer
    }

    public enum MaritalStatus
    {
      [Description("Married")] M,
      [Description("Single")] S
    }

    public class MaritalStatusDBConverter : AW.Helper.TypeConverters.BaseEnumConverter<MaritalStatus>
    {
    }

    public enum Gender : byte
    {
      [Description("Male")] M,
      [Description("Female")] F
    }

    public class GenderDBConverter : AW.Helper.TypeConverters.BaseEnumConverter<Gender>
    {
    }

    /// <summary>
    /// For IsSalaried but not used
    /// </summary>
    public enum JobClassification : byte
    {
      Hourly,
      Salaried
    }

    /// <summary>
    /// For IsCurrent but not used
    /// </summary>
    public enum Current : byte
    {
      Inactive,
      Active
    }

    public enum PayFrequency : byte
    {
      Monthly = 1,
      BiWeekly
    }

    public enum NameStyle : byte
    {
      FirstLast,
      LastFirst
    }

    /// <summary>
    ///   0 = Contact does not wish to receive e-mail promotions,
    ///   1 = Contact does wish to receive e-mail promotions from AdventureWorks,
    ///   2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners.
    /// </summary>
    public enum EmailPromotion : byte
    {
      None,
      AWOnly,
      AWAndPartners
    }

    /// <summary>
    ///   0 = StateProvinceCode exists. 1 = StateProvinceCode unavailable, using CountryRegionCode.
    /// </summary>
    public enum StateProvinceCodeExistence : byte
    {
      Exists,
      Unavailable
    }

    /// <summary>
    ///   1 = Pending approval, 2 = Approved, 3 = Obsolete
    /// </summary>
    public enum ProductMaintenanceDocumentStatus : byte
    {
      PendingApproval = 1,
      Approved,
      Obsolete
    }

    /// <summary>
    ///   0 = Product is purchased, 1 = Product is manufactured in-house.
    /// </summary>
    public enum ProductMake : byte
    {
      Purchased,
      InHouse
    }

    /// <summary>
    ///   0 = Product is not a salable item. 1 = Product is salable.
    /// </summary>
    public enum ProductFinished : byte
    {
      Salable,
      NotSalable
    }

    /// <summary>
    ///   R = Road, M = Mountain, T = Touring, S = Standard
    /// </summary>
    public enum ProductLine : byte
    {
      [Description("Road")] R,
      [Description("Mountain")] M,
      [Description("Touring")] T,
      [Description("Standard")] S
    }

    public class ProductLineDBConverter : AW.Helper.TypeConverters.BaseEnumConverter<ProductLine>
    {
    }

    /// <summary>
    ///   H = High, M = Medium, L = Low
    /// </summary>
    public enum ProductClass : byte
    {
      [Description("High")] H,
      [Description("Medium")] M,
      [Description("Low")] L
    }

    public class ProductClassDBConverter : AW.Helper.TypeConverters.BaseEnumConverter<ProductClass>
    {
    }

    /// <summary>
    ///   W = Womens, M = Mens, U = Universal
    /// </summary>
    public enum ProductStyle : byte
    {
      [Description("Womens")] W,
      [Description("Mens")] M,
      [Description("Universal")] U
    }

    public class ProductStyleDBConverter : AW.Helper.TypeConverters.BaseEnumConverter<ProductStyle>
    {
    }

    public enum PurchaseOrderStatus : byte
    {
      Pending = 1,
      Approved,
      Rejected,
      Complete,
    }

    public enum CreditRating : byte
    {
      Superior = 1,
      Excellent,
      AboveAverage,
      Average,
      BelowAverage,
    }

    public enum TaxApplication : byte
    {
      Retail = 1,
      Wholesale,
      All,
    }
}