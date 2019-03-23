using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Pages
{
    [BindProperties]
    public class ShoppingBagModel : PageModel
    {
        private readonly IShopService _shopService;
        private readonly UserManager<IdentityUser> _userManager;
        public List<ShoppingBagItem> ShoppingBagItems { get; set; }

        public ShoppingBagModel(UserManager<IdentityUser> userManager, IShopService shopService)
        {
            _userManager = userManager;
            _shopService = shopService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
                ShoppingBagItems = _shopService.GetShoppingBagItems(GetCurrentUserAsync().Result.Id);
            else
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            return Page();
        }

        public void OnPostDelete()
        {
            var stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                var requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    _shopService.DeleteShoppingBagItem(Convert.ToInt32(requestBody), GetCurrentUserAsync().Result.Id);
                }
            }
        }

        public void OnPostCheckout()
        {
            var stream = new MemoryStream();
            Request.Body.CopyTo(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                var requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    _shopService.CreateOrder(Convert.ToDouble(requestBody), GetCurrentUserAsync().Result);
                }
            }
        }

        public void OnPostWishList()
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
        }
    }
}