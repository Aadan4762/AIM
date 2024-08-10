public interface IUnitOfWork : IDisposable
{
    IRepository<Student> Students { get; }
    Task<int> CompleteAsync();
}