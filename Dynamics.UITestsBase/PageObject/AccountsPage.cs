using System;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class AccountsPage : DynamicsPage, IAccountsPage
    {
        public WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            }
        }


        public AccountsPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }


        public AccountsPage()
        {
        }

        public AccountsPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void Open()
        {
            navigationHelper.NavigateToSubarea("Customers", "Accounts");
            // sometimes we need to handle situation according to discard changes message box. For example when page is not saved.
            if (seleniumHelper.IsElementPresent(By.XPath("//button[@data-id='cancelButton']")))
                seleniumHelper.GetElement(By.XPath("//button[@data-id='cancelButton']")).Click();
        }


        public AccountPage NewAccount()
        {
            return Command<AccountPage>("New");
        }


        public AccountPage OpenAccountById(Guid accountID)
        {
            return OpenEntityById<AccountPage>("account", accountID);
        }
    }
}
