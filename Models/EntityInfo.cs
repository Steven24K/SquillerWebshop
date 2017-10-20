namespace Webshop.Models.EntityInfo
{
    using System;
    using Webshop.Utils.Xtratypes;
    using Microsoft.AspNetCore.Http;
    public class ProductInfo
    {
        //This class is used to wrap all product information into 1 object
        public int Id{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public string Category{get;set;}
        public string Brand{get;set;}
        public double Price{get;set;}
        public Gender Gender{get;set;} 
        public Extra Extra{get;set;}
        public int Amount{get;set;}
        public IFormFile File{get;set;}
        public DateTime dateTime{get;set;}
    }

    public class CustomerInfo
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
        public DateTime RegistrationDate{get;set;}
    }
}