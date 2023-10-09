using OpenQA.Selenium;

namespace Dynamics.UITestsBase.Interfaces
{
    internal interface IEmailPage
    {
        IWebElement EmailBodyDesignerFrame { get; }
        By EmailBodyDesignerFrameSelector { get; }
        IWebElement EmailBodyFrame { get; }
        IWebElement EmailBodyFrameFailed { get; }
        By EmailBodyFrameFailedSelector { get; }
        By EmailBodyFrameSelector { get; }
        string EmailDescription { get; set; }
        IWebElement ExpandIcon { get; }
        bool IsDraft { get; }
        bool IsFailed { get; }
        bool IsPendingSend { get; }
        int LookupItemIndex { get; set; }
        string StatusReason { get; set; }
        string Subject { get; set; }

        string GetEmailBodyContent(string text, By frameSelector, int location = 0);
    }
}