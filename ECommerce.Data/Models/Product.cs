using System;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public double? SalePrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public GenderCategory GenderCategory { get; set; }
        public string Image { get; set; }
        public int? Stock { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
