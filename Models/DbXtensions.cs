namespace Webshop.Models.DbXtensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Webshop.Xtratypes;

    using Webshop.Models;

    public class ProductInfo
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public string Category{get;set;}
        public string Brand{get;set;}
        public double Price{get;set;}
        public Gender Gender{get;set;} 
        public int Amount{get;set;}
        public DateTime dateTime{get;set;}
    }
    public static class DbXtensions
    {
        public static IEnumerable<ProductInfo> SelectAllProducts(this WebshopContext db){
            return (from product in db.Products
                   from brand in db.Brands
                   from category in db.Categories
                   from inventory in db.Inventory 
                   where product.Amount.Id == inventory.Id && product.Category.Id == category.Id && product.Brand.Id == brand.Id
                   select new ProductInfo {
                       Id = product.Id,
                       Name = product.Name,
                       Description = product.Description,
                       Category = category.Name,
                       Brand = brand.Name ,
                       Price = product.Price,
                       Gender = product.Gender,
                       Amount = inventory.Amount,
                       dateTime = product.DateAdded 
                   }).ToList();
        }

        public static IQueryable<Product> SelectProductById(this WebshopContext db, int id){
            return from product in db.Products
                   where product.Id == id
                   select product;
        }
    }
}