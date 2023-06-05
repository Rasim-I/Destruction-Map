using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DestructionMapDAL.IRepositories;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace DestructionMapDAL.Repositories;


public class Repository<TEntity, TIdentity> : IRepository<TEntity, TIdentity> where TEntity : class
{

    protected readonly DestructionMapContext db;

    public Repository(DestructionMapContext context)
    {
        db = context;
    }


    public void Create(TEntity entity)
    {
        db.Set<TEntity>().Add(entity);
    }

    public void Remove(TEntity entity)
    {
        db.Set<TEntity>().Remove(entity);
    }
    
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return db.Set<TEntity>().Where(predicate);
    }

    public TEntity Get(TIdentity id)
    {
        return db.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return db.Set<TEntity>();
    }

    public void Update(TEntity entity)
    {
        db.Entry(entity).State = EntityState.Modified;
    }
    
}