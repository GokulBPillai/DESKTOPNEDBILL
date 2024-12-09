
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDims.Models.Entities
{
    public class UnitMeasure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UnitMeasureDescription { get; set; }
        public bool Status { get; set; }
        public string CompanyCode { get; set; }
    }
}
