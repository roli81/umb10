using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Modules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Pages
{
    /// <summary>
    /// model class for a home page
    /// </summary>
    public class HomePage : BasePage
    {
        /// <summary>
        /// module field of the base page
        /// </summary>
        public IEnumerable<IModule> Modules { get; set; }


        /// <summary>
        /// constructor for a home page
        /// </summary>
        /// <param name="content">
        /// umbarco content node
        /// </param>
        public HomePage(IPublishedContent content) : base(content)
        {

        }



    }
}
