using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Pages
{

    /// <summary>
    /// model class for an instance of the document type article page
    /// </summary>
    public class ArticlePage : BasePage
    {
        /// <summary>
        /// abstract site description
        /// </summary>
        public string Abstract => Content.Value<string>(DocumentTypes.ArticlePage.Fields.Abstract);
        /// <summary>
        /// flag to hide the abstract site description on the rendered page
        /// </summary>
        public bool HideAbstract => Content.Value<bool>(DocumentTypes.ArticlePage.Fields.HideAbstract);
        /// <summary>
        /// main content of the page
        /// </summary>
        public string MainContent => Content.Value<string>(DocumentTypes.ArticlePage.Fields.MainContent);
        /// <summary>
        /// collection of emotion images
        /// </summary>
        public IEnumerable<Image> EmotionImages { get; set; }

        /// <summary>
        /// constructor for an article page
        /// </summary>
        /// <param name="content">umbraco published content</param>
        public ArticlePage(IPublishedContent content) : base(content)
        {

        }
    }
}
