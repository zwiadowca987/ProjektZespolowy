using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Employee
    {
        [Key]
        public int PracownikID { get; set; }


        public string Imie { get; set; }


        public string Nazwisko { get; set; }

        [Required]
        public string Stanowisko { get; set; }
        public string AdresEmail { get; set; }

        public string AdresZamieszkania { get; set; }

        public string Login { get; set; }

        public string Haslo { get; set; } 
    }
}
