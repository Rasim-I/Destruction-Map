using DestructionMapModel.Models;
using DestructionMapModel.Models.Enums;
using Type = System.Type;

namespace Destruction_Map.Models;

public class EventWebModel
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
}