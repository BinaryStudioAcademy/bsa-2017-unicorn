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
        public NotificationType Type { get; set; }
        public long SourceItemId { get; set; }
        public DateTime Time { get; set; }

        public bool IsDeleted { get; set; }
    }
}
