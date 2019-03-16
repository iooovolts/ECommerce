using System;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public IdentityUser UserAccount { get; set; }
        public Product Product { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Fufilled { get; set; }
    }
}
