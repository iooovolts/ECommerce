using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.Models;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

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
        public void OnGet()
        {
            ShoppingBagItems = _shopService.GetShoppingBagItems(GetCurrentUserAsync().Result.Id);
        }

        public JsonResult OnPostDelete()
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
                    return new JsonResult(_shopService.GetShoppingBagItems(GetCurrentUserAsync().Result.Id));
                }
            }
            return null;
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
                    _shopService.CreateOrder(Convert.ToInt32(requestBody), ShoppingBagItems, GetCurrentUserAsync().Result);
                }
            }
        }
    }
}