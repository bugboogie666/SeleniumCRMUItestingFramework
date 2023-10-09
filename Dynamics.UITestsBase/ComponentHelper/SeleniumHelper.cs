using System;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;

using Dynamics.UITestsBase.BaseClasses;

using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using log4net;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Dynamics.UITestsBase.ComponentHelper
{
    public class SeleniumHelper : DynamicsUITest, ISeleniumHelper
    {
        private readonly IJavaScriptExecutor js;
        //private static readonly ILog Logger = LogHelper.GetLogger(typeof(SeleniumHelper));
        public ILogging logging;
        private static string _error;
        private IWebDriver webDriver;
        private ITestBaseManager testBaseManager;


        public enum ElementSyncCondition
        {
            ToBeClickable,
            ToBeVisible,
            Exists,
        }


        public SeleniumHelper(ITestBaseManager testBaseManager, ILogging logging)
        {
            this.testBaseManager = testBaseManager;
            webDriver = this.testBaseManager.GetBaseTestUI().GetWebDriver();
            js = (IJavaScriptExecutor)webDriver;
            this.logging = logging;
        }


        public bool IsElementPresent(By locator, ElementSyncCondition? condition = null, WebDriverWait wait = null)
        {
            try
            {
                if (wait != null)
                {
                    switch (condition)
                    {
                        case ElementSyncCondition.ToBeClickable:
                            logging.Info($"Waiting until element {locator} is clickable.", MethodBase.GetCurrentMethod().Name);
                            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                            break;
                        case ElementSyncCondition.ToBeVisible:
                            logging.Info($"Waiting until element {locator} is visible.", MethodBase.GetCurrentMethod().Name);
                            wait.Until(ExpectedConditions.ElementIsVisible(locator));
                            break;
                        case ElementSyncCondition.Exists:
                            logging.Info($"Waiting until element {locator} exists.", MethodBase.GetCurrentMethod().Name);
                            wait.Until(ExpectedConditions.ElementExists(locator));
                            break;
                    }
                }

                return webDriver.FindElements(locator).Count > 0;
            }
            catch (Exception ex)
            {
                logging.Error(ex.StackTrace);
                _error = ex.Message;
                TakeScreenShot();
                return false;
            }
        }


        public IWebElement GetElement(By locator, ElementSyncCondition? condition = null, WebDriverWait wait = null)
        {
            if (IsElementPresent(locator, condition, wait))
            {
                logging.Info($"Returning located element: {locator}", MethodBase.GetCurrentMethod().Name);
                TakeScreenShot();
                return webDriver.FindElement(locator);
            }
            else
            {
                logging.Error($"Element not present: {locator} message: {_error}");
                throw new NoSuchElementException($"No element is present when finding by: {locator}");
            }
        }


        public IEnumerable<IWebElement> GetElements(By locator, ElementSyncCondition? condition = null, WebDriverWait wait = null)
        {
            if (IsElementPresent(locator, condition, wait))
            {
                logging.Info($"Returning located elements: {locator}", MethodBase.GetCurrentMethod().Name);
                return webDriver.FindElements(locator);
            }
            else
            {
                logging.Error($"Element not present: {locator} message: {_error}");
                throw new NoSuchElementException($"No element is present when finding by: {locator}");
            }
        }


        /// <summary>
        /// This method check for element is displayed on the form
        /// </summary>
        /// <param name="element">webelement</param>
        /// <returns>true/false based on reuslt</returns>
        public static bool CheckFieldIsDisplayed(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException(ex.Message);
            }
        }


        public bool PageIsLoaded(WebDriverWait wait)
        {
            var loaded = wait.Until(d => (IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete");
            return loaded;
        }


        public void TakeScreenShot(string filename = null)
        {
            var screen = webDriver.TakeScreenshot();
            filename = filename ?? "screen_" + testBaseManager.GetBaseTestUI().GetScenarioContext().ScenarioInfo.Tags[0];
            var executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.CreateDirectory($"{executingLocation}\\screens\\");
            var folder = $"{executingLocation}\\screens\\"; //"C:\\DEV\\internalUITests\\Dynamics.UITests\\bin\\Debug\\net472\\images\\";
            var name = $"{folder}{filename}_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.jpeg";
            screen.SaveAsFile(name);
            logging.Info($"File {name} saved.", MethodBase.GetCurrentMethod().Name);
        }


        public void SetElementValue(IWebElement element = null, string value = null)
        {
            js.ExecuteScript($"document.getElementById(arguments[0]).value=arguments[1];", element.GetAttribute("id"), value);
        }


        public static void ClickTwice(IWebElement element)
        {
            element.Click();
            element.Click();
        }


        public void SendText(IWebElement element, string text)
        {
            element.SendKeys("");
            element.SendKeys(text);
        }


        public void ClearInput(IWebElement element)
        {
            Actions action = new Actions(webDriver);
            action.ContextClick(element).KeyDown(Keys.Control).SendKeys("A").SendKeys(Keys.Backspace).Perform();
        }


        public void CreateNewTab()
        {
            ((IJavaScriptExecutor)webDriver).ExecuteScript("window.open();");
        }


        public void PressEnter(IWebElement element)
        {
            Actions action = new Actions(webDriver);
            action.MoveToElement(element).SendKeys(Keys.Enter);
        }


        public void InitializeNewBrowserTab(string url = null)
        {
            logging.Info("Creating new browser tab.", MethodBase.GetCurrentMethod().Name);
            CreateNewTab();
            webDriver.SwitchTo().Window(webDriver.WindowHandles.First()).Close();
            webDriver.SwitchTo().Window(webDriver.WindowHandles.First());
            //var navigationHleper = ServiceProvider.GetRequiredService<INavigationHelper>();
            webDriver.Navigate().GoToUrl(url ?? Settings.Reader.GetCrmUrl().ToString());
        }
    }
}
