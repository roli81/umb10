using Dit.Umb9.Mutobo.ToolBox.Components;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Dit.Umb9.Mutobo.ToolBox.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Dit.Umb9.Mutobo.ToolBox.Composer
{

    public class AppComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            AddComponents(builder);
            RegisterServices(builder);
        }


        private void AddComponents(IUmbracoBuilder builder)
        {
            builder.Components().Append<MinifierComponent>();
            builder.Components().Append<SearchConfigurationComponent>();


        }

        private void RegisterServices(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IImageService, ImageService>();
            builder.Services.AddSingleton<IConfigService, ConfigService>();
            builder.Services.AddTransient<IMutoboContentService, MutoboContentService>();
            builder.Services.AddScoped<ISeoService, SeoService>();
            builder.Services.AddScoped<IXmlSitemapService, XmlSitemapService>();
            builder.Services.AddSingleton<ICardService, CardService>();
            builder.Services.AddSingleton<ISliderService, SliderService>();
            builder.Services.AddScoped<IMailService, MailService>();
            builder.Services.AddSingleton<IFlyerService, FlyerService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IPageLayoutService, PageLayoutService>();
            builder.Services.AddSingleton<IPictureLinkService, PictureLinkService>();
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<ICallToActionService, CallToActionService>();
            builder.Services.AddSingleton<IThemeService, ThemeService>();
            builder.Services.AddSingleton<IDictionaryService, DictionaryService>();
        }
    }
}

