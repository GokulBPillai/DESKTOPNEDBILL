using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class SubModMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubModId { get; set; }
        [ForeignKey("ModMaster")]
        public int ModId { get; set; }
        [MaxLength(150)]
        public string SubModName { get; set; }
        public string DisplayName { get; set; }
        public bool Status { get; set; }
        public string CompanyCode { get; set; }
        public ModMaster ModMaster { get; set; }
        public int OrderNo { get; set; }
    }
}
