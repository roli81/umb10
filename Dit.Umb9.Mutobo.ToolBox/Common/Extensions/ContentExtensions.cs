﻿using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Common.Extensions
{
    public static class ContentExtensions
    {

        public static string GetDitUrl(this IPublishedContent content)
        {
            var redirectLink = content.Value<Link>(DocumentTypes.BasePage.Fields.RedirectLink)?.Url;

            if (string.IsNullOrEmpty(redirectLink))
                return content.Url();

            return redirectLink;
        }


        public static string GetLinkTarget(this IPublishedContent content)
        {


            var target = content.Value<Link>(DocumentTypes.BasePage.Fields.RedirectLink)?.Target;


            if (string.IsNullOrEmpty(target))
            {
                var ownPageFlag = content.Value<bool>(DocumentTypes.BasePage.Fields.OpenInNewWindow);
                return ownPageFlag ? "_blank" : "_self";
            }

            return target ?? "_self";

        }

        public static bool GetOpenInNewWindowFlag(this IPublishedContent content)
        {
            var target = content.Value<Link>(DocumentTypes.BasePage.Fields.RedirectLink)?.Target;


            if (string.IsNullOrEmpty(target))
            {
                var ownPageFlag = content.Value<bool>(DocumentTypes.BasePage.Fields.OpenInNewWindow);
                return ownPageFlag;
            }

            return target == "_blank";

        }

        public static Image GetImage(this IPublishedContent content, HttpContext context, string field, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop)
        {
            var imageService = (IImageService)context.RequestServices.GetService(typeof(IImageService));
            return content.HasValue(field)
                ? imageService.GetImage(content.Value<IPublishedContent>(field), width, height, imageCropMode)
                : null;
        }

        public static Image GetImage(this IPublishedElement element, HttpContext context, string field, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop, bool useSources = false)
        {

            var imageService = (IImageService)context.RequestServices.GetService(typeof(IImageService));
            return element.HasValue(field)
                ? imageService.GetImage(element.Value<IPublishedContent>(field), width, height, imageCropMode, isGoldenRatio: useSources)
                : null;
        }




        public static IEnumerable<Image> GetImages(this IPublishedContent content, HttpContext context, string field, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop, bool useSources = false)
        {

            var imageService = (IImageService)context.RequestServices.GetService(typeof(IImageService));

            return content.HasValue(field)
                ? imageService.GetImages(content.Value<IEnumerable<IPublishedContent>>(field), width, height, imageCropMode, isGoldenRatio: useSources)
                : null;
        }

        public static IEnumerable<Image> GetImages(this IPublishedElement element, HttpContext context, string field, int? width = null, int? height = null, ImageCropMode imageCropMode = ImageCropMode.Crop)
        {

            var imageService = (IImageService)context.RequestServices.GetService(typeof(IImageService));

            return element.HasValue(field)
                ? imageService.GetImages(element.Value<IEnumerable<IPublishedContent>>(field), width, height, imageCropMode)
                : null;
        }


    }
}
