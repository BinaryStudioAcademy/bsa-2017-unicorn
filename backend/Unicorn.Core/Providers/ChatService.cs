using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.Interfaces;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Providers
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(ChatDTO msg)
        {
            ChatMessage m = new ChatMessage
            {
                SenderId = msg.SenderId,
                ReceiverId = msg.ReceiverId,
                Message = msg.Message,
                Date = msg.Date
            };

            _unitOfWork.ChatRepository.Create(m);
            await _unitOfWork.SaveAsync();
        }

        public ICollection<ChatDTO> GetChat(long receiverId, long senderId)
        {
            // TODO: async?
            var data = _unitOfWork.ChatRepository.Query.Where(x => x.ReceiverId == receiverId && x.SenderId == senderId);
            if (data.Any())
            {
                var dto = data.Select(x => new ChatDTO
                {
                    SenderId = x.SenderId,
                    ReceiverId = x.ReceiverId,
                    Message = x.Message,
                    Date = x.Date
                }).ToList();

                return dto;
            }
            return null;
        }

        public IEnumerable<ChatDTO> GetChatByDate(long receiverId, long senderId, DateTime dateMin)
        {
            var dataDto = GetChat(receiverId, senderId);
            if (dataDto == null)
            {
                return null;
            }

            return dataDto.Where(x => x.Date >= dateMin);
        }

        public Task Remove(ChatDTO msg)
        {            
            throw new NotImplementedException();
        }

        public Task Update(ChatDTO msg)
        {
            throw new NotImplementedException();
        }
    }
}
