using Dynamics.UITestsBase.Configuration;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ITestSettings
    {
        IAppConfigReader Reader { get; set; }
        CustomBrowserOptions Options { get; set; }

        CustomBrowserOptions InitializeSettings();
    }
}