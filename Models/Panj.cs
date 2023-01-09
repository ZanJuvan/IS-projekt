using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Panj
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PanjID { get; set; }
    public string? Naziv { get; set; }

    public List<Evidenca>? Evidence { get; set;}
    
    [ForeignKey("Cebeljnjak")]
    public int CebeljnjakID { get; set; }
    public Cebeljnjak? Cebeljnjak { get; set; }
}