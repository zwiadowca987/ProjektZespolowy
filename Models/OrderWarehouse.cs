using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektZespolowy.Models
{
    public class OrderWarehouse
    {
        [Key]
        public int ZamMag { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public int ProduktId { get; set; }
        public string NazwaProduktu { get; set; }
        public int DostawcaID { get; set; }
        public int DoZamowienia { get; set; }
        public string StatusDostawy { get; set; }
        [MaxLength(500)]
        public string? Uwagi { get; set; }
        public decimal WartoscZamowienia { get; set; }
        public Supplier? Supplier { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
