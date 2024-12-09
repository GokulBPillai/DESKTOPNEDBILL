using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TableDims.Models
{
    public class Bank
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public string BankAddress { get; set; }
        public string PinCode {  get; set; }
        public string IFSC { get; set; }
        public string PhoneNo { get; set; }
        public int GnlId { get; set; }
        public string ChqStNo { get; set;}
        public string ChqEdNo { get; set; }
        public string BankAbbrv { get;set; }
        public string EmailId { get;set; }
        public string CreditLimit { get;set; }
        public bool Status {  get; set; }   
        public bool IsBank {  get; set; }
    }
}
