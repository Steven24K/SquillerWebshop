using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Collections.Generic;


namespace SquillerWebshop.Models
{
    //This class represents the database, it contains al tables, models and relations. 
    //This class is also initiated when you want to open a connection.
    public class WebshopContext : DbContext
    {
        public DbSet<ShoppingCard> shoppingCard{get;set;}
        public DbSet<Customer> Customer{get;set;}
        public DbSet<Product> Product{get;set;}
        public DbSet<Brand> Brands{get;set;}
        public DbSet<Category> Categories{get;set;}
        public DbSet<Inventory> Inventory{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseNpgsql("User ID=postgres;Password=mydatabase;Host=localhost;Port=5432;Database=WebshopDB;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<ShoppingCard>().HasKey(k => new{k.CustomerId,k.ProductId});
        modelBuilder.Entity<ShoppingCard>().HasOne(sc => sc.Customer).WithMany(c => c.Products).HasForeignKey(sc => sc.CustomerId);
        modelBuilder.Entity<ShoppingCard>().HasOne(sc => sc.Product).WithMany(p => p.Customers).HasForeignKey(sc => sc.ProductId);
        }
    }

      
    public class ShoppingCard
    {
        public int CustomerId{get;set;}
        public Customer Customer{get;set;}
        public int ProductId{get;set;}
        public Product Product{get;set;}
    }

    public class Administrator
    {
        public int Id{get;set;}
        public string UserName{get;set;}
        public string Password{get;set;}
        public string Email{get;set;}
        public DateTime RegistrationDate{get;set;} = DateTime.Now;
    }

    public class Customer
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Surname{get;set;}
        public Gender Gender{get;set;}
        public string Email{get;set;}
        public string Password{get;set;}//Don's know of this is the safest way to store a password
        public string Adress{get;set;}
        public string PostalCode{get; set;}
        public string City{get;set;}
        public DateTime RegistrationDate{get;set;}=DateTime.Now;
        public List<ShoppingCard> Products{get;set;}//This represents the customers shoppingcard, contains a list of products 
    }

    public class Product
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public Category Category{get;set;}//Like pants, shirts, watches, shoes etc.
        public Brand Brand{get;set;}//Like Nike, Gucci, Rolex etc. This a table Brand
        public double Price{get;set;}
        public Gender Gender{get;set;}//Self defined type in ExtraTypes.cs can be man or woman
        public Extra Extra{get;set;}//Tells if the Product is for SALE or LIMITED, also in ExtraTypes.cs
        public Inventory Amount{get;set;}//The amount of products in the inventory
        public DateTime DateAdded{get;set;}=DateTime.Now;
        public List<ShoppingCard> Customers{get;set;}//Tells which Customer bought this particular product
    }

    public class Brand
    {
        public int Id{get;set;}
        public string Name{get;set;}
    }

    public class Category
    {
        public int Id{get;set;}
        public string Name{get;set;}
    }

     public class Inventory
     {
         public int Id{get;set;}
         public int Amount{get;set;}
     }
}