using Dynamics.UITestsBase.Interfaces;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using System.IO;
using System.Reflection;

namespace Dynamics.UITestsBase.Reporting
{
    public class Logging : ILogging
    {
        LoggingLevelSwitch loggingLevelSwitch;
        public const string LOGGING_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3} [{MemberName}] {Message:lj}{NewLine}{Exception}";

        public Logging()
        {
            var executingLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = $"{executingLocation}\\logfileUI.txt";
            loggingLevelSwitch = new LoggingLevelSwitch(Serilog.Events.LogEventLevel.Information);
            // this.SetLogLevel(new AppConfigReader())

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(loggingLevelSwitch)
                .WriteTo.File(filePath, outputTemplate: LOGGING_TEMPLATE)
                .Enrich.FromLogContext()
                .Enrich.WithThreadId()
                .CreateLogger();
        }


        public void SetLogLevel(string logLevel)
        {
            switch (logLevel.ToLower())
            {
                case "information":
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Information;
                    break;
                case "debug":
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                    break;
                case "warning":
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Warning;
                    break;
                case "error":
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Error;
                    break;
                case "fatal":
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Fatal;
                    break;
                default:
                    loggingLevelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
                    break;
            }
        }


        public void Info(string message, string methodName = " ")
        {
            using (LogContext.PushProperty("MemberName", methodName))
            {
                Log.Logger.Information(message);
            }
        }


        public void Debug(string message, string methodName = " ")
        {
            using (LogContext.PushProperty("MemberName", methodName))
            {
                Log.Logger.Debug(message);
            }
        }


        public void Warn(string message, string methodName = " ")
        {
            using (LogContext.PushProperty("MemberName", methodName))
            {
                Log.Logger.Warning(message);
            }
        }


        public void Error(string message, string methodName = " ")
        {
            using (LogContext.PushProperty("MemberName", methodName))
            {
                Log.Logger.Error(message);
            }
        }


        public void Fatal(string message, string methodName = " ")
        {
            using (LogContext.PushProperty("MemberName", methodName))
            {
                Log.Logger.Fatal(message);
            }
        }
    }
}
