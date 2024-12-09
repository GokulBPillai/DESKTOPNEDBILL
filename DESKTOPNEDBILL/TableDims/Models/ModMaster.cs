
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDims.Models
{
    public class ModMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModId { get; set; }
        [MaxLength(150)]
        public string ModName { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
        public string CompanyCode { get; set; }
        public int OrderNo { get; set; }
    }
}
