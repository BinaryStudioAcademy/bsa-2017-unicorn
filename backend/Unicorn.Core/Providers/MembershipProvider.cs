using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Providers
{
    public class MembershipProvider : IMembershipProvider
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembershipProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<ClaimsIdentity> GetUserClaims(string provider, long? uid)
        {
            // TODO: Get from DB real data

            string login = "Test";
            string role = "Admin";

            var claims = new List<Claim>
                {
                    new Claim("login", login),
                    new Claim("role", role),
                    new Claim("somedata_bla_bla", "Make .NET great again!")
                };
            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return Task.FromResult(claimsIdentity);
        }

        public async Task<bool> VerifyUser(string provider, long? uid)
        {
            SocialAccount account;
            var socialAccounts = await _unitOfWork.SocialAccountRepository.GetAllAsync();

            switch (provider)
            {
                case "facebook":
                    account = socialAccounts.FirstOrDefault(x => x.FacebookUID == uid);
                    break;
                case "google":
                    account = socialAccounts.FirstOrDefault(x => x.GoogleUID == uid);
                    break;
                default:
                    account = null;
                    break;
            }

            return account == null ? false : true;
        }
    }
}
