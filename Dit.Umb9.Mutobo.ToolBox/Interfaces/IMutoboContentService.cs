using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{

    /// <summary>
    /// 
    /// </summary>
    public interface IMutoboContentService
    {
        BasePage GetPageModel(IPublishedContent content);
        IEnumerable<IModule> GetContent(IPublishedContent content, string fieldName, string culture = null);
    }
}
