using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class GlAccount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GnlId { get; set; }
        [MaxLength(100)]
        public  string GnlCatg { get; set;}
        [MaxLength(100)]
        public  string GnlDesc { get; set; }
        [MaxLength(50)]
        public  string GnlCode { get; set; }
        public decimal GnlMybal { get; set; }
        public decimal GnlOpBal { get; set;}
        [MaxLength(50)]
        public string GnlStat { get; set; }
        public int GroupOrder { get; set; }
        public decimal GnlBudget { get; set;}
        public int MainHead { get; set;}
        public decimal Amt1 { get; set;}
        public decimal Amt2 { get; set;}
    }
}
