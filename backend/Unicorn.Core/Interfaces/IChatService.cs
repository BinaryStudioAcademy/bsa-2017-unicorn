using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Interfaces
{
    interface IChatService
    {
        Task Create(ChatDTO msg);
        ICollection<ChatDTO> GetChat(long receiverId, long senderId);
        IEnumerable<ChatDTO> GetChatByDate(long receiverId, long senderId, DateTime dateMin);
        Task Remove(ChatDTO msg);
        Task Update(ChatDTO msg);
    }
}
