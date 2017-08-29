using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;

using Unicorn.Core.Infrastructure.SignalR;
using Unicorn.Core.Interfaces;
using Unicorn.Shared.DTOs.Notification;
using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.Core.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork, INotificationProxy proxy)
        {
            _unitOfWork = unitOfWork;
            _proxy = proxy;
        }

        public async Task CreateAsync(long accountId, NotificationDTO notificationDto)
        {
            var notification = new Notification()
            {
                AccountId = accountId,
                IsDeleted = false,
                SourceItemId = notificationDto.SourceItemId,
                Message = notificationDto.Message,
                Time = notificationDto.Time,
                Type = notificationDto.Type
            };

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveAsync();

            notificationDto.Id = notification.Id;
            await _proxy.SendNotification(accountId, notificationDto);
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllAsync()
        {
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
            return notifications.Select(n => new NotificationDTO()
            {
                Id = n.Id,
                Message = n.Message,
                SourceItemId = n.SourceItemId,
                Time = n.Time,
                Type = n.Type
            }).ToList();
        }

        public async Task<NotificationDTO> GetByIdAsync(long id)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(id);

            return new NotificationDTO()
            {
                Id = notification.Id,
                Message = notification.Message,
                SourceItemId = notification.SourceItemId,
                Time = notification.Time,
                Type = notification.Type
            };
        }

        public async Task RemoveAsync(long id)
        {
            _unitOfWork.NotificationRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<NotificationDTO>> GetByAccountIdAsync(long id)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();

            return notifications
                .Where(n => n.AccountId == id)
                .Select(n => new NotificationDTO()
            {
                Id = n.Id,
                Message = n.Message,
                SourceItemId = n.SourceItemId,
                Time = n.Time,
                Type = n.Type
            }).ToList();
        }

        private readonly IUnitOfWork _unitOfWork;
        private INotificationProxy _proxy;
    }
}
