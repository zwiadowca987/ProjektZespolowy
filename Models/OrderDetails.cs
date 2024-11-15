using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class OrderDetails
    {
        public int quantity { get; set; }

        // Relations
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
