using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class BaseService
    {
        protected readonly ILogger Logger;
        protected readonly IUmbracoContextAccessor ContextAccessor;
        protected readonly IUmbracoContext Context;
        protected readonly IPublishedContent CurrentPage;

        /// <summary>
        /// constructor for a base service
        /// </summary>
        /// <param name="logger">logger svervice</param>
        /// <param name="contextAccessor">umbraco contect accessor</param>
        protected BaseService(ILogger<BaseService> logger, IUmbracoContextAccessor contextAccessor)
        {
            Logger = logger;
            ContextAccessor = contextAccessor;

            if (ContextAccessor.TryGetUmbracoContext(out Context))
            {
                CurrentPage = Context.PublishedRequest.PublishedContent;
            }
        }
    }
}
