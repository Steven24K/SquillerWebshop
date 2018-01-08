namespace Webshop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Webshop.Utils.Xtratypes;
     public class RegisterCustomer
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Name is Required!")]
        [MaxLength(25)]
        public string Name{get;set;}
        [Required(ErrorMessage = "Surname is Required!")]
        [MaxLength(25)]
        public string Surname{get;set;}
        [Required(ErrorMessage = "Gender is Required!")]
        public Gender Gender{get;set;}
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "This does not look like an email adress")]
        public string Email{get;set;}
        [Required(ErrorMessage = "Password Required!")]
        [MinLength(7, ErrorMessage = "Password must be minimal 7 characters long")]
        //Make password stricter with uppercase and special characters
        public string Password{get;set;}
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword{get;set;}
        [Required(ErrorMessage = "Street is Required!")]
        [MaxLength(30)]
        public string Street{get;set;}
        [MaxLength(8, ErrorMessage="This does not look like a PostalCode")]
        [Required(ErrorMessage = "PostalCode is Required!")]
        public string PostalCode{get;set;}
        [Required(ErrorMessage = "City is Required!")]
        [MaxLength(40)]
        public string City{get;set;}
        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please UNCHECK to accept the terms and conditions!!!!")]
        public bool AcceptTerms{get;set;}

    }
}