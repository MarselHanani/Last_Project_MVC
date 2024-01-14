using Booky.ADL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booky.ADL.Data;

public class BookyDbContext: IdentityDbContext<ApplicationUser>
{
    public BookyDbContext(DbContextOptions<BookyDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Programming" , DisplayOrder = 1},
            new Category { Id = 2, Name = "Science" , DisplayOrder = 2},
            new Category { Id = 3, Name = "Math" , DisplayOrder = 3},
            new Category { Id = 4, Name = "History" , DisplayOrder = 4}
            );
        //------------------------------------------------------------------------------
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Title = "Book1", Description = "Book1 Description", Author = "Author1",
                ISBN = "ISBN1", ListPrice = 100,Price = 100,
                Price50 = 70, Price100 = 50,CategoryId =1
            },
            new Product
            {
                Id = 2, Title = "Book2", Description = "Book2 Description", Author = "Author2",
                ISBN = "ISBN2", ListPrice = 200,Price = 200,
                Price50 = 170, Price100 = 150,CategoryId =2
                
            }
            ,new Product
            {
                Id = 3, Title = "Book3", Description = "Book3 Description", Author = "Author3",
                ISBN = "ISBN3", ListPrice = 300,Price = 300,
                Price50 = 270, Price100 = 250,CategoryId =3
                
            }
        );
        //------------------------------------------------------------------------------
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    
}
