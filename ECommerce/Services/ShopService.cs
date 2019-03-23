using ECommerce.Data;
using ECommerce.Data.Models;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Services
{
    public interface IShopService
    {
        List<ShoppingBagItem> GetShoppingBagItems(string userId);
        List<Product> GetMensProducts(string product);
        List<Product> GetWomensProducts(string product);
        Product GetProduct(int id);
        void AddProductToBag(Product product, IdentityUser user);
        void DeleteShoppingBagItem(int productId, string userId);
        void DeleteWishListItem(int productId, string userId);
        void CreateOrder(double totalAmount, IdentityUser user);
        List<Order> GetOrders(string userId);
        List<OrderProduct> GetOrderProducts(int orderId);
        void AddProductToWishList(int productId, IdentityUser user);
        List<WishListItem> GetWishListItems(string userId);
    }
    public class ShopService : IShopService
    {
        private readonly ApplicationDbContext _context;
        public ShopService(ApplicationDbContext context)
        {
            _context = context;
        }

        private ShoppingBagItem GetShoppingBagItem(int productId, string userId)
        {
            return (from c in _context.ShoppingBagItems
                    where c.ShoppingBag.User.Id == userId && c.Product.Id == productId
                    select c).Single();

        }

        private WishListItem GetWishListItem(int productId, string userId)
        {
            return (from c in _context.WishListItems
                    where c.Wishlist.User.Id == userId && c.Product.Id == productId
                    select c).Single();
        }

        public List<ShoppingBagItem> GetShoppingBagItems(string userId)
        {
            return (from c in _context.ShoppingBagItems.Include(c => c.Product)
                    where c.ShoppingBag.User.Id == userId && c.ShoppingBag.CheckedOut == false
                    select c).ToList();
        }

        public List<Product> GetMensProducts(string product)
        {
            return (from c in _context.Products
                    where c.GenderCategory.Id == 1 && c.ProductCategory.Name == product
                    select c).ToList();
        }

        public List<Product> GetWomensProducts(string product)
        {
            return (from c in _context.Products
                    where c.GenderCategory.Id == 2 && c.ProductCategory.Name == product
                    select c).ToList();
        }

        public Product GetProduct(int id)
        {
            return (from c in _context.Products
                    where c.Id == id
                    select c).Single();
        }

        public List<Order> GetOrders(string userId)
        {
            return (from c in _context.Orders
                    where c.UserAccount.Id == userId
                    select c).ToList();
        }

        public List<OrderProduct> GetOrderProducts(int orderId)
        {
            return (from c in _context.OrderProducts.Include(c => c.Product)
                    where c.Order.Id == orderId
                    select c).ToList();
        }

        public void AddProductToWishList(int productId, IdentityUser user)
        {
            if (!WishListExists(user.Id))
                CreateWishList(user);

            var wishList = GetWishList(user.Id);
            var wishListItem = new WishListItem
            {
                Product = GetProduct(productId),
                Wishlist = wishList
            };

            _context.Add(wishListItem);
            SaveChanges();
        }

        public List<WishListItem> GetWishListItems(string userId)
        {
            return (from c in _context.WishListItems.Include(c => c.Product)
                    where c.Wishlist.User.Id == userId
                    select c).ToList();
        }

        private void CreateWishList(IdentityUser user)
        {
            var wishList = new WishList
            {
                User = user
            };
            _context.Add(wishList);
            SaveChanges();
        }

        private bool WishListExists(string userId)
        {
            var query = from c in _context.WishLists
                        where c.User.Id == userId
                        select c;
            if (query.Any())
                return true;
            return false;
        }

        private WishList GetWishList(string userId)
        {
            return (from c in _context.WishLists
                    where c.User.Id == userId
                    select c).Single();
        }

        public void AddProductToBag(Product product, IdentityUser user)
        {
            if (IsBackCheckedOut(user.Id))
                CreateBag(user);

            if (!IsAlreadyInBag(product))
            {
                var cartItem = new ShoppingBagItem
                {
                    Product = GetProduct(product.Id),
                    Price = product.Price,
                    Quantity = 1,
                    ShoppingBag = GetShoppingBag(user.Id)
                };
                _context.ShoppingBagItems.Add(cartItem);
                SaveChanges();
            }
            else
            {
                var temp = GetShoppingBagItem(product.Id, user.Id);
                temp.Quantity++;
                temp.Price = product.Price;
                _context.Update(temp);
                SaveChanges();
            }
        }

        public void DeleteShoppingBagItem(int productId, string userId)
        {
            _context.ShoppingBagItems.Remove(GetShoppingBagItem(productId, userId));
            SaveChanges();
        }

        public void DeleteWishListItem(int productId, string userId)
        {
            _context.WishListItems.Remove(GetWishListItem(productId, userId));
            SaveChanges();
        }

        public void CreateOrder(double totalAmount, IdentityUser user)
        {
            var order = new Order
            {
                TotalAmount = totalAmount,
                UserAccount = user,
                OrderDate = DateTime.Now
            };

            var orderProducts = new List<OrderProduct>();
            foreach (var item in GetShoppingBagItems(user.Id))
            {
                orderProducts.Add(new OrderProduct
                {
                    Quantity = item.Quantity,
                    Order = order,
                    Product = item.Product
                });
            }

            order.OrderProduct = orderProducts;
            _context.Orders.Add(order);
            CheckOutShoppingBag(user.Id);
            SaveChanges();
        }

        public void CheckOutShoppingBag(string userId)
        {
            var shoppingBag = GetShoppingBag(userId);
            shoppingBag.CheckedOut = true;
            _context.Update(shoppingBag);
        }

        public void CreateBag(IdentityUser user)
        {
            var cart = new ShoppingBag
            {
                DateCreated = DateTime.Now,
                User = user
            };
            _context.ShoppingBags.Add(cart);
            SaveChanges();
        }

        private bool IsBackCheckedOut(string userId)
        {
            var query = from c in _context.ShoppingBags
                        where c.CheckedOut == false && c.User.Id == userId
                        select c;
            if (query.Any())
                return false;
            return true;
        }

        private bool IsAlreadyInBag(Product product)
        {
            var query = from c in _context.ShoppingBagItems
                        where c.Product.Id == product.Id
                        select c;
            if (query.Any())
                return true;
            return false;
        }

        private ShoppingBag GetShoppingBag(string userId)
        {
            return (from c in _context.ShoppingBags
                    where c.CheckedOut == false && c.User.Id == userId
                    select c).Single();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
