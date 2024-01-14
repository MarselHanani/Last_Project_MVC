using Booky.ADL.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Booky.PL.ViewModels;

public class ProductViewModel
{
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Author is required")]
    public string Author { get; set; }

    [Required(ErrorMessage = "ISBN is required")]
    public string ISBN { get; set; }

    [Display(Name = "List Price")]
    [Range(1, 1000)]
    [Required(ErrorMessage = "ListPrice is required")]
    public double ListPrice { get; set; }

    [Display(Name = "Price for 1-50")]
    [Range(1, 1000)]
    [Required(ErrorMessage = "Price is required")]
    public double Price { get; set; }

    [Display(Name = "Price for 50+")]
    [Range(1, 1000)]
    [Required(ErrorMessage = "Price50 is required")]
    public double Price50 { get; set; }

    [Display(Name = "Price for 100+")]
    [Range(1, 1000)]
    [Required(ErrorMessage = "Price100 is required")]
    public double Price100 { get; set; }

    public Category? Category { get; set; }
    [ForeignKey("Category")] 
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }

    public IFormFile? Image { get; set; }
}
