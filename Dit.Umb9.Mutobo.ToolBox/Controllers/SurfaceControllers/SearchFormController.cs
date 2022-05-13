using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Dit.Umb9.Mutobo.ToolBox.Controllers.SurfaceControllers
{
    public class SearchFormController : SurfaceController
    {
        private readonly ISearchService _searchService;
        private readonly IPageLayoutService _pageLayoutService;


        public SearchFormController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            ISearchService searchService,
            IPageLayoutService pageLayoutService)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _searchService = searchService;
            _pageLayoutService = pageLayoutService;

        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult PostData(SearchFormModel model)
        {
            if (String.IsNullOrEmpty(model.Page))
                model.Page = string.Empty;

            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            var queryString = new NameValueCollection();
            if (!string.IsNullOrWhiteSpace(model.SearchTerm))
            {
                queryString.Add("searchTerm", model.SearchTerm);
            }

            if (!string.IsNullOrWhiteSpace(model.Page))
            {
                queryString.Add("page", model.Page);
            }

            var pageModel = new SearchResultsPage(CurrentPage)
            {
                Results = _searchService.PerformSearch(model.SearchTerm).Results,
                Term = model.SearchTerm,
                HeaderConfiguration = _pageLayoutService.GetHeaderConfiguration(CurrentPage),
                FooterConfiguration = _pageLayoutService.GetFooterConfiguration(CurrentPage)

            };

            return View("~/Views/WCSearchResults.cshtml", pageModel);
        }

        [HttpPost]
        public ActionResult PostDataClassics(SearchFormModel model)
        {
            return PartialView("~/Views/Partials/SearchResults.cshtml", new SearchResultModel()
            {
                Results = _searchService.PerformSearch(model.SearchTerm).Results,
                Term = model.SearchTerm
            });
        }

    }
}
