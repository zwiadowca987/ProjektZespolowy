using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class OrderDetails
    {
        [Key, Required]
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa od 0.")]
        public int Quantity { get; set; }

        // Relations
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
