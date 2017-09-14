using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Calendar:IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public bool WorkOnWeekend { get; set; }

        public bool SeveralTaskPerDay { get; set; }

        public virtual ICollection<ExtraDay> ExtraDayOffs { get; set; }

        public virtual ICollection<ExtraDay> ExtraWorkDays { get; set; }
    }
}