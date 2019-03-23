using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Data.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Areas.Identity.Pages.Account.Manage
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IShopService _shopService;
        private readonly UserManager<IdentityUser> _userManager;
        public List<Order> Orders { get; set; }

        public OrderHistoryModel(UserManager<IdentityUser> userManager, IShopService shopService)
        {
            _userManager = userManager;
            _shopService = shopService;
        }
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public void OnGet()
        {
            Orders = _shopService.GetOrders(GetCurrentUserAsync().Result.Id);
        }
    }
}