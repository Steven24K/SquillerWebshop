namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;

     using System;
     using System.IO;
     using System.Collections.Generic;

     using Webshop.Utils.Xtratypes;
     
    //This class represents the database, it contains al tables, models and relations. 
    //This class is also initiated when you want to open a connection
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> options) : base(options){}

        public DbSet<Administrator> Administrators{get;set;}
        public DbSet<ShoppingCart> ShoppingCart{get;set;}
        public DbSet<Customer> Customers{get;set;}
        public DbSet<Product> Products{get;set;}
        public DbSet<Order> Orders{get;set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder){
        //Reperesnets the model for ShoppingCart
        modelBuilder.Entity<ShoppingCart>().HasKey(k => new{k.CustomerId,k.ProductId});
        modelBuilder.Entity<ShoppingCart>().HasOne(sc => sc.Customer).WithMany(c => c.Products).HasForeignKey(sc => sc.CustomerId);
        modelBuilder.Entity<ShoppingCart>().HasOne(sc => sc.Product).WithMany(p => p.Customers).HasForeignKey(sc => sc.ProductId);
         //represents the model for Orders
         modelBuilder.Entity<Order>().HasKey(k => new{k.CustomerId,k.ProductsId});
         modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerId);
         modelBuilder.Entity<Order>().HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductsId);
        }
    }
      
    public class ShoppingCart
    {
        public int CustomerId{get;set;}
        public Customer Customer{get;set;}
        public int ProductId{get;set;}
        public Product Product{get;set;}
        public int Amount{get;set;}
    }

    public class Order
    {
        public int CustomerId{get;set;}
        public Customer Customer{get;set;}
        public int ProductsId{get;set;}
        public Product Product{get;set;}
        public int Amount{get;set;}
        public OrderStatus orderStatus{get;set;}
        public DateTime orderDate{get;set;} = DateTime.Now;
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
        public string Password{get;set;}
        public string Street{get;set;}
        public string PostalCode{get;set;}
        public string City{get;set;}
        public DateTime RegistrationDate{get;set;}=DateTime.Now;
        public List<ShoppingCart> Products{get;set;}//This represents the customers ShoppingCart, contains a list of Productss 
        public List<Order> Orders{get;set;}

        //A customer might be able to place many reviews
    }

    public class Product
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public string Category{get;set;}//Like pants, shirts, watches, shoes etc.
        public string Brand{get;set;}//Like Nike, Gucci, Rolex etc. This a table Brand
        public double Price{get;set;}
        public Gender Gender{get;set;}//Self defined type in ExtraTypes.cs can be man or woman
        public Extra Extra{get;set;}//Tells if the Products is for SALE or LIMITED, also in ExtraTypes.cs
        public int Amount{get;set;}//The amount of Productss in the inventory
        public DateTime DateAdded{get;set;}=DateTime.Now;
        public List<ShoppingCart> Customers{get;set;}//Tells which Customer bought this particular Products
        public List<Order> Orders{get;set;}
        
        //A Prodduct can have many reviews
    }

}