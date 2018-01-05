namespace Webshop.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please fill in your current password")]
        public string Password{get;set;}
        [Required(ErrorMessage = "Fill in new password")]
        [MinLength(7, ErrorMessage = "Password must be atleast 7 characters")]
        public string NewPassword{get;set;}
        [Required(ErrorMessage = "Please confirm your new password")]
        [Compare("NewPassword", ErrorMessage="Password does not match")]
        public string ConfirmPassword{get;set;}
    }
}