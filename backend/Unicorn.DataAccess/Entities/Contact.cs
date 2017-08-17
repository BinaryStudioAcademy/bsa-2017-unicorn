using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Contact : IEntity
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsDeleted { get; set; }
    }
}
