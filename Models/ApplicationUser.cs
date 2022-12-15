using Microsoft.AspNetCore.Identity;

namespace BeeOrganizer.Models;

public class ApplicationUser : IdentityUser
{
     //public int Id { get; set; }
     public string? FirstName { get; set; }
     public string? LastName { get; set; }
     public string? City { get; set; }
}
