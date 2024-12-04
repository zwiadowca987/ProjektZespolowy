using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
