using OpenQA.Selenium;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IProductPage
    {
        string Amount { get; set; }
        string ContractLength { get; set; }
        IWebElement ContractLengthElement { get; }
        IWebElement ContractLengthLocker { get; }
        string Description { get; set; }
        string Discount { get; set; }
        string ExistingProduct { get; set; }
        IWebElement ExistingproductContainer { get; }
        string ExpiresOn { get; set; }
        string ExtendedAmount { get; set; }
        int LookupItemIndex { get; set; }
        string ManualDiscount { get; set; }
        string Name { get; set; }
        IWebElement OkButton { get; }
        string PricePerUnit { get; set; }
        string Pricing { get; set; }
        IWebElement ProductValidationAlertBox { get; }
        string Quantity { get; set; }
        IWebElement QuantityLabel { get; }
        IWebElement QuantityLocker { get; }
        string StartsOn { get; set; }
        string SystemQuantity { get; set; }
        string Unit { get; set; }

        void ClearExistingProductContainer(string text);
        string GetMessageAndOk();
        string GetValueFromProduct(string propertyName);
        bool OKButtonIsPresent();
        void SetDataForProduct(Dictionary<string, string> data);
    }
}