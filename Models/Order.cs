namespace Webshop.Models
{
    using System;
    using System.Collections.Generic;

    using Webshop.Utils.Xtratypes;
    public class Order
    {
        public int Id{get;set;}
        public int CustomerId{get;set;}
        public OrderStatus Status{get;set;}
        public PaymentMethod paymentMethod{get;set;}
        public bool Payed{get;set;}
        public DateTime OrderDate{get;set;} = DateTime.Now;
        public List<ProductOrder> Products{get;set;}
    }

    public class ProductOrder
    {
        public int OrderId{get;set;}
        public int ProductId{get;set;}
        public int Amount{get;set;}
    }
}