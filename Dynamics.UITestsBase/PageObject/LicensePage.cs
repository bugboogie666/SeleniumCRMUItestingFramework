using System;
using System.Reflection;
using System.ComponentModel;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using OpenQA.Selenium;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{

    public class LicensePage : DynamicsPage, ILicensePage
    {
        public int LookupItemIndex { get; set; }

        public string Name
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_licensename);
            }
            set
            {
                xrmApp.Entity.SetValue(dyn_license.Fields.dyn_licensename, value);
            }
        }


        public string Account
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = dyn_license.Fields.dyn_accountid });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = dyn_license.Fields.dyn_accountid, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Currency")]
        public string Currency
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = dyn_license.Fields.TransactionCurrencyId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = dyn_license.Fields.TransactionCurrencyId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Description")]
        public string Description
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_description);
            }
            set
            {
                xrmApp.Entity.SetValue(dyn_license.Fields.dyn_description, value);
            }
        }


        public string ExpiresOn
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_expireson);
            }
            set
            {
                xrmApp.Entity.SetValue(new DateTimeControl(dyn_license.Fields.dyn_expireson) { DateAsString = value });
            }
        }


        [Description("Product")]
        public string Product
        {
            get
            {

                return xrmApp.Entity.GetValue(new LookupItem { Name = dyn_license.Fields.dyn_productid });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = dyn_license.Fields.dyn_productid, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("License Type")]
        public string LicenseType
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = dyn_license.Fields.dyn_licensetype });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = dyn_license.Fields.dyn_licensetype, Value = value });
            }
        }



        [Description("Purchase Date")]
        public string PurchaseDate
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_purchasedate);
            }
            set
            {
                xrmApp.Entity.SetValue(dyn_license.Fields.dyn_purchasedate, value);
                //XrmApp.Entity.SetValue(new DateTimeControl("dyn_date") { DateAsString = value });
            }
        }


        [Description("Base Pricing Date")]
        public string BasePricingDate
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_basepricingdate);
            }
            set
            {
                xrmApp.Entity.SetValue(dyn_license.Fields.dyn_basepricingdate, value);
                //XrmApp.Entity.SetValue(new DateTimeControl("dyn_date") { DateAsString = value });
            }
        }


        [Description("Last Upgrade Date")]
        public string LastUpgradeDate
        {
            get
            {
                return xrmApp.Entity.GetValue(dyn_license.Fields.dyn_lastupgradedate);
            }
            set
            {
                xrmApp.Entity.SetValue(dyn_license.Fields.dyn_lastupgradedate, value);
                //XrmApp.Entity.SetValue(new DateTimeControl("dyn_date") { DateAsString = value });
            }
        }


        [Description("With Source Code")]
        public string WithSourceCode
        {
            get
            {
                return xrmApp.Entity.GetValue(new BooleanItem { Name = dyn_license.Fields.dyn_withsourcecode }).ToString();
            }
            set
            {
                xrmApp.Entity.SetValue(new BooleanItem { Name = dyn_license.Fields.dyn_withsourcecode, Value = Convert.ToBoolean(value) });
            }
        }


        public LicensePage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
        }


        public LicensePage()
        {
            LookupItemIndex = 0;
        }


        public LicensePage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {
        }


        internal OrderPage NewOrder()
        {
            SelectRelatedEntityAndView("Related", "Orders");
            var order = Command<OrderPage>("New Order", RelatedGrid);
            order.SwitchToForm("Order_UCI");
            return order;
        }


        internal OpportunityPage NewOpportunity()
        {
            xrmApp.Entity.SelectTab("Related", "Opportunities");
            return Command<OpportunityPage>("New Opportunity", RelatedGrid);

        }


        public string GetValueFromLicense(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            return property.GetValue(this).ToString();
        }
    }
}