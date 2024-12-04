using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektZespolowy.Models
{
    public class Product
    {
        [Key]
        public int ProduktId { get; set; }
        public string NazwaProduktu { get; set; }
        public string? Opis { get; set; }
        public string? Producent { get; set; }
        public decimal Cena { get; set; }

        // Relations
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
