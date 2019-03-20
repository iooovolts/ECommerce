using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Models;

namespace ECommerce.Data.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public virtual Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
