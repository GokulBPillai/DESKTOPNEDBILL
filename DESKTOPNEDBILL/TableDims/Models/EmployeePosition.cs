using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class EmployeePosition
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpPositionId { get; set; }
        [MaxLength(150)]
        public string EmpPositionName { get; set; }
        public bool Status { get; set; }
    }
}
