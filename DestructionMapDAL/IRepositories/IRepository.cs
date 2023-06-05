using System.Linq.Expressions;

namespace DestructionMapDAL.IRepositories;

public interface IRepository<TEntity, TIdentity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();

    TEntity Get(TIdentity Id);

    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    void Create(TEntity entity);

    void Update(TEntity entity);

    void Remove(TEntity entity);

}