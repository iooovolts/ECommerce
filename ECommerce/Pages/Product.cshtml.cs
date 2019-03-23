using System;
using System.IO;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerce.Pages
{
    [BindProperties]
    public class ProductModel : PageModel
    {
        public string Identity { get; set; }
        public Product Product { get; set; }
        private readonly IShopService _shopService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProductModel(UserManager<IdentityUser> userManager, IShopService shopService)
        {
            _userManager = userManager;
            _shopService = shopService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public void OnGet(int id)
        {
            Product = _shopService.GetProduct(id);
        }

        public void OnPost(int id)
        {
            var stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                var requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    _shopService.AddProductToBag(_shopService.GetProduct(Convert.ToInt32(requestBody)), GetCurrentUserAsync().Result);
                }
            }
        }

        public IActionResult OnPostWishList()
        {
            if (User.Identity.IsAuthenticated)
            {
                var stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    var requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        _shopService.AddProductToWishList(Convert.ToInt32(requestBody), GetCurrentUserAsync().Result);
                    }
                }
                return Page();
            }
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
