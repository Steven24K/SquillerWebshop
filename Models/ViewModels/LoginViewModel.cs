namespace Webshop.Models
{
    using System.ComponentModel.DataAnnotations;
    
    public class ApplicationUser
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Please fill in email adress")]
        [EmailAddress(ErrorMessage = "Please fill in a valid email adress")]
        public string Email{get;set;}
        [Required(ErrorMessage = "Please fill in your password")]
        public string Password{get;set;}
        
    }
}