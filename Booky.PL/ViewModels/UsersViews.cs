using System.ComponentModel.DataAnnotations;

namespace Booky.PL.ViewModels;

public class UsersViews
{
    public string? Id { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public IEnumerable<string> Roles { get; set; }
}