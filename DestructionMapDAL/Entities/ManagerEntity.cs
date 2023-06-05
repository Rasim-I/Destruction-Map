using System.ComponentModel.DataAnnotations.Schema;

namespace DestructionMapDAL.Entities;

[Table("Managers")]
public class ManagerEntity
{
    public string Id { get; set; }
    
    [ForeignKey(nameof(UserEntity))]
    public string User_Id { get; set; }
}