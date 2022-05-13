using Dit.Umb9.Mutobo.ToolBox.Common.Extensions;
using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Pages;
using Dit.Umb9.Mutobo.ToolBox.Modules;
using Microsoft.AspNetCore.Http;
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
    public class FlyerService : BaseService, IFlyerService
    {

        private readonly IImageService _imageService;

        public FlyerService(ILogger<FlyerService> logger, 
            IUmbracoContextAccessor contextAccessor, IImageService imageService) : base(logger, contextAccessor)
        {
            _imageService = imageService;
        }


        public IEnumerable<Flyer> GetFlyer(HttpContext context, IPublishedContent node, bool firstPic = false)
        {
            var result = new List<Flyer>();
            var elements = node.HasValue(DocumentTypes.HomePage.Fields.Disturber) ?
                node.Value<IEnumerable<IPublishedElement>>(DocumentTypes.HomePage.Fields.Disturber) :
                new List<IPublishedElement>();

            foreach (IPublishedElement element in elements)
            {
                Flyer fly = new Flyer(element, null)
                {

                };
                var link = element.Value<Link>(ElementTypes.Flyer.Fields.Link);


                ArticlePage articel = null;

                //Get content from the linked ArtikelPage

                if (element.HasValue(ElementTypes.Flyer.Fields.Link) && link.Udi != null)
                {
                    
                    var content = Context.Content.GetById(link.Udi);
                    articel = new ArticlePage(content)
                    {
                        //EmotionImages = content.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages)
                        //    ? ImageService.GetImages(content.Value<IEnumerable<IPublishedContent>>(DocumentTypes.ArticlePage.Fields.EmotionImages), width: 500, imageCropMode: ImageCropMode.Max, nameSpace: "picture", isGoldenRatio: false) : null
                        EmotionImages = content.HasValue(DocumentTypes.ArticlePage.Fields.EmotionImages)
                            ? content.GetImages(context, DocumentTypes.ArticlePage.Fields.EmotionImages,
                                    width: 500, imageCropMode: ImageCropMode.Max, useSources: true) : null
                    };
                }
                if (articel == null)
                {
                    fly.TeaserText = element.HasValue(ElementTypes.Flyer.Fields.FlyerTeaserText) ?
                        element.Value<string>(ElementTypes.Flyer.Fields.FlyerTeaserText) : string.Empty;
                    fly.Image = element.GetImage(context, ElementTypes.Flyer.Fields.FlyerImage, width: 900, imageCropMode: ImageCropMode.Max, useSources: true);
                    //fly.Image = ImageService.GetImage(element.Value<IPublishedContent>(ElementTypes.Flyer.Fields.FlyerImage), width: 500, imageCropMode: ImageCropMode.Max, nameSpace: "picture", isGoldenRatio:false);
                    fly.Link = element.HasValue(ElementTypes.Flyer.Fields.FlyerLink) ?
                        element.Value<Link>(ElementTypes.Flyer.Fields.FlyerLink) : null;
                    Logger.LogWarning($"{AppConstants.LoggingPrefix} Keine ArtikelSeite auf dem Flyer verlinkt {this.GetType()}");
                }
                else //Check if flyer has the property or get it from the ArtikelPage
                {
                    fly.TeaserText = element.HasValue(ElementTypes.Flyer.Fields.FlyerTeaserText)
                        ? element.Value<string>(ElementTypes.Flyer.Fields.FlyerTeaserText)
                        : articel.Abstract;

                    //TODO: SET PICTURE SOURCES
                    fly.Image = element.HasValue(ElementTypes.Flyer.Fields.FlyerImage)
                        ? element.GetImage(context, ElementTypes.Flyer.Fields.FlyerImage,
                            imageCropMode: ImageCropMode.Max, width: 900, useSources: true)
                        : articel.EmotionImages.FirstOrDefault();

                    fly.Link = element.HasValue(ElementTypes.Flyer.Fields.FlyerLink)
                        ? element.Value<Link>(ElementTypes.Flyer.Fields.FlyerLink)
                        : new Link() { Url = articel.Content.Url() };
                }

                // check if null and warn if so
                if (fly.TeaserText == null)
                    Logger.LogWarning($"{AppConstants.LoggingPrefix} Auf dem Flyer gibt es keinen Teaser Text {this.GetType()}");
                if (fly.Image == null)
                    Logger.LogWarning($"{AppConstants.LoggingPrefix} Auf dem Flyer gibt es kein Foto {this.GetType()}");
                if (fly.Link == null)
                    Logger.LogWarning($"{AppConstants.LoggingPrefix} Auf dem Flyer gibt es keinen Link {this.GetType()}");

                result.Add(fly);
            }

            return result;
        }
    }
}
