namespace Webshop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Webshop.Utils.Xtratypes;
     public class RegisterCustomer
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Name is Required!")]
        public string Name{get;set;}
        [Required(ErrorMessage = "Surname is Required!")]
        public string Surname{get;set;}
        [Required(ErrorMessage = "Gender is Required!")]
        public Gender Gender{get;set;}
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "This does not look like an email adress")]
        public string Email{get;set;}
        [Required(ErrorMessage = "Password Required!")]
        public string Password{get;set;}
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword{get;set;}
        [Required(ErrorMessage = "Street is Required!")]
        public string Street{get;set;}
        [Required(ErrorMessage = "PostalCode is Required!")]
        public string PostalCode{get;set;}
        [Required(ErrorMessage = "City is Required!")]
        public string City{get;set;}
        [Required(ErrorMessage = "Please accept the terms and conditions")]
        public bool AcceptTerms{get;set;}

    }
}