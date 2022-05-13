using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using Microsoft.AspNetCore.Http;
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
	public class XmlSitemapService : BaseService, IXmlSitemapService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public XmlSitemapService(ILogger<XmlSitemapService> logger, IUmbracoContextAccessor contextAccessor, IHttpContextAccessor httpContextAccessor) : base(logger, contextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public IEnumerable<BasePage> GetXmlSitemap(IPublishedContent model)

		{
			var homePage = CurrentPage.Parent;


			return from n in GenerateSiteMapNodes(homePage)
				   where n.ContentType.CompositionAliases.Contains(DocumentTypes.BasePage.Alias)
				   select new BasePage(n);
		}

		private IEnumerable<IPublishedContent> GenerateSiteMapNodes(IPublishedContent content)
		{
			yield return content;
			if (content.Children == null)
			{
				yield break;
			}
			foreach (IPublishedContent child in content.Children)
			{
				foreach (IPublishedContent item in GenerateSiteMapNodes(child))
				{
					yield return item;
				}
			}
		}

		private string EnsureUrlStartsWithDomain(string url)
		{
			if (url.StartsWith("http"))
			{
				return url;
			}

			return string.Concat("https://" + _httpContextAccessor.HttpContext.Request.Host.Host + "/", url);
		}
	}
}
