using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Cebeljnjak
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CebeljnjakID { get; set; }
    public string? Naslov { get; set; }
    public int Lokacija { get; set; }

    public Uporabnik? Uporabnik { get; set; }
    public ICollection<Panj>? Panji { get; set; }
}