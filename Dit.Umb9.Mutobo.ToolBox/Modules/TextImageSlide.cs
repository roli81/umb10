using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
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
    public class TextImageSlide : MutoboContentModule, ISliderItem
    {
        public Image Image { get; set; }

        public string Text => this.HasValue(ElementTypes.TextImageSlide.Fields.Text)
            ? this.Value<string>(ElementTypes.TextImageSlide.Fields.Text)
            : null;

        public Link Link => this.HasValue(ElementTypes.TextImageSlide.Fields.Link)
            ? this.Value<Link>(ElementTypes.TextImageSlide.Fields.Link)
            : null;
        public string Title => this.HasValue(ElementTypes.TextImageSlide.Fields.Title)
            ? this.Value<string>(ElementTypes.TextImageSlide.Fields.Title)
            : null;



        public TextImageSlide(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }
    }
}
