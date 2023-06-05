using System.ComponentModel.DataAnnotations.Schema;

namespace DestructionMapDAL.Entities;

[Table ("Sources")]
public class SourceEntity
{
    public string Id { get; set; }
    
    public string Link { get; set; }
    
    [ForeignKey(nameof(EventEntity))]
    public string Event_Id { get; set; }
    
}