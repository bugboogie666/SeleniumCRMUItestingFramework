using Dynamics.UITestsBase.BaseClasses;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.ComponentHelper
{
    public interface IDynamicsPage
    {
        string RelatedGrid { get; }
        IWebElement SubgridElement { get; }

        bool CheckFieldIsLocked(string field);
        T Command<T>(string command) where T : DynamicsPage;
        T Command<T>(string command, string gridName) where T : DynamicsPage;
        string ConvertDateFormat(DateTime date);
        DateTime ConvertDateFromUSFormat(string date);
        T EditSubgridRecord<T>(string subgridName, List<IWebElement> subgridRecords, int index, bool? doubleClick = null) where T : DynamicsPage;
        T EditSubgridRecord<T>(string searchstring, List<IWebElement> records, string attribute) where T : DynamicsPage;
        string GetEntityName();
        string GetGridFullName();
        decimal GetPriceValueAsDecimal(string price);
        Guid GetRecordId();
        T OpenEntityById<T>(string entityName, Guid entityId) where T : DynamicsPage;
        T OpenLookupRecord<T>(IWebElement toEntityLookup) where T : DynamicsPage;
        void Refresh();
        void Save();
        void SwitchToForm(string formName);
    }
}