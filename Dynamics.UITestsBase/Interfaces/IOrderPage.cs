using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IOrderPage
    {
        List<IWebElement> AreYouSureValidations { get; }
        string BillingOffice { get; set; }
        By ByLicenseLocator { get; }
        IWebElement ContractedErrorMessage { get; }
        IWebElement ContractedWarningNotification { get; }
        string Currency { get; set; }
        string Customer { get; set; }
        string DateFulfilled { get; set; }
        string DateOfPayment { get; set; }
        string DealId { get; set; }
        string DeliveryContact { get; set; }
        string Description { get; set; }
        string DetailAmount { get; set; }
        List<IWebElement> Emails { get; }
        List<IWebElement> EmailsByTitle { get; }
        IWebElement ExpandIcon { get; }
        IWebElement FirstRenewal { get; }
        List<IWebElement> InputRenewals { get; }
        List<IWebElement> InputRenewalsLabels { get; }
        string IsCompleted { get; set; }
        string License { get; set; }
        IWebElement LicenseLookup { get; }
        IWebElement LicenseMaximumPercentage { get; }
        int LookupItemIndex { get; set; }
        string MovedFrom { get; set; }
        string Name { get; set; }
        IWebElement OkButton { get; }
        IWebElement OpportunityLookup { get; }
        IWebElement OrderDetailGrid { get; }
        List<IWebElement> OrderProducts { get; }
        IWebElement PriceIndexationData { get; }
        string PriceIndexationType { get; set; }
        string Pricelevel { get; set; }
        IWebElement ProcessConfirmButton { get; }
        string Purchaser { get; set; }
        IWebElement RemoveFirstRenewalButton { get; }
        IWebElement RemoveSecondRenewalButton { get; }
        IWebElement RemoveThirdRenewalButton { get; }
        IWebElement RenewalBasePrice { get; }
        string SalesOrderDetailsGrid { get; }
        IWebElement SecondRenewal { get; }
        IWebElement SecondRenewalButton { get; }
        List<IWebElement> Services { get; }
        string StatusReason { get; set; }
        IWebElement ThirdRenewal { get; }
        IWebElement ThirdRenewalButton { get; }
        WebDriverWait Wait { get; }

        ProductPage AddProduct();
        bool LicenseIsBound();
        bool OrderPageIsLoaded();
        void Process();
        void SetDataForOrder(Dictionary<string, string> data);
        void SwitchStatusReason(string statusReason);
    }
}