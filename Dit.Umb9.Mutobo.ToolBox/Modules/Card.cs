using Dit.Umb9.Mutobo.ToolBox.Constants;
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
    public class Card : PublishedElementModel
    {
        public Image Image { get; set; }

        public Link DetailPageLink => this.HasValue(ElementTypes.Card.Fields.DetailPageLink)
            ? this.Value<Link>(ElementTypes.Card.Fields.DetailPageLink)
            : null;

        public int SortOrder { get; set; }

        public Card(IPublishedElement content, PublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }
    }
}
