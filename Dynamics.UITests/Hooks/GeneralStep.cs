using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.PageObject;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace Dynamics.UITests.Hooks
{
    [Binding]
    public class GeneralStep
    {
        public ILogging logging;
        public ITestBaseManager testBaseManager;
        public ISeleniumHelper seleniumHelper;
        public INavigationHelper navigationHelper;
        public ScenarioContext scenarioContext;

        public GeneralStep()
        {
            logging = DynamicsUITest.ServiceProvider.GetRequiredService<ILogging>();
            testBaseManager = DynamicsUITest.ServiceProvider.GetRequiredService<ITestBaseManager>();
            seleniumHelper = DynamicsUITest.ServiceProvider.GetRequiredService<ISeleniumHelper>();
            navigationHelper = DynamicsUITest.ServiceProvider.GetRequiredService<INavigationHelper>();
        }

        [BeforeScenario]
        public void SetUp(ScenarioContext scenarioContext)
        {
            var url = DynamicsUITest.Settings.Reader.GetCrmUrl();
            var password = DynamicsUITest.Settings.Reader.GetCrmPassword();
            var username = DynamicsUITest.Settings.Reader.GetCrmUsername();

            navigationHelper.LoginAndOpenDynamicsApp("Kentico CRM App", url, username, password);

            this.scenarioContext = scenarioContext;

            logging.Info("Creating scenario node for HTML report.");
            testBaseManager.GetBaseTest().GetExtentReport().CreateScenario(scenarioContext.ScenarioInfo.Title);
            testBaseManager.GetBaseTestUI().SetScenarioContext(scenarioContext);
        }


        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            if(scenarioContext.TestError == null)
            {
                testBaseManager.GetBaseTest().GetExtentReport().Pass(scenarioContext.StepContext.StepInfo.Text);
            }
            else
            {
                testBaseManager.GetBaseTest().GetExtentReport().Fail(scenarioContext.StepContext.StepInfo.Text);
            }
        }


        [AfterScenario]
        public void TearDown()
        {
            var extendReport = DynamicsUITest.ServiceProvider.GetRequiredService<IExtentReport>();
            logging.Info("FLushing extend report;");
            extendReport.FlushExtendReport();
            seleniumHelper.InitializeNewBrowserTab();
            navigationHelper.SignOut();
        }
    }
}
