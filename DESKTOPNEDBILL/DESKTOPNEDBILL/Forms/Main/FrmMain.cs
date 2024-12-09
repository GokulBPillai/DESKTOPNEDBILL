using DESKTOPNEDBILL.CustomControl;
using DESKTOPNEDBILL.Forms.Banks;
using DESKTOPNEDBILL.Forms.GSTReports;
using DESKTOPNEDBILL.Forms.HR;
using DESKTOPNEDBILL.Forms.Purchase;
using DESKTOPNEDBILL.Forms.Sales;
using DESKTOPNEDBILL.Forms.Stock;
using DESKTOPNEDBILL.Forms.UserManager;
using DESKTOPNEDBILL.Forms.Vendors;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Main
{
    public partial class FrmMain : Form
    {
        //private IEnumerable<ModMaster> modMastList;
        public FrmMain()
        {
            InitializeComponent();

        }
        private void SetFullScreen()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
        private void LoadMainModules(int roleId)
        {
            try
            {
                CMPDBContext myDBContext = new CMPDBContext();
                List<RoleModule> roleModule = myDBContext.RoleModule.Where(m => m.RoleId == roleId && m.Status == true).ToList();
                if (roleId == 1 && roleModule.Count <= 0)
                {
                    List<ModMaster> mm = new List<ModMaster>
                    {
                        new ModMaster { ModName = "Sales", DisplayName = "Sales", IsActive = true,OrderNo=1 },
                        new ModMaster { ModName = "Purchase", DisplayName = "Purchase", IsActive = true, OrderNo=2 },
                        new ModMaster { ModName = "Inventory", DisplayName = "Inventory", IsActive = true, OrderNo=3 },
                        new ModMaster { ModName = "Bank", DisplayName = "Bank", IsActive = true, OrderNo=4 },
                        new ModMaster { ModName = "HR", DisplayName = "HR", IsActive = true,OrderNo=5 },
                        new ModMaster { ModName = "User Manager", DisplayName = "User Manager", IsActive = true,OrderNo=6 },
                        new ModMaster { ModName = "GST Reports", DisplayName = "GST Reports", IsActive = true,OrderNo=7 },
                    };
                    myDBContext.ModMaster.AddRange(mm);
                    myDBContext.SaveChanges();

                    List<SubModMaster> smm = new List<SubModMaster>
                    {
                        new SubModMaster { ModId = 1, SubModName = "Maintain Customer", DisplayName = "Maintain Customer", Status = true, OrderNo=1 },
                        new SubModMaster { ModId = 1, SubModName = "Billing", DisplayName = "Billing", Status = true,OrderNo=2 },
                        new SubModMaster { ModId = 1, SubModName = "Settlement",  DisplayName = "Settlement", Status = true,OrderNo=3 },
                        new SubModMaster { ModId = 1, SubModName = "Sales Ledger", DisplayName = "Sales Ledger", Status = true,OrderNo=4 },
                        new SubModMaster { ModId = 1, SubModName = "Sales By Item",  DisplayName = "Sales By Item", Status = true,OrderNo=5 },
                        new SubModMaster { ModId = 1, SubModName = "Print Bill",  DisplayName = "Print BIll", Status = false,OrderNo=6 },
                        new SubModMaster { ModId = 1, SubModName = "Opening Balance", DisplayName = "Opening Balance", Status = true,OrderNo=7 },
                        new SubModMaster { ModId = 2, SubModName = "Maintain Vendor",  DisplayName = "Maintain Vendor", Status = true,OrderNo=1 },
                        new SubModMaster { ModId = 2, SubModName = "Purchase Bill", DisplayName = "Purchase Bill", Status = true,OrderNo=2 },
                        new SubModMaster { ModId = 2, SubModName = "Purchase Settlement", DisplayName = "Settlement", Status = true,OrderNo=3 },
                        new SubModMaster { ModId = 2, SubModName = "Purchase Ledger", DisplayName = "Purchase Ledger", Status = true,OrderNo=4 },
                        new SubModMaster { ModId = 2, SubModName = "Purchase By Item", DisplayName = "Purchase By Item", Status = true,OrderNo=5 },
                        new SubModMaster { ModId = 2, SubModName = "Vendor Opening Balance", DisplayName = "Vendor OP Balance", Status = true ,OrderNo=6},
                        new SubModMaster { ModId = 3, SubModName = "Maintain Stock", DisplayName = "Maintain Stock",  Status = true,OrderNo=1 },
                        new SubModMaster { ModId = 3, SubModName = "Stock OnHand", DisplayName = "Stock OnHand", Status = true, OrderNo=2 },
                        new SubModMaster { ModId = 3, SubModName = "Stock Type", DisplayName = "Stock Type", Status = true, OrderNo=3 },
                        new SubModMaster { ModId = 3, SubModName = "Stock Category", DisplayName = "Stock Category", Status = true, OrderNo=4 },
                        new SubModMaster { ModId = 3, SubModName = "Maintain Department", DisplayName = "Department", Status = true,OrderNo=5 },
                        new SubModMaster { ModId = 3, SubModName = "Physical Stock", DisplayName = "Physical Stock", Status = true,OrderNo=6 },
                        new SubModMaster { ModId = 3, SubModName = "Opening Stock", DisplayName = "Opening Stock", Status = true,OrderNo=7 },
                        new SubModMaster { ModId = 3, SubModName = "Stock Receipt", DisplayName = "Stock Receipt", Status = true,OrderNo=8 },
                        new SubModMaster { ModId = 3, SubModName = "Stock Issue", DisplayName = "Stock Issue", Status = true,OrderNo=9 },
                        new SubModMaster { ModId = 3, SubModName = "Price List", DisplayName = "Price List", Status = true,OrderNo=10 },
                        new SubModMaster { ModId = 3, SubModName = "Day Stock", DisplayName = "Day Stock", Status = true,OrderNo=11 },
                        new SubModMaster { ModId = 4, SubModName = "Maintain Bank", DisplayName = "Miantain Bank", Status = true,OrderNo=1 },
                        new SubModMaster { ModId = 5, SubModName = "Maintain Employee", DisplayName = "Maintain Employee", Status = true ,OrderNo=1},
                        new SubModMaster { ModId = 6, SubModName = "Company Profile", DisplayName = "Company Profile", Status = true,OrderNo=1 },
                        new SubModMaster { ModId = 6, SubModName = "User Control", DisplayName = "User Control", Status = true,OrderNo=2 },
                        new SubModMaster { ModId = 5, SubModName = "Employee Position", DisplayName = "Employee Position", Status = true,OrderNo=2 },
                        new SubModMaster { ModId = 6, SubModName = "User Access", DisplayName = "User Access", Status = true,OrderNo=3 },
                        new SubModMaster { ModId = 7, SubModName = "GST Summary", DisplayName = "GST Summary", Status = true,OrderNo=1 },
                        new SubModMaster { ModId = 7, SubModName = "GST Filing Report", DisplayName = "GST Filing Report", Status = true,OrderNo=2 },
                        new SubModMaster { ModId = 1, SubModName = "Sales Return", DisplayName = "Sales Return", Status = true,OrderNo=8 },
                        new SubModMaster { ModId = 6, SubModName = "Backup/Restore Settings", DisplayName = "Backup/Restore Settings", Status = true,OrderNo=4 },
                        new SubModMaster { ModId = 3, SubModName = "Expiry Status", DisplayName = "Expiry Status", Status = true,OrderNo=12 },
                        new SubModMaster { ModId = 6, SubModName = "User Access Report", DisplayName = "User Access Report", Status = true,OrderNo=5 },
                        new SubModMaster { ModId = 2, SubModName = "Purchase Return", DisplayName = "Purchase Return", Status = true,OrderNo=7 },
                    };
                    myDBContext.SubModMaster.AddRange(smm);
                    myDBContext.SaveChanges();

                    //List<EmpCtrl> empCtrls = myDBContext.EmpCtrl.ToList();
                    //if (empCtrls.Count == 0)
                    //{
                    //    //AddDefaultAdministrator();
                    //    //return empctrls.RoleId;
                    //}
                    List<RoleModule> rmd = new List<RoleModule>();
                    foreach (var item in mm)
                    {
                        rmd.Add(new RoleModule { RoleId = 1, ModId = item.ModId, Status = true });
                    }
                    myDBContext.RoleModule.AddRange(rmd);
                    myDBContext.SaveChanges();

                    List<RoleSubModule> rsmd = new List<RoleSubModule>();
                    foreach (var item1 in mm)
                    {
                        List<SubModMaster> subModMast = myDBContext.SubModMaster.Where(m => m.ModId == item1.ModId).ToList();
                        foreach (var item2 in subModMast)
                        {
                            rsmd.Add(new RoleSubModule { RoleId = 1, ModId = item1.ModId, SubModId = item2.SubModId, Status = true });
                        }
                    }
                    myDBContext.RoleSubModule.AddRange(rsmd);
                    myDBContext.SaveChanges();
                }
                var entryPoint = (from roleMdle in myDBContext.RoleModule
                                  join modmst in myDBContext.ModMaster on roleMdle.ModId equals modmst.ModId
                                  where roleMdle.RoleId == roleId && roleMdle.Status == true && modmst.IsActive == true
                                  select new
                                  {
                                      modmst.ModName,
                                      modmst.DisplayName,
                                      modmst.ModId,
                                      modmst.OrderNo
                                  } into x
                                  group x by new { x.ModName, x.DisplayName, x.ModId, x.OrderNo } into grp
                                  orderby grp.Key.OrderNo
                                  select new
                                  {
                                      grp.Key.ModName,
                                      grp.Key.DisplayName,
                                      grp.Key.ModId
                                  }).ToList();
                Dictionary<string, Int32> mnuTextList = new Dictionary<string, Int32>();
                mnuTextList = entryPoint.ToDictionary(x => x.DisplayName, x => x.ModId);
                mnuTextList.Add("E X I T", -1);
                MdlMain.gModType = "MainMenu";
                this.LoadMMNU_Text(mnuTextList);
                btnModuleName.Text = "MAIN MENU";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string LoadMMNU_Text(Dictionary<string, Int32> MnuTxt)
        {
            int cnt = 0;
            flowLayoutPanelSidebar.Controls.Clear();
            string ret = "";
            try
            {
                foreach (KeyValuePair<string, Int32> name in MnuTxt)
                {
                    if (name.Key == "B A C K" || name.Key == "E X I T")
                    {
                        btnBackExit.Visible = true;
                        btnBackExit.Text = name.Key.ToUpper();
                        btnBackExit.BackColor = Color.FromArgb(34, 112, 173);
                        btnBackExit.ForeColor = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        cnt++;
                        SidebarButton btn = new SidebarButton();
                        //btn.Size = new Size(215, 58);
                        //btn.Width= btnModuleName.Width;
                        //btn.Height= btnModuleName.Height;
                        btn.ButtonName = name.Key;
                        btn.ButtonModId = name.Value.ToString();
                        if (name.Key != string.Empty)
                        {
                            flowLayoutPanelSidebar.Controls.Add(btn);
                        }
                        ret = "Data Fetched Successfully.. :)";
                        btn.buttonClicked += new System.EventHandler(BtnSidebar_Click);
                    }
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
                throw;
            }
            return ret;
        }
        public void BtnSidebar_Click(object sender, EventArgs e)
        {
            SidebarButton obj = (SidebarButton)sender;
            SideBarButtonClick(Convert.ToInt32(obj.ButtonModId), obj.ButtonName);
            MdlMain.CurrButtonName = obj.ButtonName;
        }
        public void SideBarButtonClick(int buttonModId, string buttonName)
        {
            try
            {
                string subName = "";
                if (MdlMain.gModType == "MainMenu")
                {
                    MdlMain.gModId = buttonModId;
                    MdlMain.gSubModid = 0;
                    MdlMain.gModuleName = buttonName;
                }
                else
                {
                    MdlMain.gSubModid = Convert.ToInt32(buttonModId);
                }
                CMPDBContext myDBContext = new CMPDBContext();
                List<RoleSubModule> roleSubModule = myDBContext.RoleSubModule.Where(m => m.ModId == MdlMain.gModId).ToList();
                if (MdlMain.gModType == "MainMenu" && roleSubModule.Count > 0)
                {
                    LoadSubModules(MdlMain.gRoleId, MdlMain.gModId, MdlMain.gSubModid);
                    btnModuleName.Visible = true;
                    btnModuleName.ForeColor = Color.White;
                    btnModuleName.Text = buttonName.ToUpper();
                }
                if (MdlMain.gModType == "SubMenu" && MdlMain.gSubModid > 0)
                {
                    var SubName = myDBContext.SubModMaster
                                              .Where(e => e.SubModId == MdlMain.gSubModid)
                                              .Select(e => e)
                                              .FirstOrDefault();
                    subName = SubName.SubModName;
                }
                if (buttonName == "E X I T" || buttonName == "B A C K")
                {
                    subName = buttonName;
                }
                if (CtrlMnuAct(subName))
                {
                    MdlMain.InsertUserAccessDetails();
                }
                //CtrlMnuAct(subName);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void AddDefaultAdministrator()
        {
            try
            {
                CMPDBContext cmpDBContext = new CMPDBContext();
                List<RoleMaster> rm = new List<RoleMaster>
                    {
                        new RoleMaster { RoleName = "Administartor", Status = true },
                        new RoleMaster { RoleName = "Sales", Status = true }
                    };
                cmpDBContext.RoleMaster.AddRange(rm);
                cmpDBContext.SaveChanges();
                var department = new Department()
                {
                    DepartmentName = "General Stores",
                    Status = true
                };
                cmpDBContext.Department.Add(department);
                cmpDBContext.SaveChanges();
                var employeePosition = new EmployeePosition()
                {
                    EmpPositionName = "Administrator",
                    Status = true
                };
                cmpDBContext.EmployeePosition.Add(employeePosition);
                cmpDBContext.SaveChanges();

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
                var empctrls = new EmpCtrl
                {
                    EmpId = 1,
                    UserID = "Admin",
                    Password = "Admin",
                    EmpName = "Administrator",
                    IsAdmin = true,
                    IsLocked = false,
                    RoleId = 1
                };
                cmpDBContext.EmpCtrl.Add(empctrls);
                cmpDBContext.SaveChanges();
                //return null;
            }
            catch (Exception)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        private void BtnBackExit_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnBackExit.ClientRectangle,
       Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
       Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
       Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
       Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset);
        }
        private void btnModuleName_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, btnModuleName.ClientRectangle,
           Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
           Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
           Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset,
           Color.FromArgb(34, 112, 173), 2, ButtonBorderStyle.Outset);
        }
        public void OpenForm(Form form)
        {
            form.MdiParent = this;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Show();
        }
        public void OpenChildForm(Form form)
        {
            OpenForm(form);
        }
        public bool CtrlMnuAct(string MnuTxt)
        {
            try
            {
                CloseMdiChildren();
                switch (MnuTxt)
                {
                    case "Billing":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmSalesInvoice frmSalesInvoice = new FrmSalesInvoice();
                            frmSalesInvoice.MdiParent = this;
                            frmSalesInvoice.TopLevel = false;
                            frmSalesInvoice.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmSalesInvoice);
                            frmSalesInvoice.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Maintain Customer":
                        FrmCustomer frmCustomer = new FrmCustomer();
                        frmCustomer.MdiParent = this;
                        frmCustomer.TopLevel = false;
                        frmCustomer.Dock = DockStyle.Fill;
                        //panelBody.Controls.Add(frmCustomer);
                        frmCustomer.Show();
                        panelSidebar.Visible = false;
                        return true;
                    case "Maintain Stock":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmStock frmStock = new FrmStock();
                            frmStock.MdiParent = this;
                            frmStock.TopLevel = false;
                            frmStock.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmStock);
                            frmStock.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Maintain Vendor":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmVendor frmVendor = new FrmVendor();
                            frmVendor.MdiParent = this;
                            frmVendor.TopLevel = false;
                            frmVendor.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmVendor);
                            frmVendor.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Purchase Bill":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPurchase frmPurchase = new FrmPurchase();
                            frmPurchase.MdiParent = this;
                            frmPurchase.TopLevel = false;
                            frmPurchase.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPurchase);
                            frmPurchase.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Purchase Ledger":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPurchaseLedger frmPurchaseLedger = new FrmPurchaseLedger();
                            frmPurchaseLedger.MdiParent = this;
                            frmPurchaseLedger.TopLevel = false;
                            frmPurchaseLedger.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPurchaseLedger);
                            frmPurchaseLedger.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Maintain Bank":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmBank frmBank = new FrmBank();
                            frmBank.MdiParent = this;
                            frmBank.TopLevel = false;
                            frmBank.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmBank);
                            frmBank.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Sales Ledger":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmSalesLedger frmSalesLedger = new FrmSalesLedger();
                            frmSalesLedger.MdiParent = this;
                            frmSalesLedger.TopLevel = false;
                            frmSalesLedger.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmSalesLedger);
                            frmSalesLedger.Show();
                            return true;
                        }
                        return false;
                    case "Sales By Item":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmSalesSummary frmSalesSummary = new FrmSalesSummary();
                            frmSalesSummary.MdiParent = this;
                            frmSalesSummary.TopLevel = false;
                            frmSalesSummary.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmSalesSummary);
                            frmSalesSummary.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Settlement":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmSalesSettlement frmSalesSettlement = new FrmSalesSettlement();
                            frmSalesSettlement.MdiParent = this;
                            frmSalesSettlement.TopLevel = false;
                            frmSalesSettlement.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmSalesSettlement);
                            frmSalesSettlement.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Stock OnHand":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmStockOnHand frmStockOnHand = new FrmStockOnHand();
                            frmStockOnHand.MdiParent = this;
                            frmStockOnHand.TopLevel = false;
                            frmStockOnHand.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmStock);
                            frmStockOnHand.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Maintain Employee":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmMaintainEmployee frmEmployee = new FrmMaintainEmployee();
                            frmEmployee.MdiParent = this;
                            frmEmployee.TopLevel = false;
                            frmEmployee.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmEmployee);
                            frmEmployee.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Purchase Settlement":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPurchaseSettlement frmPurchaseSettlement = new FrmPurchaseSettlement();
                            frmPurchaseSettlement.MdiParent = this;
                            frmPurchaseSettlement.TopLevel = false;
                            frmPurchaseSettlement.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPurchaseSettlement);
                            frmPurchaseSettlement.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Stock Type":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmInventoryType frmInventoryType = new FrmInventoryType();
                            frmInventoryType.MdiParent = this;
                            frmInventoryType.TopLevel = false;
                            frmInventoryType.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmInventoryType);
                            frmInventoryType.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Stock Category":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmInventoryCategory frmInventoryCategory = new FrmInventoryCategory();
                            frmInventoryCategory.MdiParent = this;
                            frmInventoryCategory.TopLevel = false;
                            frmInventoryCategory.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmInventoryCategory);
                            frmInventoryCategory.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Maintain Department":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmDepartment frmDepartment = new FrmDepartment();
                            frmDepartment.MdiParent = this;
                            frmDepartment.TopLevel = false;
                            frmDepartment.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmDepartment);
                            frmDepartment.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Opening Balance":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmCustomerOpBalance frmCustomerOpBalance = new FrmCustomerOpBalance();
                            frmCustomerOpBalance.MdiParent = this;
                            frmCustomerOpBalance.TopLevel = false;
                            frmCustomerOpBalance.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmCustomerOpBalance);
                            frmCustomerOpBalance.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Purchase By Item":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPurchaseSummary frmPurchaseSummary = new FrmPurchaseSummary();
                            frmPurchaseSummary.MdiParent = this;
                            frmPurchaseSummary.TopLevel = false;
                            frmPurchaseSummary.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPurchaseSummary);
                            frmPurchaseSummary.Show();
                            return true;
                        }
                        return false;
                    case "Physical Stock":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmMaintainPhysicalStock frmMaintainPhysicalStock = new FrmMaintainPhysicalStock();
                            frmMaintainPhysicalStock.MdiParent = this;
                            frmMaintainPhysicalStock.TopLevel = false;
                            frmMaintainPhysicalStock.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmMaintainPhysicalStock);
                            frmMaintainPhysicalStock.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Vendor Opening Balance":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmVendorOpBalance frmVendorOpBalance = new FrmVendorOpBalance();
                            frmVendorOpBalance.MdiParent = this;
                            frmVendorOpBalance.TopLevel = false;
                            frmVendorOpBalance.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmVendorOpBalance);
                            frmVendorOpBalance.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Opening Stock":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmOpeningStock frmOpeningStock = new FrmOpeningStock();
                            frmOpeningStock.MdiParent = this;
                            frmOpeningStock.TopLevel = false;
                            frmOpeningStock.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmOpeningStock);
                            frmOpeningStock.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Stock Receipt":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmStockReceipt frmStockReceipt = new FrmStockReceipt();
                            frmStockReceipt.MdiParent = this;
                            frmStockReceipt.TopLevel = false;
                            frmStockReceipt.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmStockReceipt);
                            frmStockReceipt.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Stock Issue":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmStockIssue frmStockIssue = new FrmStockIssue();
                            frmStockIssue.MdiParent = this;
                            frmStockIssue.TopLevel = false;
                            frmStockIssue.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmStockIssue);
                            frmStockIssue.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Price List":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPriceList frmPriceList = new FrmPriceList();
                            frmPriceList.MdiParent = this;
                            frmPriceList.TopLevel = false;
                            frmPriceList.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPriceList);
                            frmPriceList.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Day Stock":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmDayStock frmDayStock = new FrmDayStock();
                            frmDayStock.MdiParent = this;
                            frmDayStock.TopLevel = false;
                            frmDayStock.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmDayStock);
                            frmDayStock.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Company Profile":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmCompanyProfile frmcompanyProfiles = new FrmCompanyProfile();
                            frmcompanyProfiles.MdiParent = this;
                            frmcompanyProfiles.TopLevel = false;
                            frmcompanyProfiles.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmcompanyProfiles);
                            frmcompanyProfiles.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "User Control":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmUserControl frmUserControl = new FrmUserControl();
                            frmUserControl.MdiParent = this;
                            frmUserControl.TopLevel = false;
                            frmUserControl.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmUserControl);
                            frmUserControl.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Employee Position":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmEmployeePosition frmEmployeePosition = new FrmEmployeePosition();
                            frmEmployeePosition.MdiParent = this;
                            frmEmployeePosition.TopLevel = false;
                            frmEmployeePosition.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmEmployeePosition);
                            frmEmployeePosition.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "User Access":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmUserAccess frmUserAccess = new FrmUserAccess();
                            frmUserAccess.MdiParent = this;
                            frmUserAccess.TopLevel = false;
                            frmUserAccess.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmUserAccess);
                            frmUserAccess.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "GST Summary":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPeriodwiseGSTSummary frmPeriodwiseGSTSummary = new FrmPeriodwiseGSTSummary();
                            frmPeriodwiseGSTSummary.MdiParent = this;
                            frmPeriodwiseGSTSummary.TopLevel = false;
                            frmPeriodwiseGSTSummary.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmPeriodwiseGSTSummary);
                            frmPeriodwiseGSTSummary.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "GST Filing Report":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmGstFilingReports frmGstFilingReports = new FrmGstFilingReports();
                            frmGstFilingReports.MdiParent = this;
                            frmGstFilingReports.TopLevel = false;
                            frmGstFilingReports.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmGstFilingReports.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Sales Return":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmSalesCrNote frmSalesCrNote = new FrmSalesCrNote();
                            frmSalesCrNote.MdiParent = this;
                            frmSalesCrNote.TopLevel = false;
                            frmSalesCrNote.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmSalesCrNote.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Backup/Restore Settings":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmBackUp frmBackUp = new FrmBackUp();
                            frmBackUp.MdiParent = this;
                            frmBackUp.TopLevel = false;
                            frmBackUp.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmBackUp.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Expiry Status":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmExpStat frmExpStat = new FrmExpStat();
                            frmExpStat.MdiParent = this;
                            frmExpStat.TopLevel = false;
                            frmExpStat.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmExpStat.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "User Access Report":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmUserAccessReports frmUserAccessReports = new FrmUserAccessReports();
                            frmUserAccessReports.MdiParent = this;
                            frmUserAccessReports.TopLevel = false;
                            frmUserAccessReports.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmUserAccessReports.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;
                    case "Purchase Return":
                        if (!CheckOpened(MnuTxt))
                        {
                            FrmPurchaseDrNote frmPurchaseDrNote = new FrmPurchaseDrNote();
                            frmPurchaseDrNote.MdiParent = this;
                            frmPurchaseDrNote.TopLevel = false;
                            frmPurchaseDrNote.Dock = DockStyle.Fill;
                            //panelBody.Controls.Add(frmGstFilingReports);
                            frmPurchaseDrNote.Show();
                            panelSidebar.Visible = false;
                            return true;
                        }
                        return false;

                    case "E X I T":
                        DialogResult result = MessageBox.Show("Are you sure want to close the application?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Application.Exit();
                            return true;
                        }
                        return false;
                    case "B A C K":
                        LoadBackToMainModules(MdlMain.gRoleId, MdlMain.gModId);
                        btnModuleName.Text = "";
                        CloseMdiChildren();
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CloseMdiChildren()
        {
            foreach (Form c in this.MdiChildren)
            {
                c.Close();
            }
        }
        public void LoadSubModules(int roleId, int modId, int submodId)
        {
            try
            {
                if (roleId > 0 && modId > 0)
                {
                    CMPDBContext myDBContext = new CMPDBContext();
                    List<RoleSubModule> roleSubModule = myDBContext.RoleSubModule.Where(m => m.RoleId == roleId && m.ModId == modId).ToList();
                    var entryPoint = (from roleSubMdle in myDBContext.RoleSubModule
                                      join subModmst in myDBContext.SubModMaster on roleSubMdle.ModId equals subModmst.ModId
                                      where roleSubMdle.RoleId == roleId && roleSubMdle.ModId == modId && subModmst.SubModId == roleSubMdle.SubModId && roleSubMdle.Status == true && subModmst.Status == true
                                      orderby subModmst.OrderNo ascending
                                      select new
                                      {
                                          subModmst.SubModName,
                                          subModmst.DisplayName,
                                          subModmst.SubModId,
                                          subModmst.OrderNo
                                      } into x
                                      group x by new { x.SubModName, x.DisplayName, x.SubModId, x.OrderNo } into grp
                                      orderby grp.Key.OrderNo ascending
                                      select new
                                      {
                                          grp.Key.SubModName,
                                          grp.Key.DisplayName,
                                          grp.Key.SubModId,
                                          grp.Key.OrderNo
                                      }).ToList();
                    Dictionary<string, Int32> smnuTextList = new Dictionary<string, Int32>();
                    MdlMain.gModType = "SubMenu";
                    smnuTextList = entryPoint.ToDictionary(x => x.DisplayName, x => x.SubModId);
                    smnuTextList.Add("B A C K", -2);
                    this.LoadMMNU_Text(smnuTextList);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadBackToMainModules(int roleId, int modId)
        {
            try
            {
                CMPDBContext myDBContext = new CMPDBContext();
                List<RoleModule> roleModule = myDBContext.RoleModule.Where(m => m.RoleId == roleId && m.ModId == modId).ToList();
                if (roleModule.Count > 0)
                {
                    var entryPoint = (from roleMdle in myDBContext.RoleModule
                                      join modmst in myDBContext.ModMaster on roleMdle.ModId equals modmst.ModId
                                      where roleMdle.RoleId == roleId && roleMdle.Status == true && modmst.IsActive == true
                                      orderby modmst.OrderNo ascending
                                      select new
                                      {
                                          modmst.ModName,
                                          modmst.DisplayName,
                                          modmst.ModId,
                                          modmst.OrderNo
                                      } into x
                                      group x by new { x.ModName, x.DisplayName, x.ModId, x.OrderNo } into grp
                                      orderby grp.Key.OrderNo
                                      select new
                                      {
                                          grp.Key.ModName,
                                          grp.Key.DisplayName,
                                          grp.Key.ModId
                                      }).ToList();
                    Dictionary<string, Int32> mnuTextList = new Dictionary<string, Int32>();
                    mnuTextList = entryPoint.ToDictionary(x => x.DisplayName, x => x.ModId);
                    mnuTextList.Add("E X I T", -1);
                    MdlMain.gModType = "MainMenu";
                    this.LoadMMNU_Text(mnuTextList);
                    btnModuleName.Text = "MAIN MENU";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnBackExit_Click(object sender, EventArgs e)
        {
            if (btnBackExit.Text == "E X I T")
            {
                DialogResult result = MessageBox.Show("Are you sure want to close the application?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else
            {
                LoadBackToMainModules(MdlMain.gRoleId, MdlMain.gModId);
                btnModuleName.Text = "";
                CloseMdiChildren();
                btnModuleName.Text = "MAIN MENU";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToShortTimeString();
            lblDate.Text = DateTime.Now.ToShortDateString();
        }
        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to close the application?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnBackExit.Paint += BtnBackExit_Paint;
            btnModuleName.Paint += btnModuleName_Paint;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            timer1.Start();
            SetFullScreen();
            LoadMainModules(MdlMain.gRoleId);
            lblUser.Text = MdlMain.gLogEmpName;
            LblCompanyName.Text = MdlMain.gCompanyName;
            LblFinYear.Text = MdlMain.gCompFinYear;
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnBackExit_Click(sender, e);
            }
        }
        public void MakeSidePanelVisible()
        {
            panelSidebar.Visible = true;
        }

    }
}
