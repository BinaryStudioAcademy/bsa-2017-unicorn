﻿using System;
using System.Collections.Generic;

namespace Unicorn.Shared.DTOs.User
{
    public class UserShortDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string SurName { get; set; }
        public DateTime Birthday { get; set; }
        public string Avatar { get; set; }
        public long LocationId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<BookShortDto> Books { get; set; }
    }
}