using System;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Person : IEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime Birthday { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }


        public virtual Account Account { get; set; }

        public virtual Location Location { get; set; }
    }
}
