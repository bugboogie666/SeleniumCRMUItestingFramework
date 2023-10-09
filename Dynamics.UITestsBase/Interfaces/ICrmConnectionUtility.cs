using Microsoft.Xrm.Tooling.Connector;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface ICrmConnectionUtility
    {
        CrmServiceClient GetCrmService();
    }
}