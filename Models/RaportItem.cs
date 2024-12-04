using ProjektZespolowy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class RaportItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RaportItemId { get; set; }


    [Required]
    public int RaportId { get; set; } 

    [Required]
    public int ProduktId { get; set; }

    [Required]
    public string NazwaProduktu { get; set; }

    [Required]
    public int Ilosc { get; set; } 

    [ForeignKey("RaportId")]
    public Raport? Raport { get; set; }
}
