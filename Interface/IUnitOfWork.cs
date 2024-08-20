using AIM.Models.Entities;

public interface IUnitOfWork : IDisposable
{

    IRepository<Furniture> Furnitures { get; }
    IRepository<User> Users { get;  }
    IRepository<Vehicle> Vehicles { get;  }
    IRepository<Land> Lands { get; }
    IRepository<Ledger> Ledgers { get; }
    
    IRepository<Department> Departments { get; }
    Task<int> CompleteAsync();
}