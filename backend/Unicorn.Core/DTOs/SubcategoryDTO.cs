using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class SubcategoryDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public virtual CategoryDTO Category { get; set; }

        public ICollection<WorkDTO> Works { get; set; }
    }
}
