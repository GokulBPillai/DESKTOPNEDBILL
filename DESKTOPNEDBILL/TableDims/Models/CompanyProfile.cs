using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDims.Models
{
    public class CompanyProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 3001)]
        public int CompanyId { get; set; }
        [MaxLength(150)]
        public string CompanyName { get; set; }
        [MaxLength(150)]
        public string Address1 { get; set; }
        [MaxLength(150)]
        public string Address2 { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        [MaxLength(20)]
        public string PIN { get; set; }
        [MaxLength(50)]
        public string PhoneNo { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        public int GSTType { get; set; }
        public string GSTNo { get; set; }
        [MaxLength(50)]
        public string EmailId { get; set; }
        public string Web { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyType { get; set; }
        public string CompanyNature { get; set; }
        public string BusinessType { get; set; }
        public string PrintMode { get; set; }
    }
}
