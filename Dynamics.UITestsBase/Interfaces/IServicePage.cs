namespace Dynamics.UITestsBase.Interfaces
{
    public interface IServicePage
    {
        bool IsPurchased { get; }
        bool IsUnpaid { get; }
        int LookupItemIndex { get; set; }
        string Name { get; set; }
        string StatusReason { get; set; }
    }
}