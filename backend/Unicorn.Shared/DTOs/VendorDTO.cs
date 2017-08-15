﻿using System.Collections.Generic;

namespace Unicorn.Core.DTOs
{
    public class VendorDTO
    {
        public long Id { get; set; }

        public double Experience { get; set; }

        public string ExWork { get; set; }

        public string Position { get; set; }


        public CompanyDTO Company { get; set; }

        public PersonDTO Person { get; set; }

        public ICollection<WorkDTO> Works { get; set; }
    }
}
