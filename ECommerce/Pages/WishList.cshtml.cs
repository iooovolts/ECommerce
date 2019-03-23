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

namespace ECommerce.Pages
{
    public class WishListModel : PageModel
    {
        private readonly IShopService _shopService;
        private readonly UserManager<IdentityUser> _userManager;
        public List<WishListItem> WishListItems { get; set; }

        public WishListModel(UserManager<IdentityUser> userManager, IShopService shopService)
        {
            _userManager = userManager;
            _shopService = shopService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                WishListItems = _shopService.GetWishListItems(GetCurrentUserAsync().Result.Id);
            }
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
                    _shopService.DeleteWishListItem(Convert.ToInt32(requestBody), GetCurrentUserAsync().Result.Id);
                }
            }    
        }
    }
}