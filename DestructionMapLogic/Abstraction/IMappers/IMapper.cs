namespace DestructionMapModel.Abstraction.IMappers;

public interface IMapper<TEntity, TModel> : IBackMapper<TEntity, TModel>
{
    TModel ToModel(TEntity entity);

}