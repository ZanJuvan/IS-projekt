using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Evidenca
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Vrsta { get; set; }
    public int Količina { get; set; }
    public int Vrednost { get; set; }
    public DateTime Datum { get; set; }

    [ForeignKey("Panj")]
    public int PanjId { get; set; }
    public Panj Panj { get; set; }
}