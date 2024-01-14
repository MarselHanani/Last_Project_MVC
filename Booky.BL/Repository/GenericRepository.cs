
using Booky.ADL.Data;
using Booky.ADL.Models;
using Booky.BL.Interface;
using Microsoft.EntityFrameworkCore;

namespace Booky.BL.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BookyDbContext _context;
    public GenericRepository(BookyDbContext context)
    {
        _context = context;
    }
    public async Task<T> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    // here we edit GetAll method to include category => mean get data from two table by using eager loading
    public async Task<IEnumerable<T>>GetAll()
    {
        if(typeof(T) == typeof(Product))
        {
            return (IEnumerable<T>)await _context.Products.Include(c => c.Category).ToListAsync();
        }
        else
        {
            return await _context.Set<T>().ToListAsync();
            
        }
       
    }
    //==============================================================================================================

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);

    }

    public void Update(T entity)
    {
         _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
}