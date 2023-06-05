using DestructionMapDAL.Entities;
using DestructionMapDAL.IRepositories;

namespace DestructionMapDAL.Repositories;

public class ApprovalsRepository :  Repository<Managers_Approvals, string>, IApprovalsRepository
{

    public ApprovalsRepository(DestructionMapContext context) : base(context)
    {

    }
    
    public IEnumerable<Managers_Approvals> GetByEventId(string eventId)
    {
        return db.Approvals.Where(a => a.Event_Id == eventId);
    }
    
}