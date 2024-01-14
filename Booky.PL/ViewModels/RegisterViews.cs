using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels;

public class RegisterViews
{
    [Required(ErrorMessage = "First Name is required !!")]
    public string Fname { get; set; }
    [Required(ErrorMessage = "Last Name is required !!")]
    public string Lname { get; set; } 
    [Required(ErrorMessage = "Email is required !!")]
    [EmailAddress(ErrorMessage = "Invalid Email Address !!")]    
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required !!")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required(ErrorMessage = "Confirm Password is required !!")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Confirm Password does not match !!")]
    public string CPassword { get; set; }
    [Required(ErrorMessage = "you have to Agree !!")]
    public bool isAgree { get; set; }
}