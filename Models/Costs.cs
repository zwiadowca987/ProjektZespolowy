using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class Costs
    {
        [Key, Required]
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal Cost { get; set; }
        public DateOnly Period { get; set; }
    }
}
