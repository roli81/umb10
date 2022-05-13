using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;

namespace Dit.Umb9.Mutobo.ToolBox.Models.PoCo
{
    public class FooterNavBlock
    {
        public Link Title { get; set; }
        public IEnumerable<Link> Children { get; set; }
    }
}
