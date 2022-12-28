using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeeOrganizer.Models;

public class Dogodek
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public string Naziv { get; set; }
    public string Lokacija { get; set; }

    public string? Opis  { get; set; }

    [ForeignKey("Drustvo")]
    public int DrustvoId { get; set; }
    public Drustvo? Drustvo { get; set; }
    
    
}