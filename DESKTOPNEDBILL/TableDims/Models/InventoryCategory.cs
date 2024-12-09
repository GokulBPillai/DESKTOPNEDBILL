using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDims.Models
{
    public class InventoryCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryCategoryId { get; set; }       
        public string CategoryName { get; set; }
        public int InventoryTypId { get; set; }
        public bool Status { get; set; }
        public InventoryType InventoryType { get; set; }
    }
}
