using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.PageObject;

using System.Linq;

using TechTalk.SpecFlow;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamics.UITests.OrderProcess.StepDefinition
{
    [Binding]
    internal class TestPandaDocSteps
    {
        //private protected static readonly ILog Logger = LogHelper.GetLogger(typeof(TestPandaDocSteps));
        public const string RANDOM_DATA_MARKER = "<RANDOM>";

        public ILogging logging;
        public ITestBaseManager testBaseManager;

        public TestPandaDocSteps()
        {
            logging = DynamicsUITest.ServiceProvider.GetRequiredService<ILogging>();
            testBaseManager = DynamicsUITest.ServiceProvider.GetRequiredService<ITestBaseManager>();
        }

        [Given(@"user is in the accounts page")]
        public void GivenUserIsInTheAccountsPage()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().AccountsPage = (AccountsPage)DynamicsUITest.ServiceProvider.GetRequiredService<IAccountsPage>();
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().AccountsPage.Open();
        }

        [Given(@"user saves account")]
        public void GivenUserSavesAccount()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.Save();
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().AccountID =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.GetRecordId();
        }


        [Given(@"user creates new account")]
        public void GivenUserCreatesNewAccount()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().AccountsPage.NewAccount();
        }


        [Given(@"user goes to ""([^""]*)"" tab")]
        public void GivenUserGoesToTab(string tabName)
        {
            testBaseManager.GetBaseTestUI().GetXrmApp().Entity.SelectTab(tabName);
        }


        [Given(@"account data is set")]
        public void GivenAccountDataIsSet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);

            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.BuildRandomDataForAccount(data);

            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.SetDataForAccount(data);
        }


        [Given(@"user sets account type")]
        public void GivenUserSetsAccountType()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.ExpandIcon.Click();
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.AccountType = "Client";
        }


        [Given(@"user opens quick create contact")]
        public void GivenUserOpensQuickCreateContact()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().QuickCreateContactPage =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.QuickCreateContact();
        }


        [Given(@"user opens quick create contact from primary contact")]
        public void GivenUserOpensQuickCreateContactFromPrimaryContact()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().QuickCreateContactPage =
               testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.QuickCreateContactFromPrimaryContact();
        }


        [Given(@"quick create contact data is set")]
        public void GivenQuickCreateContactDataIsSet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);

            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().QuickCreateContactPage.BuildRandomDataForContact(data);

            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().QuickCreateContactPage.SetDataForContact(data);
        }


        [Given(@"user saves quick create")]
        public void GivenUserSavesQuickCreate()
        {
            testBaseManager.GetBaseTestUI().GetXrmApp().QuickCreate.Save();
        }


        [Given(@"user creates a new opportunity from account")]
        public void GivenUserCreatesANewOpportunityFromAccount()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.NewOpportunity();
        }


        [Given(@"opportunity data is set")]
        public void GivenOpportunityDataIsSet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.SetDataForOpportunity(data);
        }


        [Given(@"user saves opportunity")]
        public void GivenUserSavesOpportunity()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.Save();
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OpportunityId =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.GetRecordId();
        }

        [Given(@"the opportunity has an order product with (.*)")]
        public void GivenTheOpportunityHasAnOrderProductWith(string productName)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OpportunityProduct =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.AddProduct();
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OpportunityProduct.ClearExistingProductContainer($"*{productName}");
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OpportunityProduct.ExistingProduct = productName;
            testBaseManager.GetBaseTestUI().GetXrmApp().ThinkTime(2000);
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OpportunityProduct.Save();
        }


        [Given(@"user closes entity going back to opportunity with saving")]
        public void GivenUserClosesEntityGoingBackToOpportunityWithSaving()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper()
                    .OpportunityProduct.Command<OpportunityPage>("Save & Close");
        }


        [Given(@"owner for opportunity is changed to ""([^""]*)""")]
        public void GivenOwnerForOpportunityIsChangedTo(string owner)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.Owner = owner;
        }


        [Given(@"user activates opportunity BPF stage ""([^""]*)""")]
        public void GivenUserActivatesOpportunityBPFStage(string stageName)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.ActivateStage(stageName);
        }


        [Given(@"user leaves stage (.*)")]
        public void GivenUserLeavesStage(string stageName)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.NextStage(stageName);
        }


        [Given(@"user closes stage ""([^""]*)""")]
        public void GivenUserClosesStage(string stageName)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.CloseStage(stageName);
        }


        [Given(@"user creates a new order from opportunity")]
        public void GivenUserCreatesANewOrderFromOpportunity()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.NewOrder();
        }


        [Given(@"primary contact is memorized as ""([^""]*)""")]
        public void GivenPrimaryContactIsMemorizedAs(string key)
        {
            testBaseManager.GetBaseTestUI().GetTempValuesHolder().
                MemorizeExpectedValue(key, testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Account.PrimaryContact);
        }


        [Given(@"contact ""([^""]*)"" is set for opportunity")]
        public void GivenIsSetForOpportunity(string contact)
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.Contact =
                testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue(contact);
        }


        [Given(@"user opens opportunity lookup")]
        public void GivenUserOpensOpportunityLookup()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order
                    .OpenLookupRecord<OpportunityPage>(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.OpportunityLookup);
        }


        [Given(@"user creates an offer letter")]
        public void GivenUserCreatesAnOfferLetter()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Opportunity.CreateOfferLetter();
        }
    }
}
