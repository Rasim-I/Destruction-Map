using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Mappers;

public class ManagerMapper : IMapper<ManagerEntity, Manager>
{
    public ManagerEntity ToEntity(Manager model)
    {
        return new ManagerEntity
        {
            Id = model.Id,
            User_Id = model.User_Id
        };
    }

    public Manager ToModel(ManagerEntity entity)
    {
        return new Manager
        {
            Id = entity.Id,
            User_Id = entity.User_Id
        };
    }
}