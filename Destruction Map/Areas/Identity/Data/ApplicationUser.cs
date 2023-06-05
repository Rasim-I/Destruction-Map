using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Destruction_Map.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; }
    
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Surname { get; set; }
    
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public string Address { get; set; }
    
    [PersonalData]
    [Column(TypeName = "int")]
    [MaxLength(3)]
    public int Age { get; set; }
    
}

