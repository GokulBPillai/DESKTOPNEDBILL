using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDims.Models
{
    [Table("Stock")]
    public class Stocks
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockId { get; set; }
        [MaxLength(150)]
        public  string StockName { get; set; }
        [MaxLength(150)]
        public string BrandName { get; set; }
        public int UnitMeasure { get; set;}
        public decimal MinLevel { get; set;}
        public decimal MaxLevel { get; set;}
        [ForeignKey("Vendor")]
        public int VendId { get; set;}
        public bool Status { get; set;}
        public Vendor Vendor { get; set; }               
        public string HSNCode { get; set; }
        public string Barcode { get; set; }
        public string CompanyCode { get; set; }
        [ForeignKey("InventoryCategory")]
        public int CategoryId { get; set; }// Cosmetics, grocery, bakery,hardware etc. 
        [ForeignKey("InventoryType")]
        public int StkType { get; set; }// Inventory, Trial Item, Finished goods, Raw Materials etc. 
        public virtual InventoryCategory InventoryCategory { get; set; }
        public virtual InventoryType InventoryType { get; set; }
    }
}
