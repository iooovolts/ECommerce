﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
