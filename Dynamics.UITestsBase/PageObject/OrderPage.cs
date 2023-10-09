using System;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

[assembly: InternalsVisibleTo("Dynamics.UITests")]

namespace Dynamics.UITestsBase.PageObject
{
    public class OrderPage : DynamicsPage, IOrderPage
    {
        public int LookupItemIndex { get; set; }


        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(50));
            }
        }


        [Description("Name")]
        public string Name
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.Name);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrder.Fields.Name, value);
            }
        }


        [Description("Description")]
        public string Description
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.Description);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrder.Fields.Description, value);
            }
        }


        [Description("Total Line Amount")]
        public string DetailAmount
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.TotalLineItemAmount);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrder.Fields.TotalLineItemAmount, value);
            }
        }


        [Description("Price Indexation Type")]
        public string PriceIndexationType
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = SalesOrder.Fields.ken_PriceIndexationType });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = SalesOrder.Fields.ken_PriceIndexationType, Value = value });
            }
        }


        [Description("Moved From")]
        public string MovedFrom
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = SalesOrder.Fields.ken_MovedFrom });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = SalesOrder.Fields.ken_MovedFrom, Value = value });
            }
        }


        [Description("Deal")]
        public string DealId
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.dyn_dealid);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrder.Fields.dyn_dealid, value);
            }
        }


        [Description("Billing Office")]
        public string BillingOffice
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = SalesOrder.Fields.ken_billingofficetype });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = SalesOrder.Fields.ken_billingofficetype, Value = value });
            }
        }


        [Description("Status Reason")]
        public string StatusReason
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = SalesOrder.Fields.StatusCode });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = SalesOrder.Fields.StatusCode, Value = value });
            }
        }


        [Description("Currency")]
        public string Currency
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.TransactionCurrencyId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.TransactionCurrencyId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Price List")]
        public string Pricelevel
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.PriceLevelId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.PriceLevelId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Purchaser")]
        public string Purchaser
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.ken_purchaseraccountid });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.ken_purchaseraccountid, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Customer")]
        public string Customer
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.CustomerId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.CustomerId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Delivery Contact")]
        public string DeliveryContact
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.ken_DeliveryContact });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.ken_DeliveryContact, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Date of payment")]
        public string DateOfPayment
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.dyn_date);
            }
            set
            {
                //XrmApp.Entity.SetValue("dyn_date", value);
                xrmApp.Entity.SetValue(new DateTimeControl(SalesOrder.Fields.dyn_date) { DateAsString = value });
            }
        }


        [Description("License")]
        public string License
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrder.Fields.dyn_licenseID });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrder.Fields.dyn_licenseID, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Date Fulfilled")]
        public string DateFulfilled
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrder.Fields.DateFulfilled);
            }
            set
            {
                //XrmApp.Entity.SetValue("dyn_date", value);
                xrmApp.Entity.SetValue(new DateTimeControl(SalesOrder.Fields.DateFulfilled) { DateAsString = value });
            }
        }


        [Description("Is Completed")]
        public string IsCompleted
        {
            get
            {
                return xrmApp.Entity.GetValue(new BooleanItem { Name = SalesOrder.Fields.dyn_iscompleted }).ToString();
            }
            set
            {
                xrmApp.Entity.SetValue(new BooleanItem { Name = SalesOrder.Fields.dyn_iscompleted, Value = Convert.ToBoolean(value) });
            }
        }

        #region BySelector
        public By ByLicenseLocator => By.XPath("//div[@data-id='dyn_licenseid.fieldControl-LookupResultsDropdown_dyn_licenseid_selected_tag']");
        #endregion


        public string SalesOrderDetailsGrid => "salesorderdetailsGrid";
        public IWebElement OrderDetailGrid => seleniumHelper.GetElement(By.Id("dataSetRoot_salesorderdetailsGrid"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement ExpandIcon => seleniumHelper.GetElement(By.Id("expandIcon"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement ProcessConfirmButton => seleniumHelper.GetElement(By.XPath("//button[@data-id='confirmButton']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement OkButton => seleniumHelper.GetElement(By.XPath("//button[@data-id='okButton']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement LicenseLookup => seleniumHelper.GetElement(ByLicenseLocator, SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement OpportunityLookup => seleniumHelper.GetElement(By.XPath("//div[@data-id='opportunityid.fieldControl-LookupResultsDropdown_opportunityid_selected_tag']"),
            SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public List<IWebElement> OrderProducts => seleniumHelper.GetElements(By.CssSelector("div [col-id='priceperunit']")).ToList();
        public List<IWebElement> Emails => seleniumHelper.GetElements(By.XPath("//div[@col-id='subject']//a"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait).ToList();
        public List<IWebElement> EmailsByTitle => seleniumHelper.GetElements(By.XPath("//label[@aria-label='Email']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait).ToList();
        public List<IWebElement> Services => seleniumHelper.GetElements(By.XPath("//div[@col-id='ken_name']//a"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait).ToList();
        public IWebElement FirstRenewal => seleniumHelper.GetElement(By.Id("Input-Renewal-1"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement SecondRenewal => seleniumHelper.GetElement(By.Id("Input-Renewal-2"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement ThirdRenewal => seleniumHelper.GetElement(By.Id("Input-Renewal-3"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public List<IWebElement> InputRenewals => seleniumHelper.GetElements(By.XPath("//input[contains(@id,'Input-Renewal')]"), SeleniumHelper.ElementSyncCondition.Exists, Wait).ToList();
        public List<IWebElement> InputRenewalsLabels => seleniumHelper.GetElements(By.XPath("//div[@data-automation-key='column1'] // span[contains(text(),'Renewal')]"), SeleniumHelper.ElementSyncCondition.Exists, Wait).ToList();
        public IWebElement RemoveFirstRenewalButton => seleniumHelper.GetElement(By.Id("Btn-Remove-Renewal-1"), SeleniumHelper.ElementSyncCondition.ToBeClickable);
        public IWebElement RemoveSecondRenewalButton => seleniumHelper.GetElement(By.Id("Btn-Remove-Renewal-2"), SeleniumHelper.ElementSyncCondition.ToBeClickable);
        public IWebElement RemoveThirdRenewalButton => seleniumHelper.GetElement(By.Id("Btn-Remove-Renewal-3"), SeleniumHelper.ElementSyncCondition.ToBeClickable);
        public IWebElement SecondRenewalButton => seleniumHelper.GetElement(By.XPath("//button[@id='Btn-AddNewRenewalLine']//span[text()='Add 2nd Renewal'] "), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement ThirdRenewalButton => seleniumHelper.GetElement(By.XPath("//button[@id='Btn-AddNewRenewalLine']//span[text()='Add 3rd Renewal'] "), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement PriceIndexationData => seleniumHelper.GetElement(By.XPath("//div[@data-id='ken_priceindexationdata']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement ContractedErrorMessage => seleniumHelper.GetElement(By.XPath("//span[@data-id='ken_priceindexationdata-error-message']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public IWebElement ContractedWarningNotification => seleniumHelper.GetElement(By.XPath("//span[@data-id='warningNotification']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public IWebElement RenewalBasePrice => seleniumHelper.GetElement(By.Id("renewal_base_price"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement LicenseMaximumPercentage => seleniumHelper.GetElement(By.Id("license_maximum_percentage"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public List<IWebElement> AreYouSureValidations => seleniumHelper.GetElements(By.XPath("//button[contains(text(),'Are you sure about this?')]"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait).ToList();


        public OrderPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }

        public OrderPage()
        {
            LookupItemIndex = 0;
        }


        public OrderPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public ProductPage AddProduct()
        {
            return Command<ProductPage>("Add Product", SalesOrderDetailsGrid);
        }


        public void SwitchStatusReason(string statusReason)
        {
            ExpandIcon.Click();
            this.StatusReason = statusReason;
        }


        public void Process()
        {
            xrmApp.CommandBar.ClickCommand("Mark as processed");

            try
            {
                ProcessConfirmButton.Click();
                OkButton.Click();
            }
            catch (Exception ex)
            {
                OkButton.Click();
                Debug.WriteLine(ex.Message);
                throw new Exception("Order processing has been blocked by unexpected error");
            }
        }


        /** This method does not work properly
        internal ProductPage EditProduct2(int index)
        {
            //OrderDetailGrid.Click();
            XrmApp.Entity.SubGrid.OpenSubGridRecord(SalesOrderDetailsGrid, index);
            return new ProductPage();
        }
        **/

        public bool OrderPageIsLoaded()
        {
            return seleniumHelper.PageIsLoaded(Wait);
        }


        public void SetDataForOrder(Dictionary<string, string> data)
        {
            foreach (var record in data)
            {
                PropertyInfo property = GetType().GetProperty(record.Key);
                property.SetValue(this, record.Value);
            }
        }


        public bool LicenseIsBound()
        {
            return seleniumHelper.IsElementPresent(ByLicenseLocator, SeleniumHelper.ElementSyncCondition.Exists, Wait);
        }
    }
}
