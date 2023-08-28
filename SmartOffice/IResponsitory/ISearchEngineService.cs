using SmartOffice.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.IResponsitory
{
    public interface ISearchEngineService
    {
        Task<SearchViewModel> GetSearch(string SearchTxt,string UserId);
    }
}
