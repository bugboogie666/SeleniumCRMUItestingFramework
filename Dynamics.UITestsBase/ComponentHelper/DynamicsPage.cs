using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

using Dynamics.UITestsBase.BaseClasses;

using Microsoft.Dynamics365.UIAutomation.Api.UCI;
using log4net;
using System.ServiceModel;
using Dynamics.UITestsBase.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dynamics.UITestsBase.ComponentHelper
{
    /// <summary>
    /// A helper class which contains a methods and structures that are used by pageobjects
    /// </summary>
    public class DynamicsPage : DynamicsUITest, IDynamicsPage
    {
        private protected ITestBaseManager testBaseManager;
        private protected IWebDriver webDriver;
        private protected XrmApp xrmApp;
        private protected ISeleniumHelper seleniumHelper;
        private protected INavigationHelper navigationHelper;

        WebDriverWait Wait
        {
            get
            {
                return new WebDriverWait(webDriver, TimeSpan.FromSeconds(20));
            }
        }

        public const int GRID_HEADER_ROW = 1;

        //private protected static readonly ILog Logger = LogHelper.GetLogger(typeof(DynamicsPage));
        public ILogging logging;
        public IWebElement SubgridElement => seleniumHelper.GetElement(By.XPath("//div[contains(@data-lp-id,'entity_control')]/parent::div/parent::div/parent::div"));
        public string RelatedGrid => GetGridFullName();

        public DynamicsPage()
        {


        }


        public DynamicsPage(IWebDriver webDriver, XrmApp xrmApp)
        {

        }


        public DynamicsPage(ITestBaseManager testBaseManager, INavigationHelper navigationHelper, ISeleniumHelper seleniumHelper, ILogging logging)
        {
            this.logging = logging;
            this.testBaseManager = testBaseManager;
            webDriver = testBaseManager.GetBaseTestUI().GetWebDriver();
            xrmApp = testBaseManager.GetBaseTestUI().GetXrmApp();
            this.seleniumHelper = seleniumHelper;
            this.navigationHelper = navigationHelper;
            this.logging = logging;
        }


        public T OpenEntityById<T>(string entityName, Guid entityId) where T : DynamicsPage
        {
            xrmApp.Entity.OpenEntity(entityName, entityId);
            return ServiceProvider.GetRequiredService<T>();
            //return (T)Activator.CreateInstance(typeof(T), new object[] { this.testBaseManager });
        }


        public T Command<T>(string command) where T : DynamicsPage
        {

            logging.Info($"{GetEntityName()} - clicking {command} button --> {typeof(T)}", MethodBase.GetCurrentMethod().Name);
            try
            {
                xrmApp.CommandBar.ClickCommand(command);
            }
            catch (Exception ex)
            {
                seleniumHelper.TakeScreenShot();
                logging.Error(ex.Message);
                logging.Error(ex.StackTrace);
                throw;
            }

            //Logger.Info($"Command {command} performed successfully.");
            logging.Info($"Command {command} performed successfully.");

            return ServiceProvider.GetRequiredService<T>();
        }


        public T Command<T>(string command, string gridName) where T : DynamicsPage
        {
            //Logger.Info($"Form: {xrmApp.Entity.GetFormName()} clicking {command} button on the grid: {gridName} --> {typeof(T)}");
            logging.Info($"Form: {xrmApp.Entity.GetFormName()} clicking {command} button on the grid: {gridName} --> {typeof(T)}", MethodBase.GetCurrentMethod().Name);
            try
            {
                xrmApp.Entity.SubGrid.ClickCommand(gridName, command);
            }
            catch (Exception ex)
            {
                seleniumHelper.TakeScreenShot();
                logging.Error(ex.Message);
                logging.Error(ex.StackTrace);
                throw;
            }
            logging.Info($"Command {command} performed successfully.");
            return ServiceProvider.GetRequiredService<T>();
        }


        public void Save()
        {
            logging.Info($"Clicking save button on the entity: {GetEntityName()}", MethodBase.GetCurrentMethod().Name);
            try
            {
                seleniumHelper.TakeScreenShot();
                xrmApp.CommandBar.ClickCommand("Save");
                seleniumHelper.TakeScreenShot();
                xrmApp.ThinkTime(3000);
            }
            catch (Exception ex)
            {
                seleniumHelper.TakeScreenShot();
                logging.Error(ex.Message);
                logging.Error(ex.StackTrace);
                throw;
            }

            var recordID = GetRecordId();

            if (recordID != Guid.Empty)
            {
                logging.Info($"Record with ID: {recordID} saved", MethodBase.GetCurrentMethod().Name);
            }
            else
            {
                logging.Warn($"Record has empty guid: it´s possible it has not been saved.");
            }
        }


        public T EditSubgridRecord<T>(string subgridName, List<IWebElement> subgridRecords, int index, bool? doubleClick = null) where T : DynamicsPage
        {
            Actions action = new Actions(webDriver);
            doubleClick = doubleClick ?? false;

            if (!Convert.ToBoolean(doubleClick))
            {
                subgridRecords[index].Click();
                xrmApp.Entity.SubGrid.ClickCommand(subgridName, "Edit");
            }
            else
            {
                action.DoubleClick(subgridRecords[index]).Perform();
            }
            return ServiceProvider.GetRequiredService<T>();
        }


        public T EditSubgridRecord<T>(string searchstring, List<IWebElement> records, string attribute) where T : DynamicsPage
        {
            var element = records.Where(e => e.GetAttribute(attribute).Contains(searchstring)).FirstOrDefault();
            if (element != null)
            {
                logging.Info($"Opening subgrid record based on: {element.Text}", MethodBase.GetCurrentMethod().Name);
                element.Click();
                seleniumHelper.TakeScreenShot();
                return ServiceProvider.GetRequiredService<T>();
            }
            return default;
        }


        public T OpenLookupRecord<T>(IWebElement toEntityLookup) where T : DynamicsPage
        {
            toEntityLookup.Click();
            return ServiceProvider.GetRequiredService<T>();
        }


        internal void SelectRelatedEntityAndView(string tabName, string entityName, string viewName = null)
        {
            logging.Info($"Selecting related tab {tabName}, entity: {entityName}", MethodBase.GetCurrentMethod().Name);
            xrmApp.Entity.SelectTab(tabName, entityName);
            if (viewName != null)
            {
                xrmApp.ThinkTime(6000);
                logging.Info($"Switching view {viewName}");
                xrmApp.Entity.SubGrid.SwitchView(RelatedGrid, viewName);
            }
        }


        /// <summary>
        /// This method gets a record id from a form
        /// </summary>
        /// <returns>Guid of specific record</returns>
        public Guid GetRecordId()
        {
            try
            {
                return xrmApp.Entity.GetObjectId();
            }
            catch (Exception ex)
            {
                if (IsNotAbleToGetEntityId(ex.Message))
                {
                    logging.Warn("Not able to get record id.");
                    return Guid.Empty;
                }
                logging.Error(ex.Message);
                logging.Error(ex.StackTrace);
                throw;
            }

            bool IsNotAbleToGetEntityId(string message)
            {
                return message.Contains("Unable to retrieve object Id for this entity")
                    || message.Contains("Cannot read properties of null (reading 'entity')");
            }
        }


        public string GetEntityName()
        {
            try
            {
                seleniumHelper.TakeScreenShot();
                return xrmApp.Entity.GetEntityName();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot read properties of null (reading 'entity')"))
                {
                    logging.Warn($"Not able to get entity name from grid.");
                    return "Grid";
                }
                logging.Error(ex.Message);
                logging.Error(ex.StackTrace);
                throw;
            }
        }


        /// <summary>
        /// This method gets a datetime and parse it as string in US format, so we can comfortably use it as input for
        /// specific CRM field
        /// </summary>
        /// <param name="date">15.3.2021</param>
        /// <returns>3/15/2021</returns>
        public string ConvertDateFormat(DateTime date)
        {
            var result = $"{date.Month}/{date.Day}/{date.Year}";
            return result;
        }


        /// <summary>
        /// This method gets a date as a string in format month/day/year and parse it as datetime
        /// converted to invariant culture
        /// </summary>
        /// <param name="date">3/15/2021</param>
        /// <returns>datetime object 15.3.2021</returns>
        public DateTime ConvertDateFromUSFormat(string date)
        {
            try
            {
                return DateTime.ParseExact(date, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                seleniumHelper.TakeScreenShot();
                throw new FormatException($"Value cannot be resolved, because the string format: {date} is not valid or empty string");
            }
        }


        public decimal GetPriceValueAsDecimal(string price)
        {
            var result = price.Replace(",", "").Replace(".", ",").Remove(0, 2);
            return Convert.ToDecimal(result);
        }


        public bool CheckFieldIsLocked(string field)
        {
            return xrmApp.Entity.GetField(field).IsReadOnly;
        }


        public void Refresh()
        {
            xrmApp.CommandBar.ClickCommand("Refresh");
        }


        public void SwitchToForm(string formName)
        {
            if (!String.IsNullOrEmpty(formName))
            {
                try
                {
                    xrmApp.Entity.SelectForm(formName);
                }
                catch (NotFoundException ex)
                {
                    throw new NotFoundException(ex.Message);
                }
            }
        }


        public string GetGridFullName()
        {
            var parts = SubgridElement.GetAttribute("data-id").Split('_');
            return parts[1].ToString();
        }
    }
}
