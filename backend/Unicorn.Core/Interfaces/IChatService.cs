﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Core.Interfaces
{
    public interface IChatService
    {
        Task CreateMessage(ChatMessageDTO msg);
        Task UpdateNotReadedMessage(long dialogId, long ownerId);
        Task<ChatDialogDTO> CreateDialog(ChatDialogDTO dialog);
        Task<ChatDialogDTO> GetDialog(long dialogId);
        Task<IEnumerable<ChatDialogDTO>> GetAllDialogs(long accountId);
        Task<ChatDialogDTO> FindDialog(long participantOneId, long participantTwoId);
        Task RemoveDialog(long dialogId);
        Task RemoveMessage(long messageId);
        Task Update(ChatMessageDTO msg);
    }
}
