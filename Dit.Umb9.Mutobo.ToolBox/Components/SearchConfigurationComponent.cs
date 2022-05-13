using Examine;
using Examine.Lucene.Search;
using UmbracoExamine.PDF;
using Dit.Umb9.Mutobo.ToolBox.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;
using Examine.Lucene.Providers;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Modules;
using Umbraco.Cms.Core.Services;

namespace Dit.Umb9.Mutobo.ToolBox.Components
{
    public class SearchConfigurationComponent : IComponent
    {
        private readonly IExamineManager _examineManager;
        private readonly IUmbracoContextFactory _contextFactory;
        private readonly IMutoboContentService _mutoboContentService;
        private readonly IContentService _contentService;



        public SearchConfigurationComponent(IExamineManager examineManager, IUmbracoContextFactory contextFactory, IMutoboContentService mutoboContentService, IContentService contentService = null)
        {

            // inject ExamineManager
            _examineManager = examineManager;
            //
            _contextFactory = contextFactory;
            _mutoboContentService = mutoboContentService;
            _contentService = contentService;
        }



        public void Initialize()
        {
            IIndex externalIndex = null;
            IIndex pdfIndex = null;

            if (_examineManager.TryGetIndex("ExternalIndex", out externalIndex)
                && _examineManager.TryGetIndex(PdfIndexConstants.PdfIndexName, out pdfIndex))
            {
                // FieldDefinitionCollection contains all indexed fields 
                externalIndex.FieldDefinitions.Append(new FieldDefinition(DocumentTypes.ArticlePage.Fields.MainContent, FieldDefinitionTypes.FullText));
                externalIndex.FieldDefinitions.Append(new FieldDefinition(DocumentTypes.ArticlePage.Fields.Abstract, FieldDefinitionTypes.FullText));
                externalIndex.FieldDefinitions.Append(new FieldDefinition(DocumentTypes.BasePage.Fields.PageTitle, FieldDefinitionTypes.FullText));
                ((BaseIndexProvider)externalIndex).TransformingIndexValues += OnTransformingIndexValues;

                ////register multisearcher
                //var multisearch = new MultiIndexSearcher("MultiSearcher", new IIndex[] { externalIndex, pdfIndex });
                //_examineManager.AddSearcher(multiSearcher);

            }
            else
            {
                throw new Exception("Index not found");
            }
        }

        private void OnTransformingIndexValues(object sender, IndexingItemEventArgs e)
        {
            if (int.TryParse(e.ValueSet.Id, out var nodeId))

                using (var umbracoContext = _contextFactory.EnsureUmbracoContext())
                {
                    IPublishedContent contentNode = umbracoContext.UmbracoContext.Content.GetById(nodeId);

                    if (contentNode != null)
                    {
                        foreach (var culture in contentNode.Cultures)
                        {
                            if (contentNode != null)
                            {
                                string moduleContent;

                                switch (contentNode.ContentType.Alias)
                                {
                                    case DocumentTypes.HomePage.Alias:
                                        if (contentNode.HasValue(DocumentTypes.HomePage.Fields.Modules, culture.Key))
                                        {
                                            //scr: disabled @UMBRACO 10 Update enable this later

                                            //moduleContent = IndexModules(_mutoboContentService.GetContent(contentNode, DocumentTypes.HomePage.Fields.Modules, culture.Key) as IEnumerable<MutoboContentModule>);
                                            //e.ValueSet.Set("modules", moduleContent);
                                        }
                                        break;

                                
                                    case DocumentTypes.ContentPage.Alias:
                                        if (contentNode.HasValue(DocumentTypes.ContentPage.Fields.Modules, culture.Key))
                                        {

                                            //scr: disabled @UMBRACO 10 Update enable this later

                                            //moduleContent = IndexModules(_mutoboContentService.GetContent(contentNode, DocumentTypes.ContentPage.Fields.Modules, culture.Key) as IEnumerable<MutoboContentModule>);
                                            //e.ValueSet.Set("modules", moduleContent);
                                        }
                                        break;

                                }
                            }
                        }
                    }
                }
        }


        private string IndexModules(IEnumerable<MutoboContentModule> modules)
        {
            var result = string.Empty;

            foreach (var module in modules)
            {

                switch (module.ContentType.Alias)
                {

                    case ElementTypes.Heading.Alias:
                        if (module is Heading heading)
                        {
                            result += $"{heading.Text} ";
                        }
                        break;
                    case ElementTypes.RichTextComponent.Alias:
                        if (module is RichtextComponent richtextComponent)
                        {
                            result += $"{richtextComponent.RichText.StripHtml()} ";
                        }
                        break;
                    case ElementTypes.Flyer.Alias:
                        break;
                    case ElementTypes.Teaser.Alias:

                        break;
                    case ElementTypes.Accordeon.Alias:

                        break;
                    case ElementTypes.Quote.Alias:

                        break;

                }
            }

            return result;
        }




        public void Terminate()
        {
        }
    }
}

