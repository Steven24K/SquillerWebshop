namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;

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
}