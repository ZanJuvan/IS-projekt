using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Uporabnik
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int UporabnikID { get; set; }
    public string? Ime { get; set; }
    public int Lokacija { get; set; }

    public ICollection<Cebeljnjak>? Cebeljnjaki { get; set; }
    public ICollection<Prihodek>? Prihodki { get; set; }
    public ICollection<Odhodek>? Odhodki { get; set; }
}