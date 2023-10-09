using ExtentHTMLReports = AventStack.ExtentReports;
using Dynamics.UITestsBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dynamics.UITestsBase.Reporting
{
    public class ExtentFeatureReport : IExtentFeatureReport
    {
        IExtentReport extentReport;
        ExtentHTMLReports.ExtentTest feature, scenario, step;

        public ExtentFeatureReport(IExtentReport extentReport)
        {
            this.extentReport = extentReport;
            feature = null;
            scenario = null;
            step = null;
        }


        public void CreateFeature(string feature)
        {
            this.feature = extentReport.GetExtentReports().CreateTest(feature);
        }


        public void CreateScenario(string scenario)
        {
            this.scenario = feature.CreateNode(scenario);
        }


        public void AddStepInformation(string stepMessage, ExtentHTMLReports.Status status)
        {
            scenario.Log(status, stepMessage);
        }


        public void Error(string message)
        {
            AddStepInformation(message, ExtentHTMLReports.Status.Error);
        }


        public void Info(string message)
        {
            AddStepInformation(message, ExtentHTMLReports.Status.Info);
        }


        public void Warning(string message)
        {
            AddStepInformation(message, ExtentHTMLReports.Status.Warning);
        }


        public void Fail(string message)
        {
            AddStepInformation(message, ExtentHTMLReports.Status.Fail);
        }


        public void Pass(string message)
        {
            AddStepInformation(message, ExtentHTMLReports.Status.Pass);
        }
    }
}
