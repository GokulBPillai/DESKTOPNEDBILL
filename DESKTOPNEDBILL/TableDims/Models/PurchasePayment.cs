using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace TableDims.Models
{
    public class PurchasePayment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PayNo { get; set; }
        public DateTime TranDate { get; set; }
        public decimal PayAmount { get; set; }
        public int VendorId { get; set; }
        public int GnlId { get; set; }
        [MaxLength(50)]
        public string FinYear { get; set; }
        [MaxLength(50)]
        public string CompanyCode { get; set; }
    }
}
