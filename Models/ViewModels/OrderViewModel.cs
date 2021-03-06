namespace Webshop.Models.ViewModels
{
    using System;
    using Webshop.Utils.Xtratypes;
    public class OrderViewModel
    {
        public int OrderId{get;set;}
        public OrderStatus Status{get;set;}
        public PaymentMethod paymentMethod{get;set;}
        public bool Payed{get;set;}
        public int CustomerId{get;set;}
        public string Customer{get;set;}
        public double TotalPrice{get;set;}
        public DateTime OrderDate{get;set;}
    }
}