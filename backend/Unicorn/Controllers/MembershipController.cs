using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Unicorn.Core.Interfaces;
using Unicorn.Models;

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
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]UserSocial user)
        {
            HttpResponseMessage response = null;

            if (user == null || (string.IsNullOrWhiteSpace(user.Provider) || string.IsNullOrWhiteSpace(user.Uid)))
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "Missing provider or uid");
                return response;
            }

            //string token = await authService.GenerateJwtTokenAsync(provider, uid);
            string token = "123_TEST";

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