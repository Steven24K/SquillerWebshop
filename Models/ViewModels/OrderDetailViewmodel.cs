namespace Webshop.Models.ViewModels
{
    using Webshop.Utils.Xtratypes;
    using Webshop.Models;

    using System;
    using System.Collections.Generic;

    public class OrderDetailsView
    {
        public int OrderId{get;set;}
        public OrderStatus Status{get;set;}
        public PaymentMethod paymentMethod{get;set;}
        public bool Payed{get;set;}
        public int CustomerId{get;set;}

        public List<AmountProduct> Products{get;set;}
        public double TotalPrice{get;set;}
        public DateTime OrderDate{get;set;}
    }

    public class AmountProduct
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public double Price{get;set;}
        public int Amount{get;set;}
    }
}