using Microsoft.AspNetCore.Identity;

namespace Booky.ADL.Models;

public class ApplicationUser: IdentityUser
{
    public string Fname { get; set; }
    public string Lname { get; set; }
    public bool isAgree { get; set; }
}