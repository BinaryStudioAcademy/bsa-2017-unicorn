using System.Security.Claims;
using System.Threading.Tasks;

namespace Unicorn.Core.Interfaces
{
    public interface IMembershipProvider
    {
        Task<long> VerifyUser(string provider, long uid);
        Task<ClaimsIdentity> GetUserClaims(long accountId);
    }
}
