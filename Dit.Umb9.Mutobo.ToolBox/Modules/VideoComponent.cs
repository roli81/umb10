using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class VideoComponent : MutoboContentModule, ISliderItem, IVideoComponent, IModule
    {
        public VideoComponent(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }

        public Video Video => this.HasValue(ElementTypes.VideoComponent.Fields.VideoFile)
            ? new Video()
            {
                Source = this.Value<IPublishedContent>(ElementTypes.VideoComponent.Fields.VideoFile).MediaUrl()
            }
            : null;

        public String Embedded => this.HasValue(ElementTypes.VideoComponent.Fields.Embedded)
            ? this.Value<string>(ElementTypes.VideoComponent.Fields.Embedded)
            : null;

        public String Text => this.HasValue(ElementTypes.VideoComponent.Fields.Text)
            ? this.Value<string>(ElementTypes.VideoComponent.Fields.Text)
            : null;


        public int? Width => this.HasValue(ElementTypes.VideoComponent.Fields.Width)
            ? this.Value<int?>(ElementTypes.VideoComponent.Fields.Width)
            : null;



        public int? Height => this.HasValue(ElementTypes.VideoComponent.Fields.Height)
            ? this.Value<int?>(ElementTypes.VideoComponent.Fields.Height)
            : null;



        public IHtmlContent RenderIFrame(int? width = null, int? height = null)
        {
            var newWidth = width ?? Width;
            var newHeight = height ?? Height;
            var result = Embedded;

            if (newWidth.HasValue)
                result = Regex.Replace(result.ToLower(), "width=\"([0-9]{1,4})\"", $"width=\"{newWidth}\"");
            if (newHeight.HasValue)
                result = Regex.Replace(result.ToLower(), "height=\"([0-9]{1,4})\"", $"height=\"{newHeight}\"");

            return new HtmlString(result);
        }

        public async Task<IHtmlContent> RenderModule(Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper helper)
        {

            return await helper.PartialAsync("~/Views/Partials/Modules/VideoComponent.cshtml", this, helper.ViewData);

        }
    }
}
