using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Pages
{
    /// <summary>
    /// model class for an instance of the document type base page
    /// </summary>
    public class BasePage : ContentModel
    {
        /// <summary>
        /// field for a global slogan 
        /// </summary>
        public Slogan GlobalSlogan { get; set; }
        /// <summary>
        /// header configuration for the page
        /// </summary>
        public IHeaderConfiguration HeaderConfiguration { get; set; }
        /// <summary>
        /// footer configuration for the page
        /// </summary>
        public IFooterConfiguration FooterConfiguration { get; set; }
        /// <summary>
        /// field for the page title
        /// </summary>
        public string PageTitle => Content.Value<string>(DocumentTypes.BasePage.Fields.PageTitle);
        /// <summary>
        /// flag to hide the page from the navigation
        /// </summary>
        public bool HideFromNavigation => Content.Value<bool>(DocumentTypes.BasePage.Fields.HideFromNavigation);
        /// <summary>
        /// flag to render a non clickable link in navigation 
        /// </summary>
        public bool NotClickable => Content.Value<bool>(DocumentTypes.BasePage.Fields.NotClickable);
        /// <summary>
        /// field to redirect the page on load
        /// </summary>
        public Link RedirectLink => Content.Value<Link>(DocumentTypes.BasePage.Fields.RedirectLink);
        /// <summary>
        /// META-Tag title
        /// </summary>
        public string MetaTitle => Content.Value<string>(DocumentTypes.BasePage.Fields.MetaTitle);
        /// <summary>
        /// META-Tag description
        /// </summary>
        public string MetaDescription => Content.Value<string>(DocumentTypes.BasePage.Fields.MetaDescription);
        /// <summary>
        /// META-Tag keywords
        /// </summary>
        public string MetaKeywords => Content.Value<string>(DocumentTypes.BasePage.Fields.MetaKeywords);
        /// <summary>
        /// flag to exclude the page from the xml sitemap
        /// </summary>
        public bool ExcludeFromXMLSitemap => Content.Value<bool>(DocumentTypes.BasePage.Fields.ExcludeFromXMLSitemap);
        /// <summary>
        /// sets the search engine frequency for the page in the sitemap.xml
        /// </summary>
        public string SearchEngineFrequency => Content.HasValue(DocumentTypes.BasePage.Fields.SearchEngineFrequency) ? 
            Content.Value<string>(DocumentTypes.BasePage.Fields.SearchEngineFrequency) 
            : string.Empty;
        /// <summary>
        /// sets the search relavtive property for the page in the sitemap.xml
        /// </summary>
        public string SearchEngineRelativePriority => Content.HasValue(DocumentTypes.BasePage.Fields.SearchEngineRelativePriority) ? 
            Content.Value<string>(DocumentTypes.BasePage.Fields.SearchEngineRelativePriority) 
            : string.Empty;
       /// <summary>
       /// holds the key for google analytics
       /// </summary>
        public string GoogleAnalyticsKey => Content.HasValue(DocumentTypes.BasePage.Fields.GoogleAnalyticsKey)
            ? Content.Value<string>(DocumentTypes.BasePage.Fields.GoogleAnalyticsKey)
            : string.Empty;
        /// <summary>
        /// the theme for the page
        /// </summary>
        public ITheme Theme { get; set; }
        /// <summary>
        /// flag to exclude the page from the website search
        /// </summary>
        public bool ExcludeFromSearch => Content.Value<bool>(DocumentTypes.BasePage.Fields.ExcludeFromSearch);


        /// <summary>
        /// constructor for a base page
        /// </summary>
        /// <param name="content"></param>
        public BasePage(IPublishedContent content) : base(content)
        {

        }
    }
}

