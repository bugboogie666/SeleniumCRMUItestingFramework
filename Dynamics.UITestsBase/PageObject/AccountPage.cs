using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Misc;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class AccountPage : DynamicsPage, IAccountPage
    {

        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            }
        }


        [Description("Account Name")]
        public string AccountName
        {
            get
            {
                return xrmApp.Entity.GetValue(Account.Fields.Name);
            }
            set
            {
                xrmApp.Entity.SetValue(Account.Fields.Name, value);
            }
        }


        [Description("Currency")]
        public string Currency
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Account.Fields.TransactionCurrencyId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Account.Fields.TransactionCurrencyId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Country")]
        public string Country
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Account.Fields.dyn_countryid });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Account.Fields.dyn_countryid, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("State")]
        public string State
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Account.Fields.dyn_stateid });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Account.Fields.dyn_stateid, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Primary Contact")]
        public string PrimaryContact
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Account.Fields.PrimaryContactId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Account.Fields.PrimaryContactId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Industry")]
        public string[] Industry
        {
            get
            {
                return xrmApp.Entity.GetValue(new MultiValueOptionSet { Name = Account.Fields.ken_industry }).Values;
            }
            set
            {
                //XrmApp.Entity.SetValue(new MultiValueOptionSet { Name = Account.Fields.ken_industry, Values = value });
                foreach (var industry in value)
                {
                    IndustryElement.SendKeys(industry);
                    seleniumHelper.GetElement(By.XPath($"//label[@title='{industry}']")).Click();
                    IndustryElement.Clear();
                }

            }
        }


        [Description("Status Reason")]
        public string StatusReason
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = Account.Fields.StatusCode });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = Account.Fields.StatusCode, Value = value });
            }
        }


        [Description("Account Type")]
        public string AccountType
        {
            get
            {
                return xrmApp.Entity.GetHeaderValue(new OptionSet { Name = Account.Fields.dyn_accounttype });
            }
            set
            {
                //XrmApp.Entity.SetValue(new OptionSet { Name = Account.Fields.dyn_accounttype, Value = value });
                xrmApp.Entity.SetHeaderValue(new OptionSet { Name = Account.Fields.dyn_accounttype, Value = value });
            }
        }


        public IWebElement ExpandIcon => seleniumHelper.GetElement(By.Id("expandIcon"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement IndustryElement => seleniumHelper.GetElement(By.XPath("//input[@id='ken_industry_ledit']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement PrimaryContactElement => seleniumHelper.GetElement(By.XPath("//input[@aria-label='Primary Contact, Lookup']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement NewContactElement => seleniumHelper.GetElement(By.XPath("//span[text()='New Contact']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);


        public AccountPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
            LookupItemIndex = 0;
        }


        public AccountPage()
        {

        }


        public AccountPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager,navigationHelper,seleniumHelper, logging)
        {

        }


        public void SetDataForAccount(Dictionary<string, string> data)
        {
            foreach (var record in data)
            {
                PropertyInfo property = GetType().GetProperty(record.Key);

                switch (Type.GetTypeCode(property.PropertyType))
                {
                    case TypeCode.String:
                        property.SetValue(this, record.Value);
                        break;
                    case TypeCode.Object:
                        if (property.PropertyType.IsArray)
                        {
                            property.SetValue(this, record.Value.Split(';'));
                        }
                        break;
                }
            }
        }


        public QuickCreateContactPage QuickCreateContact()
        {
            xrmApp.Navigation.QuickCreate("contact");
            return new QuickCreateContactPage();
        }


        public QuickCreateContactPage QuickCreateContactFromPrimaryContact()
        {
            PrimaryContactElement.Click();
            NewContactElement.Click();
            return new QuickCreateContactPage();
        }


        public void BuildRandomDataForAccount(Dictionary<string, string> data)
        {
            var accountDataHandler = new AccountRandomDataHandler(this, data);
            accountDataHandler.RandomizeData();
        }


        public OpportunityPage NewOpportunity()
        {
            xrmApp.Entity.SelectTab("Related", "Opps - Customer");
            return Command<OpportunityPage>("New Opportunity", RelatedGrid);
        }
    }
}
