using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs.Chat;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public ChatService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }
        

        private async Task<string> GetParticipant(Role role, long ownerId)
        {
            if (role.Type == RoleType.Company)
            {
                var participant = await _unitOfWork.CompanyRepository
                    .Query.SingleAsync(x => x.Account.Id == ownerId);
                return participant.Name + "(company)";
            }
            else if (role.Type == RoleType.Vendor || role.Type == RoleType.Customer)
            {
                var participant = await _unitOfWork.PersonRepository
                    .Query.SingleAsync(x => x.Account.Id == ownerId);

                return participant.Name + " " + participant.Surname + "(person)";
            }
            else
            {
                var participantPerson = await _unitOfWork.PersonRepository.Query
                    .Include(p => p.Account).FirstOrDefaultAsync(p => p.Account.Id == ownerId);
                if (participantPerson == null)
                {
                    var participantCompany = await _unitOfWork.CompanyRepository.Query
                        .Include(p => p.Account).SingleAsync(p => p.Account.Id == ownerId);

                    return participantCompany.Name + "(company)";

                }

                return participantPerson.Name + " " + participantPerson.Surname + "(person)";

            }
        }


        public async Task CreateMessage(ChatMessageDTO msg)
        {
            var dialog = await _unitOfWork.ChatDialogRepository.GetByIdAsync(msg.DialogId);
            var owner = await _unitOfWork.AccountRepository.GetByIdAsync(msg.OwnerId);
            var participant = await GetParticipant(owner.Role, owner.Id);

            ChatMessage cmsg = new ChatMessage
            {
                IsReaded = false,
                Date = DateTime.Now,
                Dialog = dialog,
                Owner = owner,
                Message = msg.Message
            };

            _unitOfWork.ChatMessageRepository.Create(cmsg);
            await _unitOfWork.SaveAsync();


            var notification = new NotificationDTO()
            {
                Title = $"New message from {participant}",
                Description = $"{participant} sent you a message. Check your messages to read it.",
                SourceItemId = cmsg.Id,
                Time = DateTime.Now,
                Type = NotificationType.ChatNotification
            };

            long receiverId;
            if (dialog.Participant1.Id != owner.Id)
            {
                receiverId = dialog.Participant1.Id;
            }
            else
            {
                receiverId = dialog.Participant2.Id;
            }
            await _notificationService.CreateAsync(receiverId, notification, msg);
        }

        public async Task UpdateNotReadedMessage(long dialogId, long ownerId)
        {
            var dialog = await _unitOfWork.ChatDialogRepository.GetByIdAsync(dialogId);
            dialog.Messages.Where(x => !x.IsReaded && x.Owner.Id != ownerId).ForEach(mes => mes.IsReaded = true);
            _unitOfWork.ChatDialogRepository.Update(dialog);

            long receiverId;
            if (dialog.Participant1.Id != ownerId)
            {
                receiverId = dialog.Participant1.Id;
            }
            else
            {
                receiverId = dialog.Participant2.Id;
            }
            await _notificationService.CreateAsync(receiverId, dialogId);
            await _unitOfWork.SaveAsync();
        }


        public async Task<ChatDialogDTO> CreateDialog(ChatDialogDTO dialog)
        {
            var participant1 = await _unitOfWork.AccountRepository.GetByIdAsync(dialog.ParticipantOneId);
            var participant2 = await _unitOfWork.AccountRepository.GetByIdAsync(dialog.ParticipantTwoId);

            var dl = new ChatDialog
            {
                Participant1 = participant1,
                Participant2 = participant2,
                Messages = new List<ChatMessage>()
            };

            var createdDialog = _unitOfWork.ChatDialogRepository.Create(dl);
            await _unitOfWork.SaveAsync();

            return new ChatDialogDTO
            {
                Id = createdDialog.Id,
                ParticipantOneId = createdDialog.Participant1.Id,
                ParticipantTwoId = createdDialog.Participant2.Id,
                ParticipantAvatar = createdDialog.Participant2.Avatar,
                LastMessageTime = DateTimeOffset.Now
            };
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
                ParticipantOneId = dialog.Participant1.Id,
                ParticipantTwoId = dialog.Participant2.Id,
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

        public async Task<ChatDialogDTO> GetDialog(long dialogId, long ownerId)
        {
            var dialog = await _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Messages)
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
                .SingleAsync(x => x.Id == dialogId);

            string avatar;
            string name;

            if (dialog.Participant1.Id == ownerId)
            {
                avatar = dialog.Participant2.Avatar;
                name = GetName(dialog.Participant2);
            }
            else
            {
                avatar = dialog.Participant1.Avatar;
                name = GetName(dialog.Participant1);
            }

            ChatDialogDTO dl = new ChatDialogDTO
            {
                Id = dialogId,
                ParticipantOneId = dialog.Participant1.Id,
                ParticipantTwoId = dialog.Participant2.Id,
                ParticipantName = name,
                ParticipantAvatar = avatar,
                IsReadedLastMessage = false,
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



        public async Task<IEnumerable<ChatDialogDTO>> GetAllDialogs(long accountId)
        {
            var dialogs = await _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
                .Include(x => x.Messages)
               .Where(x => x.Participant1.Id == accountId || x.Participant2.Id == accountId)
               .ToListAsync();

            if (dialogs.Count == 0)
            {
                return null;
            }

            var names = dialogs.SelectMany(x => new List<Account> {x.Participant1, x.Participant2})
                .Where(x => x.Id != accountId)
                .Select(GetName).ToList();
            var avatars = dialogs.SelectMany(x => new List<Account> {x.Participant1, x.Participant2})
                .Where(x => x.Id != accountId)
                .Select(x => x.Avatar).ToList();
            int i = 0;

            var result = dialogs.Select(x => new ChatDialogDTO()
            {
                Id = x.Id,
                ParticipantOneId = x.Participant1.Id,
                ParticipantTwoId = x.Participant2.Id,
                ParticipantName = names[i],
                ParticipantAvatar = avatars[i++],
                IsReadedLastMessage = x.Messages?.Where(y => y.Owner.Id != accountId).LastOrDefault()?.IsReaded ?? true,
                LastMessageTime = x.Messages?.LastOrDefault()?.Date
            }).ToList();

            return result;
        }

        public async Task<ChatDialogDTO> FindDialog(long participantOneId, long participantTwoId)
        {
            var res = await _unitOfWork.ChatDialogRepository.Query
                .Include(x => x.Participant1)
                .Include(x => x.Participant2)
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(
                    x => (x.Participant1.Id == participantOneId && x.Participant2.Id == participantTwoId) ||
                         (x.Participant1.Id == participantTwoId && x.Participant2.Id == participantOneId));

            if (res == null)
            {
                return null;
            }

            string avatar;
            long ownerId;

            if (res.Participant1.Id == participantOneId)
            {
                avatar = res.Participant2.Avatar;
                ownerId = res.Participant1.Id;
            }
            else
            {
                avatar = res.Participant1.Avatar;
                ownerId = res.Participant2.Id;
            }
            return new ChatDialogDTO
            {
                Id = res.Id,
                ParticipantOneId = res.Participant1.Id,
                ParticipantTwoId = res.Participant2.Id,
                ParticipantAvatar = avatar,
                IsReadedLastMessage = res.Messages?.Where(y => y.Owner.Id != ownerId).LastOrDefault()?.IsReaded ?? true,
                Messages = res.Messages.Select(x => new ChatMessageDTO
                {
                    DialogId = x.Dialog.Id,
                    Date = x.Date,
                    IsReaded = x.IsReaded,
                    Message = x.Message,
                    OwnerId = x.Owner.Id
                }).ToList()
            };

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

        private string GetName(Account acc)
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
    }
}
