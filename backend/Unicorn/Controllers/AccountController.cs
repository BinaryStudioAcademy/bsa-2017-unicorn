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
        [TokenAuthenticate]
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
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> Ban(long id)
        {
            try
            {
                await _adminService.BanAccountAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("{id}/unban")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> Unban(long id)
        {
            try
            {
                await _adminService.UnbanAccountAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpGet]
        [Route("{id}/notifications")]
        [TokenAuthenticate]
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
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> UpdateNotifications(long id, long notificationId, [FromBody]NotificationDTO notificationDto)
        {
            await _notificationService.UpdateAsync(notificationDto);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id}/notifications/{notificationId}")]
        [TokenAuthenticate]
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
        [TokenAuthenticate]
        public async Task<AccountsPage> GetBannedAccounts(int page, int size)
        {
            var banlist = await _adminService.GetAllAsync();

            return await _adminService.GetAccountsPageAsync(banlist, page, size);
        }

        [HttpGet]
        [Route("banned/search")]
        [TokenAuthenticate]
        public async Task<AccountsPage> GetBannedAccounts(bool isBanned, string template = "", string role = "all", int page = 1, int size = 20)
        {
            var banlist = await _adminService.SearchAsync(template, isBanned, role);

            return await _adminService.GetAccountsPageAsync(banlist, page, size);
        }

        [HttpGet]
        [Route("banned/search")]
        [TokenAuthenticate]
        public async Task<AccountsPage> GetBannedAccounts(string template = "", string role = "all", int page = 1, int size = 20)
        {
            var banlist = await _adminService.SearchAsync(template, role);

            return await _adminService.GetAccountsPageAsync(banlist, page, size);
        }

        private readonly IAccountService _accountService;
        private readonly INotificationService _notificationService;
        private readonly IAdminService _adminService;
    }
}
