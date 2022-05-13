using Dit.Umb9.Mutobo.ToolBox.Common.Exceptions;
using Dit.Umb9.Mutobo.ToolBox.Common.Extensions;
using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using Examine;
using Examine.Search;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class SearchService : BaseService, ISearchService
    {
        protected readonly IPageLayoutService PageLayoutService;
        private readonly IExamineManager _examineManager;
        private readonly IMediaService _mediaService;

        public SearchService(
            ILogger<SearchService> logger,
            IUmbracoContextAccessor contextAccessor,
            IPageLayoutService pageLayoutService,
            IExamineManager examineManager,
            IMediaService mediaService)
            : base(logger, contextAccessor)
        {
            PageLayoutService = pageLayoutService;
            _examineManager = examineManager;
            _mediaService = mediaService;

        }

        public virtual ISearchResultsModel PerformSearch(string term)
        {

            ISearchResultsModel result = null;
            try
            {
                // create the result object and assign the search term 
                result = new SearchResultsPage(CurrentPage)
                {
                    HeaderConfiguration = PageLayoutService.GetHeaderConfiguration(CurrentPage),
                    FooterConfiguration = PageLayoutService.GetFooterConfiguration(CurrentPage),
                    Term = term ?? string.Empty
                };
            }
            catch (AppSettingsException e)
            {
                //TODO: Logging
                //Logger.Error(this.GetType(), e, $"{AppConstants.LoggingPrefix} {e.Message}");
                throw e;
            }


            if (_examineManager.TryGetIndex("ExternalIndex", out var index))
            {
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString().ToLower();
                var searchTerm = HtmlHelperExtensions.SearchFriendlyString(term);
                var diffFields = new string[] {
                    "nodeName",
                    "__NodeTypeAlias",
                    "fileTextContent",
                    $"abstract_{currentCulture}",
                    "abstract",
                    "mainContent",
                    $"mainContent_{currentCulture}",
                    "pageTitle",
                    $"pageTitle_{currentCulture}",
                    "modules",
                    $"modules_{currentCulture}"
                };

                if (!string.IsNullOrEmpty(term))
                {
                    var query = index.Searcher.CreateQuery(null, BooleanOperation.And)
                                    .GroupedOr(diffFields, searchTerm);
                    var results = query.Execute();

                    result.Results = results
                        .Select(r => Context.Content.GetById(false, int.Parse(r.Id)))
                        .Select(node => new Models.PoCo.SearchResult
                        {
                            Url = node.Url(),
                            Abstract = node.HasProperty(DocumentTypes.ArticlePage.Fields.Abstract) ? node.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract) : string.Empty,
                            Title = node.HasProperty(DocumentTypes.BasePage.Fields.PageTitle) &&
                                                 node.HasValue(DocumentTypes.BasePage.Fields.PageTitle) &&
                                                 !string.IsNullOrEmpty(node.Value<string>(DocumentTypes.BasePage.Fields.PageTitle).Trim()) ?
                                             node.Value<string>(DocumentTypes.BasePage.Fields.PageTitle) :
                                             node.Name,
                            UrlTitle = "Mehr erfahren"

                        });
                }
                else
                {
                    result.Results = new List<Models.PoCo.SearchResult>();
                }

            }
            else
            {
                throw new SearchException("ExternalIndex is not present");
            }



            return result;

        }

        public IEnumerable<Models.PoCo.SearchResult> Search(string term)
        {




            if (_examineManager.TryGetIndex("ExternalIndex", out var index))
            {
                var currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString().ToLower();
                var searchTerm = HtmlHelperExtensions.SearchFriendlyString(term);
                var diffFields = new string[] {
                    "nodeName",
                    "__NodeTypeAlias",
                    "fileTextContent",
                    $"abstract_{currentCulture}",
                    "abstract",
                    "mainContent",
                    $"mainContent_{currentCulture}",
                    "pageTitle",
                    $"pageTitle_{currentCulture}",
                    "modules",
                    $"modules_{currentCulture}"
                };

                if (!string.IsNullOrEmpty(term))
                {
                    var query = index.Searcher.CreateQuery(null, BooleanOperation.And)
                                    .GroupedOr(diffFields, searchTerm);
                    var results = query.Execute();

                    return results
                        .Select(r => Context.Content.GetById(false, int.Parse(r.Id)))
                        .Select(node => new Models.PoCo.SearchResult
                        {
                            Url = node.Url(),
                            Abstract = node.HasProperty(DocumentTypes.ArticlePage.Fields.Abstract) ? node.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract) : string.Empty,
                            Title = node.HasProperty(DocumentTypes.BasePage.Fields.PageTitle) &&
                                                 node.HasValue(DocumentTypes.BasePage.Fields.PageTitle) &&
                                                 !string.IsNullOrEmpty(node.Value<string>(DocumentTypes.BasePage.Fields.PageTitle).Trim()) ?
                                             node.Value<string>(DocumentTypes.BasePage.Fields.PageTitle) :
                                             node.Name,
                            UrlTitle = "Mehr erfahren"

                        });
                }
                else
                {
                    return new List<Models.PoCo.SearchResult>();
                }

            }
            else
            {
                throw new SearchException("ExternalIndex is not present");
            }




        }





        //public IEnumerable<IPublishedContent> GetLinkedPages(IPublishedContent media)
        //{
        //    var result = new List<IPublishedContent>();

        //    var homepage = Helper
        //        .ContentAtRoot()
        //        .FirstOrDefault(p => p.IsComposedOf(DocumentTypes.HomePage.Alias));

        //    foreach (var page in homepage.DescendantsOrSelf())
        //    {

        //        foreach (var prop in page.Properties)
        //        {
        //            if (prop.GetValue() is IPublishedContent content)
        //            {
        //                if (content.Id == media.Id)
        //                    result.Add(page);
        //            }
        //            else if (prop.GetValue() is IPublishedElement element)
        //            {
        //                if (IsLinkedElementType(element, media.Id))
        //                {
        //                    result.Add(page);
        //                }
        //            }
        //            else if (prop.GetValue() is IEnumerable<IPublishedElement> elementList)
        //            {
        //                foreach (var el in elementList)
        //                {
        //                    if (IsLinkedElementType(el, media.Id))
        //                    {
        //                        result.Add(page);
        //                    }
        //                }
        //            }
        //        }

        //    }

        //    return result;
        //}


        private bool IsLinkedElementType(IPublishedElement element, int mediaId)
        {
            var result = false;

            foreach (var prop in element.Properties)
            {
                // TODO fix this
                if (prop.GetValue() is IPublishedContent content)
                {
                    result = content.Id == mediaId;
                }
                else if (prop.GetValue() is IPublishedElement childElement)
                {
                    result |= IsLinkedElementType(childElement, mediaId);
                }
                else if (prop.GetValue() is IEnumerable<IPublishedElement> elementList)
                {
                    foreach (var el in elementList)
                    {
                        result |= IsLinkedElementType(el, mediaId);
                    }

                }
            }

            return result;
        }
    }
}
