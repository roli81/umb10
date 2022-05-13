using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class CardContainer : MutoboContentModule, IModule
    {

        public IEnumerable<Card> Cards { get; set; }


        public CardContainer(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }


        /// <summary>
        /// render the module
        /// </summary>
        /// <param name="helper">current html helper</param>
        /// <returns>rendered module async</returns>
        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            return await helper.PartialAsync("~/Views/Partials/Modules/CardContainer.cshtml", this, helper.ViewData);
        }
    }
}
