namespace TableDims.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        BankBranch = c.String(),
                        AccountNo = c.String(),
                        AccountType = c.String(),
                        BankAddress = c.String(),
                        PinCode = c.String(),
                        IFSC = c.String(),
                        PhoneNo = c.String(),
                        GnlId = c.Int(nullable: false),
                        ChqStNo = c.String(),
                        ChqEdNo = c.String(),
                        BankAbbrv = c.String(),
                        EmailId = c.String(),
                        CreditLimit = c.String(),
                        Status = c.Boolean(nullable: false),
                        IsBank = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.Batch",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        BatchName = c.String(),
                        StockId = c.Int(nullable: false),
                        VendId = c.Int(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                        StockOnHand = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpeningStock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StockOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhyStock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OPCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPriceA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPriceB = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellingPriceC = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mrp = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GST = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddlCess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Barcode = c.String(),
                        StkLocation = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BatchId)
                .ForeignKey("dbo.Stock", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(maxLength: 150),
                        BrandName = c.String(maxLength: 150),
                        UnitMeasure = c.Int(nullable: false),
                        MinLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxLevel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VendId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        HSNCode = c.String(),
                        Barcode = c.String(),
                        CompanyCode = c.String(),
                        CategoryId = c.Int(nullable: false),
                        StkType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.InventoryCategories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.InventoryTypes", t => t.StkType, cascadeDelete: true)
                .ForeignKey("dbo.Vendors", t => t.VendId, cascadeDelete: true)
                .Index(t => t.VendId)
                .Index(t => t.CategoryId)
                .Index(t => t.StkType);
            
            CreateTable(
                "dbo.InventoryCategories",
                c => new
                    {
                        InventoryCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        InventoryTypId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        InventoryType_InventoryTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.InventoryCategoryId)
                .ForeignKey("dbo.InventoryTypes", t => t.InventoryType_InventoryTypeId)
                .Index(t => t.InventoryType_InventoryTypeId);
            
            CreateTable(
                "dbo.InventoryTypes",
                c => new
                    {
                        InventoryTypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        IsStkDeduct = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryTypeId);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        VendorName = c.String(),
                        VendorAdd1 = c.String(),
                        VendorAdd2 = c.String(),
                        PIN = c.Int(nullable: false),
                        PhoneNo = c.String(),
                        GSTNo = c.String(),
                        VendorType = c.String(),
                        OpBal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalBal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                        StateCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VendorId);
            
            CreateTable(
                "dbo.CompanyGstTypes",
                c => new
                    {
                        CompanyGstTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyGstTypeId);
            
            CreateTable(
                "dbo.CompanyProfiles",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 150),
                        Address1 = c.String(maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        State = c.Int(nullable: false),
                        City = c.String(),
                        PIN = c.String(maxLength: 20),
                        PhoneNo = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        GSTType = c.Int(nullable: false),
                        GSTNo = c.String(),
                        EmailId = c.String(maxLength: 50),
                        Web = c.String(),
                        CompanyCode = c.String(),
                        CompanyType = c.String(),
                        CompanyNature = c.String(),
                        BusinessType = c.String(),
                        PrintMode = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 150),
                        Address1 = c.String(maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        ContactNo = c.String(maxLength: 15),
                        PinCode = c.Int(nullable: false),
                        EmailId = c.String(maxLength: 50),
                        GSTNo = c.String(maxLength: 20),
                        PaymentInTerms = c.String(maxLength: 30),
                        Creditlimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentType = c.String(maxLength: 20),
                        Status = c.Boolean(nullable: false),
                        TaxType = c.String(maxLength: 20),
                        Location = c.String(maxLength: 20),
                        TotalBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceGroup = c.String(maxLength: 20),
                        StateCode = c.Int(nullable: false),
                        State_StateCodeId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.StateCodes", t => t.State_StateCodeId)
                .Index(t => t.State_StateCodeId);
            
            CreateTable(
                "dbo.StateCodes",
                c => new
                    {
                        StateCodeId = c.Int(nullable: false, identity: true),
                        State = c.String(),
                        StatecodeNo = c.String(),
                    })
                .PrimaryKey(t => t.StateCodeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.EmpCtrls",
                c => new
                    {
                        EmpCode = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        UserID = c.String(maxLength: 150),
                        Password = c.String(maxLength: 150),
                        EmpName = c.String(maxLength: 150),
                        Address = c.String(maxLength: 150),
                        Phone = c.String(maxLength: 50),
                        Remarks = c.String(maxLength: 50),
                        IsLocked = c.Boolean(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.EmpCode)
                .ForeignKey("dbo.RoleMasters", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleMasters",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(maxLength: 150),
                        Address1 = c.String(maxLength: 150),
                        Address2 = c.String(maxLength: 150),
                        PhoneNo = c.String(),
                        EmailId = c.String(),
                        Gender = c.String(),
                        Religion = c.String(),
                        Nationality = c.String(),
                        Education = c.String(),
                        DOB = c.DateTime(nullable: false),
                        PANNO = c.String(),
                        UANNO = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        EmpPositionId = c.Int(nullable: false),
                        DateofJoined = c.DateTime(nullable: false),
                        EmpStatus = c.Boolean(nullable: false),
                        EmpType = c.String(),
                        BasicSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HRAllowance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Others = c.String(),
                        OpeningBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalaryAdvance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nominee = c.String(),
                        Relation = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.EmployeePositions", t => t.EmpPositionId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.EmpPositionId);
            
            CreateTable(
                "dbo.EmployeePositions",
                c => new
                    {
                        EmpPositionId = c.Int(nullable: false, identity: true),
                        EmpPositionName = c.String(maxLength: 150),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpPositionId);
            
            CreateTable(
                "dbo.FYearTrans",
                c => new
                    {
                        TranId = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        FYStart = c.DateTime(nullable: false),
                        FYEnd = c.DateTime(nullable: false),
                        FYear = c.String(maxLength: 50),
                        FYearDir = c.String(maxLength: 10),
                        Remarks = c.String(maxLength: 50),
                        DatabaseName = c.String(maxLength: 20),
                        ServerName = c.String(maxLength: 50),
                        UI = c.String(maxLength: 50),
                        Password = c.String(maxLength: 50),
                        YearEndStatus = c.String(maxLength: 10),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.TranId)
                .ForeignKey("dbo.CompanyProfiles", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.GlAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GnlId = c.Int(nullable: false),
                        GnlCatg = c.String(maxLength: 100),
                        GnlDesc = c.String(maxLength: 100),
                        GnlCode = c.String(maxLength: 50),
                        GnlMybal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GnlOpBal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GnlStat = c.String(maxLength: 50),
                        GroupOrder = c.Int(nullable: false),
                        GnlBudget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MainHead = c.Int(nullable: false),
                        Amt1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amt2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GlBreak",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GnlId = c.Int(nullable: false),
                        TranDate = c.DateTime(nullable: false),
                        TranType = c.String(maxLength: 50),
                        GlModule = c.String(maxLength: 50),
                        TranRef = c.String(maxLength: 50),
                        IDNo = c.Int(nullable: false),
                        DptId = c.Int(nullable: false),
                        Amt1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amt2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmtMode = c.String(maxLength: 50),
                        Status = c.String(maxLength: 50),
                        DEnd = c.String(maxLength: 50),
                        LedAc = c.String(maxLength: 50),
                        FinYear = c.String(maxLength: 15),
                        CompanyCode = c.String(maxLength: 50),
                        InvoiceNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ModMasters",
                c => new
                    {
                        ModId = c.Int(nullable: false, identity: true),
                        ModName = c.String(maxLength: 150),
                        DisplayName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                        OrderNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModId);
            
            CreateTable(
                "dbo.PhysicalAdjusts",
                c => new
                    {
                        TranId = c.Int(nullable: false, identity: true),
                        Physdate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        Narration = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TranId);
            
            CreateTable(
                "dbo.PurchaseInvMaster",
                c => new
                    {
                        PurInvNo = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.PurInvNo)
                .ForeignKey("dbo.Vendors", t => t.VendID, cascadeDelete: true)
                .Index(t => t.VendID);
            
            CreateTable(
                "dbo.PurchasePayments",
                c => new
                    {
                        PayNo = c.Int(nullable: false, identity: true),
                        TranDate = c.DateTime(nullable: false),
                        PayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VendorId = c.Int(nullable: false),
                        GnlId = c.Int(nullable: false),
                        FinYear = c.String(maxLength: 50),
                        CompanyCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PayNo);
            
            CreateTable(
                "dbo.RoleModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        ModId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ModMasters", t => t.ModId, cascadeDelete: true)
                .ForeignKey("dbo.RoleMasters", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ModId);
            
            CreateTable(
                "dbo.RoleSubModules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        SubModId = c.Int(nullable: false),
                        ModId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleMasters", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SalesInvMasters",
                c => new
                    {
                        SalesInvNo = c.Int(nullable: false, identity: true),
                        InvDate = c.DateTime(nullable: false),
                        CustID = c.Int(nullable: false),
                        InvAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvPaymode = c.String(maxLength: 15),
                        PayBankIdNo = c.Int(nullable: false),
                        RcpNo = c.Int(nullable: false),
                        Handling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HandlingPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustStatus = c.String(maxLength: 15),
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
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesInvNo)
                .ForeignKey("dbo.Customers", t => t.CustID, cascadeDelete: true)
                .Index(t => t.CustID);
            
            CreateTable(
                "dbo.SalesReceipts",
                c => new
                    {
                        RcpNo = c.Int(nullable: false, identity: true),
                        TranDate = c.DateTime(nullable: false),
                        RcpAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                        GnlId = c.Int(nullable: false),
                        FinYear = c.String(maxLength: 50),
                        CompanyCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RcpNo);
            
            CreateTable(
                "dbo.SalesRetMasters",
                c => new
                    {
                        SalesRetNo = c.Int(nullable: false, identity: true),
                        SalesInvNo = c.Int(nullable: false),
                        RetDate = c.DateTime(nullable: false),
                        InvDate = c.DateTime(nullable: false),
                        CustID = c.Int(nullable: false),
                        InvAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvPaymode = c.String(maxLength: 15),
                        PayBankIdNo = c.Int(nullable: false),
                        RcpNo = c.Int(nullable: false),
                        Handling = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HandlingPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RoundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustStatus = c.String(maxLength: 15),
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
                        EmpId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesRetNo)
                .ForeignKey("dbo.Customers", t => t.CustID, cascadeDelete: true)
                .Index(t => t.CustID);
            
            CreateTable(
                "dbo.StkTran",
                c => new
                    {
                        TranId = c.Int(nullable: false, identity: true),
                        InvNo = c.Int(nullable: false),
                        TranDate = c.DateTime(nullable: false),
                        TranRef = c.String(maxLength: 15),
                        TranType = c.String(maxLength: 15),
                        StocksId = c.Int(nullable: false),
                        BatchId = c.Int(nullable: false),
                        DptId = c.Int(nullable: false),
                        StkRetail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StkCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtyIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QtyOut = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GST = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscPer = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StkLocation = c.String(maxLength: 30),
                        DiscountType = c.String(maxLength: 20),
                        HSNCode = c.String(maxLength: 20),
                        GSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CessAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IGSTAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddlCess = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BarcodeNo = c.String(maxLength: 50),
                        FinYear = c.String(maxLength: 15),
                        CompanyCode = c.String(),
                        InvoiceRef = c.String(maxLength: 25),
                        UnitMeasure = c.Int(nullable: false),
                        Expiry = c.DateTime(nullable: false),
                        Stocks_StockId = c.Int(),
                    })
                .PrimaryKey(t => t.TranId)
                .ForeignKey("dbo.Batch", t => t.BatchId, cascadeDelete: true)
                .ForeignKey("dbo.Stock", t => t.Stocks_StockId)
                .Index(t => t.BatchId)
                .Index(t => t.Stocks_StockId);
            
            CreateTable(
                "dbo.SubModMasters",
                c => new
                    {
                        SubModId = c.Int(nullable: false, identity: true),
                        ModId = c.Int(nullable: false),
                        SubModName = c.String(maxLength: 150),
                        DisplayName = c.String(),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                        OrderNo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubModId)
                .ForeignKey("dbo.ModMasters", t => t.ModId, cascadeDelete: true)
                .Index(t => t.ModId);
            
            CreateTable(
                "dbo.UnitMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnitMeasureDescription = c.String(),
                        Status = c.Boolean(nullable: false),
                        CompanyCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QryStkDaySummary",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TranDate = c.DateTime(nullable: false),
                        BillFrom = c.Int(nullable: false),
                        BillTo = c.Int(nullable: false),
                        Amount0Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax0Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax5Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount5Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax12Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount12Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax18Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount18Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax28Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount28Per = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        cess = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubModMasters", "ModId", "dbo.ModMasters");
            DropForeignKey("dbo.StkTran", "Stocks_StockId", "dbo.Stock");
            DropForeignKey("dbo.StkTran", "BatchId", "dbo.Batch");
            DropForeignKey("dbo.SalesRetMasters", "CustID", "dbo.Customers");
            DropForeignKey("dbo.SalesInvMasters", "CustID", "dbo.Customers");
            DropForeignKey("dbo.RoleSubModules", "RoleId", "dbo.RoleMasters");
            DropForeignKey("dbo.RoleModules", "RoleId", "dbo.RoleMasters");
            DropForeignKey("dbo.RoleModules", "ModId", "dbo.ModMasters");
            DropForeignKey("dbo.PurchaseInvMaster", "VendID", "dbo.Vendors");
            DropForeignKey("dbo.FYearTrans", "CompanyId", "dbo.CompanyProfiles");
            DropForeignKey("dbo.Employees", "EmpPositionId", "dbo.EmployeePositions");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EmpCtrls", "RoleId", "dbo.RoleMasters");
            DropForeignKey("dbo.Customers", "State_StateCodeId", "dbo.StateCodes");
            DropForeignKey("dbo.Batch", "StockId", "dbo.Stock");
            DropForeignKey("dbo.Stock", "VendId", "dbo.Vendors");
            DropForeignKey("dbo.Stock", "StkType", "dbo.InventoryTypes");
            DropForeignKey("dbo.Stock", "CategoryId", "dbo.InventoryCategories");
            DropForeignKey("dbo.InventoryCategories", "InventoryType_InventoryTypeId", "dbo.InventoryTypes");
            DropIndex("dbo.SubModMasters", new[] { "ModId" });
            DropIndex("dbo.StkTran", new[] { "Stocks_StockId" });
            DropIndex("dbo.StkTran", new[] { "BatchId" });
            DropIndex("dbo.SalesRetMasters", new[] { "CustID" });
            DropIndex("dbo.SalesInvMasters", new[] { "CustID" });
            DropIndex("dbo.RoleSubModules", new[] { "RoleId" });
            DropIndex("dbo.RoleModules", new[] { "ModId" });
            DropIndex("dbo.RoleModules", new[] { "RoleId" });
            DropIndex("dbo.PurchaseInvMaster", new[] { "VendID" });
            DropIndex("dbo.FYearTrans", new[] { "CompanyId" });
            DropIndex("dbo.Employees", new[] { "EmpPositionId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.EmpCtrls", new[] { "RoleId" });
            DropIndex("dbo.Customers", new[] { "State_StateCodeId" });
            DropIndex("dbo.InventoryCategories", new[] { "InventoryType_InventoryTypeId" });
            DropIndex("dbo.Stock", new[] { "StkType" });
            DropIndex("dbo.Stock", new[] { "CategoryId" });
            DropIndex("dbo.Stock", new[] { "VendId" });
            DropIndex("dbo.Batch", new[] { "StockId" });
            DropTable("dbo.QryStkDaySummary");
            DropTable("dbo.UnitMeasures");
            DropTable("dbo.SubModMasters");
            DropTable("dbo.StkTran");
            DropTable("dbo.SalesRetMasters");
            DropTable("dbo.SalesReceipts");
            DropTable("dbo.SalesInvMasters");
            DropTable("dbo.RoleSubModules");
            DropTable("dbo.RoleModules");
            DropTable("dbo.PurchasePayments");
            DropTable("dbo.PurchaseInvMaster");
            DropTable("dbo.PhysicalAdjusts");
            DropTable("dbo.ModMasters");
            DropTable("dbo.GlBreak");
            DropTable("dbo.GlAccounts");
            DropTable("dbo.FYearTrans");
            DropTable("dbo.EmployeePositions");
            DropTable("dbo.Employees");
            DropTable("dbo.RoleMasters");
            DropTable("dbo.EmpCtrls");
            DropTable("dbo.Departments");
            DropTable("dbo.StateCodes");
            DropTable("dbo.Customers");
            DropTable("dbo.CompanyProfiles");
            DropTable("dbo.CompanyGstTypes");
            DropTable("dbo.Vendors");
            DropTable("dbo.InventoryTypes");
            DropTable("dbo.InventoryCategories");
            DropTable("dbo.Stock");
            DropTable("dbo.Batch");
            DropTable("dbo.Banks");
        }
    }
}
