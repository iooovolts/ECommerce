using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Pages
{
    [BindProperties]
    public class ShopModel : PageModel
    {
        public string Identity { get; set; }

        public List<Product> Products;
        public void OnGet(string identity)
        {
            switch (identity)
            {
                case "Mens":
                    //get men page
                    Identity = identity;
                    break;
                case "Womens":
                    //get womens page
                    Identity = identity;
                    break;
            }
        }
    }
}