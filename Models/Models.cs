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
      
    public class ShoppingCart
    {
        public int CustomerId{get;set;}
        public Customer Customer{get;set;}
        public int ProductId{get;set;}
        public Product Product{get;set;}
        public int Amount{get;set;}
    }

    public class Wishlist
    {
        public int CustomerId{get; set;}
        public Customer Customer{get; set;}
        public int ProductId{get; set;}
        public Product Product{get; set;}
        public int Amount{get;set;}
    }

    public class Administrator
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Please fill in your username")]
        public string UserName{get;set;}
        [Required(ErrorMessage = "You have to fill in a password you stupid!")]
        public string Password{get;set;}
        public string Email{get;set;}
        public DateTime RegistrationDate{get;set;} = DateTime.Now;
    }

    public class Customer
    {
        public int Id{get;set;}
        [Required]
        [MaxLength(25)]
        public string Name{get;set;}
        [Required()]
        [MaxLength(25)]
        public string Surname{get;set;}
        [Required()]
        public Gender Gender{get;set;}
        [Required()]
        [EmailAddress(ErrorMessage = "This does not look like an email adress")]
        public string Email{get;set;}
        public string Password{get;set;}
        [Required()]
        [MaxLength(30)]
        public string Street{get;set;}
        [Required()]
        [MaxLength(8, ErrorMessage = "This does not look like a PostalCode")]
        public string PostalCode{get;set;}
        [Required()]
        [MaxLength(40)]
        public string City{get;set;}
        public DateTime RegistrationDate{get;set;}=DateTime.Now;
        public List<ShoppingCart> Products{get;set;}//This represents the customers ShoppingCart, contains a list of Productss
        public List<Wishlist> WishlistProducts{get;set;} 
        public List<Order> Orders{get;set;}
    }
    public class Product
    {
        public int Id{get;set;}
        [Required()]
        [MaxLength(25)]
        public string Name{get;set;}
        [Required()]
        [MaxLength(512, ErrorMessage="Description can't be longer than 512 characters")]
        public string Description{get;set;}
        [Required()]
        public string Category{get;set;}//Like pants, shirts, watches, shoes etc.
        [Required()]
        public string Brand{get;set;}//Like Nike, Gucci, Rolex etc. This a table Brand
        [Required()]
        [RegularExpression("([0-9]+)")]
        public double Price{get;set;}
        [Required()]
        public Gender Gender{get;set;}//Self defined type in ExtraTypes.cs can be man or woman
        [Required()]
        public Extra Extra{get;set;}//Tells if the Products is for SALE or LIMITED, also in ExtraTypes.cs
        [Required()]
        [RegularExpression("([0-9]+)")]
        public int Amount{get;set;}//The amount of Productss in the inventory
        [Required()]
        [Url()]
        public string ImageUrl{get;set;}
        public DateTime DateAdded{get;set;}=DateTime.Now;
        public List<ShoppingCart> Customers{get;set;}//Tells which Customer bought this particular Products
        public List<Wishlist> WishlistCustomers{get; set;}
        public List<Order> Orders{get;set;}

    }

}