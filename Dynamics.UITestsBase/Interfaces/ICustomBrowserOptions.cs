using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ICustomBrowserOptions
    {
        ChromeOptions ToChrome();
        EdgeOptions ToEdge();
        FirefoxOptions ToFireFox();
    }
}