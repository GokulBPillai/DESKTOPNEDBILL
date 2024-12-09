using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDims.Models
{
    public class Vendor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorId { get; set; }
        public  string VendorName { get; set; }
        public string VendorAdd1 { get; set;}
        public string VendorAdd2 { get; set;}
        public int PIN { get; set;}
        public string PhoneNo { get; set; }        
        public string GSTNo { get;set;}
        public string VendorType { get;set;}
        public decimal OpBal { get; set; }
        public decimal TotalBal { get; set; }
        public bool Status { get; set; }
        public string CompanyCode { get; set; }
        public int StateCode { get; set; }
    }
}
