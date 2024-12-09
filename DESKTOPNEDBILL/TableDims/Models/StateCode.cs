
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDims.Models
{
    public class StateCode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateCodeId { get; set; }
        public string State { get; set; }
        public string StatecodeNo { get; set;}
    }
}
