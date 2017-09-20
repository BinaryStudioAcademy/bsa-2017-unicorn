using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using Unicorn.Core.Interfaces;
using Unicorn.Filters;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Subcategory;

namespace Unicorn.Controllers
{
    [RoutePrefix("categories")]
    [EnableCors("*", "*", "*")]    
    public class CategoriesController : ApiController
    {
        public CategoriesController(ICategoryService categoryService, ISubcategoryService subcategoryService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> GetAll()
        {
            var result = await _categoryService.GetAllAsync();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> Create([FromBody] CategoryDTO dto)
        {
            var result = await _categoryService.CreateAsync(dto);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetById(long id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> Update(long id, [FromBody] CategoryDTO dto)
        {
            var result = await _categoryService.UpdateAsync(dto);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> RemoveById(long id)
        {
            await _categoryService.RemoveAsync(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("{id}/subcategories")]
        public async Task<HttpResponseMessage> GetSubcategories(long id)
        {
            var result = await _subcategoryService.GetByCategoryId(id);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("{id}/subcategories")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> CreateSubcategory(long id, [FromBody] SubcategoryShortDTO dto)
        {
            var result = await _subcategoryService.CreateAsync(id, dto);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{categoryId}/subcategories/{id}")]
        public async Task<HttpResponseMessage> GetSubcategoryById(long categoryId, long id)
        {
            var result = await _subcategoryService.GetByIdAsync(id);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPut]
        [Route("{categoryId}/subcategories/{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateSubcategory(long categoryId, long id, [FromBody] SubcategoryShortDTO dto)
        {
            var result = await _subcategoryService.UpdateAsync(dto);

            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("{categoryId}/subcategories/{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> RemoveSubcategory(long categoryId, long id)
        {
            await _subcategoryService.RemoveAsync(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("search")]
        public async Task<List<CategoryDTO>> SearchByName(string template)
        {
            return await _categoryService.SearchByNameAsync(template);
        }

        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
    }
}
