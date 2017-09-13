using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Admin;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Controllers
{
    [RoutePrefix("account")]
    [EnableCors("*", "*", "*")]
    public class AccountController : ApiController
    {
        public AccountController(
            IAccountService accountService,
            INotificationService notificationService,
            IAdminService adminService)
        {
            _accountService = accountService;
            _notificationService = notificationService;
            _adminService = adminService;
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

        [HttpPost]
        [Route("{id}/ban")]
        public async Task<HttpResponseMessage> Ban(long id, [FromBody]DateTimeOffset endTime)
        {
            var result = await _adminService.BanAccountAsync(id, endTime);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("{id}/ban")]
        public async Task<HttpResponseMessage> UpdateBan(long id, [FromBody]DateTimeOffset endTime)
        {
            var result = await _adminService.UpdateBanTime(id, endTime);

            if (result == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            else
                return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpDelete]
        [Route("{id}/unban")]
        public async Task<HttpResponseMessage> Unban(long id, [FromBody]DateTimeOffset endTime)
        {
            await _adminService.LiftBanByAccountAsync(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
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

        [HttpGet]
        [Route("search")]
        public async Task<List<ShortProfileInfoDTO>> SearchByTemplate(string template, int count)
        {
            return await _accountService.SearchByTemplate(template, count);
        }

        [HttpGet]
        [Route("banned")]
        public async Task<BannedAccountsPage> GetBannedAccounts(int page, int size)
        {
            var banlist = await _adminService.GetAllBannedAccountsAsync();

            return await _adminService.GetBannedAccountsPageAsync(page, size, banlist);
        }

        [HttpGet]
        [Route("banned/search")]
        public async Task<BannedAccountsPage> GetBannedAccounts(string template, int page, int size)
        {
            var banlist = await _adminService.SearchAccountsAsync(template);

            return await _adminService.GetBannedAccountsPageAsync(page, size, banlist);
        }

        private readonly IAccountService _accountService;
        private readonly INotificationService _notificationService;
        private readonly IAdminService _adminService;
    }
}
