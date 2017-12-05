namespace Webshop.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Webshop.Utils.Xtratypes;

    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please select a Title")]
        public string Title{get;set;}
        [Required(ErrorMessage = "Name is Required!")]
        public string Name{get;set;}
        [Required(ErrorMessage = "Surname is Required!")]
        public string Surname{get;set;}
        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "This does not look like an email adress")]
        public string Email{get;set;}
        [Required(ErrorMessage = "Phonenumber is Required")]
        public int Phonenumber{get; set;}
        [Required(ErrorMessage = "Description is Required!")]
        public string Description{get; set;}
        [Required(ErrorMessage = "Subject is Required")]
        public string Subject{get; set;}

        [Required()]
        public int Ordernumber{get; set;}
        
    }
}