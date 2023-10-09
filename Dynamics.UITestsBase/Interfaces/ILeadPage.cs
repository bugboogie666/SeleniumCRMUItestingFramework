using OpenQA.Selenium;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ILeadPage
    {
        IWebElement IntercomLinkFrame { get; }
        IWebElement KenticoLink { get; }
        IWebElement KenticoLinkFrame { get; }
        IWebElement KlentyLink { get; }
        IWebElement KlentyLinkFrame { get; }
        int LookupItemIndex { get; set; }

        void GetKenticoLinkElement(out IWebElement link);
        void GetKlentyLinkElement(out IWebElement link);
        string GetLeadUrl();
    }
}