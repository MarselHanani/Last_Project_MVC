using Booky.ADL.Data;
using Booky.BL.Interface;

namespace Booky.BL.Repository;

public class UnitOfWork : IunitOfWork, IDisposable
{
    private readonly BookyDbContext _dbContext;
    public ICategoryRepository CategoryRepository { get; set; }
    public IproductReopsitory ProductRepository { get; set; }

    public UnitOfWork(BookyDbContext dbContext)
    {
        _dbContext = dbContext;
        CategoryRepository = new CategroyRepository(dbContext);
        ProductRepository = new ProductRepository(dbContext);
    }

    public async Task<int> Save() => await _dbContext.SaveChangesAsync();
    
    public void Dispose() => _dbContext.Dispose(); 

}