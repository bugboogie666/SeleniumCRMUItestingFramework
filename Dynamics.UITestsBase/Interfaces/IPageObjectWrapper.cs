using Dynamics.UITestsBase.PageObject;
using System;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IPageObjectWrapper
    {
        AccountPage Account { get; set; }
        Guid AccountID { get; set; }
        AccountsPage AccountsPage { get; set; }
        EmailPage Email { get; set; }
        LeadPage Lead { get; set; }
        Guid LeadID { get; set; }
        LeadsPage LeadsPage { get; set; }
        LicensePage License { get; set; }
        OpportunitiesPage OpportunitiesPage { get; set; }
        OpportunityPage Opportunity { get; set; }
        Guid OpportunityId { get; set; }
        ProductPage OpportunityProduct { get; set; }
        OrderPage Order { get; set; }
        Guid OrderID { get; set; }
        ProductPage OrderProduct { get; set; }
        OrdersPage OrdersPage { get; set; }
        QuickCreateContactPage QuickCreateContactPage { get; set; }
    }
}