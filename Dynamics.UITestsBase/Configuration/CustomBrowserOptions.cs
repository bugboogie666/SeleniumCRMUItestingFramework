using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

using Microsoft.Dynamics365.UIAutomation.Browser;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.Configuration
{
    public class CustomBrowserOptions : BrowserOptions, ICustomBrowserOptions
    {
        public override ChromeOptions ToChrome()
        {
            var options = base.ToChrome();
            options.AddArgument("window-size=1920,1080");
            options.AddArgument("proxy-server='direct://'");
            options.AddArgument("proxy-bypass-list=*");
            SetBrowserVersion(options);
            return options;
        }


        public override EdgeOptions ToEdge()
        {
            var options = base.ToEdge();
            options.AddArgument("window-size=1920,1080");
            SetBrowserVersion(options);
            return options;
        }


        public override FirefoxOptions ToFireFox()
        {
            var options = base.ToFireFox();
            options.AddArgument("window-size=1920,1080");
            SetBrowserVersion(options);
            return options;
        }


        private void SetBrowserVersion<T>(T options) where T : DriverOptions
        {
            if (!string.IsNullOrEmpty(AppConfigReader.BrowserVersion))
            {
                options.BrowserVersion = AppConfigReader.BrowserVersion;
            }
        }
    }
}
