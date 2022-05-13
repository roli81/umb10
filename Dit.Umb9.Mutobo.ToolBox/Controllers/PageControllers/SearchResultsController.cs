using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Dit.Umb9.Mutobo.ToolBox.Controllers.PageControllers
{
    public class SearchResultsController : BasePageController
    {
        public SearchResultsController(
            ILogger<SearchResultsController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IImageService imageService,
            IPageLayoutService pageLayoutService,
            IMutoboContentService contentService,
            ISearchService searchService) : base(
                logger,
                compositeViewEngine,
                umbracoContextAccessor,
                imageService,
                pageLayoutService,
                contentService,
                searchService)
        {
        }


    }
}
