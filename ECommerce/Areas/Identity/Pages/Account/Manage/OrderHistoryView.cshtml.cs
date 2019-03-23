using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Data.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerce.Pages
{
    public class OrderHistoryViewModel : PageModel
    {
        private readonly IShopService _shopService;
        public List<OrderProduct> OrderProducts { get; set; }

        public OrderHistoryViewModel( IShopService shopService)
        {
            _shopService = shopService;
        }
        public void OnGet(int orderId)
        {
            OrderProducts = _shopService.GetOrderProducts(orderId);
        }
    }
}