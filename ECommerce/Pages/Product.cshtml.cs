using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

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
        public void OnGet(int id, string identity)
        {
            Product = _shopService.GetProduct(id);
            Identity = identity;
        }

        public void OnPost(int id)
        {
            _shopService.AddProductToBag(_shopService.GetProduct(id), GetCurrentUserAsync().Result);
        }
    }
}
