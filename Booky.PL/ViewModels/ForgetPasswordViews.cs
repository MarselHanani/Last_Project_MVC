using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels;

public class ForgetPasswordViews
{
    [Required(ErrorMessage = "Email is required !!")]
    [EmailAddress(ErrorMessage = "Invalid Email Address !!")]    
    public string Email { get; set; } 
}