using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Enum;
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
    public class Heading : MutoboContentModule, IModule
    {
        public string Text => this.HasValue(ElementTypes.Heading.Fields.Text)
    ? this.Value<string>(ElementTypes.Heading.Fields.Text)
    : string.Empty;

        public EHeadingRenderType RenderAs => this.HasValue(ElementTypes.Heading.Fields.RenderAs)
            ? (EHeadingRenderType)System.Enum.Parse(typeof(EHeadingRenderType), this.Value<string>(ElementTypes.Heading.Fields.RenderAs))
            : EHeadingRenderType.Heading1;

        public string NavigationAnchor => this.HasValue(ElementTypes.Heading.Fields.NavigationAnchor)
            ? this.Value<string>(ElementTypes.Heading.Fields.NavigationAnchor)
            : null;


        public Heading(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            var bld = new StringBuilder();

            var anchor = $"id=\"{NavigationAnchor}\"" ?? string.Empty;

            switch (RenderAs)
            {
                case EHeadingRenderType.Heading1:
                    bld.Append($"<h1  {anchor}>{Text.ToUpper()}</h1>");
                    break;
                case EHeadingRenderType.Heading2:
                    bld.Append($"<h2  {anchor}>{Text.ToUpper()}</h2>");
                    break;
                case EHeadingRenderType.Heading3:
                    bld.Append($"<h3  {anchor}>{Text.ToUpper()}</h3>");
                    break;
                case EHeadingRenderType.Heading4:
                    bld.Append($"<h4  {anchor}>{Text.ToUpper()}</h4>");
                    break;
                case EHeadingRenderType.Heading5:
                    bld.Append($"<h5  {anchor}>{Text.ToUpper()}</h5>");
                    break;
                case EHeadingRenderType.Heading6:
                    bld.Append($"<h6  {anchor}>{Text.ToUpper()}</h6>");
                    break;
            }



            return await Task.FromResult<IHtmlContent>(new HtmlString(bld.ToString()));
        }
    }
}
