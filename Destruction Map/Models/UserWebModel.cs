using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Destruction_Map.Models;

public class UserWebModel //: IdentityUser
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Address { get; set; }
    
    
    /*
    [PersonalData]
    [Column]
    public string Name { get; set; }
    
    [PersonalData]
    [Column]
    public string Surname { get; set; }
    
    [PersonalData]
    [Column]
    public string Address { get; set; }
    
    [PersonalData]
    [Column]
    public int Age { get; set; }
    */
    
    
}