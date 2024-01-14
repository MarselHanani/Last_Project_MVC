using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels;

public class ResetPasswordViews
{
    [Required(ErrorMessage = "Password is required !!")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required(ErrorMessage = "Confirm Password is required !!")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Password and Confirm Password does not match !!")]
    public string CPassword { get; set; } 
}