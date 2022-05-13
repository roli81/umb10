using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Dit.Umb9.Mutobo.ToolBox.Modules;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    /// <summary>
    /// implementation of the IMutoboContentService service interface
    /// </summary>
    public class MutoboContentService : BaseService, IMutoboContentService
    {
        protected readonly IImageService ImageService;
        protected readonly ISliderService SliderService;
        protected readonly IConfigService ConfigurationService;
        private readonly ICardService _cardService;
        protected readonly IThemeService ThemeService;
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        /// <summary>
        /// constructor for a MUTOBO content service.
        /// PYrovides some methods to get Umbraco nodes as MUTOBO Pages or modules
        /// </summary>
        /// <param name="logger">the logging service</param>
        /// <param name="imageService">the image service</param>
        /// <param name="sliderService">the slider service</param>
        /// <param name="cardService">the card service</param>
        /// <param name="contextAccessor">the umbraco context accessor to get content from umbraco</param>
        /// <param name="contextFactory">factors to create the umbraco context</param>
        /// <param name="themeService">the theme service</param>
        public MutoboContentService(
            ILogger<MutoboContentService> logger,
            IImageService imageService,
            ISliderService sliderService,
            ICardService cardService,
            IUmbracoContextAccessor contextAccessor,
            IUmbracoContextFactory contextFactory,
            IThemeService themeService)
                : base(logger, contextAccessor)
        {
            SliderService = sliderService;
            _cardService = cardService;
            ImageService = imageService;
            ThemeService = themeService;
            _umbracoContextFactory = contextFactory;
        }


        /// <summary>
        /// returns a list with all MUTOBO modules placed in a module field of the page
        /// </summary>
        /// <param name="content">
        /// </param>
        /// <param name="fieldName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<IModule> GetContent(IPublishedContent content, string fieldName, string culture = null)
        {
            if (culture == null)
            {
                culture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            }


            if (content.HasValue(fieldName, culture))
            {
                var result = new List<MutoboContentModule>();

                var elements =
                    content.Value<IEnumerable<IPublishedElement>>(fieldName, culture);

                foreach (var element in elements.Select((value, index) => new { index, value }))
                {

                    MutoboContentModule module = null;
                    switch (element.value.ContentType.Alias)
                    {
                        case ElementTypes.Accordeon.Alias:
                            module = new Accordeon(element.value, null);
                            break;
                        case ElementTypes.Heading.Alias:
                            module = new Heading(element.value, null);
                            break;
                        case ElementTypes.VideoComponent.Alias:
                            module = new VideoComponent(element.value, null);
                            break;
                        case ElementTypes.RichTextComponent.Alias:
                            module = new RichtextComponent(element.value, null);
                            break;
                        case ElementTypes.Flyer.Alias:
                            module = new Flyer(element.value, null)
                            {
                                Image = element.value.HasValue(ElementTypes.Flyer.Fields.FlyerImage) ? ImageService
                                .GetImage(element.value.Value<IPublishedContent>(ElementTypes.Flyer.Fields.FlyerImage),
                                width: 900,
                                imageCropMode: ImageCropMode.Max)
                                : null,
                                TeaserText = element.value.Value<string>(ElementTypes.Flyer.Fields.FlyerTeaserText),
                                Link = element.value.Value<Link>(ElementTypes.Flyer.Fields.Link)
                            };
                            break;
                        case ElementTypes.Teaser.Alias:
                            module = GetTeaser(element.value, element.index, culture);
                            break;
                        case ElementTypes.SliderComponent.Alias:
                            var sliderModule = new SliderComponent(element.value, null);
                            var useGoldenRatio = (sliderModule.Height == null && sliderModule.Width == null);
                            sliderModule.Slides = SliderService.GetSlides(element.value,
                                ElementTypes.SliderComponent.Fields.Slides, sliderModule.Width, sliderModule.Height);
                            module = sliderModule;
                            break;
                        case ElementTypes.PictureModule.Alias:
                            var picModule = new PictureModule(element.value, null);
                            var isGoldenRatio = (picModule.Height == null && picModule.Width == null);
                            picModule.Image = element.value.HasValue(ElementTypes.Picture.Fields.Image)
                                ? ImageService.GetImage(
                                    element.value.Value<IPublishedContent>(ElementTypes.Picture.Fields.Image),
                                    height: 450,
                                    width: 800)
                                : null;
                            module = picModule;
                            break;
                        //case DocumentTypes.BlogModule.Alias:
                        //    var model = new BlogModule(element.value, null)
                        //    {
                        //        SortOrder = element.index
                        //    };

                        //    model.BlogEntries = model.ParentPage != null ?
                        //        model.ParentPage.Children.Select(c => new ArticlePage(c)
                        //        {
                        //            EmotionImages = c.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages) ?
                        //            ImageService.GetImages(c.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages)) : null
                        //        })
                        //        : CurrentPage.Children.Select(c => new ArticlePage(c)
                        //        {
                        //            EmotionImages = c.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages) ?
                        //            ImageService.GetImages(c.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages)) : null
                        //        });


                        //    result.Add(model);
                        //    break;

                        case ElementTypes.DoubleSliderComponent.Alias:
                            var dblSliderComponent = new DoubleSliderComponent(element.value, null);
                            dblSliderComponent.Slides = SliderService.GetDoubleSlides(element.value, ElementTypes.DoubleSliderComponent.Fields.Slides, width: dblSliderComponent.Width, height: dblSliderComponent.Height) as IEnumerable<TextImageSlide>;
                            module = dblSliderComponent;
                            break;
                        case ElementTypes.Quote.Alias:
                            module = new Quote(element.value, null);
                            break;
                        case ElementTypes.CardContainer.Alias:
                            module = new CardContainer(element.value, null)
                            {
                                Cards = _cardService.GetCards(element.value, ElementTypes.CardContainer.Fields.Cards),
                            };
                            break;
                            //case DocumentTypes.ContactForm.Alias:
                            //    var contactFormModel = new ContactForm(element.value, null);

                            //    contactFormModel.Data = new ContactFormData
                            //    {
                            //        ReceiverMailConfigId = contactFormModel.ReceiverMailConfig.Content.Id,
                            //        SenderMailConfigId = contactFormModel.SenderMailConfig.Content.Id,
                            //        LandingPageId = contactFormModel.LandingPage.Key
                            //    };

                            //    result.Add(contactFormModel);
                            //    break;
                            //case DocumentTypes.TwoColumnWrapper.Alias:
                            //    var twoColModel = new TwoColumnWrapper(element.value, null);

                            //    var childElements = element.value.HasValue(DocumentTypes.TwoColumnWrapper.Fields.Elements) ?
                            //        element.value.Value<IEnumerable<IPublishedElement>>(DocumentTypes.TwoColumnWrapper.Fields.Elements)
                            //        : null;

                            //    var childModules = new List<IWrappable>();
                            //    foreach (var childElement in childElements)
                            //    {
                            //        switch (childElement.ContentType.Alias)
                            //        {
                            //            case DocumentTypes.FlipTeaser.Alias:
                            //                var flipTeaser = new FlipTeaser(childElement, null);
                            //                flipTeaser.Image = childElement.HasValue(DocumentTypes.FlipTeaser.Fields.FrontImage) ?
                            //                    ImageService.GetImage(childElement.Value<IPublishedContent>(DocumentTypes.FlipTeaser.Fields.FrontImage), height: 300, width: 300) : null;
                            //                childModules.Add(flipTeaser);
                            //                break;
                            //        }

                            //    }
                            //    twoColModel.Elements = childModules;
                            //    result.Add(twoColModel);
                            //    break;

                    }
                    module.SortOrder = element.index;
                    result.Add(module);

                }




                return result;
            }

            return null;
        }


        /// <summary>
        /// method to get the   
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public BasePage GetPageModel(IPublishedContent content)
        {
            BasePage result = null;

            switch (content.ContentType.Alias)
            {
                case DocumentTypes.BasePage.Alias:
                default:
                    result = new BasePage(content)
                    {

                    };
                    break;

                case DocumentTypes.ArticlePage.Alias:


                    result = new ArticlePage(content)
                    {
                        EmotionImages = content.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages) ?
                        ImageService.GetImages(content.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages),
                        width: 800,
                        height: 450) : null,
                    };
                    break;


                case DocumentTypes.ContentPage.Alias:
                    result = new ContentPage(content)
                    {
                        EmotionImages = content.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages) ?
                        ImageService.GetImages(content.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages),
                        width: 800,
                        height: 450) : null,
                        Modules = content.HasValue(DocumentTypes.ContentPage.Fields.Modules) ? GetContent(content, DocumentTypes.ContentPage.Fields.Modules) : null
                    };
                    break;


                case DocumentTypes.HomePage.Alias:
                    result = new HomePage(content)
                    {
                        Modules = content.HasValue(DocumentTypes.HomePage.Fields.Modules) ? GetContent(content, DocumentTypes.HomePage.Fields.Modules) : null
                    };
                    break;

                case DocumentTypes.SearchResultsPage.Alias:
                    result = new SearchResultsPage(content)
                    {


                    };
                    break;

            }

            return result;
        }

        private Teaser GetTeaser(IPublishedElement element, int index, string culture)
        {
            var teaser = new Teaser(element, null);


            if (teaser.UseArticleData)
            {
                if (teaser.Link?.Udi != null)
                {
                    IPublishedContent content;

                    if (Context != null)
                    {
                        content = Context.Content.GetById(teaser.Link.Udi);
                    }
                    else
                    {
                        using (var ctx = _umbracoContextFactory.EnsureUmbracoContext())
                        {
                            content = ctx.UmbracoContext.Content.GetById(teaser.Link.Udi);
                        }
                    }

                    teaser.Images = GetHighlightImages(content, culture);
                    teaser.TeaserText = content.HasValue(DocumentTypes.ArticlePage.Fields.Abstract, culture) ? content.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract, culture) : string.Empty;
                    teaser.TeaserTitle = content.HasValue(DocumentTypes.BasePage.Fields.PageTitle, culture) ? content.Value<string>(DocumentTypes.BasePage.Fields.PageTitle, culture) : string.Empty;

                }
            }
            else
            {
                teaser.Images = element.HasValue(ElementTypes.Teaser.Fields.Images)
                    ? ImageService.GetImages(
                        element.Value<IEnumerable<IPublishedContent>>(ElementTypes.Teaser.Fields.Images), width: 800, height: 450)
                    : null;
                teaser.TeaserText = element.HasValue(ElementTypes.Teaser.Fields.TeaserText) ?
                    element.Value<string>(ElementTypes.Teaser.Fields.TeaserText) : null;

                teaser.TeaserTitle = element.HasValue(ElementTypes.Teaser.Fields.TeaserTitle) ?
                    element.Value<string>(ElementTypes.Teaser.Fields.TeaserTitle) : null;
            }

            return teaser;

        }


        private IEnumerable<Image> GetHighlightImages(IPublishedContent content, string culture)
        {
            var result = new List<Image>();
            if (content.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages))
                result.AddRange(ImageService.GetImages(content.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages), width: 800, height: 450));

            return result;
        }
    }
}
