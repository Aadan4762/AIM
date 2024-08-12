using AIM.Models.Entities;

public interface IUnitOfWork : IDisposable
{

    IRepository<Furniture> Furnitures { get; }
    IRepository<User> Users { get;  }
    IRepository<Vehicle> Vehicles { get;  }
    Task<int> CompleteAsync();
}