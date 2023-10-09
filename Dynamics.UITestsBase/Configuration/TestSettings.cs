using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Animation;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Dynamics.UITestsBase.Configuration
{
    public class TestSettings : ITestSettings
    {
        public IAppConfigReader Reader { get; set; }
        public CustomBrowserOptions Options { get; set; }
        public IExtentReport extendReport;

        public TestSettings(IAppConfigReader reader, ICustomBrowserOptions customBrowserOptions, IExtentReport extendReport)
        {
            this.extendReport = extendReport;
            Reader = reader;
            Options = (CustomBrowserOptions)customBrowserOptions;
        }

        public CustomBrowserOptions InitializeSettings()
        {
            Options.BrowserType = Reader.GetBrowser();
            // To ensure that we are testing at "cleaned" working environment without cache and conflicts
            Options.PrivateMode = Reader.RunAsIncognito;
            // To get a maximum of performance and we want to run silently on the server
            Options.Headless = Reader.RunAsHeadless;
            // We need to set control mode to 0 to avoid powerapps cookie error documented here: https://github.com/microsoft/EasyRepro/issues/1252
            Options.CookieСontrolsMode = Reader.CookiesControlMode;
            // Set the path to webdriver binary dynamically
            Options.DriversPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Options.NoSandbox = true;
            Options.UCIPerformanceMode = false;
            Options.DisableGpu = true;
            Options.CommandTimeout = TimeSpan.FromMinutes(3);
            Options.PageLoadTimeout = TimeSpan.FromSeconds(30);

            switch (Options.BrowserType)
            {
                case BrowserType.Chrome:
                    Options.ToChrome();
                    break;
                case BrowserType.Firefox:
                    Options.ToFireFox();
                    break;
                case BrowserType.Edge:
                    Options.ToEdge();
                    break;
            }

            extendReport.InitializeExtendReport();

            return Options;
        }
    }
}
