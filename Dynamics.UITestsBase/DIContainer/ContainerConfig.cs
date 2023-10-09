using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Configuration;
using Dynamics.UITestsBase.DynamicsUtilities;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.Misc;
using Dynamics.UITestsBase.PageObject;
using Dynamics.UITestsBase.Params;
using Dynamics.UITestsBase.Reporting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dynamics.UITestsBase.DIContainer
{
    public class ContainerConfig
    {
        public static IServiceProvider ConfigureService()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IAppConfigReader, AppConfigReader>();
            serviceCollection.AddSingleton<ICustomBrowserOptions, CustomBrowserOptions>();
            serviceCollection.AddSingleton<ITestSettings, TestSettings>();
            serviceCollection.AddSingleton<ILogging, Logging>();
            serviceCollection.AddSingleton<IDefaultVariables, DefaultVariables>();
            serviceCollection.AddSingleton<ICrmConnectionUtility, CrmConnectionUtility>();
            serviceCollection.AddSingleton<IExtentReport, ExtentReport>();
            serviceCollection.AddTransient<ITestBaseManager, TestBaseManager>();
            serviceCollection.AddTransient<IBaseTestUI, BaseTestUI>();
            serviceCollection.AddTransient<IBaseTest, BaseTest>();
            serviceCollection.AddTransient<ITempValuesHolder, TempValuesHolder>();
            serviceCollection.AddTransient<IPageObjectWrapper, PageObjectWrapper>();
            serviceCollection.AddTransient<ISeleniumHelper, SeleniumHelper>();
            serviceCollection.AddTransient<INavigationHelper, NavigationHelper>();
            serviceCollection.AddTransient<IExtentFeatureReport,ExtentFeatureReport>();

            serviceCollection.AddTransient<IAccountPage, AccountPage>();
            serviceCollection.AddTransient<IAccountsPage, AccountsPage>();
            serviceCollection.AddTransient<AccountPage>();

            serviceCollection.AddTransient<IEmailsPage, EmailsPage>();
            serviceCollection.AddTransient<IEmailPage, EmailPage>();
            serviceCollection.AddTransient<EmailPage>();

            serviceCollection.AddTransient<ILeadsPage, LeadsPage>();
            serviceCollection.AddTransient<ILeadPage, LeadPage>();
            serviceCollection.AddTransient<LeadPage>();

            serviceCollection.AddTransient<ILicensesPage, LicensesPage>();
            serviceCollection.AddTransient<ILicensePage, LicensePage>();
            serviceCollection.AddTransient<LicensePage>();

            serviceCollection.AddTransient<IOpportunitiesPage, OpportunitiesPage>();
            serviceCollection.AddTransient<IOpportunityPage, OpportunityPage>();
            serviceCollection.AddTransient<OpportunityPage>();

            serviceCollection.AddTransient<IOrdersPage, OrdersPage>();
            serviceCollection.AddTransient<IOrderPage, OrderPage>();
            serviceCollection.AddTransient<OrderPage>();

            serviceCollection.AddTransient<IProductPage, ProductPage>();
            serviceCollection.AddTransient<ProductPage>();

            serviceCollection.AddTransient<IServicePage,ServicePage>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
