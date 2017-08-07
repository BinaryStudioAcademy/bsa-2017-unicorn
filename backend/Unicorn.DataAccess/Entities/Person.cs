using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    class Person
    {
        public int Id { get; set; }

        public Account Account { get; set; }

        public DateTime Birthday { get; set; }

        public string Name { get; set; }

        public string SurnameName { get; set; }

        public string MiddleName { get; set; }

        public string Gender { get; set; }

        public string Phone { get; set; }
    }
}
