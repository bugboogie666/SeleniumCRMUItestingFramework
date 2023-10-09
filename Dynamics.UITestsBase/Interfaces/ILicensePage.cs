namespace Dynamics.UITestsBase.Interfaces
{
    internal interface ILicensePage
    {
        string Account { get; set; }
        string BasePricingDate { get; set; }
        string Currency { get; set; }
        string Description { get; set; }
        string ExpiresOn { get; set; }
        string LastUpgradeDate { get; set; }
        string LicenseType { get; set; }
        int LookupItemIndex { get; set; }
        string Name { get; set; }
        string Product { get; set; }
        string PurchaseDate { get; set; }
        string WithSourceCode { get; set; }

        string GetValueFromLicense(string propertyName);
    }
}