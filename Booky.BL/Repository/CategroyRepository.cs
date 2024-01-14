using Booky.ADL.Data;
using Booky.ADL.Models;
using Booky.BL.Interface;

namespace Booky.BL.Repository;

public class CategroyRepository: GenericRepository<Category>, ICategoryRepository
{
    public CategroyRepository(BookyDbContext context) : base(context)
    {
        
    }
}