using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;

namespace Dynamics.UITestsBase.PageObject
{
    public class LicensesPage : DynamicsPage, ILicensesPage
    {

        public LicensesPage()
        {
        }


        public LicensesPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
        }


        public LicensesPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void Open()
        {
            navigationHelper.NavigateToSubarea("Licensing", "Licenses");
        }

    }
}