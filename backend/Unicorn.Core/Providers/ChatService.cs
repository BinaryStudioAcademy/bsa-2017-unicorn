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
                ParticipantOne = new Participant() { Id = dialog.Participant1.Id, Name = GetName(dialog.Participant1) },
                ParticipantTwo = new Participant() { Id = dialog.Participant2.Id, Name = GetName(dialog.Participant2) },
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
            string GetName(Account acc)
            {
                string name = null;

                switch (acc.Role.Type)
                {
                    case RoleType.Customer:
                        var customer = _unitOfWork.CustomerRepository.Query.First(x => x.Person.Account.Id == acc.Id).Person;
                        name = $"{customer.Name} {customer.Surname}";
                        break;
                    case RoleType.Company:
                        name = _unitOfWork.CompanyRepository.Query.First(x => x.Account.Id == acc.Id).Name;
                        break;
                    case RoleType.Vendor:
                        var vendor = _unitOfWork.VendorRepository.Query.First(x => x.Person.Account.Id == acc.Id).Person;
                        name = $"{vendor.Name} {vendor.Surname}";
                        break;
                }

                return name;
            }

            var dialogs = _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
               .Where(x => x.Participant1.Id == accountId || x.Participant2.Id == accountId)               
               .Select(x => new ChatDialogDTO()
               {
                   Id = x.Id,
                   ParticipantOne = new Participant() { Id = x.Participant1.Id, Name = GetName(x.Participant1) },
                   ParticipantTwo = new Participant() { Id = x.Participant2.Id, Name = GetName(x.Participant2) },
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
