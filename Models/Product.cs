using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Product
    {
        [Key, Required]
        public int Id { get; set; }

        // Relations
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
