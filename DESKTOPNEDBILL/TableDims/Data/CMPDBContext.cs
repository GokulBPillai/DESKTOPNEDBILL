using System.Data.Entity;
using System.Reflection.Emit;
using TableDims.Models;
using TableDims.Models.Entities;

namespace TableDims.Data
{
    public class CMPDBContext : DbContext
    {
        public CMPDBContext() : base("name=SqlConnection")
        {

        }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<FYearTrans> FYearTranss { get; set; }
        public DbSet<ModMaster> ModMaster { get; set; }
        public DbSet<SubModMaster> SubModMaster { get; set; }
        public DbSet<RoleMaster> RoleMaster { get; set; }
        public DbSet<RoleModule> RoleModule { get; set; }
        public DbSet<RoleSubModule> RoleSubModule { get; set; }
        public DbSet<EmpCtrl> EmpCtrl { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EmployeePosition> EmployeePosition { get; set; }
        public DbSet<Stocks> Stock { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<UnitMeasure> UnitMeasure { get; set; }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<PurchaseInvMaster> PurchaseInvMasters { get; set; }
        public DbSet<StkTran> StkTrans { get; set; }
        public DbSet<GlBreak> GlBreaks { get; set; }
        public DbSet<GlAccount> GlAccounts { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesInvMaster> SalesInvMasters { get; set; }
        public DbSet<SalesReceipt> SalesReceipt { get; set; }
        public DbSet<InventoryCategory> InventoryCategories { get; set; }
        public DbSet<InventoryType> InventoryTypes { get; set; }
        public DbSet<PurchasePayment> PurchasePayments { get; set; }
        public DbSet<PhysicalAdjust> PhysicalAdjusts { get; set; }
        public DbSet<StateCode> StateCodes { get; set; }
        public DbSet<CompanyGstType> CompanyGstTypes { get; set; }
        public DbSet<SalesRetMaster> SalesRetMasters { get; set; }
        public DbSet<UserAccessDetails> UserAccessDetails { get; set; }
        public DbSet<PurchaseRetMaster> PurchaseRetMaster { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {
            modelBuilder.Entity<QryStkDaySummary>().ToTable("QryStkDaySummary");
        }
    }
}
