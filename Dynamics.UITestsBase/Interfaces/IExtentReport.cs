namespace Dynamics.UITestsBase.Interfaces
{
    public interface IExtentReport
    {
        AventStack.ExtentReports.ExtentReports GetExtentReports();
        void InitializeExtendReport();
        void FlushExtendReport();
    }
}