using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class EmpCtrl
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpCode { get; set; }
        public int EmpId { get; set; }
        [MaxLength(150)]
        public string UserID { get; set; }
        [MaxLength(150)]
        public string Password { get; set; }
        [MaxLength(150)]
        public string EmpName { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string Remarks { get; set; }
        public bool IsLocked { get; set; }
        public bool IsAdmin { get; set; }
        [ForeignKey("RoleMaster")]
        public int RoleId { get; set; }
        public string CompanyCode { get; set; }
        public virtual RoleMaster RoleMaster { get; set; }
    }
}
