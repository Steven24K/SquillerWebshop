namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;

     //This class is not used anymore, but in order to delete this class you have to run a new migration on the database,
     //Wich results in dropping the database and losing all the data!!!!!!!!
    public class Wishlist
    {
        public int CustomerId{get; set;}
        public Customer Customer{get; set;}
        public int ProductId{get; set;}
        public Product Product{get; set;}
        public int Amount{get;set;}
    }
}