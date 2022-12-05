using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Panj
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CebeljnjakID { get; set; }
    public string? Naslov { get; set; }
    public int Lokacija { get; set; }
    
    public Cebeljnjak? Cebeljnjak { get; set; }
}