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
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.Shared.DTOs.Chat;
using Unicorn.Shared.DTOs.Book;
using Unicorn.Shared.DTOs;

namespace Unicorn.Core.Services
{
    public class NotificationService : INotificationService
    {
        public NotificationService(IUnitOfWork unitOfWork, INotificationProxy proxy)
        {
            _unitOfWork = unitOfWork;
            _proxy = proxy;
        }

        private Notification CreateNotification(long accountId, NotificationDTO notificationDto)
        {
            return new Notification()
            {
                AccountId = accountId,
                IsDeleted = false,
                SourceItemId = notificationDto.SourceItemId,
                Title = notificationDto.Title,
                Description = notificationDto.Description,
                Time = notificationDto.Time,
                Type = notificationDto.Type,
                IsViewed = false
            };
        }

        public async Task CreateAsync(long accountId, NotificationDTO notificationDto)
        {
            var notification = CreateNotification(accountId, notificationDto);

            _unitOfWork.NotificationRepository.Query
                .Where(n => !n.IsDeleted && !n.IsViewed && n.Type == NotificationType.ChatNotification)
                .ToList()
                .ForEach(n => 
                {
                    n.IsViewed = true;
                    _unitOfWork.NotificationRepository.Update(n);
                });

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveAsync();

            switch (notification.Type)
            {
                case NotificationType.TaskNotification:
                    await _proxy.RefreshOrdersForAccount(accountId);
                    break;
            }

            notificationDto.Id = notification.Id;
            await _proxy.SendNotification(accountId, notificationDto);
        }

        public async Task CreateAsync<T>(long accountId, NotificationDTO notificationDto, T payload)
        {
            var notification = CreateNotification(accountId, notificationDto);

            _unitOfWork.NotificationRepository.Create(notification);
            await _unitOfWork.SaveAsync();

            switch (notification.Type)
            {
                case NotificationType.ChatNotification:
                    await _proxy.RefreshMessagesForAccount(accountId, payload);
                    break;
            }

            notificationDto.Id = notification.Id;
            await _proxy.SendNotification(accountId, notificationDto);
        }

        public async Task CreateAsync(long accountId, long dialogId)
        {
            await _proxy.ReadNotReadedMessages(accountId, dialogId);
        }

        public async Task CreateAsync(long accountId, VendorBookDTO book)
        {
            await _proxy.RefreshCalendarsEvents(accountId, book);
        }

        public async Task CreateAsync(long accountId, ReportDTO report)
        {
            await _proxy.RefreshAdminFeedbacks(accountId, report);
        }

        public async Task CreateDelAsync(long accountId, long dialogId)
        {
            await _proxy.DeleteMessage(accountId, dialogId);
        }

        public async Task<IEnumerable<NotificationDTO>> GetAllAsync()
        {
            var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();
            return notifications.Select(n => new NotificationDTO()
            {
                Id = n.Id,
                Title = n.Title,
                Description = n.Description,
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
                Title = notification.Title,
                Description = notification.Description,
                SourceItemId = notification.SourceItemId,
                IsViewed = notification.IsViewed, 
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
                    Title = n.Title,
                    Description = n.Description,
                    SourceItemId = n.SourceItemId,
                    Time = n.Time,
                    Type = n.Type,
                    IsViewed = n.IsViewed
                }).ToList();
        }

        public async Task UpdateAsync(NotificationDTO notificationDto)
        {
            var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(notificationDto.Id);

            notification.IsViewed = notificationDto.IsViewed;

            _unitOfWork.NotificationRepository.Update(notification);
            await _unitOfWork.SaveAsync();
        }

        private readonly IUnitOfWork _unitOfWork;
        private INotificationProxy _proxy;
    }
}
