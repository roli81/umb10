using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Enum;
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
    public class SliderComponent : MutoboContentModule, ISliderComponent, IModule
    {

        public IEnumerable<ISliderItem> Slides { get; set; }

        public int? Height => this.HasValue(ElementTypes.SliderComponent.Fields.Height) && this.Value<int?>(ElementTypes.SliderComponent.Fields.Height) > 0
            ? this.Value<int?>(ElementTypes.SliderComponent.Fields.Height)
            : null;
        public int? Interval => this.HasValue(ElementTypes.SliderComponent.Fields.Interval)
            ? this.Value<int?>(ElementTypes.SliderComponent.Fields.Interval)
        : null;



        public int? Width => this.HasValue(ElementTypes.SliderComponent.Fields.Width) && this.Value<int?>(ElementTypes.SliderComponent.Fields.Width) > 0
            ? this.Value<int?>(ElementTypes.SliderComponent.Fields.Width)
            : null;

        public string GetPictureNameSpace()
        {
            var result = "carousel-picture-";
            EGalleryType galleryType = EGalleryType.FullWidth;

            if (this.HasValue(ElementTypes.SliderComponent.Fields.DisplayType))
            {
                galleryType = (EGalleryType)System.Enum.Parse(typeof(EGalleryType),
                    this.Value<string>(ElementTypes.SliderComponent.Fields.DisplayType));

                if (galleryType == EGalleryType.Boxed)
                    result = "picture-";
            }

            return result;
        }

        public SliderComponent(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public async Task<IHtmlContent> RenderModule(Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper helper)
        {
            return await helper.PartialAsync("~/Views/Partials/Modules/Slider.cshtml", this, helper.ViewData);
        }

    }
}
