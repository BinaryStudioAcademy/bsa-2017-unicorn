using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unicorn.Core.Interfaces;

namespace Unicorn.Controllers
{
    public class MembershipController : ApiController
    {
        private IAuthService authService;

        public MembershipController(IAuthService authService)
        {
            this.authService = authService;
        }

        // POST: Membership
        [HttpGet]
        public async Task<HttpResponseMessage> Post(string provider, string uid)
        {
            HttpResponseMessage response = null;

            if (string.IsNullOrWhiteSpace(provider))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Missing provider or uid");
                return response;
            }

            string token = await authService.GenerateJwtTokenAsync(provider, uid);

            if (token == null)
            {
                // Special status code for registration
                response = Request.CreateResponse(HttpStatusCode.NoContent, "Uid not found");
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Token", token);

            return response;

        }
    }
}