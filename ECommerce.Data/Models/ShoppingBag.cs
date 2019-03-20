using System;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Models
{
    public class ShoppingBag
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public DateTime DateCreated { get; set; }
        public bool CheckedOut { get; set; }
    }
}
