using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Search;

namespace Unicorn.Core.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchWorkDTO>> GetAllWorks();

        Task<List<SearchWorkDTO>> GetWorksByBaseFilters(string category, string subcategory, DateTimeOffset? date, int timeZone);

        Task<List<SearchWorkDTO>> GetWorksByFilters(  string category, string subcategory, DateTimeOffset? date, int timeZone,
                                                      string vendor, string ratingcompare, double? rating, bool? reviews,
                                                      double? latitude, double? longitude, double? distance,
                                                      string[] categories, string[] subcategories, string city,
                                                      int? sort  );
    }
}
