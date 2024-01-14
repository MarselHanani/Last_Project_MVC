namespace Booky.BL.Interface;

public interface IunitOfWork
{
    public ICategoryRepository CategoryRepository { get; set;}
    public IproductReopsitory ProductRepository { get; set;}
    public Task<int> Save();
}