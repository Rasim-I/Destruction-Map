using DestructionMapModel.Models;

namespace Destruction_Map.Models;

public class EventWebModelMapper
{
    
    public EventWebModel ToEventWebModel(Event model)
    {
        return new EventWebModel()
        {
            Id = model.Id,
            Description = model.Description,
            BuildingType = model.BuildingType,
            WeaponSystem = model.WeaponSystem,
            Location = model.Location,
            EventDate = model.EventDate,
            User_Id = model.User_Id,
            SourceList = model.SourceList
        };
    }
    
}