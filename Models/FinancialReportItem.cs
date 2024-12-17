using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class FinancialReportItem
    {
        [Key]
        public int FinancialReportItemId { get; set; }
        [Required]
        public int FinancialReportId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public Decimal Value { get; set; }

        [Required]
        public String Flow {  get; set; }

        public FinancialReport? FinancialReport { get; set; }
    }
}
