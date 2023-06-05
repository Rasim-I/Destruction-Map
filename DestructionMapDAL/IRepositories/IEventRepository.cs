using DestructionMapDAL.Entities;
using DestructionMapDAL.Entities.Enums;
using Type = DestructionMapDAL.Entities.Enums.Type;

namespace DestructionMapDAL.IRepositories;

public interface IEventRepository : IRepository<EventEntity, string>
{
    public IEnumerable<EventEntity> GetByWeaponSystem(WeaponSystem weaponSystem);

    public IEnumerable<EventEntity> GetByBuildingType(BuildingType buildingType);

    public IEnumerable<EventEntity> GetByType(Type type);

    public IEnumerable<EventEntity> GetAll_IncludeAll();

    public IEnumerable<EventEntity> GetByLocation(string location);

    public IEnumerable<EventEntity> GetByDate(DateTime eventDate);

    public IEnumerable<EventEntity> GetByDescription(string description);

    public IEnumerable<EventEntity> GetEventsToApprove();

    public int GetIntensityByLocation(string location);
}