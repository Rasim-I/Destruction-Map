using DestructionMapModel.Models.Enums;
using Type = DestructionMapModel.Models.Enums.Type;

namespace DestructionMapModel.Models;

public class Event
{
    public string Id { get; set; }
    public string User_Id { get; set; }
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public BuildingType BuildingType { get; set; }
    public WeaponSystem WeaponSystem { get; set; }
    public Type Type { get; set; }

    public List<Source> SourceList { get; set; }

    public Event(string userId, DateTime eventDate, string location, string description, BuildingType buildingType,
        WeaponSystem weaponSystem)
    {
        Random rnd = new Random();
        string id = Guid.NewGuid().ToString(); // "CIV" + rnd.Next(3000, 10000);
        Id = id;
        User_Id = userId;
        EventDate = eventDate;
        Location = location;
        Description = description;
        BuildingType = buildingType;
        WeaponSystem = weaponSystem;
        SourceList = new List<Source>();


    }

    public Event(){}


}