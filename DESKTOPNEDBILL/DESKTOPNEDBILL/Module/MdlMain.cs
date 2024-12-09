using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models.Entities;
using TableDims.Models;
using System.Security.Cryptography;

namespace DESKTOPNEDBILL.Module
{
    public class MdlMain
    {
        public static string gUserID;
        public static string gLogName;
        public static string gLogEmpId;
        public static string gLogEmpName;
        public static string gCompanyName;
        public static int gRoleId;
        public static int gModId;
        public static int gSubModid;
        public static string gModuleName;
        public static string gModType;
        public static string YearEndStatus;
        public static string InitalCat;
        public static string DataSour;
        public static string UsId;
        public static string PaWord;
        public static string PSInfo;
        public static string CompDire;
        public static string CompFYearDir;
        public static string FYrTraID;
        public static string FYrTraCompID;
        public static string lastButtonName;
        public static string CurrButtonName;
        public static Panel lastButtonClicked = null;
        public static Button lastSideButtonClicked = null;
        public static Form lastSideButtonFormClicked = null;
        public static int gStockId;
        public static int gBatchId;
        public static int gVendorId;
        public static int gPurchaseInvNo;
        public static int gStkTranId;
        public static int gBnkGnlId;
        public static int gBankId;
        public static int gCustomerId;
        public static int gSalesInvNo;
        public static int gSalesRcpNo;
        public static int gSalesCrNoteNo;
        public static int gPurchaseDrNoteNo;
        public static int gPurchasePayNo;
        public static DateTime gFYStart;
        public static DateTime gFYEnd;
        public static string gFinYear;
        public static string gCompFinYear;
        public static string gCompCode;
        public static int gCompanyId;
        public static string SerilNoStat;
        public static int gInvTypeId;
        public static int gStkCategoryId;
        public static int gEmpPositionId;
        public static int gDepartmentId;
        public static int gCompanyStateCodeId;
        public static string gCompanyStateCode;
        public static string gCompanyState;
        public static int gCompanyGSTTypeId;
        public static string gCompanyGSTType;
        public static string gInvCategory;
        public static string gPrintMode;
        public static string gCompType;
        public static string gCompanyNature;
        public static string gBusinessType;
        public static DataSet gStkTranDS = new DataSet();
        public static DataTable gCompTbl = new DataTable();
        public static DataSet gDs_SalesInv1 = new DataSet();
        static CMPDBContext cmpDBContext = new CMPDBContext();
        public static int gEmployeeId;
        public static int gLogId;

