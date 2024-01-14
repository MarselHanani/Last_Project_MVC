using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Booky.ADL.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }
    [Required(ErrorMessage = "ISBN is required")]
    public string ISBN { get; set; }
    
  public double ListPrice { get; set; }
   public double Price { get; set; }
    
  public double Price50 { get; set; }
    
   public double Price100 { get; set; }
    public Category? Category { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }




    // Property to store the image path in the database
   
}