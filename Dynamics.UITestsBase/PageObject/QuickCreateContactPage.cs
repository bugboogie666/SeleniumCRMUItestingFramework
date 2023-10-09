using System;
using System.ComponentModel;

using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.ComponentHelper;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;

using Kentico.Internal.Dynamics.Types;
using System.Collections.Generic;
using System.Reflection;

using System.Linq;
using Bogus;
using log4net;
using Dynamics.UITestsBase.BaseClasses;
using Dynamics.UITestsBase.Misc;
using OpenQA.Selenium;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.PageObject
{
    public class QuickCreateContactPage : DynamicsPage, IQuickCreateContactPage
    {
        public int LookupItemIndex { get; set; }

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(15));
            }
        }


        [Description("FirstName")]
        public string FirstName
        {
            get
            {
                return xrmApp.QuickCreate.GetValue(Contact.Fields.FirstName);
            }
            set
            {
                xrmApp.QuickCreate.SetValue(Contact.Fields.FirstName, value);
            }
        }


        [Description("LastName")]
        public string LastName
        {
            get
            {
                return xrmApp.QuickCreate.GetValue(Contact.Fields.LastName);
            }
            set
            {
                xrmApp.QuickCreate.SetValue(Contact.Fields.LastName, value);
            }
        }


        [Description("Email")]
        public string Email
        {
            get
            {
                return xrmApp.QuickCreate.GetValue(Contact.Fields.EMailAddress1);
            }
            set
            {
                xrmApp.QuickCreate.SetValue(Contact.Fields.EMailAddress1, value);
            }
        }


        [Description("Account name")]
        public string AccountName
        {
            get
            {
                return xrmApp.QuickCreate.GetValue(new LookupItem { Name = Contact.Fields.ParentCustomerId });
            }
            set
            {
                xrmApp.QuickCreate.SetValue(new LookupItem { Name = Contact.Fields.ParentCustomerId, Value = value, Index = LookupItemIndex });
            }
        }


        public QuickCreateContactPage(IWebDriver webDriver, XrmApp xrmApp) : base(webDriver, xrmApp)
        {
        }


        public QuickCreateContactPage()
        {
            LookupItemIndex = 0;
        }


        public QuickCreateContactPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging) : base(testBaseManager, navigationHelper, seleniumHelper, logging)
        {

        }


        public void SetDataForContact(Dictionary<string, string> data)
        {
            foreach (var record in data)
            {
                PropertyInfo property = GetType().GetProperty(record.Key);
                property.SetValue(this, record.Value);
            }
        }


        public void BuildRandomDataForContact(Dictionary<string, string> data, ILog Logger = null)
        {
            var quickContactDataHandler = new ContactRandomDataHandler(this, data);
            quickContactDataHandler.RandomizeData();
        }
    }
}
