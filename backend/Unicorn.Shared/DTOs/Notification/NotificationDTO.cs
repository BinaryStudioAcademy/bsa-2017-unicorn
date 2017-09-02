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
        public DateTimeOffset Time { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsViewed { get; set; }
    }
}
