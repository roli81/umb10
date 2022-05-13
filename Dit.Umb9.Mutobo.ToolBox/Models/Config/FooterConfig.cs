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

namespace Dit.Umb9.Mutobo.ToolBox.Models.Config
{
    public class FooterConfig : PublishedElementModel, IFooterConfiguration
    {
        public IEnumerable<FooterNavBlock> FooterNavBlocks { get; set; }

        public IEnumerable<Link> FooterLinks =>
            this.Value<IEnumerable<Link>>(ElementTypes.FooterConfiguration.Fields.Links);

        public IEnumerable<FooterContactArea> FooterContactBlock { get; set; }

        public IEnumerable<PoCo.Language> Languages { get; set; }


        public IEnumerable<PictureLink> PictureLinks { get; set; }

        public Image HomePageLogo { get; set; }

        public string Copyright => $"&copy; {DateTime.Today.Year} {this.Value<string>(ElementTypes.FooterConfiguration.Fields.CopyRight)}";

        public IEnumerable<Link> BlockLinks => this.Value<IEnumerable<Link>>(ElementTypes.FooterConfiguration.Fields.BlockLinks);


        public string Theme { get; set; }



        public FooterConfig(IPublishedElement content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
        }
    }
}
