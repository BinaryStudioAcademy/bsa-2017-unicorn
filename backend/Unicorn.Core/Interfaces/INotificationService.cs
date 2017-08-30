using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.Shared.DTOs.Notification;

namespace Unicorn.Core.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDTO>> GetAllAsync();
        Task<NotificationDTO> GetByIdAsync(long id);
        Task<IEnumerable<NotificationDTO>> GetByAccountIdAsync(long id);
        Task CreateAsync(long acoountId, NotificationDTO notificationDto);
        Task RemoveAsync(long id);
        Task UpdateAsync(NotificationDTO notificationDto);
    }
}
