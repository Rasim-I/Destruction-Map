using DestructionMapDAL.IRepositories;
using DestructionMapDAL.Repositories;

namespace DestructionMapDAL;

public class UnitOfWork : IUnitOfWork
{

    private readonly DestructionMapContext _context;
    
    public IEventRepository Events { get; private set; }
    
    public IUserRepository Users { get; private set; }
    
    public IManagerRepository Managers { get; private set; }
    
    public ISourceRepository Sources { get; private set; }
    
    public IApprovalsRepository Approvals { get; private set; }

    public UnitOfWork(DestructionMapContext context)
    {
        _context = context;
        Events = new EventRepository(_context);
        Users = new UserRepository(_context);
        Managers = new ManagerRepository(_context);
        Sources = new SourceRepository(_context);
        Approvals = new ApprovalsRepository(_context);

    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    private bool disposed = false;

    public void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();;
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
}