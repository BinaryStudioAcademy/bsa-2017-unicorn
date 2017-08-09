using System.Threading.Tasks;

namespace Unicorn.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(string provider, string uid);
        Task<bool> ValidateTokenAsync(string tokenString);       
    }
}
