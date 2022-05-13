using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Common.Exceptions
{
    /// <summary>
    /// Exception for error within image processing
    /// </summary>
    public class ImageException : MutoboException
    {
        public ImageException(string message) 
            : base(message)
        {
        }
    }
}
