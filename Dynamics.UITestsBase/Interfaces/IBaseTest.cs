using Dynamics.UITestsBase.Misc;
using Dynamics.UITestsBase.PageObject;
using TechTalk.SpecFlow;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IBaseTest
    {
        FeatureContext GetContext();
        void SetContext(FeatureContext featureContext);
        void SetExtentReport(IExtentFeatureReport extentReport);
        IExtentFeatureReport GetExtentReport();

    }
}