using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DestructionMapDAL.Entities.Enums;
using Type = DestructionMapDAL.Entities.Enums.Type;

namespace DestructionMapDAL.Entities;

[Table("Events")]
public class EventEntity
{
    public string Id { get; set; }
    
    [ForeignKey(nameof(UserEntity))]
    public string User_Id { get; set; }
    
    public DateTime EventDate { get; set; }
    
    public string Location { get; set; }
    
    public string Description { get; set; }
    
    [EnumDataType(typeof(BuildingType))]
    public BuildingType BuildingType { get; set; }
    
    [EnumDataType(typeof(WeaponSystem))]
    public WeaponSystem WeaponSystem { get; set; }
    
    public virtual List<SourceEntity> SourceList { get; set; }

    [EnumDataType(typeof(Type))]
    public Type Type { get; set; }
    
}