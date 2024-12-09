using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TableDims.Models.Entities
{
    public class SalesReceipt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RcpNo { get; set; }       
        public DateTime TranDate { get; set; }
        public decimal RcpAmount { get; set; }
        public int CustomerId { get; set; }
        public int GnlId { get; set; }
        [MaxLength(50)]
        public string FinYear {  get; set; }
        [MaxLength(50)]
        public string CompanyCode { get; set; }        
    }
}
