using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ITempValuesHolder
    {
        Dictionary<string, string> ExpectedValuesHolder { get; set; }

        string GetExpectedValue(string key);
        void MemorizeExpectedValue(string key, string value);
    }
}