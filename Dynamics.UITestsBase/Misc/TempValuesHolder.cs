using System.Collections.Generic;
using Dynamics.UITestsBase.ComponentHelper;
using Dynamics.UITestsBase.Interfaces;
using log4net;


namespace Dynamics.UITestsBase.Misc
{
    public class TempValuesHolder : ITempValuesHolder
    {
        //private static readonly ILog Logger = LogHelper.GetLogger(typeof(TempValuesHolder));
        ILogging logging;
        public Dictionary<string, string> ExpectedValuesHolder { get; set; }


        public TempValuesHolder(ILogging logging)
        {
            this.logging = logging;
            ExpectedValuesHolder = ExpectedValuesHolder ?? new Dictionary<string, string>();
        }


        public string GetExpectedValue(string key)
        {
            if (ExpectedValuesHolder.TryGetValue(key, out var value))
            {
                return value;
            }
            //Logger.Warn($"Key {key} is not seem to be memorized.");
            logging.Warn($"Key {key} is not seem to be memorized.");
            return null;
        }


        public void MemorizeExpectedValue(string key, string value)
        {
            if (!ExpectedValuesHolder.ContainsKey(key))
            {
                ExpectedValuesHolder.Add(key, value);
                //Logger.Info($"Key {key} with value {value} has been memorized.");
                logging.Info($"Key {key} with value {value} has been memorized.");
            }
            else
            {
                ExpectedValuesHolder[key] = value;
                //Logger.Info($"Value {value} has been updated for key {key}.");
                logging.Info($"Value {value} has been updated for key {key}.");
            }
        }
    }
}
