using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Enum;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class Teaser : MutoboContentModule, IModule
    {
        public Link Link => this.HasValue(ElementTypes.Teaser.Fields.Link)
    ? this.Value<Link>(ElementTypes.Teaser.Fields.Link)
    : null;

        public IEnumerable<Image> Images { get; set; }

        public bool UseArticleData => this.HasValue(ElementTypes.Teaser.Fields.UseArticleData) && this.Value<bool>(ElementTypes.Teaser.Fields.UseArticleData);

        public string TeaserTitle { get; set; }

        public string TeaserText { get; set; }

        public EHighlightRendering RenderAs => this.HasValue(ElementTypes.Teaser.Fields.RenderAs)
            ? (EHighlightRendering)System.Enum.Parse(typeof(EHighlightRendering),
                this.Value<string>(ElementTypes.Teaser.Fields.RenderAs))
            : EHighlightRendering.None;


        public Teaser(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            var bld = new StringBuilder();

            switch (RenderAs)
            {
                default:
                case EHighlightRendering.Teaser:
                    return await helper.PartialAsync("~/Views/Partials/Modules/NestedTeaser.cshtml", this);


                case EHighlightRendering.Gallery:
                    return await helper.PartialAsync("~/Views/Partials/Modules/GalleryTeaser.cshtml", this);

            }


        }
    }
}
