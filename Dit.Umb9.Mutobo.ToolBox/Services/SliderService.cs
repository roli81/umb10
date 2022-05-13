using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Dit.Umb9.Mutobo.ToolBox.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class SliderService : ISliderService
    {

        private readonly IImageService _imageService;

        public SliderService(IImageService imageService)
        {
            _imageService = imageService;
        }

        public IEnumerable<ISliderItem> GetSlides(IPublishedElement content, string fieldName, int? width = null, int? height = null, bool isGoldenRatio = false)
        {
            var result = new List<ISliderItem>();

            if (content.HasValue(fieldName))
            {
                var slideContent = content.Value<IEnumerable<IPublishedElement>>(fieldName);

                foreach (var slideNode in slideContent)
                {
                    if (slideNode.ContentType.Alias == ElementTypes.VideoComponent.Alias)
                    {
                        var vc = new VideoComponent(slideNode, null);

                        result.Add(new EmptyVideoComponent()
                        {
                            Height = height,
                            Width = width,
                            Embedded = vc.Embedded,
                            Text = vc.Text,
                            Video = vc.Video
                        });
                    }
                    else if (slideNode.ContentType.Alias == ElementTypes.PictureModule.Alias)
                    {
                        result.Add(new PictureModule(slideNode, null)
                        {
                            Image = slideNode.HasValue(ElementTypes.PictureModule.Fields.Image) ? _imageService.GetImage(
                                slideNode.Value<IPublishedContent>(ElementTypes.Picture.Fields.Image), width, height, isGoldenRatio: isGoldenRatio) : null
                        });
                    }
                    else if (slideNode.ContentType.Alias == ElementTypes.TextImageSlide.Alias)
                    {
                        result.Add(new TextImageSlide(slideNode, null) { 
                        
                            Image = slideNode.HasValue(ElementTypes.TextImageSlide.Fields.Image) ?
                             _imageService.GetImage(
                                slideNode.Value<IPublishedContent>(ElementTypes.TextImageSlide.Fields.Image), width, height, isGoldenRatio: isGoldenRatio) : null
                        });
                    }
                }
            }

            return result;
        }


        public IEnumerable<TextImageSlide> GetDoubleSlides(IPublishedElement content, string fieldName, int? width = null, int? height = null,
    bool isGoldenRatio = false)
        {

            var result = new List<TextImageSlide>();
            if (content.HasValue(fieldName))
            {
                var slideContent = content.Value<IEnumerable<IPublishedElement>>(fieldName);

                foreach (var slideNode in slideContent)
                {

                    var textImageComponent = new TextImageSlide(slideNode, null);

                    result.Add(new TextImageSlide(slideNode, null)
                    {
                        Image = slideNode.HasValue(ElementTypes.TextImageSlide.Fields.Image) ? _imageService.GetImage(
                           slideNode.Value<IPublishedContent>(ElementTypes.TextImageSlide.Fields.Image), width: width, height: height, isGoldenRatio: isGoldenRatio) : null
                    });



                }

            }

            return result;
        }
    }
}
