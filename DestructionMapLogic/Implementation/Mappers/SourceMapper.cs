using DestructionMapDAL.Entities;
using DestructionMapModel.Abstraction.IMappers;
using DestructionMapModel.Models;

namespace DestructionMapModel.Implementation.Mappers;

public class SourceMapper : IMapper<SourceEntity, Source>
{
    public SourceEntity ToEntity(Source model)
    {
        return new SourceEntity
        {
            Id = model.Id,
            Event_Id = model.Event_Id,
            Link = model.Link
        };
    }

    public Source ToModel(SourceEntity entity)
    {
        return new Source
        {
            Id = entity.Id,
            Event_Id = entity.Event_Id,
            Link = entity.Link
        };
    }
}