using Dynamics.UITestsBase.BaseClasses;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ITestBaseManager
    {
        BaseTest GetBaseTest();
        BaseTestUI GetBaseTestUI();
        void SetBaseTest(BaseTest baseTest);
    }
}