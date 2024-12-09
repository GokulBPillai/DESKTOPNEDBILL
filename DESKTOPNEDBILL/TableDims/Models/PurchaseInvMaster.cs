﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TableDims.Models
{
    [Table("PurchaseInvMaster")]
    public class PurchaseInvMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurInvNo { get; set; }
        public DateTime InvDate { get; set; }
        [ForeignKey("Vendor")]
        public int VendID { get; set; }
        public decimal InvAmount { get; set;}
        [MaxLength(15)]
        public string InvPaymode { get; set;}
        public int PayBankIdNo { get; set;}
        public int PayNo { get; set; }   
        public decimal Handling { get;set;}
        public decimal HandlingPer { get; set; }
        public decimal TotDiscount { get; set;}
        public decimal RoundAmount { get; set;}
        [MaxLength(15)]
        public string VendStatus { get; set;}
        [MaxLength(20)]
        public string InvRef { get; set;}
        public decimal TotalCessAmount { get; set; }
        public decimal TotalGSTAmount { get; set; }
        public decimal TotalCGSTAmount { get; set; }
        public decimal TotalSGSTAmount { get; set; }
        public decimal TotalIGSTAmount { get; set; }
        public decimal AddlCess { get; set;}
        public decimal AddlDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal NetTotal { get; set; }
        [MaxLength(15)]
        public string FinYear { get; set; }
        [MaxLength(15)]
        public string CompanyCode { get; set; }
        [MaxLength(15)]
        public string BillType { get; set; }
        public Vendor Vendor { get; set; }
    }
}
