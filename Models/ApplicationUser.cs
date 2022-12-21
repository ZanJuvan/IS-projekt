using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class ApplicationUser : IdentityUser
{
     [Key]
     public int InternalId { get; set; }
     public string? FirstName { get; set; }
     public string? LastName { get; set; }
     public string? City { get; set; }
     public List<Cebeljnjak>? Cebeljnjaki { get; set; }

     [ForeignKey("Drustvo")]
     public int DrustvoId { get; set; }
     public Drustvo Drustvo { get; set; }
}
