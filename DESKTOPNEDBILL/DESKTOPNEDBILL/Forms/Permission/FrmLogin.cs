using DESKTOPNEDBILL.Forms.Main;
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
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DESKTOPNEDBILL.Forms.Permission
{
    public partial class FrmLogin : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // height of ellipse
          int nHeightEllipse // width of ellipse
      );
        CMPDBContext cmpDBContext = new CMPDBContext();
        public int CompanyID;
        public FrmLogin()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            btnSubmit.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSubmit.Width, btnSubmit.Height, 5, 5));
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetAllCompanies();
            GetCompanyStateCode();
            GetCompanyDetails();
            txtUserName.Focus();
        }
        #region Get & Post methods
        private int Connct()
        {
            try
            {
                List<EmpCtrl> empCtrls = cmpDBContext.EmpCtrl.ToList();
                var userList = (from emp in cmpDBContext.EmpCtrl
                                where emp.UserID == txtUserName.Text.Trim() && emp.Password == txtPassword.Text.Trim()
                                select new
                                {
                                    emp.EmpCode,
                                    emp.UserID,
                                    emp.EmpName,
                                    emp.EmpId,
                                    emp.RoleId
                                });
                if (userList.Count() > 0)
                {
                    foreach (var item in userList)
                    {
                        MdlMain.gUserID = item.EmpCode.ToString();
                        MdlMain.gLogName = item.UserID.ToString();
                        MdlMain.gLogEmpName = item.EmpName;
                        MdlMain.gLogEmpId = item.EmpId.ToString();
                        MdlMain.gRoleId = item.RoleId;
                    }
                    return MdlMain.gRoleId;
                }
                else return 0;
            }
            catch (Exception)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        private bool GetAllCompanies()
        {
            try
            {
                List<CompanyProfile> cmpProfiles = cmpDBContext.CompanyProfiles.ToList();
                BindingSource bindingSource = new BindingSource();

                if (cmpProfiles.Count <= 0)
                {
                    //BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = MdlMNU.AddDefaultCompany();
                    cmbCompany.DataSource = bindingSource;
                    cmbCompany.DisplayMember = "CompanyName";
                    cmbCompany.ValueMember = "CompanyId";
                }
                else
                {
                    bindingSource.DataSource = cmpProfiles;
                    cmbCompany.DataSource = bindingSource;//cmpProfiles;
                    cmbCompany.DisplayMember = "CompanyName";
                    cmbCompany.ValueMember = "CompanyId";
                }
                CompanyID = Convert.ToInt32(cmbCompany.SelectedValue.ToString());
                //MdlMain.gCompanyName = cmbCompany.Text;
                MdlMain.gCompanyName = cmbCompany.GetItemText(cmbCompany.SelectedItem);
                GetFinYearList(CompanyID);
                return true;
            }
            catch (Exception)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        private void GetFinYearList(int compId)
        {
            try
            {
                List<FYearTrans> fYearTrans = cmpDBContext.FYearTranss.Where(m => m.CompanyId == compId).ToList();
                cmbFinancialYear.DataSource = fYearTrans;
                cmbFinancialYear.DisplayMember = "FYear";
                cmbFinancialYear.ValueMember = "TranId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool FieldValidation()
        {
            try
            {
                if (cmbCompany.Text == "")
                {
                    MessageBox.Show("Please select a Company to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCompany.Focus();
                    return false;
                }
                if (cmbFinancialYear.Text == "")
                {
                    MessageBox.Show("Please select a Financial Year to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbFinancialYear.Focus();
                    return false;
                }
                else if (txtUserName.Text == "")
                {
                    MessageBox.Show("Please enter User Name to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    return false;
                }
                else if (txtPassword.Text == "")
                {
                    MessageBox.Show("Please enter Password to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GetCompanyStateCode()
        {
            try
            {
                var CompanyStateCode =
                    (from p in cmpDBContext.StateCodes
                     join pc in cmpDBContext.CompanyProfiles on p.StateCodeId equals pc.State
                     select new
                     {
                         statecodeId = p.StateCodeId,
                         statecodeNo = p.StatecodeNo,
                         statename = p.State,

                     }).ToList();
                foreach (var cus in CompanyStateCode)
                {
                    MdlMain.gCompanyStateCodeId = cus.statecodeId;
                    MdlMain.gCompanyStateCode = cus.statecodeNo;
                    MdlMain.gCompanyState = cus.statename;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool GetCompanyDetails()
        {
            try
            {
                List<CompanyProfile> typ = cmpDBContext.CompanyProfiles.ToList();
                if (typ.Count > 0)
                {
                    MdlMain.gCompanyGSTTypeId = typ[0].GSTType;
                    MdlMain.gPrintMode = typ[0].PrintMode;
                    MdlMain.gCompType = typ[0].CompanyType;
                    MdlMain.gCompanyNature = typ[0].CompanyNature;
                    MdlMain.gBusinessType = typ[0].BusinessType;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Event Handling methods

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    int roleId = Connct();
                    if (roleId > 0)
                    {
                        MdlMain.gRoleId = roleId;
                        this.Hide();
                        FrmMain frmMain = new FrmMain();
                        frmMain.Closed += (s, args) => this.Close();
                        frmMain.Show();
                    }
                    else
                    {
                        MessageBox.Show("User Name or Passwrod you entered is wrong!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cmbFinancialYear_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFinancialYear.ValueMember != "")
                {
                    int TrId = Convert.ToInt32(cmbFinancialYear.SelectedValue);
                    if (TrId > 0)
                    {
                        var FyearTran = from ftr in cmpDBContext.FYearTranss
                                        where ftr.TranId == TrId
                                        select ftr;
                        foreach (var ftr in FyearTran)
                        {
                            MdlMain.gFYStart = ftr.FYStart;
                            MdlMain.gFYEnd = ftr.FYEnd;
                            MdlMain.gFinYear = ftr.FYearDir;
                            MdlMain.gCompCode = ftr.CompanyCode;
                            MdlMain.gCompFinYear = ftr.FYear;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSubmit.Focus();
            }
        }
        #endregion
    }
}
