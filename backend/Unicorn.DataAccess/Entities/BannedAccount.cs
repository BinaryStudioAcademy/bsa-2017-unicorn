using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class BannedAccount : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public Account Account { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
