using System;
using System.Linq;
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
    public class TestOrderProcessingSteps
    {
        //private protected static readonly ILog Logger = LogHelper.GetLogger(typeof(TestOrderProcessingSteps));

        public ILogging logging;
        public ITestBaseManager testBaseManager;
        public ISeleniumHelper seleniumHelper;

        public TestOrderProcessingSteps()
        {
            logging = DynamicsUITest.ServiceProvider.GetRequiredService<ILogging>();
            testBaseManager = DynamicsUITest.ServiceProvider.GetRequiredService<ITestBaseManager>();
            seleniumHelper = DynamicsUITest.ServiceProvider.GetRequiredService<ISeleniumHelper>();
        }


        [Given(@"a user is on the orders page")]
        public void GivenAUserIsOnTheOrdersPage()
        {
            GetWrapped().OrdersPage = (OrdersPage)DynamicsUITest.ServiceProvider.GetRequiredService<IOrdersPage>();
            GetWrapped().OrdersPage.Open();
        }


        [Given(@"a user creates a new order")]
        public void GivenAUserCreatesANewOrder()
        {
            GetWrapped().Order = GetWrapped().OrdersPage.NewOrder();
        }


        [Given(@"the order data is set")]
        public void GivenTheOrderDataIsSet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);
            GetWrapped().Order.SetDataForOrder(data);
        }


        [When(@"the user saves the order")]
        [Given(@"the user saves the order")]
        public void GivenTheUserSavesTheOrder()
        {
            GetWrapped().Order.Save();
            GetWrapped().OrderID = GetWrapped().Order.GetRecordId();
        }


        [Given(@"user saves product")]
        public void GivenUserSavesProduct()
        {
            GetWrapped().OrderProduct.Save();
        }


        [Given(@"the user saves the license")]
        public void GivenTheUserSavesTheLicense()
        {
            GetWrapped().License.Save();
        }


        [Given(@"the order has an order product with (.*)")]
        public void GivenTheOrderHasAnOrderProductWith(string productName)
        {
            GetWrapped().OrderProduct = GetWrapped().Order.AddProduct();

            GetWrapped().OrderProduct.ClearExistingProductContainer($"*{productName}");

            GetWrapped().OrderProduct.ExistingProduct = productName;

            testBaseManager
                .GetBaseTestUI()
                .GetXrmApp()
                .ThinkTime(2000);

            GetWrapped().OrderProduct.Save();
        }


        [Given(@"the order has not saved an order product called (.*)")]
        public void GivenTheOrderHasNotSavedAnOrderProductCalledXperienceSubscriptionRenewal(string productName)
        {
            GetWrapped().OrderProduct = GetWrapped().Order.AddProduct();
            GetWrapped().OrderProduct.ClearExistingProductContainer($"*{productName}");
            GetWrapped().OrderProduct.ExistingProduct = productName;
            testBaseManager.GetBaseTestUI().GetXrmApp().ThinkTime(2000);
        }


        [Given(@"""([^""]*)"" value for field ""([^""]*)"" is memorized")]
        public void GivenValueForFieldIsMemorized(string keyName, string propertyName)
        {
            var fieldValue = GetWrapped().OrderProduct.GetValueFromProduct(propertyName);
            testBaseManager.GetBaseTestUI().GetTempValuesHolder().MemorizeExpectedValue(keyName, fieldValue);
        }


        [Given(@"user changes pricing to ""(.*)""")]
        public void GivenUserChangesPricingTo(string pricingValue)
        {
            GetWrapped().OrderProduct.Pricing = pricingValue;
        }


        [Given(@"the product data is set")]
        public void GivenTheProductDataIsSet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);
            GetWrapped().OrderProduct.SetDataForProduct(data);
        }


        [Given(@"user saves entity")]
        public void GivenUserSavesEntity()
        {
            testBaseManager.GetBaseTestUI().GetXrmApp().Entity.Save();
        }


        [Given(@"user closes entity going back to order with saving")]
        [Then(@"user closes entity going back to order with saving")]
        public void GivenUserClosesEntityGoingBackToOrderWithSaving()
        {
            GetWrapped().Order = GetWrapped().OrderProduct.Command<OrderPage>("Save & Close");
        }


        [Given(@"the user processes the order")]
        [When(@"the user processes the order")]
        public void WhenUserProcessTheOrder()
        {
            GetWrapped().Order.Process();
        }


        [Given(@"user is going to order product (.*)")]
        [When(@"user is going to order product (.*)")]
        public void WhenUserIsGoingToOrderProduct(int index)
        {
            GetWrapped().OrderProduct = GetWrapped().Order
                    .EditSubgridRecord<ProductPage>(GetWrapped().Order.SalesOrderDetailsGrid,
                                                    GetWrapped().Order.OrderProducts,
                                                    index, doubleClick: true);
        }


        [Then(@"product fields are properly set (.*)")]
        public void ThenProductFieldsAreProperlySet(string contractLengthExpected)
        {
            // Wait 3 seconds until values loaded for the form
            testBaseManager.GetBaseTestUI().GetXrmApp().ThinkTime(3000);

            // Convert posting dates values to the date-time format
            var startsOn = GetWrapped().OrderProduct.ConvertDateFromUSFormat(GetWrapped().OrderProduct.StartsOn);
            var expiresOn = GetWrapped().OrderProduct.ConvertDateFromUSFormat(GetWrapped().OrderProduct.ExpiresOn);

            // Calculate expected expires on
            var expiresOnExpected = startsOn.AddMonths(int.Parse(contractLengthExpected)).Subtract(TimeSpan.FromDays(1));
            // We would like to get information when deviation from expected expires on is present
            var deviation = expiresOnExpected.Subtract(expiresOn);
            // Assert
            seleniumHelper.TakeScreenShot();
            Assert.That(expiresOn.Equals(expiresOnExpected), "Posting dates range is less/more than expected, difference is: " + deviation.TotalDays);
        }


        [Given(@"a license should be bound to the order")]
        public void GivenALicenseShouldBeBoundToTheOrder()
        {
            var isLicenseBound = GetWrapped().Order.LicenseIsBound();
            seleniumHelper.TakeScreenShot();
            Assert.That(isLicenseBound, "License is not bound to order with id:");
        }


        [When(@"the user opens the license lookup")]
        [Given(@"the user opens the license lookup")]
        public void WhenTheUserOpensTheLicenseLookup()
        {
            GetWrapped().License = GetWrapped().Order.OpenLookupRecord<LicensePage>(GetWrapped().Order.LicenseLookup);

        }


        [Then(@"license parameters are properly set")]
        public void ThenLicenseParametersAreProperlySet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);
            seleniumHelper.TakeScreenShot();

            foreach (var record in data)
            {
                var fieldValue = GetWrapped().License.GetValueFromLicense(record.Key);
                Assert.That(fieldValue.Equals(record.Value), $"Expected value: {record.Value} does not match with entity field value: {fieldValue}");
            }
        }


        [Then(@"product parameters are properly set")]
        public void ThenProductParametersAreProperlySet(Table table)
        {
            var data = table.Rows[0].ToDictionary(fn => fn.Key, fv => fv.Value);
            seleniumHelper.TakeScreenShot();

            foreach (var record in data)
            {
                var fieldValue = GetWrapped().OrderProduct.GetValueFromProduct(record.Key);
                Assert.That(fieldValue.Equals(record.Value), $"Expected value: {record.Value} does not match with entity field value: {fieldValue}");
            }
        }


        [Given(@"user goes to related activities and select email (.*)")]
        [When(@"user goes to related activities and select email (.*)")]
        public void GivenUserGoesToRelatedActivitiesAndSelectEmail(int index)
        {
            testBaseManager.GetBaseTestUI().GetXrmApp().ThinkTime(2000);
            GetWrapped().Order.SelectRelatedEntityAndView("Related", "Activities", "All Activities");

            GetWrapped().Email = GetWrapped().Order
                .EditSubgridRecord<EmailPage>(GetWrapped().Order.RelatedGrid,
                                               GetWrapped().Order.EmailsByTitle,
                                               index: index,
                                               doubleClick: true);
        }


        [Then(@"email exists and it is a draft")]
        public void ThenEmailExistsAndItIsADraft()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(GetWrapped().Email != null, $"Email is not related to this order.");
            Assert.That(GetWrapped().Email.IsDraft, "Status is not draft and it should be.");
        }


        [Then(@"email exists and it is sent")]
        public void ThenEmailExistsAndItIsSent()
        {
            Assert.That(GetWrapped().Email != null, $"Email is not related to this order.");
            Assert.IsTrue(GetWrapped().Email.IsFailed);
        }


        [Given(@"the license expires in (.*) days")]
        public void GivenTheLicenseExpiresInDays(int numberOfDays)
        {
            GetWrapped().License.ExpiresOn =
                GetWrapped().License.ConvertDateFormat(DateTime.Now.AddDays(numberOfDays));
        }


        [Given(@"a form is switched to ""(.*)""")]
        public void GivenAFormIsSwitchedTo(string formName)
        {
            GetWrapped().License.SwitchToForm(formName);
        }


        [Given(@"the user creates a new order from the license")]
        public void GivenTheUserCreatesANewOrderFromTheLicense()
        {
            GetWrapped().Order = GetWrapped().License.NewOrder();
        }


        [Then(@"renewal product is splitted to renewal and reinstatement")]
        public void ThenRenewalProductIsSplittedToRenewalAndReinstatement()
        {
            seleniumHelper.TakeScreenShot();
            Assert.That(GetWrapped().Order.OrderProducts.Count == (DynamicsPage.GRID_HEADER_ROW + 2), "One of expected product is not present");
        }


        [Then(@"order detail amount is equal to original renewal ""([^""]*)""")]
        public void ThenOrderDetailAmountIsEqualToOriginalRenewal(string key)
        {
            var expected = testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue(key);
            Assert.AreEqual(expected, GetWrapped().Order.DetailAmount);
        }


        [Given(@"the original license expiration is memorized as key ""([^""]*)""")]
        public void GivenTheOriginalLicenseExpirationIsMemorizedAsKey(string key)
        {
            var originalLicenseExpiration = GetWrapped().License.ExpiresOn;
            testBaseManager.GetBaseTestUI().GetTempValuesHolder().MemorizeExpectedValue(key, originalLicenseExpiration);
        }


        [Given(@"starts on is set correctly based on date of ""([^""]*)""")]
        public void GivenStartsOnIsSetCorrectlyBasedOnDateOf(string key)
        {
            var originalLicenseExpiration = testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue(key);
            var renewalStartsOn = GetWrapped().OrderProduct.StartsOn;
            var startsOnExpected = GetWrapped().OrderProduct.ConvertDateFromUSFormat(originalLicenseExpiration).AddDays(1);
            Assert.AreEqual(startsOnExpected, GetWrapped().OrderProduct.ConvertDateFromUSFormat(renewalStartsOn));
        }


        [Given(@"expires on is set correctly based on CL ""([^""]*)"" and date of ""([^""]*)""")]
        public void GivenExpiresOnIsSetCorrectlyBasedOnCLAndDateOf(string contractLength, string key)
        {
            var originalLicenseExpiration = testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue(key);
            var renewalExpiresOn = GetWrapped().OrderProduct.ExpiresOn;
            var expiresOnExpected = GetWrapped().OrderProduct.ConvertDateFromUSFormat(originalLicenseExpiration).AddMonths(int.Parse(contractLength));
            Assert.AreEqual(expiresOnExpected, GetWrapped().OrderProduct.ConvertDateFromUSFormat(renewalExpiresOn));
        }


        [Given(@"the renewal product expiration is memorized as ""([^""]*)""")]
        public void GivenTheRenewalProductExpirationIsMemorizedAs(string key)
        {
            var licenseExpirationExpected = GetWrapped().OrderProduct.ExpiresOn;
            testBaseManager.GetBaseTestUI().GetTempValuesHolder().MemorizeExpectedValue(key, licenseExpirationExpected);
        }


        [Given(@"the license expiration is set correctly to the renewal product expiration")]
        public void GivenTheLicenseExpirationIsSetCorrectlyToTheRenewalProductExpiration()
        {
            var expectedLicenseExpiration = testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue("renewal product expiration");
            Assert.AreEqual(expectedLicenseExpiration, GetWrapped().License.ExpiresOn);
        }


        [Given(@"the date of processing is memorized as ""([^""]*)""")]
        public void GivenTheDateOfProcessingIsMemorizedAs(string key)
        {
            var dateofFulfilled = GetWrapped().Order.DateFulfilled;
            testBaseManager.GetBaseTestUI().GetTempValuesHolder().MemorizeExpectedValue(key, dateofFulfilled);
        }


        [Then(@"the order product starts on is set as date of fulfilled")]
        public void ThenTheOrderProductStartsOnIsSetAsDateOfFulfilled()
        {
            var expectedProductStartsOn = testBaseManager.GetBaseTestUI().GetTempValuesHolder().GetExpectedValue("date fulfilled");
            Assert.AreEqual(expectedProductStartsOn, GetWrapped().OrderProduct.StartsOn);
        }


        [Given(@"starts on and expires on fields are empty and not editable")]
        public void GivenStartsOnAndExpiresOnFieldsAreEmptyAndNotEditable()
        {
            Assert.That(GetWrapped().OrderProduct.StartsOn.Equals(""));
            Assert.That(GetWrapped().OrderProduct.ExpiresOn.Equals(""));

            Assert.That(GetWrapped().OrderProduct.CheckFieldIsLocked("ken_startson"), "Starts on is not locked and it should be.");
            Assert.That(GetWrapped().OrderProduct.CheckFieldIsLocked("dyn_expireson"), "Expires on is not locked and it should be.");
        }


        private PageObjectWrapper GetWrapped()
        {
            return testBaseManager.GetBaseTestUI().GetPageObjectWrapper();
        }
    }
}
