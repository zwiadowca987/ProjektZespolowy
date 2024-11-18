using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Order
    {
        [Key, Required]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        // Relations
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
