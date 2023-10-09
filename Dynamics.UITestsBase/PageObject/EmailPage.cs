using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Interfaces;

[assembly: InternalsVisibleTo("Dynamics.UITests")]

namespace Dynamics.UITestsBase.PageObject
{
    public class EmailPage : DynamicsPage, IEmailPage
    {
        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            }
        }


        public string Subject
        {
            get
            {
                return xrmApp.Entity.GetValue(Email.Fields.Subject);
            }
            set
            {
                xrmApp.Entity.SetValue(Email.Fields.Subject, value);
            }
        }


        public string EmailDescription
        {
            get
            {
                return xrmApp.Entity.GetValue("email description");
            }
            set
            {
                xrmApp.Entity.SetValue("email description", value);
            }
        }


        [Description("Status Reason")]
        public string StatusReason
        {
            get
            {
                //ExpandIcon.Click();
                return xrmApp.Entity.GetHeaderValue(new OptionSet { Name = Email.Fields.StatusCode });
            }
            set
            {
                //ExpandIcon.Click();
                xrmApp.Entity.SetHeaderValue(new OptionSet { Name = Email.Fields.StatusCode, Value = value });
            }
        }

        public By EmailBodyFrameSelector => By.XPath("//iframe[@title='Browser Preview Iframe']");
        public By EmailBodyFrameFailedSelector => By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']");
        public By EmailBodyDesignerFrameSelector => By.XPath("//iframe[@title='Designer']");

        private List<By> Frames => new List<By> { EmailBodyDesignerFrameSelector, EmailBodyFrameFailedSelector };

        public IWebElement ExpandIcon => seleniumHelper.GetElement(By.Id("expandIcon"), SeleniumHelper.ElementSyncCondition.ToBeClickable, Wait);
        public IWebElement EmailBodyFrame => seleniumHelper.GetElement(By.XPath("//iframe[@title='Browser Preview Iframe']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);

        public IWebElement EmailBodyFrameFailed => seleniumHelper.GetElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);

        public IWebElement EmailBodyDesignerFrame => seleniumHelper.GetElement(By.XPath("//iframe[@title='Designer']"), SeleniumHelper.ElementSyncCondition.Exists, Wait);


        public EmailPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
            LookupItemIndex = 0;
        }


        public EmailPage()
        {
        }


        public EmailPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {
        }


        public bool IsPendingSend => seleniumHelper.IsElementPresent(By.XPath("//div[@aria-label='Pending Send']"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);
        public bool IsFailed => seleniumHelper.IsElementPresent(By.XPath("//div[contains(text(),'Failed')]"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);

        public bool IsDraft => seleniumHelper.IsElementPresent(By.XPath("//div[contains(text(),'Draft')]"), SeleniumHelper.ElementSyncCondition.ToBeVisible, Wait);


        public string GetEmailBodyContent(string text, By frameSelector, int location = 0)
        {
            var xpathSelector = $"//div[contains(text(),'{text}')]";

            while (true)
            {
                try
                {
                    var frame = seleniumHelper.GetElement(frameSelector, SeleniumHelper.ElementSyncCondition.Exists, Wait);
                    webDriver.SwitchTo().Frame(frame);
                    var element = seleniumHelper.GetElement(By.XPath(xpathSelector), SeleniumHelper.ElementSyncCondition.Exists, Wait);
                    var bodyContent = element.Text;
                    webDriver.SwitchTo().DefaultContent();
                    return bodyContent;
                }
                catch (Exception ex)
                {
                    if (location < Frames.Count)
                    {
                        return GetEmailBodyContent(text, Frames[location], location + 1);
                    }
                    return ex.Message;
                }
            }
        }
    }
}
