using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Product
    {
        [Key, Required]
        public int ProductId { get; set; }

        public string NazwaProduktu { get; set; }
        public string? Opis { get; set; }
        public string? Producent { get; set; }
        public decimal Cena { get; set; }

        // Relations
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
