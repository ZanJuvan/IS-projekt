using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Cebeljnjak
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CebeljnjakID { get; set; }
    public string? Naslov { get; set; }
    public int Lokacija { get; set; }

    public ApplicationUser Uporabnik { get; set; }

    public ICollection<Panj>? Panji { get; set; }
    
    public ICollection<Prihodek>? Prihodki { get; set; }
    public ICollection<Odhodek>? Odhodki { get; set; }
}