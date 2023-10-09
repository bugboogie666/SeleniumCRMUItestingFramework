using System;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.PageObject;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Dynamics.UITests.LeadProcess.StepDefinition
{
    [Binding]
    public class TestLeadProcessingSteps
    {
        public ILogging logging;
        public ITestBaseManager testBaseManager;
        public ISeleniumHelper seleniumHelper;

        public TestLeadProcessingSteps()
        {
            logging = DynamicsUITest.ServiceProvider.GetRequiredService<ILogging>();
            testBaseManager = DynamicsUITest.ServiceProvider.GetRequiredService<ITestBaseManager>();
            seleniumHelper = DynamicsUITest.ServiceProvider.GetRequiredService<ISeleniumHelper>();
        }


        [Given(@"user is in the leads page")]
        public void GivenUserIsInTheLeadsPage()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().LeadsPage = (LeadsPage)DynamicsUITest.ServiceProvider.GetRequiredService<ILeadsPage>(); //new LeadsPage(testBaseManager);
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().LeadsPage.Open();
            logging.Info("Lead page opened");
        }


        [When(@"user open lead with Id ""([^""]*)""")]
        public void WhenUserOpenLeadWithId(string leadId)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Lead =
                testBaseManager
                    .GetBaseTestUI()
                    .GetPageObjectWrapper()
                    .LeadsPage.OpenLeadById(new Guid(leadId));

            logging.Info("Working with Lead");
        }


        [Then(@"links are loaded properly")]
        public void ThenLinksAreLoadedProperly()
        {
            AssertLeadLinksAreLoadedCorrectly();
        }


        [Then(@"links are not broken after every from (.*) page loads")]
        public void ThenLinksAreNotBrokenAfterEveryFromPageLoads(int attemptsNumber)
        {
            var leadUrl = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Lead.GetLeadUrl();
            for (int i = 0; i < attemptsNumber; i++)
            {
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Lead.Refresh();

                AssertLeadLinksAreLoadedCorrectly();

                if (i % 20 == 0)
                {
                    seleniumHelper.InitializeNewBrowserTab(leadUrl);
                }
            }
        }


        private void AssertLeadLinksAreLoadedCorrectly()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Lead.GetKenticoLinkElement(out IWebElement kenticoLink);
            Assert.AreEqual("Show lead's info from kentico.com", kenticoLink.Text);
            testBaseManager.GetBaseTestUI().GetWebDriver().SwitchTo().DefaultContent();

            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Lead.GetKlentyLinkElement(out IWebElement klentyLink);
            Assert.AreEqual("Show lead's info from Klenty", klentyLink.Text);
            testBaseManager.GetBaseTestUI().GetWebDriver().SwitchTo().DefaultContent();
        }
    }
}
