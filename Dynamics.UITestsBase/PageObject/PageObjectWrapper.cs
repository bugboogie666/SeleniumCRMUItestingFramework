using System;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.Misc;

namespace Dynamics.UITestsBase.PageObject
{
    public class PageObjectWrapper : IPageObjectWrapper
    {
        public OrdersPage OrdersPage { get; set; }
        public OrderPage Order { get; set; }
        public Guid OrderID { get; set; }
        public OpportunitiesPage OpportunitiesPage { get; set; }
        public OpportunityPage Opportunity { get; set; }
        public Guid OpportunityId { get; set; }
        public ProductPage OrderProduct { get; set; }
        public ProductPage OpportunityProduct { get; set; }
        public LicensePage License { get; set; }
        public EmailPage Email { get; set; }
        public LeadsPage LeadsPage { get; set; }
        public LeadPage Lead { get; set; }
        public Guid LeadID { get; set; }
        public AccountsPage AccountsPage { get; set; }
        public AccountPage Account { get; set; }
        public Guid AccountID { get; set; }
        public QuickCreateContactPage QuickCreateContactPage { get; set; }
    }
}
