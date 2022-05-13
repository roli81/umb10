using Dit.Umb9.Mutobo.ToolBox.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Models.PoCo
{
    public class FooterContactArea : PublishedElementModel
    {
        public string Headline { get; set; }
        public string AddressBlock { get; set; }

        public FooterContactArea(IPublishedElement content) : base(content, null)
        {
            Headline = content.HasValue(ElementTypes.FooterContact.Headline) ? content.Value<string>(ElementTypes.FooterContact.Headline) : string.Empty;
            AddressBlock = content.HasValue(ElementTypes.FooterContact.AddressBlock) ? content.Value<string>(ElementTypes.FooterContact.AddressBlock) : string.Empty;
        }
    }
}
