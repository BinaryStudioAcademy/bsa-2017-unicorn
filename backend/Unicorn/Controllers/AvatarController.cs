using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Filters;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    [TokenAuthenticate]
    public class AvatarController : ApiController
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpPost]
        [Route("avatar")]
        public async Task<IHttpActionResult> UploadAvatar([FromBody] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }

            string token = Request.Headers.Authorization.Parameter;
            try
            {
                await _avatarService.UploadAvatar(token, imageUrl);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}