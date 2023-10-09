using ExtentHTMLReports = AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dynamics.UITestsBase.Interfaces;

namespace Dynamics.UITestsBase.Reporting
{
    public class ExtentReport : IExtentReport
    {
        ExtentHtmlReporter extentHtmlReporter;
        ExtentHTMLReports.ExtentReports extendReports;
        IDefaultVariables defaultVariables;


        public ExtentReport(IDefaultVariables defaultVariables)
        {
            this.defaultVariables = defaultVariables;
        }


        public void InitializeExtendReport()
        {
            extentHtmlReporter = new ExtentHtmlReporter(defaultVariables.ExtendReport);
            extendReports = new ExtentHTMLReports.ExtentReports();
            extendReports.AttachReporter(extentHtmlReporter);
        }


        public ExtentHTMLReports.ExtentReports GetExtentReports()
        {
            return extendReports;
        }


        public void FlushExtendReport()
        {
            extendReports.Flush();
        }
    }
}
