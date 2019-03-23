using System.Collections.Generic;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Data.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public ICollection<WishListItem> WishListItems { get; set; }
        public IdentityUser User { get; set; }
    }
}