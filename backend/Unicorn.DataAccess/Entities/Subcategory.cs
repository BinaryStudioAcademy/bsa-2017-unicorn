using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    public class Subcategoty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category ParentCategory { get; set; }
    }
}
