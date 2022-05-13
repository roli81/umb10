using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Common.Exceptions
{

    /// <summary>
    /// Exception for errors in appsetting
    /// </summary>
    public class AppSettingsException : MutoboException
    {
        public AppSettingsException(string message) : base(message)
        {
        }
    }
}
