using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Search;

namespace Unicorn.Core.Interfaces
{
    public interface ISearchService
    {
        //Task<List<SearchWorkDTO>> GetWorksByBaseFilters(string category, string subcategory, int? date);

        Task<List<SearchWorkDTO>> GetWorksByFilters(  string category, string subcategory, int? date,
                                                      string vendor, string ratingcompare, double? rating, bool? reviews,
                                                      double? latitude, double? longitude, double? distance,
                                                      string[] categories, string[] subcategories,
                                                      string sort  );
        //Task<List<SearchWorkDTO>> GetAllWorks();
    }
}
