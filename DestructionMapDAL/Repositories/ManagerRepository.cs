using DestructionMapDAL.Entities;
using DestructionMapDAL.IRepositories;

namespace DestructionMapDAL.Repositories;

public class ManagerRepository : Repository<ManagerEntity, string>, IManagerRepository
{
    public ManagerRepository(DestructionMapContext context) : base(context)
    {
        
    }
    
}