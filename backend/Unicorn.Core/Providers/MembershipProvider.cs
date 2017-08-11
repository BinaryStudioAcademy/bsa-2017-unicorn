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
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);

            var claims = new List<Claim>
                {
                    new Claim("id", account.Id.ToString()),
                    new Claim("role", account.Role.Name)
                };

            return new ClaimsIdentity(claims, "Token");
        }

        public async Task<long> VerifyUser(string provider, long uid)
        {
            var _accounts = await _unitOfWork.SocialAccountRepository.GetAllAsync();
            var socialAccount = _accounts.FirstOrDefault(x => x.Provider == provider && x.Uid == uid);

            return socialAccount == null ? 0 : socialAccount.Account.Id;
        }
    }
}
