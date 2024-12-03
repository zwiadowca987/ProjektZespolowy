using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Customer
    {
        [Key, Required]
        public int CustomerId { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }

        // Relations
        public ICollection<Order> Orders { get; set; }
    }
}
