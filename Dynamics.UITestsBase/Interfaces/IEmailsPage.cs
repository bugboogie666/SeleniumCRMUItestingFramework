using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface IEmailsPage
    {
        IWebElement ModifiedOnColumn { get; }
        IWebElement SortNewerToOlderButtom { get; }

        EmailPage OpenLastRecord(string viewName);
    }
}