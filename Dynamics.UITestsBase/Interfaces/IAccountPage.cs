using OpenQA.Selenium;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.PageObject
{
    internal interface IAccountPage
    {
        string AccountName { get; set; }
        string AccountType { get; set; }
        string Country { get; set; }
        string Currency { get; set; }
        IWebElement ExpandIcon { get; }
        string[] Industry { get; set; }
        IWebElement IndustryElement { get; }
        int LookupItemIndex { get; set; }
        IWebElement NewContactElement { get; }
        string PrimaryContact { get; set; }
        IWebElement PrimaryContactElement { get; }
        string State { get; set; }
        string StatusReason { get; set; }

        void BuildRandomDataForAccount(Dictionary<string, string> data);
        OpportunityPage NewOpportunity();
        QuickCreateContactPage QuickCreateContact();
        QuickCreateContactPage QuickCreateContactFromPrimaryContact();
        void SetDataForAccount(Dictionary<string, string> data);
    }
}