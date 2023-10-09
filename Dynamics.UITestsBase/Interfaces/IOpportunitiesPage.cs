using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium.Support.UI;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface IOpportunitiesPage
    {
        WebDriverWait Wait { get; }

        OpportunityPage NewOpportunity();
        void Open();
    }
}