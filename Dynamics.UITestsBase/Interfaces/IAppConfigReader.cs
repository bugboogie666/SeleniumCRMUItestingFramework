using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IAppConfigReader
    {
        bool RunAsHeadless { get; }
        bool RunAsIncognito { get; }
        int CookiesControlMode { get; }
        BrowserType GetBrowser();
        SecureString GetCrmPassword();
        Uri GetCrmUrl();
        SecureString GetCrmUsername();
        string GetPassword();
        string GetUsername();
        string GetWebsite();
    }
}