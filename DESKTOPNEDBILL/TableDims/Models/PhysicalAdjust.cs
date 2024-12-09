using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TableDims.Models
{
    public class PhysicalAdjust
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TranId { get; set; }
        public DateTime Physdate { get; set; }
        public int UserId { get; set; }
        public string Narration { get; set; }
        public bool Status { get; set; }
    }
}
