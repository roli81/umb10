using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Dictionary;
using Umbraco.Cms.Core.Web;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class DictionaryService : BaseService, IDictionaryService
    {
        private readonly ICultureDictionaryFactory _cultureDictionaryFactory;
        private ICultureDictionary _cultureDictionary;

        T Ensure<T>(T o) where T : class => o ?? throw new InvalidOperationException("This UmbracoHelper instance has not been initialized.");

        private ICultureDictionaryFactory CultureDictionaryFactory => Ensure(_cultureDictionaryFactory);


        public ICultureDictionary CultureDictionary => _cultureDictionary
             ?? (_cultureDictionary = CultureDictionaryFactory.CreateDictionary());

        public DictionaryService(ILogger<DictionaryService> logger, IUmbracoContextAccessor contextAccessor, ICultureDictionaryFactory cultureDictionary) : base(logger, contextAccessor)
        {
            _cultureDictionaryFactory = cultureDictionary ?? throw new ArgumentNullException(nameof(cultureDictionary));
        }

        public string GetDictionaryValue(string key, string defaultValue = "TRANSLATE")
        {
            var dictionaryValue = CultureDictionary[key];
            if (string.IsNullOrEmpty(dictionaryValue))
            {
                dictionaryValue = defaultValue;
            }
            return dictionaryValue;
        }
    }
}
