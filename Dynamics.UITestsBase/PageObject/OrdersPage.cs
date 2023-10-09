using System;
using System.Runtime.CompilerServices;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

[assembly: InternalsVisibleTo("Dynamics.UITests")]

namespace Dynamics.UITestsBase.PageObject
{
    public class OrdersPage : DynamicsPage, IOrdersPage
    {
        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            }
        }


        public OrdersPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }


        public OrdersPage()
        {

        }

        public OrdersPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void Open()
        {
            navigationHelper.NavigateToSubarea("Sales", "Orders");
            // sometimes we need to handle situation according to discard changes message box. For example when page is not saved.
            if (seleniumHelper.IsElementPresent(By.XPath("//button[@data-id='cancelButton']")))
                seleniumHelper.GetElement(By.XPath("//button[@data-id='cancelButton']")).Click();
        }


        public OrderPage NewOrder()
        {
            var order = Command<OrderPage>("New");
            SwitchToForm("Order_UCI");
            return order;
        }
    }
}