using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class DbController : ApiController
    {
        private readonly IDBService _dbService;

        public DbController(IDBService dbService)
        {
            _dbService = dbService;
        }

        // GET: db/
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                _dbService.RecreateDatabase();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
