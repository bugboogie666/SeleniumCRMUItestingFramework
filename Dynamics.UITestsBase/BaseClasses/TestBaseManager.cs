using Dynamics.UITestsBase.Interfaces;
using System.Threading;

namespace Dynamics.UITestsBase.BaseClasses
{
    public class TestBaseManager : ITestBaseManager
    {
        private static ThreadLocal<BaseTest> _baseTest = new ThreadLocal<BaseTest>();

        public void SetBaseTest(BaseTest baseTest)
        {
            _baseTest.Value = baseTest;
        }


        public BaseTest GetBaseTest()
        {
            return _baseTest.Value;
        }


        public BaseTestUI GetBaseTestUI()
        {
            return (BaseTestUI)_baseTest.Value;
        }




    }
}
