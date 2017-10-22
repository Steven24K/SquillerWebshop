namespace Webshop.Utils.Login
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
    public virtual string Email { get; set; }
    public virtual string Password{get;set;}
    }
}

