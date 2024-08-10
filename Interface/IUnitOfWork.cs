using AIM.Models.Entities;

public interface IUnitOfWork : IDisposable
{
    IRepository<Student> Students { get; }
    IRepository<Teacher> Teachers { get; }
    Task<int> CompleteAsync();
}