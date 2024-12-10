using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektZespolowy.Models
{
    public class FinancialReportItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RaportItemId { get; set; }

        [Required]
        public int RaportId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public Decimal Value { get; set; }

        [Required]
        public String Flow {  get; set; }

        [ForeignKey("FinancialReportId")]
        public FinancialReport? FinancialReport { get; set; }
    }
}
