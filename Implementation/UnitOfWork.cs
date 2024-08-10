using AIM.Data;
using AIM.Models.Entities;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private Repository<Student> _studentRepository;
    private Repository<Teacher> _teacherRepository; 

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IRepository<Student> Students
    {
        get
        {
            return _studentRepository ??= new Repository<Student>(_context);
        }
    }

    public IRepository<Teacher> Teachers
    {
        get
        {
            return _teacherRepository ??= new Repository<Teacher>(_context); 
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