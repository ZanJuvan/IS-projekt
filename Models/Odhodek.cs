using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Odhodek
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int DohodekID { get; set; }
    public string? Vrsta { get; set; }
    public int Količina { get; set; }
    public int Vrednost { get; set; }
    public DateOnly Datum { get; set; }
    
    public Uporabnik? Uporabnik { get; set; }
}