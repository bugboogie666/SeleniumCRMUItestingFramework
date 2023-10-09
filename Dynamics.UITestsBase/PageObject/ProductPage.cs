using System;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class ProductPage : DynamicsPage, IProductPage
    {
        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(50));
            }
        }


        [Description("Existing Product")]
        public string ExistingProduct
        {
            get
            {

                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrderDetail.Fields.ProductId });
            }
            set
            {
                xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrderDetail.Fields.ProductId, Value = value, Index = LookupItemIndex });
            }
        }


        [Description("Description")]
        public string Description
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.Description);
            }
            set
            {
                try
                {
                    xrmApp.Entity.SetValue(SalesOrderDetail.Fields.Description, value);
                }
                catch (Exception ex)
                {
                    throw new ElementClickInterceptedException(ex.Message);
                }

            }
        }


        [Description("Pricing")]
        public string Pricing
        {
            get
            {
                return xrmApp.Entity.GetValue(new BooleanItem { Name = SalesOrderDetail.Fields.IsPriceOverridden }).ToString();
            }
            set
            {
                xrmApp.Entity.SetValue(new BooleanItem { Name = SalesOrderDetail.Fields.IsPriceOverridden, Value = Convert.ToBoolean(value) });
            }
        }


        [Description("Price per unit")]
        public string PricePerUnit
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.PricePerUnit);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.PricePerUnit, value);
            }
        }


        [Description("Quantity")]
        public string Quantity
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ken_quantity);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ken_quantity, value);
            }
        }


        [Description("System Quantity")]
        public string SystemQuantity
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.Quantity);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.Quantity, value);
            }
        }


        [Description("Contract Length")]
        public string ContractLength
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ken_contractlength);
            }
            set
            {
                if (value == null)
                {
                    xrmApp.Entity.ClearValue(SalesOrderDetail.Fields.ken_contractlength);
                    return;
                }
                try
                {
                    xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ken_contractlength, value);
                }
                catch (Exception ex)
                {
                    throw new ElementClickInterceptedException(ex.Message);
                }

            }
        }


        [Description("Amount")]
        public string Amount
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.BaseAmount);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.BaseAmount, value);
            }
        }


        [Description("Starts On")]
        public string StartsOn
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ken_startson);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ken_startson, value);
                //XrmApp.Entity.SetValue(new DateTimeControl("dyn_date") { DateAsString = value });
            }
        }


        [Description("Expires On")]
        public string ExpiresOn
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.dyn_ExpiresOn);
            }
            set
            {
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.dyn_ExpiresOn, value);
                //XrmApp.Entity.SetValue(new DateTimeControl("dyn_date") { DateAsString = value });
            }
        }


        [Description("Name")]
        public string Name
        {
            get
            {
                return xrmApp.Entity.GetValue(OpportunityProduct.Fields.OpportunityProductName);
            }
            set
            {
                xrmApp.Entity.SetValue(OpportunityProduct.Fields.OpportunityProductName, value);
            }
        }


        [Description("Discount")]
        public string Discount
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ken_discountpercent);
            }
            set
            {
                if (value == null)
                {
                    xrmApp.Entity.ClearValue(SalesOrderDetail.Fields.ken_discountpercent);
                    return;
                }
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ken_discountpercent, value);
            }
        }


        [Description("Manual Discount")]
        public string ManualDiscount
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ManualDiscountAmount);
            }
            set
            {
                if (value == null)
                {
                    xrmApp.Entity.ClearValue(SalesOrderDetail.Fields.ManualDiscountAmount);
                    return;
                }
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ManualDiscountAmount, value);
            }
        }


        [Description("Extended Amount")]
        public string ExtendedAmount
        {
            get
            {
                return xrmApp.Entity.GetValue(SalesOrderDetail.Fields.ExtendedAmount);
            }
            set
            {
                if (value == null)
                {
                    xrmApp.Entity.ClearValue(SalesOrderDetail.Fields.ExtendedAmount);
                    return;
                }
                xrmApp.Entity.SetValue(SalesOrderDetail.Fields.ExtendedAmount, value);
            }
        }


        public string Unit
        {
            get
            {
                return xrmApp.Entity.GetValue(new LookupItem { Name = SalesOrderDetail.Fields.UoMId });
            }
            set
            {
                try
                {
                    if (value == null)
                    {
                        xrmApp.Entity.ClearValue(new LookupItem { Name = SalesOrderDetail.Fields.UoMId });
                        return;
                    }
                    xrmApp.Entity.SetValue(new LookupItem { Name = SalesOrderDetail.Fields.UoMId, Value = value, Index = LookupItemIndex });
                }
                catch (Exception ex)
                {
                    seleniumHelper.TakeScreenShot();
                    logging.Error(ex.Message);
                    logging.Error(ex.StackTrace);
                    throw;
                }
            }
        }


        public IWebElement ExistingproductContainer => seleniumHelper.GetElement(By.XPath("//input[@aria-label='Existing Product, Lookup']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement ContractLengthLocker => seleniumHelper.GetElement(By.XPath("//div[@aria-label='Locked Contract Length']"));
        public IWebElement QuantityLocker => seleniumHelper.GetElement(By.XPath("//div[@aria-label='Locked Quantity']"));
        public IWebElement QuantityLabel => seleniumHelper.GetElement(By.Id("id-2bb64321-6da0-4a1a-8466-62b40cc6bc7a-22-ken_quantity-field-label"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement ProductValidationAlertBox => seleniumHelper.GetElement(By.XPath("//span[@data-id='dialogMessageText']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public IWebElement OkButton => seleniumHelper.GetElement(By.XPath("//button[@data-id='okButton']"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement ContractLengthElement => seleniumHelper.GetElement(By.Id("id-2bb64321-6da0-4a1a-8466-62b40cc6bc7a-24-ken_contractlengthc6d124ca-7eda-4a60-aea9-7fb8d318b68f-ken_contractlength.fieldControl-decimal-number-text-input"), SeleniumHelper.ElementSyncCondition.Exists, Wait);


        public ProductPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {

        }


        public ProductPage()
        {
            LookupItemIndex = 0;
        }


        public ProductPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper,ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {
        }


        public string GetMessageAndOk()
        {
            var message = ProductValidationAlertBox.Text;
            OkButton.Click();
            return message;
        }


        public void ClearExistingProductContainer(string text)
        {
            ExistingproductContainer.Click();
            ExistingproductContainer.SendKeys(text);
            ExistingproductContainer.Clear();
        }


        public bool OKButtonIsPresent()
        {
            return seleniumHelper.IsElementPresent(By.Id("okButton"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        }


        public void SetDataForProduct(Dictionary<string, string> data)
        {
            foreach (var record in data)
            {
                PropertyInfo property = GetType().GetProperty(record.Key);
                property.SetValue(this, record.Value);
            }
        }


        public string GetValueFromProduct(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            return property.GetValue(this).ToString();
        }
    }
}
