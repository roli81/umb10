using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{

    /// <summary>
    /// abstract class for a MUTOBO content module
    /// </summary>
    public abstract class MutoboContentModule : PublishedElementModel, IModule
    {

        /// <summary>
        /// constructor for a MUTOBO content module
        /// </summary>
        /// <param name="content">
        /// umbraco published element
        /// </param>
        /// <param name="publishedValueFallback">
        /// ????
        /// </param>
        public MutoboContentModule(
            IPublishedElement content, 
            IPublishedValueFallback publishedValueFallback) 
            : base(content, publishedValueFallback)
        {
        }

        public bool SpaceBeforeModule { get; }
        public string ModuleTitle { get; }
        public bool SpacerAfterModule { get; }
        public int SortOrder { get; set; }
        public string Anchor { get; set; }
    }
}
