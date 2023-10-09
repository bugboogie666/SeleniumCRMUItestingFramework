namespace Dynamics.UITestsBase.Interfaces
{
    public interface IContactRandomDataHandler
    {
        void GenerateEmail(string key);
        void GenerateLastName(string key);
        void RandomizeData();
    }
}