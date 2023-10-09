using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class EmailsPage : DynamicsPage, IEmailsPage
    {
        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(50));
            }
        }


        public IWebElement ModifiedOnColumn => seleniumHelper.GetElement(By.XPath("//label[@title='Modified On']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement SortNewerToOlderButtom => seleniumHelper.GetElement(By.XPath("//span[text()='Sort newer to older']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);

        internal void Open()
        {
            navigationHelper.NavigateToSubarea("Activities", "Email Messages");
            if (seleniumHelper.IsElementPresent(By.XPath("//button[@data-id='cancelButton']")))
                seleniumHelper.GetElement(By.XPath("//button[@data-id='cancelButton']")).Click();
        }

        public EmailsPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }


        public EmailsPage()
        {

        }


        public EmailsPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public EmailPage OpenLastRecord(string viewName)
        {
            xrmApp.Grid.SwitchView(viewName);
            //XrmApp.Grid.Sort("Modified On",, "Sort newer to older");
            ModifiedOnColumn.Click();
            SortNewerToOlderButtom.Click();
            xrmApp.Grid.OpenRecord(0);
            return new EmailPage();
        }
    }
}
