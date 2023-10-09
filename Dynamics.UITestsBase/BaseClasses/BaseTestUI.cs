using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Configuration;
using Dynamics.UITestsBase.DIContainer;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.Misc;
using Dynamics.UITestsBase.PageObject;
using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Tooling.Connector;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Dynamics.UITestsBase.BaseClasses
{
    public class BaseTestUI : BaseTest, IBaseTestUI
    {
        public WebClient Client { get; set; }
        private IWebDriver webDriver;
        private XrmApp xrmApp;
        private ITestSettings settings;
        private ITempValuesHolder tempValuesHolder;
        private IPageObjectWrapper pageObjectWrapper;
        private ICrmConnectionUtility crmConnectionUtility;
        private ScenarioContext scenarioContext;
        private ILogging logging;

        public BaseTestUI(ILogging logging ,ITestSettings settings, ITempValuesHolder tempValuesHolder, IPageObjectWrapper pageObjectWrapper, ICrmConnectionUtility crmConnectionUtility)
        {
            this.logging = logging;
            this.settings = settings;
            this.tempValuesHolder = tempValuesHolder;
            this.pageObjectWrapper = pageObjectWrapper;
            this.crmConnectionUtility = crmConnectionUtility;
            Init();
        }


        public void Init()
        {
            Client = new WebClient(settings.InitializeSettings());
            xrmApp = new XrmApp(Client);
            webDriver = Client?.Browser.Driver;
        }


        public XrmApp GetXrmApp()
        {
            if (xrmApp == null)
            {
                Init();
            }
            return xrmApp;
        }


        public IWebDriver GetWebDriver()
        {
            if (webDriver == null)
            {
                Init();
            }
            logging.Info("Getting driver");
            return webDriver;
        }


        public TempValuesHolder GetTempValuesHolder()
        {
            return (TempValuesHolder)tempValuesHolder;
        }


        public PageObjectWrapper GetPageObjectWrapper()
        {
            return (PageObjectWrapper)pageObjectWrapper;
        }


        public CrmServiceClient GetCrmService()
        {
            return crmConnectionUtility.GetCrmService();
        }


        public void SetScenarioContext(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }


        public ScenarioContext GetScenarioContext()
        {
            return scenarioContext;
        }

    }
}
