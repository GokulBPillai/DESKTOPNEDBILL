using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class InventoryType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryTypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsStkDeduct {  get; set; }
        public bool Status { get; set; }
    }
}
