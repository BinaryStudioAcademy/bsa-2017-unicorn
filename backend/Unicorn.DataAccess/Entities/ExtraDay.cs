using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class ExtraDay:IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset Day { get; set; }

        public bool DayOff { get; set; }

        public virtual Calendar Calendar { get; set; }
    }
}