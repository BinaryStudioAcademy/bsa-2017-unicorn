using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Controllers
{
    [RoutePrefix("account")]
    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        public AccountController(
            IAccountService accountService, 
            INotificationService notificationService)
        {
            _accountService = accountService;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<HttpResponseMessage> GetProfileById(long id)
        {
            var result = await _accountService.GetProfileInfoAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("{id}/notifications")]
        public async Task<HttpResponseMessage> GetNotifications(long id)
        {
            var result = await _notificationService.GetByAccountIdAsync(id);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/notifications/{notificationId}")]
        public async Task<HttpResponseMessage> UpdateNotifications(long id, long notificationId, [FromBody]NotificationDTO notificationDto)
        {
            await _notificationService.UpdateAsync(notificationDto);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}/notifications/{notificationId}")]
        public async Task<HttpResponseMessage> RemoveNotifications(long id, long notificationId)
        {
            await _notificationService.RemoveAsync(notificationId);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        private IAccountService _accountService;
        private INotificationService _notificationService;
    }
}
