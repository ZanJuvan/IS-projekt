using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Panj
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int PanjID { get; set; }
    public string? Naziv { get; set; }
    
    public Cebeljnjak? Cebeljnjak { get; set; }
}