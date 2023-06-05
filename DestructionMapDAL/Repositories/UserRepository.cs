using DestructionMapDAL.Entities;
using DestructionMapDAL.IRepositories;

namespace DestructionMapDAL.Repositories;

public class UserRepository : Repository<UserEntity, string>, IUserRepository
{
    public UserRepository(DestructionMapContext context) : base(context)
    {
        
    }
    
}