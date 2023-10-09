using Dynamics.UITestsBase.PageObject;
using OpenQA.Selenium.Support.UI;
using System;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface ILeadsPage
    {
        WebDriverWait Wait { get; }

        LeadPage NewLead();
        void Open();
        LeadPage OpenLeadById(Guid leadID);
    }
}