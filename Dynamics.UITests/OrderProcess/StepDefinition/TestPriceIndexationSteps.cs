using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.PageObject;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Dynamics.UITests.OrderProcess.StepDefinition
{
    [Binding]
    public class TestPriceIndexationSteps
    {

        public ILogging logging;
        public ITestBaseManager testBaseManager;
        public ISeleniumHelper seleniumHelper;

        public TestPriceIndexationSteps()
        {
            logging = DynamicsUITest.ServiceProvider.GetRequiredService<ILogging>();
            testBaseManager = DynamicsUITest.ServiceProvider.GetRequiredService<ITestBaseManager>();
            seleniumHelper = DynamicsUITest.ServiceProvider.GetRequiredService<ISeleniumHelper>();
        }

        [When(@"user saves and closes the order")]
        public void WhenUserSavesAndClosesTheOrder()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OrdersPage =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.Command<OrdersPage>("Save & Close");
        }


        [Then(@"order is saved")]
        public void ThenOrderIsSaved()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order =
                testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OrdersPage.OpenEntityById<OrderPage>("salesorder", testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OrderID);
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.GetRecordId().Equals(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().OrderID));
        }


        [Then(@"error message for contracted control is raised ""(.*)""")]
        public void ThenErrorMessageForContractedControlIsRaised(string message)
        {
            var errorMessage = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ContractedErrorMessage.Text;
            seleniumHelper.TakeScreenShot();
            Assert.That(errorMessage.Equals(message));
        }


        [Then(@"warning notification for contracted control is raised ""(.*)""")]
        public void ThenWarningNotificationForContractedControlIsRaised(string message)
        {
            var warningNotification = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ContractedWarningNotification.Text;
            seleniumHelper.TakeScreenShot();
            Assert.That(warningNotification.Equals(message));
        }


        [Then(@"Input for first renewal is present")]
        [Given(@"Input for first renewal is present")]
        public void ThenInputForFirstRenewalIsPresent()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.FirstRenewal.Displayed.Equals(true));
        }


        [Then(@"new second renewal button is present")]
        [Given(@"new second renewal button is present")]
        public void ThenNewSecondRenewalButtonIsPresent()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.SecondRenewalButton.Displayed.Equals(true));
        }


        [Given(@"user clicks on second renewal button")]
        [When(@"user clicks on second renewal button")]
        public void WhenUserClicksOnSecondRenewalButton()
        {
            var button = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.SecondRenewalButton;
            SeleniumHelper.ClickTwice(button); // when clicking the renewal button, it needs to be performed twice. Why? I still don´t now.
        }


        [When(@"user clicks on third renewal button")]
        public void WhenUserClicksOnThirdRenewalButton()
        {
            var button = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ThirdRenewalButton;
            button.Click();
        }


        [Given(@"Input for second renewal is present")]
        [Then(@"Input for second renewal is present")]
        public void ThenInputForSecondRenewalIsPresent()
        {
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.SecondRenewal.Displayed.Equals(true));
        }


        [Given(@"input for third renewal is present")]
        [Then(@"input for third renewal is present")]
        public void ThenInputForThirdRenewalIsPresent()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ThirdRenewal.Displayed.Equals(true));
        }


        [Given(@"new third renewal button is present")]
        [Then(@"new third renewal button is present")]
        public void ThenNewThirdRenewalButtonIsPresent()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ThirdRenewalButton.Displayed.Equals(true));
        }


        [Given(@"user fills first renewal (.*)")]
        public void GivenUserFillsFirstRenewal(int price)
        {
            seleniumHelper.SendText(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.FirstRenewal, price.ToString());
        }


        [Given(@"user fills second renewal (.*)")]
        public void GivenUserFillsSecondRenewal(int price)
        {
            seleniumHelper.SendText(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.SecondRenewal, price.ToString());
        }


        [Given(@"user fills third renewal (.*)")]
        public void GivenUserFillsThirdRenewal(int price)
        {
            seleniumHelper.SendText(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.ThirdRenewal, price.ToString());
        }


        [When(@"user removes second renewal")]
        public void WhenUserRemovesSecondRenewal()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.RemoveSecondRenewalButton.Click();
        }


        [When(@"user removes first renewal")]
        public void WhenUserRemovesFirstRenewal()
        {
            testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.RemoveFirstRenewalButton.Click();
        }


        [Then(@"only first and second renewal input is displayed")]
        public void ThenOnlyFirstAndSecondRenewalInputIsDisplayed()
        {
            var renewalInputs = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.InputRenewals;
            var renewalLabels = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.InputRenewalsLabels;

            // We are expecting 2 inputs and 2 labels after delete 3th
            seleniumHelper.TakeScreenShot();
            Assert.That(renewalInputs.Count.Equals(2));
            Assert.That(renewalLabels.Count.Equals(2));

            // Labels are properly named
            foreach (var element in renewalLabels)
            {
                Assert.That(element.Text.Equals("1st Renewal") || element.Text.Equals("2nd Renewal"));
            }
        }


        [Then(@"price of second renewal is (.*)")]
        public void ThenPriceOfSecondRenewalIs(int price)
        {
            var secondRenewalValue = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.SecondRenewal.GetAttribute("value");
            Assert.That(secondRenewalValue.Equals(price.ToString()));
        }


        [Then(@"price of first renewal is (.*)")]
        public void ThenPriceOfFirstRenewalIs(int price)
        {
            var secondRenewalValue = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.FirstRenewal.GetAttribute("value");
            seleniumHelper.TakeScreenShot();
            Assert.That(secondRenewalValue.Equals(price.ToString()));
        }


        [Then(@"renewal base price is (.*)")]
        public void ThenRenewalBasePriceIs(int price)
        {
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.RenewalBasePrice.GetAttribute("value").Equals(price.ToString()));
        }


        [Then(@"maximum percentage rule is (.*)")]
        public void ThenMaximumPercentageRuleIs(int price)
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.LicenseMaximumPercentage.GetAttribute("value").Equals(price.ToString()));
        }


        [Given(@"renewal base price is set (.*)")]
        public void GivenRenewalBasePriceIsSet(int price)
        {
            seleniumHelper.TakeScreenShot();
            seleniumHelper.SendText(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.RenewalBasePrice, price.ToString());
        }


        [Given(@"user clear renewal base price")]
        public void GivenUserClearRenewalBasePrice()
        {
            seleniumHelper.ClearInput(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.RenewalBasePrice);
        }


        [Given(@"user clear maximum percentage")]
        public void GivenUserClearMaximumPercentage()
        {
            seleniumHelper.ClearInput(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.LicenseMaximumPercentage);
        }


        [Given(@"maximum percentage is set (.*)")]
        public void GivenMaximumPercentageIsSet(int percentage)
        {
            seleniumHelper
                .SendText(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.LicenseMaximumPercentage, percentage.ToString());
        }


        [Then(@"input overflow validation is displayed in count (.*)")]
        public void ThenInputOverflowValidationIsDisplayedInCount(int validationCount)
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.AreYouSureValidations.Count.Equals(validationCount), "At least 1 validation is not working.");
        }


        [Then(@"first renewal input is read only")]
        public void ThenFirstRenewalInputIsReadOnly()
        {
            var enabled = testBaseManager.GetBaseTestUI().GetPageObjectWrapper().Order.FirstRenewal.Enabled;
            seleniumHelper.TakeScreenShot();
            Assert.That(enabled.Equals(false));
        }
    }
}
