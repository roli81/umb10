using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Pages
{
    public class ContentPage : ArticlePage
    {
        public IEnumerable<IModule> Modules { get; set; }


        public ContentPage(IPublishedContent content) : base(content)
        {
        }
    }
}
