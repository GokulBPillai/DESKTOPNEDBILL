using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDims.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 5001)]
        public int EmployeeId { get; set; }
        [MaxLength(150)]
        public string EmployeeName { get; set; }
        [MaxLength(150)]
        public string Address1 { get; set; }
        [MaxLength(150)]
        public string Address2 { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string Education { get; set; }
        public DateTime DOB { get; set; }
        public string PANNO { get; set; }
        public string UANNO { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("EmployeePosition")]
        public int EmpPositionId { get; set; }
        public DateTime DateofJoined { get; set; }
        public bool EmpStatus { get; set; }
        public string EmpType { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal TAllowance { get; set; }
        public decimal DAllowance { get; set; }
        public decimal HRAllowance { get; set; }
        public string Others { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal SalaryAdvance { get; set; }
        public string Nominee { get; set; }
        public string Relation { get; set; }
        public Department Department { get; set; }
        public EmployeePosition EmployeePosition { get; set; }
    }
}
