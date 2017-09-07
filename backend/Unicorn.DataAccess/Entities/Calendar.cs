using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Calendar:IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool WorkOnWeekend { get; set; }

        public virtual ICollection<ExtraDay> ExtraDayOffs { get; set; }

        public virtual ICollection<ExtraDay> ExtraWorkDays { get; set; }
    }
}