using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Data.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        public WishList Wishlist { get; set; }
        public Product Product { get; set; }
    }
}
