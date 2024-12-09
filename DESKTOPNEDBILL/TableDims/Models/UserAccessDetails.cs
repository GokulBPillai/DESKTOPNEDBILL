using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class UserAccessDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LoginTime { get; set; }
        public int ModIdAccessed { get; set; }
        public int SubModIdAccessed { get; set; }
        [MaxLength(150)]
        public string FormAccessed { get; set; }
        public DateTime LogOutDate { get; set; }
        public DateTime LogOutTime { get; set;}
        public virtual RoleMaster RoleMaster { get; set; }
        public virtual ModMaster ModMaster { get; set; }
        public virtual SubModMaster SubModMaster { get; set; }

    }
}
