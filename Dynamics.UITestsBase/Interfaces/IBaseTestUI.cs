using Dynamics.UITestsBase.ComponentHelper;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IBaseTestUI
    {
        IWebDriver GetWebDriver();
        XrmApp GetXrmApp();
        void Init();
    }
}