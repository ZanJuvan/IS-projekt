using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Evidenca
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string? KratekOpis { get; set; }
    public bool? CistostCebele { get; set; }
    public int? Moc { get; set; }
    public bool? Mirnost { get; set; }
    public bool? Rojivost { get; set; }
    public bool? Zalega { get; set; }
    public bool? IzrezovanjeTrotovine { get; set; }
    public bool? ZalogaHrane { get; set; }
    public int? SteviloVsSatnic { get; set; }
    public int? donosMedu { get; set; }
    public bool? MenjavaMatice { get; set; }
    public DateTime Datum { get; set; }

    [ForeignKey("Panj")]
    public int PanjId { get; set; }
    public Panj? Panj { get; set; }
}