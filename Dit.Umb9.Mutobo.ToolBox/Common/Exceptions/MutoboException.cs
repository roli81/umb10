using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Common.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class MutoboException : Exception
    {
        public MutoboException(string message)
            : base(message)
        {

        }
    }
}
