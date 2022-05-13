using Dit.Umb9.Mutobo.ToolBox.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface ISliderService
    {


            IEnumerable<ISliderItem> GetSlides(IPublishedElement content, string fieldName, int? width = null,
                int? height = null, bool isGoldenRatio = false);

            IEnumerable<TextImageSlide> GetDoubleSlides(IPublishedElement content, string fieldName, int? width = null,
                int? height = null, bool isGoldenRatio = false);
  
    }
}
