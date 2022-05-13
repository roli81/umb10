using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Enum;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class Flyer : MutoboContentModule, IModule
    {

        public string Title => this.Value<string>(ElementTypes.Flyer.Fields.FlyerTitle);

        // Attributes for Frontend
        public string Color => this.Value<string>(ElementTypes.Flyer.Fields.Color);

        public EDirection Direction => this.HasValue(ElementTypes.Flyer.Fields.Direction) ?
            (EDirection)System.Enum.Parse(typeof(EDirection), this.Value<string>(ElementTypes.Flyer.Fields.Direction)) :
            (EDirection)System.Enum.Parse(typeof(EDirection), "Undefined");

        public int Timer => this.Value<int>(ElementTypes.Flyer.Fields.Timer);

        public EPosition Position =>
            (EPosition)System.Enum.Parse(typeof(EPosition), this.Value<string>(ElementTypes.Flyer.Fields.Position));

        public int Height => this.Value<int>(ElementTypes.Flyer.Fields.Height);
        public int Width => this.Value<int>(ElementTypes.Flyer.Fields.Width);
        public int Rotation => this.Value<int>(ElementTypes.Flyer.Fields.Rotation);
        public int MarginTop => this.Value<int>(ElementTypes.Flyer.Fields.MarginTop);
        public int MarginSide => this.Value<int>(ElementTypes.Flyer.Fields.MarginSide);
        public int? TextHeight => this.Value<int?>(ElementTypes.Flyer.Fields.TextHeight);
        public int? TextWidth => this.Value<int?>(ElementTypes.Flyer.Fields.TextWidth);



        public Image Image { get; set; }
        public string TeaserText { get; set; }
        public Link Link { get; set; }

        public Flyer(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }


        public async Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
           
            // mayb e usefull for classics 
            //if (Timer > 0)
            //{
            //    switch (Direction)
            //    {
            //        case EDirection.Right:
            //            bld.Append(await helper.PartialAsync("~/Views/Partials/Flyer_right.cshtml",
            //                this, helper.ViewData));
            //            break;

            //        default:
            //        case EDirection.Left:
            //            bld.Append(await helper.PartialAsync("~/Views/Partials/Flyer_left.cshtml", this, helper.ViewData));
            //            break;
            //    }
            //}
            //else
            //{
            //    switch (Direction)
            //    {
            //        case EDirection.Right:
            //            bld.Append(await helper.PartialAsync("~/Views/Partials/IntersectionFlyer_right.cshtml",
            //                this, helper.ViewData));
            //            break;

            //        default:
            //        case EDirection.Left:
            //            bld.Append(await helper.PartialAsync("~/Views/Partials/IntersectionFlyer_left.cshtml", this, helper.ViewData));
            //            break;
            //    }

            //}


            return await helper.PartialAsync("~/Views/Partials/Modules/FlyerTeaser.cshtml", this, helper.ViewData);
        }
    }

}

