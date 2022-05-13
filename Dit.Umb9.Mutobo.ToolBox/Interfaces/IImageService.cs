using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{

    /// <summary>
    /// interface for an image service
    /// </summary>
    public interface IImageService
    {

        /// <summary>
        /// should return a MUTOBO image object
        /// </summary>
        /// <param name="node">umbraco media node</param>
        /// <param name="width">width of the image in pixels</param>
        /// <param name="height">height of the image in pixels</param>
        /// <param name="imageCropMode">image cropping mode</param>
        /// <param name="nameSpace">name space for the web component cms template </param>
        /// <param name="isGoldenRatio">flag for using golden ratyio as defined in config</param>
        /// <returns>an image object can be used for model classes</returns>
        Image GetImage(
            IPublishedContent node,
            int? width = null,
            int? height = null,
            ImageCropMode imageCropMode = ImageCropMode.Crop,
            string nameSpace = "picture",
            bool isGoldenRatio = false);



        /// <summary>
        /// should return a collection of MUTOBO image objects
        /// </summary>
        /// <param name="contentNodes">umbraco media nodes</param>
        /// <param name="width">width of the images in pixels</param>
        /// <param name="height">height of the image in pixels</param>
        /// <param name="imageCropMode">image cropping mode</param>
        /// <param name="nameSpace">name space for the web component cms template</param>
        /// <param name="isGoldenRatio">flag for using golden ratyio as defined in config</param>
        /// <returns></returns>
        IEnumerable<Image> GetImages(
            IEnumerable<IPublishedContent>
            contentNodes,
            int? width = null,
            int? height = null,
            ImageCropMode imageCropMode = ImageCropMode.Crop,
            string nameSpace = "picture",
            bool isGoldenRatio = false);
    }

}
