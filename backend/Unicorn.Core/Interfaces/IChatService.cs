using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Core.Interfaces
{
    public interface IChatService
    {
        Task CreateMessage(ChatMessageDTO msg);
        Task CreateDialog(ChatDialogDTO dialog);
        Task <ChatDialogDTO> GetDialog(long dialogId);        
        Task Remove(ChatMessageDTO msg);
        Task Update(ChatMessageDTO msg);
    }
}
