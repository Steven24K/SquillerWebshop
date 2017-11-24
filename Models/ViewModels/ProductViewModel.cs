namespace Webshop.Models.EntityInfo
{
    using System;
    using Webshop.Utils.Xtratypes;
    public class ProductViewModel

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
        public DateTime DateAdded{get;set;}
        public string ImageUrl{get;set;}

    }
}