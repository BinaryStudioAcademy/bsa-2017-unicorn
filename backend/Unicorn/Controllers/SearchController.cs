using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Search;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SearchController : ApiController
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<List<SearchWorkDTO>> GetPerformersByFilters(  string category, string subcategory, int? date,
                                                                        string vendor, string ratingcompare, double? rating, bool? reviews,
                                                                        double? latitude, double? longitude, double? distance,
                                                                        [FromUri] string[] categories, [FromUri] string[] subcategories, string city,
                                                                        int? sort  )
        {
            return await _searchService.GetWorksByFilters(  category, subcategory, date,
                                                            vendor, ratingcompare, rating, reviews,
                                                            latitude, longitude, distance,
                                                            categories, subcategories, city,
                                                            sort );
        }

        [HttpGet]
        [Route("search")]
        public async Task<List<SearchWorkDTO>> GetPerformersByBaseFilters(string category, string subcategory, int? date)
        {
            return await _searchService.GetWorksByBaseFilters(category, subcategory, date);
        }

        [HttpGet]
        [Route("search")]
        public async Task<List<SearchWorkDTO>> GetAllPerformers()
        {
            return await _searchService.GetAllWorks();
        }
    }
}