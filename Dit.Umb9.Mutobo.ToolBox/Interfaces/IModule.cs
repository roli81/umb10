using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{

    /// <summary>
    /// interface for a MUTOBO content module
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// module title
        /// </summary>
        string ModuleTitle { get; }
        /// <summary>
        /// flag to render a empty spacing div above the module
        /// </summary>
        bool SpacerAfterModule { get; }
        /// <summary>
        /// flag to render a empty spacing div about the module
        /// </summary>
        bool SpaceBeforeModule { get; }


 

        /// <summary>
        /// render method with default implementation
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            return Task.FromResult<IHtmlContent>(new HtmlString("No Rendering for Module"));
        }
    }
}
