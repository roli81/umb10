using Dit.Umb9.Mutobo.ToolBox.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface ICardService
    {
        IEnumerable<Card> GetCards(IPublishedElement element, string fieldName);
    }
}
