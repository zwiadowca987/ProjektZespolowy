using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Order
    {
        [Key, Required]
        public int OrderId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public string Status { get; set; }

        // Relations
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
