using System.ComponentModel.DataAnnotations.Schema;

namespace DestructionMapDAL.Entities;

[Table("Managers_Approvals")]
public class Managers_Approvals
{
    public string Id { get; set; }
    
    [ForeignKey(nameof(ManagerEntity))]
    public string Manager_Id { get; set; }
    
    [ForeignKey(nameof(EventEntity))]
    public string Event_Id { get; set; }
    
    public bool IsApproved { get; set; }
}