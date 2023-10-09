using System;
using System.Reflection;
using System.Security;

using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;
using log4net;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using OpenQA.Selenium;

namespace Dynamics.UITestsBase.ComponentHelper
{
    public class NavigationHelper : DynamicsUITest, INavigationHelper
    {
        private ITestBaseManager testBaseManager;
        private protected IWebDriver webDriver;
        private protected XrmApp xrmApp;
        //private static readonly ILog Logger = LogHelper.GetLogger(typeof(NavigationHelper));
        private ILogging logging;

        public NavigationHelper(ITestBaseManager testBaseManager, ILogging logging)
        {
            this.logging = logging;
            this.testBaseManager = testBaseManager;
            webDriver = testBaseManager.GetBaseTestUI().GetWebDriver();
            xrmApp = testBaseManager.GetBaseTestUI().GetXrmApp();
        }


        public void NavigateToUrl(string url)
        {
            logging.Info($"Navigating {url}", MethodBase.GetCurrentMethod().Name);
            webDriver.Navigate().GoToUrl(url);
        }


        public void LoginAndOpenDynamicsApp(string appName, Uri url, SecureString username, SecureString password)
        {
            logging.Info("oppening app: " + appName + " " + url, MethodBase.GetCurrentMethod().Name);
            xrmApp.OnlineLogin.Login(url, username, password);
            xrmApp.Navigation.OpenApp(appName);
        }


        public void NavigateToSubarea(string area, string subarea)
        {
            logging.Info("Navigating to subarea " + subarea, MethodBase.GetCurrentMethod().Name);
            xrmApp.Navigation.OpenSubArea(area, subarea);
            xrmApp.ThinkTime(1000);
        }


        public void SignOut()
        {
            logging.Info("Signing out", MethodBase.GetCurrentMethod().Name);
            xrmApp.Navigation.SignOut();
            xrmApp.ThinkTime(5000);
        }
    }
}
