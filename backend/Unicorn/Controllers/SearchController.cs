﻿using System.Collections.Generic;
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
        public async Task<List<SearchWorkDTO>> GetPerformersByBaseFilters(string category, string subcategory, int date)
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