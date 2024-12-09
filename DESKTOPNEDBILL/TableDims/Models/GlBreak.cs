using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TableDims.Models
{
    [Table("GlBreak")]
    public class GlBreak
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }      
        public int GnlId { get; set; }       
        public DateTime TranDate { get; set; }
        [MaxLength(50)]
        public string TranType { get; set; }
        [MaxLength(50)]
        public string GlModule { get; set; }
        [MaxLength(50)]
        public string TranRef { get; set; }
        public int IDNo { get; set; }
        public int DptId { get; set; }
        public decimal Amt1 { get; set; }
        public decimal Amt2 { get; set; }
        [MaxLength(50)]
        public string AmtMode { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        [MaxLength(50)]
        public string DEnd { get; set; }
        [MaxLength(50)]
        public string LedAc { get; set; }
        [MaxLength(15)]
        public string FinYear { get; set; }
        [MaxLength(50)]
        public string CompanyCode { get; set; } 
        public int InvoiceNo { get; set; }
    }
}
