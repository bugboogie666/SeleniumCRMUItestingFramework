namespace Dynamics.UITestsBase.Interfaces
{
    public interface IExtentFeatureReport
    {
        void CreateFeature(string feature);
        void CreateScenario(string scenario);
        void Error(string message);
        void Fail(string message);
        void Pass(string message);
        void Info(string message);
        void Warning(string message);
    }
}