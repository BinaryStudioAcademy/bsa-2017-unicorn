using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Unicorn.Core.DTOs;
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

            //string token = await authService.GenerateJwtTokenAsync(provider, uid);
            //string token = "123_TEST";
            string token = null;

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
            var customerDto = new CustomerDTO()
            {
                Books = new List<BookDTO>(),
                Person = new PersonDTO()
                {
                    Location = new LocationDTO(),
                    Birthday = customer.Birthday,
                    Phone = customer.Phone,
                    Name = customer.FirstName,
                    MiddleName = customer.MiddleName,
                    SurnameName = customer.LastName,
                    Account = new AccountDTO()
                    {
                        Role = new RoleDTO(),
                        Permissions = new List<PermissionDTO>(),
                        DateCreated = DateTime.Now,
                        Email = customer.Email,
                        SocialAccounts = new List<SocialAccountDTO>
                        {
                            new SocialAccountDTO()
                            {
                                Provider = customer.Provider,
                                Uid = customer.Uid
                            }
                        }
                    }

                }
            };
            await customerService.CreateAsync(customerDto);
        
            
            return Request.CreateResponse(HttpStatusCode.OK, "Success saved");
        }

        [Route("membership/vendor")]
        public async Task<HttpResponseMessage> ConfirmVendor(VendorRegisterDTO vendor)
        {
            var vendorDto = new VendorDTO()
            {
                Person = new PersonDTO()
                {
                    Birthday = vendor.Birthday,
                    Phone = vendor.Phone,
                    Name = vendor.FirstName,
                    MiddleName = vendor.MiddleName,
                    SurnameName = vendor.LastName,
                    Account = new AccountDTO()
                    {
                        DateCreated = DateTime.Now,
                        Email = vendor.Email,
                        SocialAccounts = new List<SocialAccountDTO>
                        {
                            new SocialAccountDTO()
                            {
                                Provider = vendor.Provider,
                                Uid = vendor.Uid
                            }
                        }
                    }

                },
                Experience = vendor.Experience,
                Position = vendor.Position,
                ExWork = vendor.Speciality
            };
            
            await vendorService.Create(vendorDto);

            return Request.CreateResponse(HttpStatusCode.OK, "Success saved");
        }

        [Route("membership/company")]
        public async Task<HttpResponseMessage> ConfirmCompany(CompanyRegisterDTO company)
        {
            var companyDto = new CompanyDTO()
            {
                Staff = company.Stuff,
                FoundationDate = company.Foundation,
                Account = new AccountDTO()
                {
                    DateCreated = DateTime.Now,
                    Email = company.Email,
                    SocialAccounts = new List<SocialAccountDTO>
                    {
                        new SocialAccountDTO()
                        {
                            Provider = company.Provider,
                            Uid = company.Uid
                        }
                    }
                }
            };

            await companyService.Create(companyDto);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}