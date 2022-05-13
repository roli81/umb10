using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using System.Collections.Generic;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface ISearchService
    {
        ISearchResultsModel PerformSearch(string term);

        IEnumerable<SearchResult> Search(string term);
    }
}
