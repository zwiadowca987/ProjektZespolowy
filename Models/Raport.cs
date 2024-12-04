using System.ComponentModel.DataAnnotations;
namespace ProjektZespolowy.Models
{
    public class Raport
    {
        [Key]
        public int RaportId { get; set; }

        [Required]
        public string Typ { get; set; } 

        public DateTime DataWygenerowania { get; set; } = DateTime.Now; 

        public string Uwagi { get; set; } 

        public ICollection<RaportItem> RaportItems { get; set; } = new List<RaportItem>();
    }
}