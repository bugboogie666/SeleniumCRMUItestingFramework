using log4net;
using System.Collections.Generic;

namespace Dynamics.UITestsBase.Interfaces
{
    public interface IQuickCreateContactPage
    {
        string AccountName { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int LookupItemIndex { get; set; }

        void BuildRandomDataForContact(Dictionary<string, string> data, ILog Logger = null);
        void SetDataForContact(Dictionary<string, string> data);
    }
}