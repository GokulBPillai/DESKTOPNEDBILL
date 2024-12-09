using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class RoleSubModule
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("RoleMaster")]
        public int RoleId { get; set; }
        public int SubModId { get; set; }
        public int ModId { get; set; }
        public bool Status { get; set; }
        public string CompanyCode { get; set; }
        public RoleMaster RoleMaster { get; set; }
    }
}
