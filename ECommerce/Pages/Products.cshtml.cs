using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ECommerce.Pages
{
    public class ShopTypeModel : PageModel
    {
        public List<Product> Products;
        public string Identity { get; set; }
        private readonly IShopService _shopService;

        public ShopTypeModel(IShopService shopService)
        {
            _shopService = shopService;
        }

        public void OnGet(string product, string identity)
        {
            switch (identity)
            {
                case "Mens":
                    Products = _shopService.GetMensProducts(product);
                    break;
                case "Womens":
                    Products = _shopService.GetWomensProducts(product);
                    break;
            }
            Identity = identity;
        }
    }
}