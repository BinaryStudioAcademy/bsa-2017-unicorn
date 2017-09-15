using System;
using System.Collections.Generic;
using Unicorn.DataAccess.Entities.Enum;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Report: IEntity
    {
        public long Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Date { get; set; }

        public ReportType Type { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public long? ProfileId { get; set; }

        public string ProfileName { get; set; }

        public string ProfileType { get; set; }

    }
}
