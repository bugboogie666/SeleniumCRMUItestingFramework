using System;
using System.Configuration;
using System.Security;
using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.Settings;

using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Dynamics.UITestsBase.Configuration
{
    public class AppConfigReader : IAppConfigReader
    {
        public static string DriversPath => ConfigurationManager.AppSettings.Get(AppConfigKeys.DriversPath);

        public bool RunAsIncognito => Convert.ToBoolean(ConfigurationManager.AppSettings.Get(AppConfigKeys.Incognito));
        public bool RunAsHeadless => Convert.ToBoolean(ConfigurationManager.AppSettings.Get(AppConfigKeys.Headless));
        public int CookiesControlMode => Convert.ToInt32(ConfigurationManager.AppSettings.Get(AppConfigKeys.CookiesControlMode));
        public static string BrowserVersion = ConfigurationManager.AppSettings.Get(AppConfigKeys.BrowserVersion);

        public BrowserType GetBrowser()
        {
            var browser = ConfigurationManager.AppSettings.Get(AppConfigKeys.Browser);

            return (BrowserType)Enum.Parse(typeof(BrowserType), browser);
        }


        public SecureString GetCrmPassword()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.CrmPassword).ToSecureString();
        }


        public Uri GetCrmUrl()
        {
            return new Uri(ConfigurationManager.AppSettings.Get(AppConfigKeys.OnlineCrmUrl));
        }


        public SecureString GetCrmUsername()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.CrmUsername).ToSecureString();
        }


        public string GetPassword()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Password);
        }


        public string GetUsername()
        {

            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Username);
        }


        public string GetWebsite()
        {
            return ConfigurationManager.AppSettings.Get(AppConfigKeys.Website);

        }
    }
}
