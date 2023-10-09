using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.UITestsBase.PageObject
{
    public class LeadPage : DynamicsPage, ILeadPage
    {
        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            }
        }

        public IWebElement KenticoLinkFrame => seleniumHelper.GetElement(By.XPath("//iframe[@id='WebResource_lead_link_to_kentico_com']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement KlentyLinkFrame => seleniumHelper.GetElement(By.XPath("//iframe[@id='WebResource_lead_link_to_klenty']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement IntercomLinkFrame => seleniumHelper.GetElement(By.XPath("//iframe[@id='WebResource_lead_link_to_intercom']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);
        public IWebElement KenticoLink => seleniumHelper.GetElement(By.LinkText("Show lead's info from kentico.com"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public IWebElement KlentyLink => seleniumHelper.GetElement(By.LinkText("Show lead's info from Klenty"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);


        public LeadPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
        }


        public LeadPage()
        {
            LookupItemIndex = 0;
        }


        public LeadPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void GetKenticoLinkElement(out IWebElement link)
        {
            webDriver.SwitchTo().Frame(KenticoLinkFrame);
            link = KenticoLink;
        }


        public void GetKlentyLinkElement(out IWebElement link)
        {
            webDriver.SwitchTo().Frame(KlentyLinkFrame);
            link = KlentyLink;
        }


        public string GetLeadUrl()
        {
            return webDriver.Url;
        }
    }
}
