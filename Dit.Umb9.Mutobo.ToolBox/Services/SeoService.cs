using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.Config;
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

    public class SeoService : BaseService, ISeoService
    {
        public SeoService(ILogger<SeoService> logger, IUmbracoContextAccessor contextAccessor) : base(logger, contextAccessor)
        {
        }

        public SeoConfig GetSeoConfiguration()
        {
            var keywords = string.Empty;
            keywords = GetKeyWords();

            return new SeoConfig
            {
                MetaTitle = GetMetaDataValue(DocumentTypes.BasePage.Fields.MetaTitle, CurrentPage),
                MetaDescription = GetMetaDescription(CurrentPage),
                MetaKeywords = keywords,
                ThumbNailWidth = 300,
                ThumbNailHeight = 300,
            };
        }

        private string GetAbstract()
        {
            var result = string.Empty;
            return result;
        }

        private string GetKeyWords()
        {
            string result = String.Empty;

            var allKeywords = CurrentPage
                .AncestorsOrSelf()
                .ToList()
                .FirstOrDefault(c => c.ContentType.Alias == DocumentTypes.HomePage.Alias)
                ?.DescendantsOrSelf()
                .ToList()
                .Where(c =>
                    c.HasProperty(DocumentTypes.BasePage.Fields.MetaKeywords) &&
                    c.HasValue(DocumentTypes.BasePage.Fields.MetaKeywords));

            if (allKeywords != null)
                foreach (var keyWords in allKeywords)
                {
                    var value = keyWords.Value<string>(DocumentTypes.BasePage.Fields.MetaKeywords);
                    if (value.EndsWith(","))
                        result += value.TrimEnd() + " ";
                    else
                    {
                        result += $"{value}, ";
                    }
                }

            return result?.TrimEnd().TrimEnd(',');
        }


        private string GetMetaDescription(IPublishedContent content)
        {
            var result = string.Empty;
            var firstnodeWithDescription = content.AncestorsOrSelf().FirstOrDefault(c => c.HasProperty(DocumentTypes.BasePage.Fields.MetaDescription) && c.HasValue(DocumentTypes.BasePage.Fields.MetaDescription));

            if (firstnodeWithDescription != null)
            {
                result = firstnodeWithDescription.Value<string>(DocumentTypes.BasePage.Fields.MetaDescription);
            }
            else
            {
                result = content.HasProperty(DocumentTypes.ArticlePage.Fields.Abstract) &&
                    content.HasProperty(DocumentTypes.ArticlePage.Fields.Abstract) ?
                    content.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract) : string.Empty;
            }

            return result;
        }


        private string GetMetaDataValue(string fieldName, IPublishedContent content)
        {
            var nodes = content.AncestorsOrSelf();

            foreach (var node in nodes)
            {
                var result = node.Value<string>(fieldName);




                if (fieldName == DocumentTypes.BasePage.Fields.MetaDescription && string.IsNullOrEmpty(result))
                {



                    result = node.HasProperty(DocumentTypes.ArticlePage.Fields.Abstract) &&
                             node.HasValue(DocumentTypes.ArticlePage.Fields.Abstract)
                        ? node.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract)
                        : string.Empty;


                }

                if (!string.IsNullOrEmpty(result))
                    return result;
            }

            return string.Empty;
        }

    }
}

