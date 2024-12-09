using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDims.Models
{
    [Table("FYearTrans")]
    public class FYearTrans
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TranId { get; set; }
        [ForeignKey("CompanyProfile")]
        public int CompanyId { get; set; }
        public DateTime FYStart { get; set;}
        public DateTime FYEnd { get; set;}
        [MaxLength(50)]
        public  string FYear { get; set; }
        [MaxLength(10)]
        public  string FYearDir { get; set; }
        [MaxLength(50)]
        public string Remarks { get; set;}
        [MaxLength(20)]
        public  string DatabaseName { get; set;}
        [MaxLength(50)]
        public  string ServerName { get; set; }
        [MaxLength(50)]
        public string UI { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(10)]
        public  string YearEndStatus { get; set; }  
        public CompanyProfile CompanyProfile { get; set; }
        public string CompanyCode { get; set; }
        //public virtual ICollection<CompanyProfile> CompanyProfile { get; set; }
    }
}
