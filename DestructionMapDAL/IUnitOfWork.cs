using DestructionMapDAL.IRepositories;

namespace DestructionMapDAL;

public interface IUnitOfWork
{
    IEventRepository Events { get; }
    
    IUserRepository Users { get; }
    
    IManagerRepository Managers { get; }
    
    ISourceRepository Sources { get; }

    IApprovalsRepository Approvals { get; }
    int Save();

}