        public static bool isNumber(char ch, string text)
        {
            try
            {
                bool res = true;
                char decimalChar = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalDigits);
                //check if it´s a decimal separator and if doesn´t already have one in the text string
                if (ch == decimalChar && text.IndexOf(decimalChar) != -1)
                {
                    res = false;
                    return res;
                }
                //check if it´s a digit, decimal separator and backspace
                if (!Char.IsDigit(ch) && ch != decimalChar && ch != (char)Keys.Back)
                    res = false;
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Dictionary<string, decimal> SalesGSTCalculation(decimal Cost, decimal Quantity, decimal GSTPercentage, decimal CessPercentage, decimal DiscountPercentage, decimal DiscountAmount)
        {
            try
            {
                Dictionary<string, decimal> SalesGSTlist = new Dictionary<string, decimal>();
                decimal TotalAmount = 0;
                //decimal SubTotalAmount = 0;
                decimal NetTotalAmount = 0;
                decimal TotalGSTAmount = 0;
                decimal TotalCGSTAmount = 0;
                decimal TotalSGSTAmount = 0;
                decimal TotalIGSTAmount = 0;
                decimal TotalCessAmount = 0;
                //decimal TotalAddlCessAmount = 0;
                decimal TotalDiscountAmountPerUnit = 0;
                decimal TotalDiscountAmount = 0;
                if (gCompanyGSTTypeId == 1 || gCompanyGSTTypeId == 3)//----------if GST is composite and Non Gst
                {
                    GSTPercentage = 0;
                    CessPercentage = 0;
                }
                if (DiscountPercentage > 0)
                {
                    TotalDiscountAmountPerUnit = (Cost * DiscountPercentage * Convert.ToDecimal(0.01));
                }
                else if (DiscountAmount > 0)
                {
                    TotalDiscountAmountPerUnit = DiscountAmount;
                }
                TotalGSTAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) * (GSTPercentage * Convert.ToDecimal(0.01));
                if (gInvCategory == "GST" && gCompanyGSTTypeId == 2)//----------if GST is regular
                {
                    TotalCGSTAmount = TotalGSTAmount / 2;
                    TotalSGSTAmount = TotalGSTAmount / 2;
                    TotalIGSTAmount = 0;
                }
                else if (gInvCategory == "IGST" && gCompanyGSTTypeId == 2)//----------if GST is regular
                {
                    TotalCGSTAmount = 0;
                    TotalSGSTAmount = 0;
                    TotalIGSTAmount = TotalGSTAmount;
                }
                TotalCessAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) * (CessPercentage * Convert.ToDecimal(0.01));
                NetTotalAmount = Quantity * Cost;
                //NetTotalAmount = Quantity * (Cost - TotalDiscountAmountPerUnit);
                TotalAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) + TotalGSTAmount + TotalCessAmount;
                TotalDiscountAmount = Quantity * TotalDiscountAmountPerUnit;
                SalesGSTlist.Add("TotalDiscountAmount", Math.Round(TotalDiscountAmount, 2));
                SalesGSTlist.Add("TotalGSTAmount", Math.Round(TotalGSTAmount, 2));
                SalesGSTlist.Add("TotalCGSTAmount", Math.Round(TotalCGSTAmount, 2));
                SalesGSTlist.Add("TotalSGSTAmount", Math.Round(TotalSGSTAmount, 2));
                SalesGSTlist.Add("TotalIGSTAmount", Math.Round(TotalIGSTAmount, 2));
                SalesGSTlist.Add("TotalCessAmount", Math.Round(TotalCessAmount, 2));
                //SalesGSTlist.Add("SubTotalAmount", Math.Round(SubTotalAmount, 2));
                SalesGSTlist.Add("NetTotalAmount", Math.Round(NetTotalAmount, 2));
                SalesGSTlist.Add("TotalAmount", Math.Round(TotalAmount, 2));
                return SalesGSTlist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Dictionary<string, decimal> PurchaseGSTCalculation(decimal Cost, decimal Quantity, decimal GSTPercentage, decimal CessPercentage, decimal DiscountPercentage, decimal DiscountAmount)
        {
            try
            {
                Dictionary<string, decimal> PurchaseGSTlist = new Dictionary<string, decimal>();
                decimal TotalAmount = 0;
                decimal SubTotalAmount = 0;
                decimal NetTotalAmount = 0;
                decimal TotalGSTAmount = 0;
                decimal TotalCGSTAmount = 0;
                decimal TotalSGSTAmount = 0;
                decimal TotalIGSTAmount = 0;
                decimal TotalCessAmount = 0;
                //decimal TotalAddlCessAmount = 0;
                decimal TotalDiscountAmountPerUnit = 0;
                decimal TotalDiscountAmount = 0;
                if (DiscountPercentage > 0)
                {
                    TotalDiscountAmountPerUnit = (Cost * DiscountPercentage * Convert.ToDecimal(0.01));
                }
                else if (DiscountAmount > 0)
                {
                    TotalDiscountAmountPerUnit = DiscountAmount;
                }
                TotalGSTAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) * (GSTPercentage * Convert.ToDecimal(0.01));
                TotalCGSTAmount = TotalGSTAmount / 2;
                TotalSGSTAmount = TotalGSTAmount / 2;
                TotalIGSTAmount = TotalGSTAmount;
                TotalCessAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) * (CessPercentage * Convert.ToDecimal(0.01));
                SubTotalAmount = Quantity * Cost;
                NetTotalAmount = Quantity * (Cost - TotalDiscountAmountPerUnit);
                TotalAmount = Quantity * (Cost - TotalDiscountAmountPerUnit) + TotalGSTAmount + TotalCessAmount;
                TotalDiscountAmount = Quantity * TotalDiscountAmountPerUnit;
                PurchaseGSTlist.Add("TotalDiscountAmount", Math.Round(TotalDiscountAmount, 2));
                PurchaseGSTlist.Add("TotalGSTAmount", Math.Round(TotalGSTAmount, 2));
                PurchaseGSTlist.Add("TotalCGSTAmount", Math.Round(TotalCGSTAmount, 2));
                PurchaseGSTlist.Add("TotalSGSTAmount", Math.Round(TotalSGSTAmount, 2));
                PurchaseGSTlist.Add("TotalIGSTAmount", Math.Round(TotalIGSTAmount, 2));
                PurchaseGSTlist.Add("TotalCessAmount", Math.Round(TotalCessAmount, 2));
                PurchaseGSTlist.Add("SubTotalAmount", Math.Round(SubTotalAmount, 2));
                PurchaseGSTlist.Add("NetTotalAmount", Math.Round(NetTotalAmount, 2));
                PurchaseGSTlist.Add("TotalAmount", Math.Round(TotalAmount, 2));
                return PurchaseGSTlist;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ClearTextBoxesInForm(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    control.Text = "";
                }
            }
        }
        public static void ClearTextBoxesInPanel(TableLayoutPanel pnl)
        {
            foreach (Control control in pnl.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
        }
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }
        public static void AddInitialUnitMeasures()
        {
            try
            {
                List<UnitMeasure> um = cmpDBContext.UnitMeasure.ToList();
                List<string> units = new List<string>
                {
                    "Nos.","Pcs.","Kg.","Gms.","Ltr.","Ml.","Mtr.","Feet","Sqft.","Inch","Pack","Box","Case","Pair","Units","Set","Load","doz.","onz."
                };
                if (um.Count <= 0)
                {

                    foreach (string s in units)
                    {
                        UnitMeasure unit = new UnitMeasure()
                        {
                            UnitMeasureDescription = s,
                            Status = true
                        };
                        cmpDBContext.UnitMeasure.Add(unit);
                    }
                    cmpDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialVendor()
        {
            try
            {
                List<Vendor> vend = cmpDBContext.Vendor.ToList();
                if (vend.Count <= 0)
                {
                    var vendor = new Vendor()
                    {
                        VendorName = "Cash Purchase",
                        VendorAdd1 = "",
                        VendorAdd2 = "",
                        PIN = 0,
                        PhoneNo = "",
                        VendorType = "Local",
                        OpBal = 0,
                        TotalBal = 0,
                        Status = true
                    };
                    cmpDBContext.Vendor.Add(vendor);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialCustomer()
        {
            try
            {
                List<Customer> cust = cmpDBContext.Customers.ToList();
                if (cust.Count <= 0)
                {
                    var customer = new Customer()
                    {
                        CustomerName = "Cash Sales",
                        Address1 = "",
                        Address2 = "",
                        PinCode = 0,
                        ContactNo = "",
                        Location = "Local",
                        TaxType = "UnRegistered",
                        PriceGroup = "Selling Price A",
                        PaymentType = "Cash",
                        OpeningBalance = 0,
                        TotalBalance = 0,
                        Status = true
                    };
                    cmpDBContext.Customers.Add(customer);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialDepartment()
        {
            try
            {
                List<Department> dpt = cmpDBContext.Department.ToList();
                if (dpt.Count <= 0)
                {
                    var department = new Department()
                    {
                        DepartmentName = "General",
                        Status = true
                    };
                    cmpDBContext.Department.Add(department);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialEmployee()
        {
            try
            {
                List<Employee> emp = cmpDBContext.Employee.ToList();
                if (emp.Count <= 0)
                {
                    var employees = new Employee()
                    {
                        EmployeeName = "Administrator",
                        DepartmentId = 1,
                        EmpPositionId = 1,
                        DateofJoined = DateTime.Now.Date,
                        DOB = DateTime.Now.Date
                    };
                    cmpDBContext.Employee.Add(employees);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialEmployeePosition()
        {
            try
            {
                List<EmployeePosition> empPos = cmpDBContext.EmployeePosition.ToList();
                if (empPos.Count <= 0)
                {
                    var employeePosition = new EmployeePosition()
                    {
                        EmpPositionName = "Administrator",
                        Status = true
                    };
                    cmpDBContext.EmployeePosition.Add(employeePosition);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialRoleMaster()
        {
            try
            {
                List<RoleMaster> roleMast = cmpDBContext.RoleMaster.ToList();
                if (roleMast.Count <= 0)
                {
                    var roleMaster = new RoleMaster()
                    {
                        RoleName = "1",
                        Status = true
                    };
                    cmpDBContext.RoleMaster.Add(roleMaster);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialBankCash()
        {
            try
            {
                List<Bank> bnk = cmpDBContext.Bank.ToList();
                if (bnk.Count <= 0)
                {
                    var bank = new Bank()
                    {
                        BankName = "Cash",
                        BankBranch = "",
                        AccountNo = "",
                        AccountType = "",
                        BankAddress = "",
                        IFSC = "",
                        PhoneNo = "",
                        GnlId = 15000,
                        ChqStNo = "",
                        ChqEdNo = "",
                        BankAbbrv = "",
                        Status = true,
                        IsBank = false
                    };
                    cmpDBContext.Bank.Add(bank);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddStateCode()
        {
            try
            {
                List<StateCode> sCode = cmpDBContext.StateCodes.ToList();
                if (sCode.Count <= 0)
                {
                    List<StateCode> sc = new List<StateCode>();
                    sc.Add(new StateCode { StateCodeId = 1, State = "Jammu and Kashmir", StatecodeNo = "01" });
                    sc.Add(new StateCode { StateCodeId = 2, State = "Himacha1 Pradesh", StatecodeNo = "02" });
                    sc.Add(new StateCode { StateCodeId = 3, State = "Punjab", StatecodeNo = "03" });
                    sc.Add(new StateCode { StateCodeId = 4, State = "Chandigarh", StatecodeNo = "04" });
                    sc.Add(new StateCode { StateCodeId = 5, State = "Uttarakhand", StatecodeNo = "05" });
                    sc.Add(new StateCode { StateCodeId = 6, State = "Hariyana", StatecodeNo = "06" });
                    sc.Add(new StateCode { StateCodeId = 7, State = "Delhi", StatecodeNo = "07" });
                    sc.Add(new StateCode { StateCodeId = 8, State = "Rajasthan", StatecodeNo = "08" });
                    sc.Add(new StateCode { StateCodeId = 9, State = "Uttar Pradesh", StatecodeNo = "09" });
                    sc.Add(new StateCode { StateCodeId = 10, State = "Bihar", StatecodeNo = "10" });
                    sc.Add(new StateCode { StateCodeId = 11, State = "Sikkim", StatecodeNo = "11" });
                    sc.Add(new StateCode { StateCodeId = 12, State = "Arunachal Pradesh", StatecodeNo = "12" });
                    sc.Add(new StateCode { StateCodeId = 13, State = "Nagaland", StatecodeNo = "13" });
                    sc.Add(new StateCode { StateCodeId = 14, State = "Manipur", StatecodeNo = "14" });
                    sc.Add(new StateCode { StateCodeId = 15, State = "Mizoram", StatecodeNo = "15" });
                    sc.Add(new StateCode { StateCodeId = 16, State = "Tripura", StatecodeNo = "16" });
                    sc.Add(new StateCode { StateCodeId = 17, State = "Meghalaya", StatecodeNo = "17" });
                    sc.Add(new StateCode { StateCodeId = 18, State = "Assam", StatecodeNo = "18" });
                    sc.Add(new StateCode { StateCodeId = 19, State = "West Bengal", StatecodeNo = "19" });
                    sc.Add(new StateCode { StateCodeId = 20, State = "Jarkhand", StatecodeNo = "20" });
                    sc.Add(new StateCode { StateCodeId = 21, State = "Odisha", StatecodeNo = "21" });
                    sc.Add(new StateCode { StateCodeId = 22, State = "Chattisgarh", StatecodeNo = "22" });
                    sc.Add(new StateCode { StateCodeId = 23, State = "Madhya Pradesh", StatecodeNo = "23" });
                    sc.Add(new StateCode { StateCodeId = 24, State = "Gujarat", StatecodeNo = "24" });
                    sc.Add(new StateCode { StateCodeId = 25, State = "Daman and Diu", StatecodeNo = "25" });
                    sc.Add(new StateCode { StateCodeId = 26, State = "Dadra and Nagar Haveli", StatecodeNo = "26" });
                    sc.Add(new StateCode { StateCodeId = 27, State = "Maharashta", StatecodeNo = "27" });
                    sc.Add(new StateCode { StateCodeId = 28, State = "Karnataka", StatecodeNo = "29" });
                    sc.Add(new StateCode { StateCodeId = 29, State = "Goa", StatecodeNo = "30" });
                    sc.Add(new StateCode { StateCodeId = 30, State = "Lakshadweep", StatecodeNo = "31" });
                    sc.Add(new StateCode { StateCodeId = 31, State = "Kerala", StatecodeNo = "32" });
                    sc.Add(new StateCode { StateCodeId = 32, State = "Tamil Nadu", StatecodeNo = "33" });
                    sc.Add(new StateCode { StateCodeId = 33, State = "Puducherry", StatecodeNo = "34" });
                    sc.Add(new StateCode { StateCodeId = 34, State = "Andaman and Nicobar Islands", StatecodeNo = "35" });
                    sc.Add(new StateCode { StateCodeId = 35, State = "Telangana", StatecodeNo = "36" });
                    sc.Add(new StateCode { StateCodeId = 36, State = "Andra Pradesh", StatecodeNo = "37" });
                    sc.Add(new StateCode { StateCodeId = 37, State = "Ladakh", StatecodeNo = "38" });
                    sc.Add(new StateCode { StateCodeId = 38, State = "Other Territory", StatecodeNo = "97" });
                    sc.Add(new StateCode { StateCodeId = 39, State = "Centre Jurisdiction", StatecodeNo = "99" });
                    cmpDBContext.StateCodes.AddRange(sc);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialCompanyGstType()
        {
            try
            {
                List<CompanyGstType> compGstType = cmpDBContext.CompanyGstTypes.ToList();
                if (compGstType.Count <= 0)
                {
                    List<CompanyGstType> gstType = new List<CompanyGstType>();
                    gstType.Add(new CompanyGstType { CompanyGstTypeId = 1, Type = "Non GST", Status = true });
                    gstType.Add(new CompanyGstType { CompanyGstTypeId = 2, Type = "Regular GST", Status = true });
                    gstType.Add(new CompanyGstType { CompanyGstTypeId = 3, Type = "Composition Scheme", Status = true });
                    cmpDBContext.CompanyGstTypes.AddRange(gstType);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialInventoryType()
        {
            try
            {
                List<InventoryType> invType = cmpDBContext.InventoryTypes.ToList();
                if (invType.Count <= 0)
                {
                    List<InventoryType> inventoryType = new List<InventoryType>();
                    inventoryType.Add(new InventoryType { InventoryTypeId = 1, TypeName = "Inventory", IsStkDeduct = true, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 2, TypeName = "Services", IsStkDeduct = false, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 3, TypeName = "Raw Material", IsStkDeduct = true, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 4, TypeName = "Finished Goods", IsStkDeduct = true, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 5, TypeName = "Equipments", IsStkDeduct = true, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 6, TypeName = "Spares", IsStkDeduct = true, Status = true });
                    inventoryType.Add(new InventoryType { InventoryTypeId = 7, TypeName = "Trial Item", IsStkDeduct = false, Status = true });
                    cmpDBContext.InventoryTypes.AddRange(inventoryType);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialInventoryCategory()
        {
            try
            {
                List<InventoryCategory> invCatType = cmpDBContext.InventoryCategories.ToList();
                if (invCatType.Count <= 0)
                {
                    var invCat = new InventoryCategory()
                    {
                        CategoryName = "Inventory",
                        InventoryTypId = 1,
                        Status = true
                    };
                    cmpDBContext.InventoryCategories.Add(invCat);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool AddInitialGlAccount()
        {
            try
            {
                List<GlAccount> glAcc = cmpDBContext.GlAccounts.ToList();
                if (glAcc.Count <= 0)
                {
                    List<GlAccount> glAccount = new List<GlAccount>();
                    glAccount.Add(new GlAccount { Id = 1, GnlId = 10000, GnlCatg = "FIXED ASSET", GnlDesc = "FIXED ASSET", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 1, GnlBudget = 0, MainHead = 10000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 2, GnlId = 10001, GnlCatg = "FIXED ASSET", GnlDesc = "Machinery", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 10000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 3, GnlId = 10002, GnlCatg = "FIXED ASSET", GnlDesc = "Furniture", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 10000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 4, GnlId = 11000, GnlCatg = "CURRENT ASSET", GnlDesc = "CURRENT ASSET", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 2, GnlBudget = 0, MainHead = 11000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 5, GnlId = 11001, GnlCatg = "CURRENT ASSET", GnlDesc = "Stock In Hand", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 11000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 6, GnlId = 11002, GnlCatg = "CURRENT ASSET", GnlDesc = "Advances & Deposits", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 11000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 7, GnlId = 11900, GnlCatg = "INVESTMENT", GnlDesc = "INVESTMENT", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 3, GnlBudget = 0, MainHead = 11900, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 8, GnlId = 12000, GnlCatg = "CURRENT ASSET", GnlDesc = "Accounts Receivables", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 9, GnlId = 12001, GnlCatg = "CURRENT ASSET", GnlDesc = "GST Paid", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 10, GnlId = 12002, GnlCatg = "CURRENT ASSET", GnlDesc = "GST Paid 5%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 11, GnlId = 12003, GnlCatg = "CURRENT ASSET", GnlDesc = "GST Paid 12%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 12, GnlId = 12004, GnlCatg = "CURRENT ASSET", GnlDesc = "GST Paid 18%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 13, GnlId = 12005, GnlCatg = "CURRENT ASSET", GnlDesc = "GST Paid 28%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 14, GnlId = 12010, GnlCatg = "CURRENT ASSET", GnlDesc = "Advances Paid", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 12000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 15, GnlId = 12500, GnlCatg = "CURRENT ASSET", GnlDesc = "BANK - CURRENT AC", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 4, GnlBudget = 0, MainHead = 12500, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 16, GnlId = 15000, GnlCatg = "CURRENT ASSET", GnlDesc = "Cash In Hand", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 15000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 17, GnlId = 16000, GnlCatg = "CURRENT ASSET", GnlDesc = "Staff Advance", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 16000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 18, GnlId = 17500, GnlCatg = "DIFFERED ASSET", GnlDesc = "DIFFERED ASSET", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 5, GnlBudget = 0, MainHead = 17500, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 19, GnlId = 17501, GnlCatg = "DIFFERED ASSET", GnlDesc = "Pre-Paid Expenses", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 17500, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 20, GnlId = 18000, GnlCatg = "OTHER ASSET", GnlDesc = "OTHER  ASSET", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 6, GnlBudget = 0, MainHead = 18000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 21, GnlId = 18500, GnlCatg = "RESERVE ACCOUNT", GnlDesc = "RESERVE ACCOUNT", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 7, GnlBudget = 0, MainHead = 18500, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 22, GnlId = 18701, GnlCatg = "OTHER  ASSET", GnlDesc = "Securities & Deposits", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 7, GnlBudget = 0, MainHead = 18701, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 23, GnlId = 20000, GnlCatg = "PROFIT & LOSS ACCOUNT", GnlDesc = "PROFIT & LOSS ACCOUNT", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 8, GnlBudget = 0, MainHead = 20000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 24, GnlId = 20001, GnlCatg = "PROFIT & LOSS ACCOUNT", GnlDesc = "Profit & Loss Account", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 20000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 25, GnlId = 30000, GnlCatg = "LIABILITIES", GnlDesc = "LIABILITIES", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 9, GnlBudget = 0, MainHead = 30000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 26, GnlId = 30001, GnlCatg = "CAPITAL", GnlDesc = "CAPITAL", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 10, GnlBudget = 0, MainHead = 30001, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 27, GnlId = 30002, GnlCatg = "CAPITAL", GnlDesc = "Capital 1", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 30001, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 28, GnlId = 30025, GnlCatg = "CAPITAL", GnlDesc = "CURRENT ACCOUNT", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 11, GnlBudget = 0, MainHead = 30025, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 29, GnlId = 30050, GnlCatg = "LONG TERM LIABILITIES", GnlDesc = "LOANS & ADVANCES", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 12, GnlBudget = 0, MainHead = 30050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 30, GnlId = 30100, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "BANK - OVER DRAFTS", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 13, GnlBudget = 0, MainHead = 30100, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 31, GnlId = 31000, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "CURRENT LIABILITIES", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 14, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 32, GnlId = 32000, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "Accounts  Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 33, GnlId = 32001, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "GST Collected", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 34, GnlId = 32002, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "GST Collected 5%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 35, GnlId = 32003, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "GST Collected 12%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 36, GnlId = 32004, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "GST Collected 18%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 37, GnlId = 32005, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "GST Collected 28%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 38, GnlId = 32006, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "CST Collected", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 39, GnlId = 32010, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "Cess [Output]", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 40, GnlId = 33000, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "PF Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 41, GnlId = 33001, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "LIC Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 42, GnlId = 33002, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "TDS Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 43, GnlId = 33003, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "Others Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 44, GnlId = 34000, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "Salaries & Wages Payable", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 45, GnlId = 35000, GnlCatg = "CURRENT LIABILITIES", GnlDesc = "ADVANCES & DEPOSITS", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 31000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 46, GnlId = 40000, GnlCatg = "INCOME - DIRECT", GnlDesc = "INCOME - DIRECT", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 15, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 47, GnlId = 40001, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Income", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 48, GnlId = 40002, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Income 5%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 49, GnlId = 40003, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Income 12%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 50, GnlId = 40004, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Income 18%", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 51, GnlId = 40005, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Income 28%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 52, GnlId = 40010, GnlCatg = "INCOME - DIRECT", GnlDesc = "Sales Return", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 53, GnlId = 40011, GnlCatg = "INCOME - DIRECT", GnlDesc = "Carriage Out", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 40000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 54, GnlId = 45000, GnlCatg = "INCOME - INDIRECT", GnlDesc = "INCOME - INDIRECT", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 16, GnlBudget = 0, MainHead = 45000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 55, GnlId = 45001, GnlCatg = "INCOME - INDIRECT", GnlDesc = "Miscellaneous Income", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 45000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 56, GnlId = 45025, GnlCatg = "INCOME - INDIRECT", GnlDesc = "Discounts Received", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 45000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 57, GnlId = 45026, GnlCatg = "INCOME - INDIRECT", GnlDesc = "BANK INTEREST", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 45000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 58, GnlId = 50000, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "DIRECT EXPENSES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 17, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 59, GnlId = 50001, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cost of Goods", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 60, GnlId = 50002, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cost of Goods 5%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 61, GnlId = 50003, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cost of Goods 12%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 62, GnlId = 50004, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cost of Goods 18%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 63, GnlId = 50005, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cost of Goods 28%", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 64, GnlId = 50010, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Cess [Input]", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 65, GnlId = 50011, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Carriage In", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 66, GnlId = 50012, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Discounts Paid", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 67, GnlId = 50015, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "Purchase Return", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 68, GnlId = 50016, GnlCatg = "EXPENSES - DIRECT", GnlDesc = "CST Paid", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 50000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 69, GnlId = 50050, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "INDIRECT EXPENCES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 18, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 70, GnlId = 50051, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "Round Off", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 71, GnlId = 50052, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "BANK CHARGES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 72, GnlId = 50053, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "PRINTING AND STATIONERY", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 73, GnlId = 50054, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "ELECTRICITY CHARGES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 74, GnlId = 50055, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "TELEPHONE EXPENSES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 75, GnlId = 50056, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "Internet Charges", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 76, GnlId = 50057, GnlCatg = "EXPENSES - INDIRECT", GnlDesc = "Fuel Charges", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Active", GroupOrder = 0, GnlBudget = 0, MainHead = 50050, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 77, GnlId = 60000, GnlCatg = "WAGES - DIRECT", GnlDesc = "WAGES REGISTER", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 19, GnlBudget = 0, MainHead = 60000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 78, GnlId = 60250, GnlCatg = "WAGES - DIRECT", GnlDesc = "DIRECET WAGES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 20, GnlBudget = 0, MainHead = 60250, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 79, GnlId = 61000, GnlCatg = "WAGES - INDIRECT", GnlDesc = "INDIRECT WAGES", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 21, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 80, GnlId = 61001, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Salaries & Wages", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 81, GnlId = 61003, GnlCatg = "WAGES - INDIRECT", GnlDesc = "House Rent Allowance", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 82, GnlId = 61004, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Dearness Allowance", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 83, GnlId = 61005, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Travelling Allowance", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 84, GnlId = 61006, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Overtime", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 85, GnlId = 61007, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Other Allowances", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 86, GnlId = 61008, GnlCatg = "WAGES - INDIRECT", GnlDesc = "Other Deductions", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 61000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 87, GnlId = 70000, GnlCatg = "DEPRICIATION", GnlDesc = "DEPRECIATION ACCOUNT", GnlCode = "Dr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "MainHead", GroupOrder = 22, GnlBudget = 0, MainHead = 70000, Amt1 = 0, Amt2 = 0 });
                    glAccount.Add(new GlAccount { Id = 88, GnlId = 90001, GnlCatg = "Difference In Opening Balance", GnlDesc = "Allocation Pending", GnlCode = "Cr", GnlMybal = 0, GnlOpBal = 0, GnlStat = "Prefer", GroupOrder = 0, GnlBudget = 0, MainHead = 90000, Amt1 = 0, Amt2 = 0 });
                    cmpDBContext.GlAccounts.AddRange(glAccount);
                    cmpDBContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static decimal GetVendorBalance(int VendId)
        {
            try
            {
                decimal sum1 = 0;
                decimal sum2 = 0;
                var invList = (from glbr in cmpDBContext.GlBreaks
                               join vnd in cmpDBContext.Vendor on glbr.IDNo equals vnd.VendorId
                               where glbr.IDNo == VendId && (glbr.GlModule == "Purchase" || glbr.GlModule == "Journal") && glbr.GnlId == 32000
                               group glbr by
                               new
                               {
                                   vnd.OpBal,
                                   glbr.IDNo,
                                   glbr.GlModule
                               } into g
                               select new
                               {
                                   g.Key.OpBal,
                                   g.Key.IDNo,
                                   g.Key.GlModule,
                                   SumAmt1 = g.Sum(m => m.Amt1),
                                   SumAmt2 = g.Sum(m => m.Amt2)
                               }).ToList();
                foreach (var g in invList)
                {
                    sum1 = g.SumAmt1;
                    sum2 = g.SumAmt2;
                    return (g.OpBal + g.SumAmt2) - g.SumAmt1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static decimal GetCustomerBalance(int VendId)
        {
            try
            {
                decimal sum1 = 0;
                decimal sum2 = 0;
                var invList = (from glbr in cmpDBContext.GlBreaks
                               join cust in cmpDBContext.Customers on glbr.IDNo equals cust.CustomerId
                               where glbr.IDNo == VendId && (glbr.GlModule == "Sales" || glbr.GlModule == "Journal") && (glbr.GnlId == 12000 || glbr.GnlId == 35000)
                               group glbr by
                               new
                               {
                                   cust.OpeningBalance,
                                   glbr.IDNo,
                                   glbr.GlModule
                               } into g
                               select new
                               {
                                   g.Key.OpeningBalance,
                                   g.Key.IDNo,
                                   g.Key.GlModule,
                                   SumAmt1 = g.Sum(m => m.Amt1),
                                   SumAmt2 = g.Sum(m => m.Amt2)
                               }).ToList();
                foreach (var g in invList)
                {
                    sum1 = g.SumAmt1;
                    sum2 = g.SumAmt2;
                    return g.OpeningBalance + (g.SumAmt1 - g.SumAmt2);
                }

                return 0;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^([0]|\+91)?[6-9]\d{9}$").Success;
        }
        public static bool IsEmailId(string number)
        {
            return Regex.Match(number, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Success;
        }
        public static bool IsGSTNo(string number)
        {
            return Regex.Match(number, @"\d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}").Success;
        }
        public static bool IsPINCode(string number)
        {
            return Regex.Match(number, @"^[1-9][0-9]{5}$").Success;
        }
        public static string NumberToWords(decimal myNumber)
        {
            int beforeFloatingPoint = (int)Math.Floor(myNumber);
            string beforeFloatingPointWord = string.Format("{0} Rupees", NumberToWords(beforeFloatingPoint));
            string afterFloatingPointWord = string.Format("{0} Paisa Only.", SmallNumberToWord((int)((myNumber - beforeFloatingPoint) * 100), ""));
            if ((int)((myNumber - beforeFloatingPoint) * 100) > 0)
            {
                return string.Format("{0} and {1}", beforeFloatingPointWord, afterFloatingPointWord);
            }
            else
            {
                return string.Format("{0} Only", beforeFloatingPointWord);
            }
        }
        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }
        private static string SmallNumberToWord(int number, string words)
        {
            if (number <= 0) return words;
            if (words != "")
                words += " ";

            var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
            return words;
        }
        public static void InsertUserAccessDetails()
        {
            try
            {
                var user = new UserAccessDetails()
                {
                    RoleId = gRoleId,
                    UserName = gUserID,
                    LoginDate = DateTime.Now.Date,
                    LoginTime = DateTime.Now,
                    ModIdAccessed = gModId,
                    SubModIdAccessed = gSubModid,
                    FormAccessed = "",
                    LogOutDate = DateTime.Now.Date,
                    LogOutTime = DateTime.Now,
                };
                cmpDBContext.UserAccessDetails.Add(user);
                cmpDBContext.SaveChanges();
                gLogId = user.LogId;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void UpdateUserAccessDetails()
        {
            try
            {
                List<UserAccessDetails> userAccessDetails = cmpDBContext.UserAccessDetails.Where(m => m.LogId == gLogId).ToList();
                foreach (UserAccessDetails user in userAccessDetails)
                {
                    user.LogOutDate = DateTime.Now.Date;
                    user.LogOutTime = DateTime.Now;
                }                
                cmpDBContext.SaveChanges();                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
