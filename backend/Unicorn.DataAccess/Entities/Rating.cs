﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.DataAccess.Interfaces;

namespace Unicorn.DataAccess.Entities
{
    public class Rating : IEntity
    {
        public long Id { get; set; }
        
        public bool IsDeleted { get; set; }

        public Account Reciever { get; set; }

        public Account Sender { get; set; }

        public int Grade { get; set; }

    }
}