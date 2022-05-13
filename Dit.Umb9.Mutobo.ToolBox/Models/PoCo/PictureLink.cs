using Dit.Umb9.Mutobo.ToolBox.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Models.PoCo
{
    public class PictureLink : PublishedElementModel
    {

        public Image Image { get; set; }
        public Link Link => this.HasValue(ElementTypes.PictureLink.Fields.Link)
            ? this.Value<Link>(ElementTypes.PictureLink.Fields.Link)
        : null;
        public Link LogoLink => this.HasValue(ElementTypes.PictureLink.Fields.LogoLink)
            ? this.Value<Link>(ElementTypes.PictureLink.Fields.LogoLink)
            : null;
        public string Text => this.HasValue(ElementTypes.PictureLink.Fields.Text) ? this.Value<string>(ElementTypes.PictureLink.Fields.Text)
        : string.Empty;


        public int? MaxHeight => this.Value<int?>(ElementTypes.PictureLink.Fields.MaxHeight);
        public int? PaddingTop => this.Value<int?>(ElementTypes.PictureLink.Fields.PaddingTop);
        public int? PaddingRight => this.Value<int?>(ElementTypes.PictureLink.Fields.PaddingRight);
        public int? PaddingBottom => this.Value<int?>(ElementTypes.PictureLink.Fields.PaddingBottom);
        public int? PaddingLeft => this.Value<int?>(ElementTypes.PictureLink.Fields.PaddingLeft);

        public PictureLink(IPublishedElement content) : base(content, null)
        {


        }
    }
}
