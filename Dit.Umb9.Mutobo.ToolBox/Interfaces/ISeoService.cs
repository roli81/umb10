using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Models.Config;
using Dit.Umb9.Mutobo.ToolBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface ISeoService
    {
        public SeoConfig GetSeoConfiguration();

     
    }
}
