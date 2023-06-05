using DestructionMapDAL.Entities;
using DestructionMapDAL.Entities.Enums;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Models;
using Type = DestructionMapDAL.Entities.Enums.Type;

namespace DestructionMapModel.Implementation.Mappers;

public class EventMapper : IMapper<EventEntity, Event>
{
    private IMapper<SourceEntity, Source> _sourceMapper = new SourceMapper();

    /*
    public EventMapper(IMapper<SourceEntity, Source> sourceMapper)
    {
        _sourceMapper = sourceMapper;
    }
    */
    
    public EventEntity ToEntity(Event model)
    {
        return new EventEntity
        {
            Id = model.Id,
            User_Id = model.User_Id,
            Description = model.Description,
            Location = model.Location,
            WeaponSystem = (WeaponSystem)model.WeaponSystem,
            BuildingType = (BuildingType)model.BuildingType,
            Type = (Type)model.Type,
            EventDate = model.EventDate,
            SourceList = new List<SourceEntity>(model.SourceList.ConvertAll(s => _sourceMapper.ToEntity(s)))
        };
    }

    public Event ToModel(EventEntity entity)
    {
        return new Event
        {
            Id = entity.Id,
            User_Id = entity.User_Id,
            Description = entity.Description,
            Location = entity.Location,
            WeaponSystem = (Models.Enums.WeaponSystem)entity.WeaponSystem,
            BuildingType = (Models.Enums.BuildingType)entity.BuildingType,
            Type = (Models.Enums.Type)entity.Type,
            EventDate = entity.EventDate,
            SourceList = new List<Source>(entity.SourceList.ConvertAll(s => _sourceMapper.ToModel(s)))
        };

    }
    
}