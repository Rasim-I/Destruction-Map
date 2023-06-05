using DestructionMapDAL.Entities;
using DestructionMapDAL.IRepositories;

namespace DestructionMapDAL.Repositories;

public class SourceRepository : Repository<SourceEntity, string>, ISourceRepository
{
    public SourceRepository(DestructionMapContext context) : base(context)
    {
        
    }
    
}