using Dynamics.UITestsBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamics.UITestsBase.Params
{
    public class DefaultVariables : IDefaultVariables
    {
        public const string REPORT_DIR_SUFFIX_FORMAT = "yyyyMMdd hhmmss";
        public string Results => System.IO.Directory.GetParent($"{AppDomain.CurrentDomain.BaseDirectory}../../../").FullName + @"\Results\Reports_" + DateTime.Now.ToString(REPORT_DIR_SUFFIX_FORMAT);
        public string Log => $"{Results}\\log.txt";
        public string ExtendReport => $"{Results}\\result.html";
        public string ScreenFolder => $"{Results}\\screens\\";
    }
}
