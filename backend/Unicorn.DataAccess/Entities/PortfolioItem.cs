using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.DataAccess.Entities
{
    public class PortfolioItem
    {
        public long Id { get; set; }
        public string Image { get; set; }
        public History HistoryEntry { get; set; }
        public bool IsDeleted { get; set; }
    }
}
