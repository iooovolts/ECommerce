using ECommerce.Data;
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
    }
    public class ShopService : IShopService
    {
        private readonly ApplicationDbContext _context;
        public ShopService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingBagItem GetShoppingBagItem(int productId, string userId)
        {
            var query = (from c in _context.ShoppingBagItems
                         where c.ShoppingBag.User.Id == userId && c.Product.Id == productId
                         select c).Single();
            return query;
        }
        public List<ShoppingBagItem> GetShoppingBagItems(string userId)
        {
            return (from c in _context.ShoppingBagItems.Include(c => c.Product)
                    where c.ShoppingBag.User.Id == userId
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
                    ShoppingBag = GetShoppingBagId(user)
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

        /// <summary>
        /// Checks whether a product is already in the users bag
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private bool IsAlreadyInBag(Product product)
        {
            var query = from c in _context.ShoppingBagItems
                        where c.Product.Id == product.Id
                        select c;
            if (query.Any())
                return true;
            return false;
        }

        private ShoppingBag GetShoppingBagId(IdentityUser user)
        {
            return (from c in _context.ShoppingBags
                    where c.CheckedOut == false && c.User.Id == user.Id
                    select c).Single();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
