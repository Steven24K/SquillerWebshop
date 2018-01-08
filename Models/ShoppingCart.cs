namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;

    public class ShoppingCart
    {
        public int CustomerId{get;set;}
        public Customer Customer{get;set;}
        public int ProductId{get;set;}
        public Product Product{get;set;}
        public int Amount{get;set;}
    }
}