using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Entities.Enum;

namespace Unicorn.Shared.DTOs.Notification
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public NotificationType Type { get; set; }
        public long SourceItemId { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
