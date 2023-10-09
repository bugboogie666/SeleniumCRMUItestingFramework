using System.Reflection;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ILogging
    {
        void Debug(string message, string methodName = " ");
        void Error(string message, string methodName = " ");
        void Fatal(string message, string methodName = " ");
        void Info(string message, string methodName = " ");
        void SetLogLevel(string logLevel);
        void Warn(string message, string methodName = " ");
    }
}