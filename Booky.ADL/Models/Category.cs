using System.ComponentModel.DataAnnotations;

namespace Booky.ADL.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Display order is required")]
    [Range(1,20,ErrorMessage = "Display order must be between 1 and 20")]
    public int DisplayOrder { get; set; }

    public ICollection<Product> Products = new HashSet<Product>();
}