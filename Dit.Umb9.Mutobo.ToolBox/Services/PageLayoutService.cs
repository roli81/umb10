using Dit.Umb9.Mutobo.ToolBox.Common.Extensions;
using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Config;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class PageLayoutService : BaseService, IPageLayoutService
    {
        private readonly INavigationService _navigationService;
        private readonly ILocalizationService _localizationService;
        private readonly IImageService _imageService;
        private readonly IPictureLinkService _pictureLinkService;



        public PageLayoutService(
            INavigationService navigationService,
            ILocalizationService localizationService,
            IImageService imageService,
            IPictureLinkService pictureLinkService,
            ILogger<PageLayoutService> logger,
            IUmbracoContextAccessor contextAccessor) : base(logger, contextAccessor)
        {
            _navigationService = navigationService;
            _localizationService = localizationService;
            _imageService = imageService;
            _pictureLinkService = pictureLinkService;

        }

        /// <summary>
        /// Returns the first found IHeaderConfiguration recursively upwards 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IHeaderConfiguration GetHeaderConfiguration(IPublishedContent content = null)
        {
            HeaderConfig headerConfig = null;
            var branch = content?.AncestorsOrSelf().ToList();
            var index = 0;

            if (branch != null)
            {
                do
                {
                    headerConfig = branch[index].HasValue(DocumentTypes.BasePage.Fields.HeaderConfiguration)
                        ? new HeaderConfig(branch[index].Value<IEnumerable<IPublishedElement>>(DocumentTypes.BasePage.Fields.HeaderConfiguration).FirstOrDefault(), null) : null;
                    index++;

                } while (index < branch.Count() && headerConfig == null);
            }

            if (headerConfig != null)
            {
                return new HeaderConfig(headerConfig, null)
                {
                    NavigationItems = _navigationService.GetNavigation(),
                    Logo = _imageService.GetImage(headerConfig.Value<IPublishedContent>(ElementTypes.HeaderConfiguration.Logo), height: 100),
                    Languages = _localizationService.GetAllLanguages()
                        .OrderBy(l => l.CultureInfo.TwoLetterISOLanguageName)
                        .Select(a => new Models.PoCo.Language()
                        {
                            Name = a.CultureInfo.NativeName.Split(' ')[0],
                            ISO = a.CultureInfo.TwoLetterISOLanguageName
                        })

                };
            }
            else
            {
                return new EmptyHeaderConfiguration();
            }
        }

        /// <summary>
        /// Returns the first found IFooterConfiguration recursively upwards 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IFooterConfiguration GetFooterConfiguration(IPublishedContent content = null)
        {
            FooterConfig footerConfig = null;
            var branch = content?.AncestorsOrSelf().ToList();
            var index = 0;

            if (branch != null)
            {
                do
                {
                    footerConfig = branch[index].HasValue(DocumentTypes.BasePage.Fields.FooterConfiguration)
                        ? new FooterConfig(branch[index].Value<IEnumerable<IPublishedElement>>(DocumentTypes.BasePage.Fields.FooterConfiguration).FirstOrDefault(), null) : null;
                    index++;
                } while (index < branch.Count() && footerConfig == null);
            }


            if (footerConfig != null)
            {
                var nodes = footerConfig.HasValue(ElementTypes.FooterConfiguration.Fields.NavigationArea) ?
    footerConfig.Value<IEnumerable<IPublishedElement>>(ElementTypes.FooterConfiguration.Fields.NavigationArea)
    : new List<IPublishedElement>();
                var linkBlocks = new List<FooterNavBlock>();





                foreach (var node in nodes)
                {
                    var contentNode = Context.Content.GetById(node.Value<IPublishedContent>(ElementTypes.FooterNavBlock.StartNode).Id);
                    linkBlocks.Add(GetFooterLinkBlock(contentNode));
                }



                var footerLinks = footerConfig.Value<IEnumerable<Link>>(ElementTypes.FooterConfiguration.Fields.Links) ??
                                  new List<Link>();
                var contactNode = footerConfig.HasValue(ElementTypes.FooterConfiguration.Fields.ContactArea) ?
                    footerConfig.Value<IEnumerable<IPublishedElement>>(ElementTypes.FooterConfiguration.Fields.ContactArea)
                        .Select(e => new FooterContactArea(e))
                    : new List<FooterContactArea>();
                var langs = _localizationService.GetAllLanguages().ToList();
                var headerConfiguration = GetHeaderConfiguration(content);
                return new FooterConfig(footerConfig, null)
                {
                    FooterNavBlocks = linkBlocks,
                    FooterContactBlock = contactNode,
                    Languages = _localizationService.GetAllLanguages()
                        .OrderBy(l => l.CultureInfo.TwoLetterISOLanguageName)
                        .Select(a => new Models.PoCo.Language()
                        {
                            Name = a.CultureInfo.NativeName.Split(' ')[0],
                            ISO = a.CultureInfo.TwoLetterISOLanguageName,
                            CultureName = a.CultureInfo.Name
                        }),
                    PictureLinks = footerConfig.HasValue(ElementTypes.FooterConfiguration.Fields.PictureLinks) ?
                        _pictureLinkService.GetPictureLinks(footerConfig.Value<IEnumerable<IPublishedElement>>(ElementTypes.FooterConfiguration.Fields.PictureLinks)) :
                        new List<PictureLink>(),
                    HomePageLogo = headerConfiguration?.Logo
                };
            }

            return null;
        }



        private FooterNavBlock GetFooterLinkBlock(IPublishedContent parentNode)
        {

            return new FooterNavBlock()
            {
                Title = new Link() { Name = parentNode.Name, Url = parentNode.Url() },
                Children = parentNode.Children.Select(c => new Link()
                {
                    Url = c.GetDitUrl(),
                    Name = c.Name,
                    Target = c.GetLinkTarget()

                })
            };
        }
    }
}
