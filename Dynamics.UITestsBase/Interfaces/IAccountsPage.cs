using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium.Support.UI;
using System;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface IAccountsPage
    {
        WebDriverWait Wait { get; }

        AccountPage NewAccount();
        void Open();
        AccountPage OpenAccountById(Guid accountID);
    }
}