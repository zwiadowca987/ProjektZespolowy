using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class FinancialReport
    {
        [Key, Required]
        public int Id { get; set; }
        public String Name { get; set; }
        public DateOnly CreationDate { get; set; }
        public ICollection<FinancialReportItem> FinancialReportItems { get; set; } = new List<FinancialReportItem>();
    }
}
