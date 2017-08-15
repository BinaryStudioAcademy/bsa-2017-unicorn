using System;

namespace Unicorn.Shared.DTOs
{
    public class PersonDTO
    {
        public long Id { get; set; }

        public DateTime Birthday { get; set; }

        public string Name { get; set; }

        public string SurnameName { get; set; }

        public string MiddleName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }


        public virtual AccountDTO Account { get; set; }

        public virtual LocationDTO Location { get; set; }
    }
}
