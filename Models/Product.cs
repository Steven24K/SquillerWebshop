namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;

    public class Product
    {
        public int Id{get;set;}
        [Required()]
        [MaxLength(50, ErrorMessage = "The name cannot be longer 50 characters")]
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