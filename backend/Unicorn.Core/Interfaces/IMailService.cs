using Unicorn.Shared.DTOs.Email;

namespace Unicorn.Core.Interfaces
{
    public interface IMailService
    {
        void Send(EmailMessage msg);
    }
}
