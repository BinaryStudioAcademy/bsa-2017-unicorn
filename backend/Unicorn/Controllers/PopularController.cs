using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Popular;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PopularController : ApiController
    {
        private readonly IPopularService _popularService;

        public PopularController(IPopularService popularService)
        {
            _popularService = popularService;
        }

        [HttpGet]
        [Route("popular/categories")]
        public async Task<List<PopularCategoryDTO>> GetCategories()
        {
            return await _popularService.GetPopularCategories();
        }

        [HttpGet]
        [Route("popular/performers")]
        public async Task<List<PerformerDTO>> GetPerformers()
        {
            return await _popularService.GetPopularPerformers();
        }

        [HttpGet]
        [Route("popular/performers/{id}")]
        public async Task<List<PerformerDTO>> GetPerformersById(long id) // id - category id
        {
            return await _popularService.GetPopularPerformers(id);
        }


    }
}
