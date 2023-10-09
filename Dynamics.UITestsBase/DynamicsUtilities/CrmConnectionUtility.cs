using Dynamics.UITestsBase.Interfaces;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.UITestsBase.DynamicsUtilities
{
    public class CrmConnectionUtility : ICrmConnectionUtility
    {
        private CrmServiceClient serviceClient;
        IAppConfigReader configReader;
        ILogging logging;

        public CrmConnectionUtility(IAppConfigReader configReader, ILogging logging)
        {
            this.logging = logging;
            this.configReader = configReader;
            serviceClient = new CrmServiceClient(CreateConnectionString());
        }


        private string CreateConnectionString()
        {
            var userName = configReader.GetCrmUsername();
            var password = configReader.GetCrmPassword();
            var url = configReader.GetCrmUrl();

            return $@"AuthType=OAuth;Username={userName};Password={password};Url={url};AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97\";
        }


        public CrmServiceClient GetCrmService()
        {
            logging.Info("Try to get client service");
            return serviceClient;
        }
    }
}
