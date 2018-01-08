namespace Webshop.Models
{
     using Microsoft.EntityFrameworkCore;
     using Microsoft.Extensions.Logging;

     using System;
     using System.IO;
     using System.Collections.Generic;
     using System.ComponentModel.DataAnnotations;

     using Webshop.Utils.Xtratypes;

    public class Administrator
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Please fill in your username")]
        public string UserName{get;set;}
        [Required(ErrorMessage = "You have to fill in a password you stupid!")]
        public string Password{get;set;}
        public string Email{get;set;}
        public DateTime RegistrationDate{get;set;} = DateTime.Now;
    }
}