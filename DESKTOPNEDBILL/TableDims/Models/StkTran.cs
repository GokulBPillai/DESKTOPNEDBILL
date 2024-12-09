using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models.Entities
{
    [Table("StkTran")]
    public class StkTran
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TranId { get; set; }       
        public int InvNo { get; set; }
        public DateTime TranDate { get; set; }
        [MaxLength(15)]
        public string TranRef { get; set; }
        [MaxLength(15)]
        public string TranType { get; set; }       
        public int StocksId { get; set; }        
        public int BatchId { get; set; }
        public int DptId { get; set; }
        public decimal StkRetail { get; set; }
        public decimal StkCost { get; set; }
        public decimal QtyIn { get; set; }
        public decimal QtyOut { get; set; }
        public decimal GST { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscPer { get; set; }
        [MaxLength(30)]
        public string StkLocation { get; set; }
        [MaxLength(20)]
        public string DiscountType { get; set; }
        [MaxLength(20)]
        public string HSNCode { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal CessAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal AddlCess { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        [MaxLength(50)]
        public string BarcodeNo { get; set; }       
        [MaxLength(15)]
        public string FinYear { get; set; }
        public string CompanyCode { get; set; }        
        public virtual Stocks Stocks { get; set; }  
        public virtual Batch Batch { get; set; }
        [MaxLength(25)]
        public string InvoiceRef { get; set; }
        public int UnitMeasure { get; set; }
        public DateTime Expiry { get; set; }

    }
}
