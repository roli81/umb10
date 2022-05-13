using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    class RichtextComponent : MutoboContentModule, IModule
    {
        public string RichText => this.HasValue(ElementTypes.RichTextComponent.Fields.RichText)
    ? this.Value<string>(ElementTypes.RichTextComponent.Fields.RichText)
    : string.Empty;

        public RichtextComponent(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            var bld = new StringBuilder();
            bld.Append($"<article>{helper.Raw(RichText)}</article>");
            return await Task.FromResult<IHtmlContent>(new HtmlString(bld.ToString()));
        }
    }
}
