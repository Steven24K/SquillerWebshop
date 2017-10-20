namespace Webshop.Models.DbXtensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;


    using Webshop.Utils.Xtratypes;
    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Utils.Xtensions;

    public static class DbXtensions
    {
        public static void AddCustomer(this WebshopContext db, string name, string surname, Gender gender, string email, string password, string adress, string postal_code, string city)
        {
            db.Customers.Add(
                new Customer
                {
                    Name = name,
                    Surname = surname,
                    Gender = gender,
                    Email = email,
                    Password = password,
                    //Add check if adress allready exists
                    Adress = new Adress{Street =adress, PostalCode = postal_code, City = city}
                }
            );
            db.SaveChanges();
        }
        public static void AddProduct(this WebshopContext db, string name, string description, string category, string brand, double price, Gender gender, Extra extra,int amount)
        {
             db.Products.Add(
                 new Product
                 {
                     Name = name,
                     Description = description,
                     //Add check if category allready exists
                     Category = new Category{Name = category.ToUpper()},
                     //Add check if brand allready exists
                     Brand = new Brand{Name = brand.ToUpper()},
                     Price = price,
                     Gender = gender,
                     Extra = extra,
                     //Add check if inventory allready exists
                     Amount = new Inventory{Amount = amount}
                 }
             );
             db.SaveChanges();
        }
        public static IEnumerable<ProductInfo> BindSelectAllProducts(this WebshopContext db){
            return (from product in db.Products
                   from brand in db.Brands
                   from category in db.Categories
                   from inventory in db.Inventory 
                   where product.Amount.Id == inventory.Id && product.Category.Id == category.Id && product.Brand.Id == brand.Id
                   orderby product.DateAdded descending
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

        public static Product SelectProductById(this WebshopContext db, int id)
        {
            return (from product in db.Products
                    where product.Id == id
                    select product ).FirstOrDefault();
        }

        public static ProductInfo BindSelectProductById(this WebshopContext db, int id){
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
                       Gender = product.Gender,
                       Amount = inventory.Amount,
                       dateTime = product.DateAdded 
                   }).FirstOrDefault();
        }

        public static IEnumerable<ProductInfo> SearchProducts(this WebshopContext db, string keyword)
        {
            keyword = keyword.ToLower();
            return (
                from product in db.Products
                where product.Name.ToLower().Contains(keyword) || product.Description.ToLower().Contains(keyword)
                select new ProductInfo{
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                }
            ).ToList();
        }

        public static IEnumerable<CustomerInfo> BindSelectAllCustomer(this WebshopContext db)
        {
            return (
                from customer in db.Customers
                from adress in db.Adresses
                where customer.Adress.Id == adress.Id
                select new CustomerInfo{
                    Id = customer.Id,
                    Name = customer.Name,
                    Surname = customer.Surname,
                    Gender = customer.Gender,
                    Email = customer.Email,
                    Password = customer.Password,
                    Street = adress.Street,
                    PostalCode = adress.PostalCode,
                    City = adress.City
                }
            ).ToList();
        }
    }
}