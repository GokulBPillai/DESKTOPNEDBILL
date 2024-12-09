using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TableDims.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [MaxLength(150)]
        public  string CustomerName { get; set; }
        [MaxLength(150)]
        public string Address1 { get; set; }
        [MaxLength(150)]
        public string Address2 { get; set; }
        [MaxLength(15)]
        public string ContactNo { get; set; }
        public int PinCode { get; set; }
        [MaxLength(50)]
        public string EmailId { get; set; }
        [MaxLength(20)]
        public string GSTNo { get; set; }
        [MaxLength(30)]
        public string PaymentInTerms { get; set; }        
        public decimal Creditlimit { get; set; }        
        public decimal OpeningBalance { get; set; }
        [MaxLength(20)]
        public string PaymentType { get; set; }
        public  bool Status { get; set; }
        [MaxLength(20)]
        public string TaxType { get; set; }
        [MaxLength(20)]
        public string Location { get; set; }        
        public decimal TotalBalance { get; set; }       
        [MaxLength(20)]
        public string PriceGroup { get; set; }
        public int StateCode { get; set; }
        public StateCode State { get; set; }

    }
}
