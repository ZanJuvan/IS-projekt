using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Cebeljnjak
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string? Naslov { get; set; }

    [ForeignKey("ApplicationUser")]
    public String UporabnikId { get; set; }

    public List<Panj>? Panji { get; set; }
    
}