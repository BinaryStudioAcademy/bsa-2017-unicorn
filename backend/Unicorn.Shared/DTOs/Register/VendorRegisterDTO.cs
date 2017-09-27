using System;

namespace Unicorn.Shared.DTOs.Register
{
    public class VendorRegisterDTO
    {
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Provider { get; set; }
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Experience { get; set; }
        public string Position { get; set; }
        public string Speciality { get; set; }
        public LocationDTO Location { get; set; }
    }
}
