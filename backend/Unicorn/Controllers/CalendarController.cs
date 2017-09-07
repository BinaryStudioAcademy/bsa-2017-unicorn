using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CalendarController:ApiController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        [Route("calendar/{calendarId}")]
        public async Task<HttpResponseMessage> GetCalendarById(long calendarId)
        {
            try
            {
                var result = await _calendarService.GetCalendarById(calendarId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
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
        [Route("calendar")]
        public async Task<HttpResponseMessage> CreateCalendar([FromBody]long accountId)
        {
            try
            {
                var result = await _calendarService.CreateCalendar(accountId);
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