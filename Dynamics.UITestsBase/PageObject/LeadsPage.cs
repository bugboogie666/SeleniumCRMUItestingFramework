using Dynamics.UITestsBase.ComponentHelper;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class LeadsPage : DynamicsPage, ILeadsPage
    {
        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            }
        }


        public LeadsPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
        }


        public LeadsPage()
        {

        }


        public LeadsPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void Open()
        {
            navigationHelper.NavigateToSubarea("Sales", "Leads");
            // sometimes we need to handle situation according to discard changes message box. For example when page is not saved.
            if (seleniumHelper.IsElementPresent(By.XPath("//button[@data-id='cancelButton']")))
                seleniumHelper.GetElement(By.XPath("//button[@data-id='cancelButton']")).Click();
        }


        public LeadPage NewLead()
        {
            return Command<LeadPage>("New");
        }


        public LeadPage OpenLeadById(Guid leadID)
        {
            return OpenEntityById<LeadPage>("lead", leadID);
        }
    }
}
