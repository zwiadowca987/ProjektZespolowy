using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Supplier
    {
        [Key]
        public int DostawcaID { get; set; }
        public string? NazwaDostawcy { get; set; }
        public string? Adres { get; set; }
        public string? Kontakt { get; set; }
        public ICollection<OrderWarehouse>? OrderWarehouses { get; set; }
    }
}
