using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dit.Umb9.Mutobo.ToolBox.Modules;

namespace Dit.Umb9.Mutobo.ToolBox.Controllers.PageControllers
{
    public class HomePageController : BasePageController
    {


        public IEnumerable<MutoboContentModule> Modules { get; set; }
        public HomePageController(
            ILogger<HomePageController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IPageLayoutService pageLayoutService,
            IMutoboContentService contentService,
            IImageService imageService,
            ISearchService searchService)
            : base(
                  logger,
                  compositeViewEngine,
                  umbracoContextAccessor,
                  imageService,
                  pageLayoutService,
                  contentService,
                  searchService
                )
        {
        }
    }
}
