using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.DTOs;
using Unicorn.Shared.DTOs.Book;
using Unicorn.Shared.DTOs.Chat;
using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetAllAsync();
        Task<NotificationDTO> GetByIdAsync(long id);
        Task<IEnumerable<NotificationDTO>> GetByAccountIdAsync(long id);
        Task CreateAsync(long accountId, NotificationDTO notificationDto);
        Task RemoveAsync(long id);
        Task UpdateAsync(NotificationDTO notificationDto);
        Task CreateAsync<T>(long receiverId, NotificationDTO notification, T payload);
        Task CreateAsync(long accountId, long dialogId);
        Task CreateAsync(long accountId, VendorBookDTO book);
        Task CreateAsync(long accountId, ReportDTO report);
        Task CreateDelAsync(long accountId, long dialogId);
    }
}
