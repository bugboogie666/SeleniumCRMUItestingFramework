using System;
using System.ComponentModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Crm.Sdk.Messages;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class OpportunityPage : DynamicsPage, IOpportunityPage
    {
        public int LookupItemIndex { get; set; }


        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(50));
            }
        }


        #region Opportunity Sales Process fields
        public string WinReasons
        {
            get
            {
                return xrmApp.BusinessProcessFlow.GetValue(Opportunity.Fields.dyn_descriptioncloseopp);
            }
            set
            {
                xrmApp.BusinessProcessFlow.SetValue(Opportunity.Fields.dyn_descriptioncloseopp, value);
            }
        }


        public string OriginatingLeadBPF
        {
            get
            {
                return xrmApp.BusinessProcessFlow.GetValue(new LookupItem { Name = Opportunity.Fields.OriginatingLeadId });
            }
            set
            {
                xrmApp.BusinessProcessFlow.SetValue(new LookupItem { Name = Opportunity.Fields.OriginatingLeadId, Value = value, Index = LookupItemIndex });
            }
        }


        public string EstimateCloseDateBPF
        {
            get
            {
                return xrmApp.BusinessProcessFlow.GetValue(Opportunity.Fields.EstimatedCloseDate);
            }
            set
            {
                var dateTimeValue = value.Equals("<NOW>") ? DateTime.Now : ConvertDateFromUSFormat(value);
                xrmApp.BusinessProcessFlow.SetValue(Opportunity.Fields.EstimatedCloseDate, dateTimeValue);
            }
        }
        #endregion


        [Description("Name")]
        public string Name
        {
            get
            {
                return xrmApp.Entity.GetValue(Opportunity.Fields.Name);
            }
            set
            {
                xrmApp.Entity.SetValue(Opportunity.Fields.Name, value);
            }
        }


        public string ProductFamily
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = Opportunity.Fields.ken_productfamily });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = Opportunity.Fields.ken_productfamily, Value = value });
            }
        }


        public string Type
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = Opportunity.Fields.dyn_opportunitytype });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = Opportunity.Fields.dyn_opportunitytype, Value = value });
            }
        }


        public string OriginatingLead
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.OriginatingLeadId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.OriginatingLeadId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Potential Customer")]
        public string PotentialCustomer
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.ParentAccountId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.ParentAccountId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Contact")]
        public string Contact
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.ParentContactId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.ParentContactId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("License")]
        public string License
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.dyn_licenseID });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.dyn_licenseID, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Source")]
        public string Source
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = Opportunity.Fields.ken_Source });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = Opportunity.Fields.ken_Source, Value = value });
            }
        }


        [Description("Currency")]
        public string Currency
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.TransactionCurrencyId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.TransactionCurrencyId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Price List")]
        public string PriceList
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = Opportunity.Fields.PriceLevelId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = Opportunity.Fields.PriceLevelId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Owner")]
        public string Owner
        {
            get
            {
                return xrmApp.Entity.GetHeaderValue(new LookupItem { Name = Opportunity.Fields.OwnerId });
            }
            set
            {
                xrmApp.Entity.SetHeaderValue(new LookupItem { Name = Opportunity.Fields.OwnerId, Value = value, Index = LookupItemIndex });
            }
        }


        public string OpportunityProductsGrid => "opportunityproductsGrid";
        public IWebElement LicenseLookup => seleniumHelper.GetElement(By.XPath("//div[@data-id='dyn_licenseid.fieldControl-LookupResultsDropdown_dyn_licenseid_selected_tag_text']"),
           SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);


        public OpportunityPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
            LookupItemIndex = 0;
        }


        public OpportunityPage()
        {

        }


        public OpportunityPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        internal ProductPage AddProduct()
        {
            return Command<ProductPage>("Add product", OpportunityProductsGrid);
        }


        internal OrderPage NewOrder()
        {
            xrmApp.Entity.SelectTab("Orders");
            return Command<OrderPage>("New Order", "Orders");
        }


        public void SetDataForOpportunity(Dictionary<string, string> data)
        {
            foreach (var record in data)
            {
                PropertyInfo property = GetType().GetProperty(record.Key);
                property.SetValue(this, record.Value);
            }
        }


        public void ActivateStage(string stageName)
        {
            xrmApp.BusinessProcessFlow.SelectStage(stageName);
        }


        public void NextStage(string stageName)
        {
            xrmApp.BusinessProcessFlow.NextStage(stageName);
        }


        public void CloseStage(string stageName)
        {
            xrmApp.BusinessProcessFlow.Close(stageName);
        }


        public void CreateOfferLetter()
        {
            xrmApp.CommandBar.ClickCommand("Create Document", "Offer Letter");
        }
    }
}