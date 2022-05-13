using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class CallToActionButton : MutoboContentModule, IModule
    {


        public string Title => this.HasValue(ElementTypes.CallToActionButton.Fields.Title) ?
            this.Value<string>(ElementTypes.CallToActionButton.Fields.Title) : string.Empty;

        public string Text => this.HasValue(ElementTypes.CallToActionButton.Fields.Text) ?
            this.Value<string>(ElementTypes.CallToActionButton.Fields.Text) : string.Empty;

        public Link Link => this.HasValue(ElementTypes.CallToActionButton.Fields.Link) ?
            this.Value<Link>(ElementTypes.CallToActionButton.Fields.Link) : null;


        public CallToActionButton(
            IPublishedElement content,
            IPublishedValueFallback publishedValueFallback)
            : base(content, publishedValueFallback)
        {



        }
    }
}
