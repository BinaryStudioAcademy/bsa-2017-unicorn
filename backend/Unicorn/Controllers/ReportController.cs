using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Filters;
using Unicorn.Shared.DTOs;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]    
    public class ReportController : ApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        [Route("report")]
        public async Task<HttpResponseMessage> GetAllReportsAsync()
        {
            try
            {
                var reports = await _reportService.GetAllAsync();
                return Request.CreateResponse(HttpStatusCode.OK, reports);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [Route("report/{id}")]
        public async Task<HttpResponseMessage> GetReportAsync(long id)
        {
            try
            {
                var report = await _reportService.GetByIdAsync(id);
                return Request.CreateResponse(HttpStatusCode.OK, report);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [Route("report")]
        public async Task<HttpResponseMessage> SendReportAsync(ReportDTO report)
        {            
            try
            {
                if (string.IsNullOrEmpty(report.Message) || (report.Type != ReportType.Feedback && report.Type != ReportType.Complaint))
                {
                    throw new ArgumentException();
                }
                await _reportService.CreateAsync(report);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("report")]
        public async Task<HttpResponseMessage> UpdateReportAsync(ReportDTO report)
        {
            try
            {
                await _reportService.UpdateAsync(report);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("report/{id}")]
        [TokenAuthenticate]
        public async Task<HttpResponseMessage> DeleteReportAsync(long id)
        {
            try
            {
                await _reportService.DeleteAsync(id);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
