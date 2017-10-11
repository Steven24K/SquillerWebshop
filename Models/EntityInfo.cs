namespace Webshop.Models.EntityInfo
{
    using System;
    using Webshop.Utils.Xtratypes;
    public class ProductInfo
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string Description{get;set;}
        public string Category{get;set;}
        public string Brand{get;set;}
        public double Price{get;set;}
        public string Gender{get;set;} 
        public int Amount{get;set;}
        public DateTime dateTime{get;set;}
    }
}