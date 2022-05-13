using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
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
    public class PictureModule : MutoboContentModule, ISliderItem, IModule
    {
        public Image Image { get; set; }

        public int? Height => this.HasValue(ElementTypes.PictureModule.Fields.Height)
            ? this.Value<int?>(ElementTypes.PictureModule.Fields.Height)
            : null;


        public int? Width => this.HasValue(ElementTypes.PictureModule.Fields.Width)
            ? this.Value<int?>(ElementTypes.PictureModule.Fields.Width)
            : null;


        public PictureModule(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }



        public async Task<IHtmlContent> RenderModule(Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper helper)
        {
            return await helper.PartialAsync("~/Views/Partials/Modules/PictureModule.cshtml", this, helper.ViewData);
        }
    }
}
