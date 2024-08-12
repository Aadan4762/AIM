using AIM.Models.Entities;

public interface IUnitOfWork : IDisposable
{

    IRepository<Furniture> Furnitures { get; }
    IRepository<User> Users { get;  }
    Task<int> CompleteAsync();
}