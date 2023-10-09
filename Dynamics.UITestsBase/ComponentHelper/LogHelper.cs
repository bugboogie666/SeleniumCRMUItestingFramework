using System;

using log4net;
using log4net.Config;


namespace Dynamics.UITestsBase.ComponentHelper
{
    internal class LogHelper
    {
        #region Field
        private static ILog _logger;
        #endregion


        #region Public
        public static ILog GetLogger(Type type)
        {
            if (_logger != null)
            {
                return _logger;
            }
            XmlConfigurator.Configure();
            _logger = LogManager.GetLogger(type);

            return _logger;
        }
        #endregion
    }
}
