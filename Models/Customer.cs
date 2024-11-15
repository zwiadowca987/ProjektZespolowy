using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Customer
    {
        [Key, Required]
        public int Id { get; set; }

        // Relations
        public ICollection<Order> Orders { get; set; }
    }
}
