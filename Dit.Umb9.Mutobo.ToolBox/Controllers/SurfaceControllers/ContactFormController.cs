using Microsoft.AspNetCore.Mvc;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;

namespace Dit.Umb9.Mutobo.ToolBox.Controllers.SurfaceControllers
{
    public class ContactFormController : SurfaceController
    {
        private readonly IMailService _mailService;

        public ContactFormController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IMailService mailService)
            : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _mailService = mailService;

        }


        [HttpPost]
        public IActionResult Submit(ContactFormData data)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            _mailService.SendContactMail(data);
            _mailService.SendConfirmationMail(data);

            return RedirectToUmbracoPage(data.LandingPageId);
        }
    }
}
