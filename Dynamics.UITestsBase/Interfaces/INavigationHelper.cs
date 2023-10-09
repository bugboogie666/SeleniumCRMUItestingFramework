using System;
using System.Security;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface INavigationHelper
    {
        void LoginAndOpenDynamicsApp(string appName, Uri url, SecureString username, SecureString password);
        void NavigateToSubarea(string area, string subarea);
        void NavigateToUrl(string url);
        void SignOut();
    }
}