using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Core.DTOs
{
    public class LocationDTO
    {
        public long Id { get; set; }

        public string City { get; set; }

        public string Adress { get; set; }

        public string PostIndex { get; set; }

        public double CoordinateX { get; set; }

        public double CoordinateY { get; set; }
    }
}
