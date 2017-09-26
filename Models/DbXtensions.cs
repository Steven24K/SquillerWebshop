namespace Webshop.Models.DbXtensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Webshop.Xtratypes;
    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Xtensions;

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
                       Gender = product.Gender.GenderToString(),
                       Amount = inventory.Amount,
                       dateTime = product.DateAdded 
                   }).ToList();
        }

        public static ProductInfo SelectProductById(this WebshopContext db, int id){
            return (from product in db.Products
                    from brand in db.Brands
                   from category in db.Categories
                   from inventory in db.Inventory 
                   where product.Id == id && product.Amount.Id == inventory.Id && product.Category.Id == category.Id && product.Brand.Id == brand.Id
                   select new ProductInfo{
                       Id = product.Id,
                       Name = product.Name,
                       Description = product.Description,
                       Category = category.Name,
                       Brand = brand.Name ,
                       Price = product.Price,
                       Gender = product.Gender.GenderToString(),
                       Amount = inventory.Amount,
                       dateTime = product.DateAdded 
                   }).FirstOrDefault();
        }
    }
}