using AIM.Data;
using AIM.Models.Entities;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private Repository<Furniture> _furnitureRepository;
    private Repository<User> _userRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public IRepository<Furniture> Furnitures
    {
        get
        {
            return _furnitureRepository ??= new Repository<Furniture>(_context);
        }
    }

    public IRepository<User> Users 
    {
        get
        {
            return _userRepository ??= new Repository<User>(_context);
        }
        
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}