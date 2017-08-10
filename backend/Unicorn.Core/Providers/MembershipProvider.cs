using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
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
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(Convert.ToInt32(accountId));

            var claims = new List<Claim>
                {
                    new Claim("id", account.Id.ToString()),
                    new Claim("role", account.Role.Name)
                };

            return new ClaimsIdentity(claims, "Token");
        }

        public async Task<long> VerifyUser(string provider, long uid)
        {
            var socialAccounts = await _unitOfWork.SocialAccountRepository.GetAllAsync();
            // var account = socialAccounts.FirstOrDefault(x => x.Provider == provider && x.Uid == uid).Account.Id
            // if(account == null) { return 0; }
            //return account == null ? false : true;
            return await Task.FromResult(1);
        }
    }
}
