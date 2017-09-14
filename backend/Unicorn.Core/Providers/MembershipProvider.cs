using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities.Enum;
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

        public async Task<ClaimsIdentity> GetUserClaims(long accountId)
        {
            var account = await _unitOfWork.AccountRepository.Query
                .Include(a => a.Role)
                .Where(a => !a.IsDeleted)
                .Where(a => !a.IsBanned)
                .SingleOrDefaultAsync(a => a.Id == accountId);
            long profileId = 0;

            switch (account.Role.Type)
            {
                case RoleType.Customer:
                    profileId = _unitOfWork.CustomerRepository.Query.First(x => x.Person.Account.Id == account.Id).Id;
                    break;
                case RoleType.Company:
                    profileId = _unitOfWork.CompanyRepository.Query.First(x => x.Account.Id == account.Id).Id;
                    break;
                case RoleType.Vendor:
                    profileId = _unitOfWork.VendorRepository.Query.First(x => x.Person.Account.Id == account.Id).Id;
                    break;
            }

            var claims = new List<Claim>
                {
                    new Claim("accountid", account.Id.ToString()),
                    new Claim("roleid", account.Role.Id.ToString()),
                    new Claim("profileid", profileId.ToString())
                };

            return new ClaimsIdentity(claims, "Token");
        }

        public async Task<long> VerifyUser(string provider, string uid)
        {
            var socialAccount = await _unitOfWork.SocialAccountRepository.Query.Include(x => x.Account)
                .FirstOrDefaultAsync(x => x.Provider == provider && x.Uid == uid);
            return socialAccount == null ? 0 : socialAccount.Account.Id;
        }
    }
}
