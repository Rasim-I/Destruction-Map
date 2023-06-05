using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Mappers;

public class UserMapper : IMapper<UserEntity, User>
{
    public UserEntity ToEntity(User model)
    {
        return new UserEntity
        {
            Id = model.Id,
            Address = model.Address,
            Age = model.Age,
            Name = model.Name,
            Surname = model.Surname
        };
    }

    public User ToModel(UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Address = entity.Address,
            Age = entity.Age,
            Name = entity.Name,
            Surname = entity.Surname
        };
    }
}