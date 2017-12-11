namespace Webshop.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Webshop.Utils.Xtratypes;

    public class CreateOrderViewModel
    {
        public int CustomerId{get;set;}
        public string Street{get;set;}
        public string PostalCode{get;set;}
        public string City{get;set;}
        public IEnumerable<ShoppingCartView> Products{get;set;}  
        public double TotalPrice{get;set;}
        public PaymentMethod paymentMethod{get;set;}
    }
}