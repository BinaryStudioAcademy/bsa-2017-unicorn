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
    //[TokenAuthenticate]
    public class AvatarController : ApiController
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpPost]
        [Route("avatar/{id}")]
        public async Task<IHttpActionResult> UploadAvatar([FromBody] string imageUrl, int id)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            try
            {
                await _avatarService.UploadAvatar(imageUrl, id);
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPost]
        [Route("background/{id}")]
        public async Task<IHttpActionResult> UploadBackground([FromBody] string imageUrl, int id)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest();
            }
            try
            {
                await _avatarService.UploadBackground(imageUrl, id);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}