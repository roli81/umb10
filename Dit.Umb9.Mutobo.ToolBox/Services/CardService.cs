using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
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
    public class CardService : BaseService, ICardService
    {
        private readonly IImageService _imageService;

        public CardService(ILogger<BaseService> logger, IUmbracoContextAccessor contextAccessor, IImageService imageService) : base(logger, contextAccessor)
        {
            _imageService = imageService;
        }

        public IEnumerable<Card> GetCards(IPublishedElement content, string fieldName)
        {
            if (!content.HasValue(fieldName))
                return null;

            var result = content.Value<IEnumerable<IPublishedElement>>(fieldName)
                .Where(c => c.ContentType.Alias == ElementTypes.Card.Alias).Select((element, index) => new
                {
                    element = new Card(element, null)
                    {
                        SortOrder = index,
                        Image = element.HasValue(ElementTypes.Card.Fields.Image)
                            ? _imageService.GetImage(element.Value<IPublishedContent>(ElementTypes.Card.Fields.Image), 850,
                                450, ImageCropMode.Crop)
                            : null
                    },
                    index
                }).Select(e => e.element).ToList();

            result.AddRange(content.Value<IEnumerable<IPublishedElement>>(fieldName)
                .Where(c => c.ContentType.Alias == ElementTypes.PersonalCard.Alias)
                .Select((element, index) => new
                {
                    element = new PersonalCard(element, null)
                    {
                        SortOrder = index,
                        Image = element.HasValue(ElementTypes.Card.Fields.Image)
                            ? _imageService.GetImage(element.Value<IPublishedContent>(ElementTypes.Card.Fields.Image),
                                500, 500,
                                imageCropMode: ImageCropMode.Stretch)
                            : null
                    },
                    index
                }).Select(e => e.element));

            return result.OrderBy(e => e.SortOrder);
        }
    }
}
