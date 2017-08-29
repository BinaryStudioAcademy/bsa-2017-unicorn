using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Chat;

namespace Unicorn.Core.Providers
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateMessage(ChatMessageDTO msg)
        {
            var dialog = await _unitOfWork.ChatDialogRepository.GetByIdAsync(msg.DialogId);
            var owner = await _unitOfWork.AccountRepository.GetByIdAsync(msg.OwnerId);

            ChatMessage cmsg = new ChatMessage
            {
                IsReaded = false,
                Date = msg.Date,
                Dialog = dialog,
                Owner = owner,
                Message = msg.Message
            };

            _unitOfWork.ChatMessageRepository.Create(cmsg);
            await _unitOfWork.SaveAsync();
        }

        public async Task CreateDialog(ChatDialogDTO dialog)
        {
            var participant1 = await _unitOfWork.AccountRepository.GetByIdAsync(dialog.ParticipantOne.Id);
            var participant2 = await _unitOfWork.AccountRepository.GetByIdAsync(dialog.ParticipantTwo.Id);

            var dl = new ChatDialog
            {
                Participant1 = participant1,
                Participant2 = participant2,
                Messages = new List<ChatMessage>()
            };

            _unitOfWork.ChatDialogRepository.Create(dl);
            await _unitOfWork.SaveAsync();
        }

        public async Task<ChatDialogDTO> GetDialog(long dialogId)
        {
            var dialog = await _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Messages)
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
                .SingleAsync(x => x.Id == dialogId);

            ChatDialogDTO dl = new ChatDialogDTO
            {
                Id = dialogId,
                ParticipantOne = new Participant() { Id = dialog.Participant1.Id },
                ParticipantTwo = new Participant() { Id = dialog.Participant2.Id },
                Messages = dialog.Messages.Select(x => new ChatMessageDTO
                {
                    DialogId = x.Dialog.Id,
                    Date = x.Date,
                    IsReaded = x.IsReaded,
                    Message = x.Message,
                    OwnerId = x.Owner.Id
                }).ToList()
            };

            return dl;
        }

        public IEnumerable<ChatDialogDTO> GetAllDialogs(long accountId)
        {
            var dialogs = _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
               .Where(x => x.Participant1.Id == accountId || x.Participant2.Id == accountId)
               .Select(x => new ChatDialogDTO()
               {
                   Id = x.Id,
                   ParticipantOne = new Participant() { Id = x.Participant1.Id },
                   ParticipantTwo = new Participant() { Id = x.Participant2.Id },
               }).ToList();

            return dialogs;
        }

        public async Task RemoveDialog(long dialogId)
        {
            _unitOfWork.ChatDialogRepository.Delete(dialogId);
            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveMessage(long messageId)
        {
            _unitOfWork.ChatMessageRepository.Delete(messageId);
            await _unitOfWork.SaveAsync();
        }

        public Task Update(ChatMessageDTO msg)
        {
            throw new NotImplementedException();
        }
    }
}
