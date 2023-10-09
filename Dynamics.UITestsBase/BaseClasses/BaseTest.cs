using Dynamics.UITestsBase.Interfaces;
using Dynamics.UITestsBase.Misc;
using Dynamics.UITestsBase.PageObject;
using TechTalk.SpecFlow;

namespace Dynamics.UITestsBase.BaseClasses
{
    public class BaseTest : IBaseTest
    {
        private FeatureContext featureContext;
        private IExtentFeatureReport extentReport;

        public BaseTest()
        {
        }


        public void SetContext(FeatureContext featureContext)
        {
            this.featureContext = featureContext;
        }


        public void SetExtentReport(IExtentFeatureReport extentReport)
        {
            this.extentReport = extentReport;
            extentReport.CreateFeature(featureContext.FeatureInfo.Title);
        }


        public FeatureContext GetContext()
        {
            return featureContext;
        }


        public IExtentFeatureReport GetExtentReport()
        {
            return extentReport;
        }
    }
}
