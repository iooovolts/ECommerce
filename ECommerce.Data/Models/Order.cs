using System;
using System.Collections.Generic;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public IdentityUser UserAccount { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
