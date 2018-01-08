namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;
     using Webshop.Utils.Logger;
     
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
        public DbSet<ProductOrder> ProductOrders{get;set;}
        public DbSet<Wishlist> Wishlist{get; set;}

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
           var lf = new LoggerFactory();
           lf.AddProvider(new MyLoggerProvider());
           optionsBuilder.UseLoggerFactory(lf);
       }


        protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Product>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Administrator>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Customer>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Order>().Property(e => e.Id).ValueGeneratedOnAdd();
            
        //Represents the model for ShoppingCart
        modelBuilder.Entity<ShoppingCart>().HasKey(k => new{k.CustomerId,k.ProductId});
        modelBuilder.Entity<ShoppingCart>().HasOne(sc => sc.Customer).WithMany(c => c.Products).HasForeignKey(sc => sc.CustomerId);
        modelBuilder.Entity<ShoppingCart>().HasOne(sc => sc.Product).WithMany(p => p.Customers).HasForeignKey(sc => sc.ProductId);

        modelBuilder.Entity<Wishlist>().HasKey(k => new{k.CustomerId,k.ProductId});
        modelBuilder.Entity<Wishlist>().HasOne(wl => wl.Customer).WithMany(c => c.WishlistProducts).HasForeignKey(wl => wl.CustomerId);
        modelBuilder.Entity<Wishlist>().HasOne(wl => wl.Product).WithMany(p => p.WishlistCustomers).HasForeignKey(wl => wl.ProductId);
         //represents the model for Orders
        //  modelBuilder.Entity<Order>().HasMany(o => o.Products);
         modelBuilder.Entity<ProductOrder>().HasKey(po => new {po.OrderId, po.ProductId});
        }
    }
      
}