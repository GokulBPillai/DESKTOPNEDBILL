using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TableDims.Models.Entities
{
    [Table("vw_QryStkDaySummary")]
    public class QryStkDaySummary
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime TranDate { get; set; }
        public int BillFrom { get; set; }
        public int BillTo { get; set; }       
        public decimal Amount0Per { get; set; }        
        public decimal Tax0Per { get; set; }        
        public decimal Tax5Per { get; set; }        
        public decimal Amount5Per { get; set; }        
        public decimal Tax12Per { get; set; }        
        public decimal Amount12Per { get; set; }        
        public decimal Tax18Per { get; set; }        
        public decimal Amount18Per { get; set; }        
        public decimal Tax28Per { get; set; }        
        public decimal Amount28Per { get; set; }        
        public decimal InvoiceAmt { get; set; }        
        public decimal cess { get; set; }
    }
}
