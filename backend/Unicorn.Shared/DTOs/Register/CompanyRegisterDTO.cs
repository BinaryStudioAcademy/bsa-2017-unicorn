using System;

namespace Unicorn.Shared.DTOs.Register
{
    public class CompanyRegisterDTO
    {
        public DateTime Foundation { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Provider { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public int Staff { get; set; }
        public string Description { get; set; }
        public LocationDTO Location { get; set; }
    }
}
