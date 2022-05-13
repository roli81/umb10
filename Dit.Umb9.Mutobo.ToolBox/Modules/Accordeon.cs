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
    /// <summary>
    /// model class for an accordeon module rendered as html summary details
    /// </summary>
    public class Accordeon : MutoboContentModule, IModule
    {

        /// <summary>
        /// summary text
        /// </summary>
        public string Summary => this.HasValue(ElementTypes.Accordeon.Fields.Summary)
            ? this.Value<string>(ElementTypes.Accordeon.Fields.Summary)
            : null;

        /// <summary>
        /// details text
        /// </summary>
        public string Details => this.HasValue(ElementTypes.Accordeon.Fields.Details)
            ? this.Value<string>(ElementTypes.Accordeon.Fields.Details)
            : null;

        /// <summary>
        /// constructor for an accordeon 
        /// </summary>
        /// <param name="content">umbraco content node</param>
        /// <param name="publishedValueFallback">???</param>
        public Accordeon(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        /// <summary>
        /// render the module
        /// </summary>
        /// <param name="helper">current html helper</param>
        /// <returns>rendered module async</returns>
        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            return await helper.PartialAsync("~/Views/Partials/Modules/Accordeon.cshtml", this, helper.ViewData);
        }
    }
}
