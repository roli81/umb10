using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Config
{
    public class EmptyHeaderConfiguration : IHeaderConfiguration
    {
        public IEnumerable<NavItem> NavigationItems { get; set; }
        public Image Logo { get; set; }
        public Link LogoUrl { get; set; }
        public IEnumerable<PoCo.Language> Languages { get; set; }
        public Image StageImage { get; set; }

        public Slogan GlobalSlogan => throw new NotImplementedException();

        Link IHeaderConfiguration.LogoUrl => throw new NotImplementedException();
    }
}
