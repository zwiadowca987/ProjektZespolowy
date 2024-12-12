using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class FinancialReport
    {
        [Key, Required]
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public ICollection<FinancialReportItem> FinancialReportItems { get; set; } = new List<FinancialReportItem>();
    }
}
