using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Drustvo
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Naziv { get; set; }
    public string? Lokacija { get; set; }

    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }

    public List<Dogodek>? Dogodeki { get; set; }
    
}