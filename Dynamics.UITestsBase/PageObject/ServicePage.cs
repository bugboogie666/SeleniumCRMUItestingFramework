using System;
using System.ComponentModel;
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
    public class ServicePage : DynamicsPage, IServicePage
    {
        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            }
        }


        [Description("Name")]
        public string Name
        {
            get
            {
                return xrmApp.Entity.GetValue(ken_service.Fields.ken_name);
            }
            set
            {
                xrmApp.Entity.SetValue(ken_service.Fields.ken_name, value);
            }
        }


        [Description("Status Reason")]
        public string StatusReason
        {
            get
            {
                return xrmApp.Entity.GetValue(new OptionSet { Name = ken_service.Fields.StatusCode });
            }
            set
            {
                xrmApp.Entity.SetValue(new OptionSet { Name = ken_service.Fields.StatusCode, Value = value });
            }
        }


        public bool IsPurchased => seleniumHelper.IsElementPresent(By.XPath("//div[text()='Purchased']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public bool IsUnpaid => seleniumHelper.IsElementPresent(By.XPath("//div[text()='Unpaid']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);


        public ServicePage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }

        public ServicePage()
        {

        }


        public ServicePage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper,ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }

    }
}
