using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class PictureLinkService : BaseService, IPictureLinkService
    {
        private readonly IImageService _imageService;

        public PictureLinkService(ILogger<PictureLinkService> logger, IUmbracoContextAccessor contextAccessor, IImageService imageService) : base(logger, contextAccessor)
        {
            _imageService = imageService;
        }

        public IEnumerable<PictureLink> GetPictureLinks(IEnumerable<IPublishedElement> elements)
        {
            return elements.Select(e => new PictureLink(e)
            {
                Image = e.HasValue(ElementTypes.PictureLink.Fields.Image) ?
                    _imageService.GetImage(e.Value<IPublishedContent>(ElementTypes.PictureLink.Fields.Image))
                    : null
            });
        }
    }
}
