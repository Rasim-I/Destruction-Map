using System.ComponentModel.DataAnnotations.Schema;

namespace DestructionMapDAL.Entities;

[Table("Users")]
public class UserEntity
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public string Address { get; set; }
    
    public int Age { get; set; }
    
    
}