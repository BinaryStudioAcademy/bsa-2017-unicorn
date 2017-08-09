using System.Security.Claims;
using System.Threading.Tasks;

namespace Unicorn.Core.Interfaces
{
    public interface IMembershipProvider
    {
        Task<bool> VerifyUser(string provider, string uid);
        Task<ClaimsIdentity> GetUserClaims(string provider, string uid);
    }
}
