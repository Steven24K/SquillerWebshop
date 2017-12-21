namespace Webshop.Models
{
    public class WishlistView
    {
        public int Id{get;set;}//This is the product ID
        public int CustomerId{get;set;}
        public string ProductName{get;set;}
        public int Amount{get;set;}
        public string Price{get;set;}
        public string TotalPrice{get;set;}
    }
}