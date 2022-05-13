using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
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
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Controllers.SurfaceControllers
{
    public class GoogleAnalyticsController : SurfaceController
    {
        public GoogleAnalyticsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
        }

        // GET: GoogleAnalytics
        public ActionResult Index()
        {
            var homePage = CurrentPage.AncestorsOrSelf().FirstOrDefault(c =>
                c.ContentType.Alias == DocumentTypes.HomePage.Alias);

            var code = homePage?.Value<string>(DocumentTypes.BasePage.Fields.GoogleAnalyticsKey) ?? string.Empty;

            if (homePage == null)
                return new EmptyResult();

            return View("~/Views/Partials/GoogleAnalytics.cshtml", new GoogleAnalyticsModel()
            {
                Key = code

            });
        }
    }
}
