using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Filters;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    [TokenAuthenticate]
    public class CalendarController:ApiController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [Route("calendar/account/{accountId}")]
        public async Task<HttpResponseMessage> GetCalendarByAccountId(long accountId)
        {
            try
            {
                var result = await _calendarService.GetCalendarByAccountId(accountId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("calendar/{accountId}")]
        public async Task<HttpResponseMessage> CreateCalendar([FromBody]CalendarDTO calendar, long accountId)
        {
            try
            {
                var result = await _calendarService.CreateCalendar(accountId, calendar);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [HttpPost]
        [Route("calendar/save")]
        public async Task<HttpResponseMessage> SaveCalendar(CalendarDTO calendar)
        {
            try
            {
                await _calendarService.SaveCalendar(calendar);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

    }
}