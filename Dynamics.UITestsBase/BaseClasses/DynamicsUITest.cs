using Dynamics.UITestsBase.Configuration;
using Dynamics.UITestsBase.DIContainer;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.PageObject;
using Dynamics.UITestsBase.Reporting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using TechTalk.SpecFlow;



namespace Dynamics.UITestsBase.BaseClasses
{
    [Binding]
    public class DynamicsUITest
    {
        public static ITestSettings Settings { get; set; }
        public static ITestBaseManager TestBaseManager { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        public static INavigationHelper NavigationHelper { get; set; }


        [BeforeTestRun]
        public static void InitDriverWithAdvancedSettings()
        {
            ServiceProvider = ContainerConfig.ConfigureService();
            Settings = ServiceProvider.GetRequiredService<ITestSettings>();
        }


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            BaseTestUI baseTestUI = (BaseTestUI)ServiceProvider.GetRequiredService<IBaseTestUI>();
            baseTestUI.SetContext(featureContext);

            TestBaseManager = ServiceProvider.GetRequiredService<ITestBaseManager>();
            TestBaseManager.SetBaseTest(baseTestUI);

            IExtentFeatureReport extentFeatureReport = ServiceProvider.GetRequiredService<IExtentFeatureReport>();
            TestBaseManager.GetBaseTest().SetExtentReport(extentFeatureReport);
        }


        [AfterFeature]
        public static void AfterFeature()
        {
            var logging = ServiceProvider.GetRequiredService<ILogging>();
            logging.Info("I am on closing driver after feature");
            TestBaseManager.GetBaseTestUI().GetWebDriver().Close();
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            var logging = ServiceProvider.GetRequiredService<ILogging>();
            logging.Info("I am on quiting driver after feature");
            TestBaseManager.GetBaseTestUI().GetWebDriver().Quit();
        }
    }
}
