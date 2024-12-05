using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Warehouse
    {
        [Key]
        public int ProduktId { get; set; } 
        public string NazwaProduktu { get; set; }
        public int DostepnaIlosc { get; set; }
        public int Pojemnosc { get; set; }
        public Product? Product { get; set; }
        public ICollection<OrderWarehouse>? OrderWarehouses { get; set; }

    }
}
