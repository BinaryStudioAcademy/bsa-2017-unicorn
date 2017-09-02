using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Notification : IEntity
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long SourceItemId { get; set; }
        public NotificationType Type { get; set; }
        public DateTimeOffset Time { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public bool IsViewed { get; set; }

        public bool IsDeleted { get; set; }
    }
}
