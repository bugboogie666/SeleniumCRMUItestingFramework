using OpenQA.Selenium;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface IOpportunityPage
    {
        string Contact { get; set; }
        string Currency { get; set; }
        string EstimateCloseDateBPF { get; set; }
        string License { get; set; }
        IWebElement LicenseLookup { get; }
        int LookupItemIndex { get; set; }
        string Name { get; set; }
        string OpportunityProductsGrid { get; }
        string OriginatingLead { get; set; }
        string OriginatingLeadBPF { get; set; }
        string Owner { get; set; }
        string PotentialCustomer { get; set; }
        string PriceList { get; set; }
        string ProductFamily { get; set; }
        string Source { get; set; }
        string Type { get; set; }
        string WinReasons { get; set; }

        void ActivateStage(string stageName);
        void CloseStage(string stageName);
        void CreateOfferLetter();
        void NextStage(string stageName);
        void SetDataForOpportunity(Dictionary<string, string> data);
    }
}