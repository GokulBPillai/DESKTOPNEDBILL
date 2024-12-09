namespace TableDims.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mFourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseRetMaster",
                c => new
                    {
                        PurRetNo = c.Int(nullable: false, identity: true),
                        PurInvNo = c.Int(nullable: false),
                        RetDate = c.DateTime(nullable: false),
                        InvDate = c.DateTime(nullable: false),
                        VendID = c.Int(nullable: false),
                        InvAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvPaymode = c.String(maxLength: 15),
                        PayBankIdNo = c.Int(nullable: false),
                        PayNo = c.Int(nullable: false),
                        Handling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HandlingPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VendStatus = c.String(maxLength: 15),
                        InvRef = c.String(maxLength: 20),
                        TotalCessAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalIGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddlCess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddlDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FinYear = c.String(maxLength: 15),
                        CompanyCode = c.String(maxLength: 15),
                        BillType = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.PurRetNo)
                .ForeignKey("dbo.Vendors", t => t.VendID, cascadeDelete: true)
                .Index(t => t.VendID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRetMaster", "VendID", "dbo.Vendors");
            DropIndex("dbo.PurchaseRetMaster", new[] { "VendID" });
            DropTable("dbo.PurchaseRetMaster");
        }
    }
}
