using Booky.ADL.Data;
using Booky.ADL.Models;
using Booky.BL.Interface;

namespace Booky.BL.Repository;

public class ProductRepository: GenericRepository<Product>,IproductReopsitory
{
    public ProductRepository(BookyDbContext context) : base(context)
    {
        
    }
}