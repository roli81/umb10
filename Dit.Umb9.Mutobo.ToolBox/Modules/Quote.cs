using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class Quote : MutoboContentModule, IModule
    {
        public string QuoteText => this.HasValue(ElementTypes.Quote.Fields.QuoteText)
    ? this.Value<string>(ElementTypes.Quote.Fields.QuoteText)
    : string.Empty;

        public string SpellerName => this.HasValue(ElementTypes.Quote.Fields.SpellerName)
            ? this.Value<string>(ElementTypes.Quote.Fields.SpellerName)
            : string.Empty;

        public string SpellerFunction => this.HasValue(ElementTypes.Quote.Fields.SpellerFunction)
            ? this.Value<string>(ElementTypes.Quote.Fields.SpellerFunction)
            : string.Empty;


        public Quote(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }


        public async Task<IHtmlContent> RenderModule(Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper helper)
        {
            return await helper.PartialAsync("~/Views/Partials/Modules/Quote.cshtml", this, helper.ViewData);
        }
    }
}
