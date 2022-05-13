using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Common.Exceptions
{
    public class NavigationException : MutoboException
    {
        public NavigationException(string message) : base(message)
        {
        }
    }
}
