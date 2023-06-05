using DestructionMapDAL.Entities;

namespace DestructionMapDAL.IRepositories;

public interface IApprovalsRepository : IRepository<Managers_Approvals, string>
{
    public IEnumerable<Managers_Approvals> GetByEventId(string eventId);
}