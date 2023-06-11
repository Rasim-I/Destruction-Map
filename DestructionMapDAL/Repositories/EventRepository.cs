using DestructionMapDAL.Entities;
using DestructionMapDAL.Entities.Enums;
using DestructionMapDAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using Type = DestructionMapDAL.Entities.Enums.Type;

namespace DestructionMapDAL.Repositories;

public class EventRepository : Repository<EventEntity, string>, IEventRepository
{
    public EventRepository(DestructionMapContext context) : base(context)
    {
        
    }

    public IEnumerable<EventEntity> GetByWeaponSystem(WeaponSystem weaponSystem)
    {
        return db.Events.Where(e => e.WeaponSystem == weaponSystem && e.Type == Type.Event); //+ type
    }

    public IEnumerable<EventEntity> GetByBuildingType(BuildingType buildingType)
    {
        return db.Events.Where(e => e.BuildingType == buildingType && e.Type == Type.Event); //+ type
    }

    public IEnumerable<EventEntity> GetByType(Type type)
    {
        return db.Events.Where(e => e.Type == type);
    }

    public IEnumerable<EventEntity> GetAll_IncludeAll()
    {
        return db.Events.Include(e => e.SourceList);
    }

    public IEnumerable<EventEntity> GetByLocation(string location)
    {
        return db.Events.Where(e => (e.Location.ToLower().Contains(location) ||
                                    e.Description.ToLower().Contains(location)) && e.Type == Type.Event) //+ type
            .Include(e => e.SourceList);
    }

    public IEnumerable<EventEntity> GetByDate(DateTime eventDate)
    {
        return db.Events.Where(e => e.EventDate == eventDate && e.Type == Type.Event); //+ type
    }

    public IEnumerable<EventEntity> GetByLocationOnly(string location)
    {
        return db.Events.Where(e => e.Location.ToLower().Contains(location.ToLower()));
    }
    public IEnumerable<EventEntity> GetByDescription(string description)
    {
        return db.Events.Where(e => e.Description.ToLower().Contains(description.ToLower()) && e.Type == Type.Event); //+ type
    }

    public IEnumerable<EventEntity> GetEventsToApprove()
    {
        return db.Events.Where(e => e.Type == Type.Proposal).Include(e => e.SourceList);
    }

    public int GetIntensityByLocation(string location)
    {
        return db.Events.Count(e => e.Location.ToLower().Contains(location.ToLower()) && e.Type == Type.Event); //+ type

    }
}