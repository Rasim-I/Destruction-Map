using DestructionMapModel.Models.Enums;
using Type = System.Type;

namespace DestructionMapModel.Implementation.Services.Utility;

public class EventParameters
{
    public DateTime EventDate { get; set; }
    public string Location { get; set; }
    public List<BuildingType> BuildingType { get; set; }
    public List<WeaponSystem> WeaponSystem { get; set; }



}