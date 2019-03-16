using System;
using System.Collections.Generic;
using System.Linq;
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
        public void OnGet()
        {
            ShoppingBagItems = _shopService.GetShoppingBagItems(GetCurrentUserAsync().Result.Id);
        }

        public void OnPostDelete(int id)
        {
            _shopService.DeleteShoppingBagItem(id, GetCurrentUserAsync().Result.Id);
        }
    }
}