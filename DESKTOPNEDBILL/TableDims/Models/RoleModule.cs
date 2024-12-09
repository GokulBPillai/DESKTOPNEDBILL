using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class RoleModule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("RoleMaster")]
        public int RoleId { get; set; }
        [ForeignKey("ModMaster")]
        public int ModId { get; set; }
        public bool Status { get; set; }
        public string CompanyCode { get; set; }
        public RoleMaster RoleMaster { get; set; }
        public ModMaster ModMaster { get; set; }
    }
}
