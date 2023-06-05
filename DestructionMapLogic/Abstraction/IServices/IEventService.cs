using DestructionMapModel.Implementation.Services.Utility;
using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;

namespace DestructionMapModel.Abstraction.IServices;

public interface IEventService
{
    public List<Event> GetAll();

    public List<Event> GetByLocation(string region);

    public List<Event> FindByKeyWords(string keyWords);

    public List<Event> GetByWeaponSystem(WeaponSystem weaponSystem);

    public List<Event> GetByBuildingType(BuildingType buildingType);

    public List<Event> GetByDate(DateTime eventDate);

    public List<Event> GetByDescription(string description);
    
    public List<Event> FindByParameters(EventParameters eventParameters);

    public void CreateEvent(string userId, DateTime eventDate, string location, string description, BuildingType buildingType, WeaponSystem weaponSystem, string sources);

    public List<Event> GetEventsToApprove(string userId);

    public Dictionary<string, int> GetEventsIntensity();

}