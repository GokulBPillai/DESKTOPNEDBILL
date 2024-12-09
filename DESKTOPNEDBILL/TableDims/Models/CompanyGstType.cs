using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models.Entities
{
    public class CompanyGstType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyGstTypeId { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}
