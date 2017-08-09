using System.Threading.Tasks;

namespace Unicorn.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string provider, string uid);
        bool ValidateToken(string tokenString);       
    }
}
