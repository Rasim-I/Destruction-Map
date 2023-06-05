namespace DestructionMapModel.Abstraction.IMappers;

public interface IBackMapper<TEntity, TModel>
{
    TEntity ToEntity(TModel model);

}