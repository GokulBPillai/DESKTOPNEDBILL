using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TableDims.Models
{
    [Table("Batch")]
    public class Batch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BatchId { get; set; }
        public  string BatchName { get; set; }
        [ForeignKey("Stocks")]
        public int StockId { get; set; }
        public int VendId { get; set;}
        public DateTime Expiry { get; set; }        
        public decimal StockOnHand { get; set;}
        public decimal OpeningStock { get;set;}
        public decimal StockIn { get;set;}
        public decimal StockOut { get; set;}
        public decimal PhyStock { get; set; }
        public decimal PurchasePrice { get;set;}        
        public decimal OPCost { get;set;}        
        public decimal SellingPriceA { get; set;}        
        public decimal SellingPriceB { get; set; }        
        public decimal SellingPriceC { get; set; }        
        public decimal Mrp { get; set;}        
        public decimal Cess { get; set;}        
        public decimal GST { get; set; }        
        public decimal AddlCess { get; set;}
        public string Barcode { get; set;}
        public int StkLocation { get; set; }
        public bool Status { get; set; }
        public Stocks Stocks { get; set;}
    }
}
