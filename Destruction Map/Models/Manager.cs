using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Destruction_Map.Models;

public class Manager : IdentityUser
{
    
    [Column]
    [ForeignKey(nameof(User))]
    public string User_Id { get; set; }
    
}