using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    public interface IChatService
    {
        Task Create(ChatDTO msg);
        ICollection<ChatDTO> GetChat(long senderId, long receiverId);
        IEnumerable<ChatDTO> GetChatByDate(long senderId, long receiverId, DateTime dateMin);
        Task Remove(ChatDTO msg);
        Task Update(ChatDTO msg);
    }
}
