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
    public class DoubleSliderComponent : MutoboContentModule, IModule
    {
        public IEnumerable<TextImageSlide> Slides { get; set; }


        public int? Height => this.HasValue(ElementTypes.DoubleSliderComponent.Fields.Height)
            ? this.Value<int?>(ElementTypes.DoubleSliderComponent.Fields.Height)
            : null;

        public int? Interval => this.HasValue(ElementTypes.DoubleSliderComponent.Fields.Interval)
            ? this.Value<int?>(ElementTypes.DoubleSliderComponent.Fields.Interval) : null;



        public int? Width => this.HasValue(ElementTypes.DoubleSliderComponent.Fields.Width)
            ? this.Value<int?>(ElementTypes.DoubleSliderComponent.Fields.Width)
            : null;



        public DoubleSliderComponent(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }


        public string GetPictureNameSpace()
        {
            var result = "carousel-picture-";
            EGalleryType galleryType = EGalleryType.FullWidth;

            if (this.HasValue(ElementTypes.DoubleSliderComponent.Fields.DisplayType))
            {
                galleryType = (EGalleryType)System.Enum.Parse(typeof(EGalleryType),
                    this.Value<string>(ElementTypes.DoubleSliderComponent.Fields.DisplayType));

                if (galleryType == EGalleryType.Boxed)
                    result = "picture-";
            }

            return result;
        }

        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {

            return await helper.PartialAsync("~/Views/Partials/Modules/DoubleSlider.cshtml", this, helper.ViewData);

        }
    }
}
