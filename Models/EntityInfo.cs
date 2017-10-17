namespace Webshop.Models.EntityInfo
{
    using System;
    using Webshop.Utils.Xtratypes;
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
        public DateTime dateTime{get;set;}
    }
}