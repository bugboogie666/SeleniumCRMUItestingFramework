using Dynamics.UITestsBase.ComponentHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ISeleniumHelper
    {
        void ClearInput(IWebElement element);
        void CreateNewTab();
        IWebElement GetElement(By locator, SeleniumHelper.ElementSyncCondition? condition = null, WebDriverWait wait = null);
        IEnumerable<IWebElement> GetElements(By locator, SeleniumHelper.ElementSyncCondition? condition = null, WebDriverWait wait = null);
        void InitializeNewBrowserTab(string url = null);
        bool IsElementPresent(By locator, SeleniumHelper.ElementSyncCondition? condition = null, WebDriverWait wait = null);
        void PressEnter(IWebElement element);
        void SendText(IWebElement element, string text);
        void SetElementValue(IWebElement element = null, string value = null);
        void TakeScreenShot(string filename = null);
        bool PageIsLoaded(WebDriverWait wait);
    }
}