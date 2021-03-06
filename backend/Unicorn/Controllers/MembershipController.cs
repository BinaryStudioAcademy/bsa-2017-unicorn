﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.Interfaces;
using Unicorn.Models;
using Unicorn.Shared.DTOs.Register;

namespace Unicorn.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MembershipController : ApiController
    {
        private IAuthService authService;
        private ICustomerService customerService;
        private IVendorService vendorService;
        private ICompanyService companyService;

        public MembershipController(IAuthService authService, ICustomerService customerService,
            IVendorService vendorService, ICompanyService companyService)
        {
            this.authService = authService;
            this.customerService = customerService;
            this.vendorService = vendorService;
            this.companyService = companyService;
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

            string token = await authService.GenerateJwtTokenAsync(user.Provider, user.Uid);

            if (token == null)
            {
                // Special status code for registration
                response = Request.CreateResponse(HttpStatusCode.NoContent, "Uid not found");
                return response;
            }

            response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }

        [Route("membership/customer")]
        public async Task<HttpResponseMessage> ConfirmCustomer(CustomerRegisterDTO customer)
        {
            string token = null;
            try
            {
                await customerService.CreateAsync(customer);
                token = await authService.GenerateJwtTokenAsync(customer.Provider, customer.Uid);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }

        [Route("membership/vendor")]
        public async Task<HttpResponseMessage> ConfirmVendor(VendorRegisterDTO vendor)
        {
            string token = null;

            try
            {
                await vendorService.CreateAsync(vendor);
                token = await authService.GenerateJwtTokenAsync(vendor.Provider, vendor.Uid);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }

        [Route("membership/company")]
        public async Task<HttpResponseMessage> ConfirmCompany(CompanyRegisterDTO company)
        {
            string token = null;

            try
            {
                await companyService.Create(company);
                token = await authService.GenerateJwtTokenAsync(company.Provider, company.Uid);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Expose-Headers", "Token");
            response.Headers.Add("Token", token);

            return response;
        }
    }
}
