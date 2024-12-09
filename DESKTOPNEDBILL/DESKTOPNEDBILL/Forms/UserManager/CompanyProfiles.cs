using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models.Entities;
using TableDims.Models;
using DESKTOPNEDBILL.Forms.Permission;
using DESKTOPNEDBILL.Module;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class CompanyProfiles : Form
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
        int GstTypeId = 0;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public CompanyProfiles()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            
        }

        #region Get & Post Methods
        private void GetStateList()
        {
            try
            {
                List<StateCode> stat = cmpDBContext.StateCodes.ToList();
                if (stat.Count > 0)
                {
                    cmbState.DataSource = stat;
                }
                cmbState.DisplayMember = "State";
                cmbState.ValueMember = "StateCodeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetCompanyGstType()
        {
            try
            {
                List<CompanyGstType> gst = cmpDBContext.CompanyGstTypes.ToList();
                if (gst.Count > 0)
                {
                    cmbGstType.DataSource = gst;
                }
                cmbGstType.DisplayMember = "Type";
                cmbGstType.ValueMember = "CompanyGstTypeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveCompany()
        {
            try
            {
                if (FieldValidation())
                {
                    DialogResult result = MessageBox.Show("Are you sure want to save the company details as state: " + cmbState.Text + " ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        var company = new CompanyProfile()
                        {
                            CompanyName = txtCompanyName.Text.Trim(),
                            Address1 = txtAddress1.Text.Trim(),
                            Address2 = txtAddress2.Text.Trim(),
                            PhoneNo = txtPhoneNo.Text.Trim(),
                            PIN = txtPin.Text.Trim(),
                            State = Convert.ToInt32(cmbState.SelectedValue),
                            Country = txtCountry.Text.Trim(),
                            EmailId = txtEmail.Text.Trim(),
                            Web = txtWeb.Text.Trim(),
                            GSTNo = txtGstNo.Text.Trim(),
                            GSTType = Convert.ToInt32(cmbGstType.SelectedValue),
                            CompanyCode = GetCompanyCode(txtCompanyName.Text.Trim(), txtPhoneNo.Text.Trim()),
                            CompanyType = cmbCompType.Text.Trim(),
                            CompanyNature = cmbCompNature.Text.Trim(),
                            BusinessType = cmbBusinessType.Text.Trim(),
                            PrintMode = cmbPrintMode.Text.Trim(),
                        };
                        cmpDBContext.CompanyProfiles.Add(company);
                        cmpDBContext.SaveChanges();

                        int currentYear = DateTime.Now.Year;
                        int nextYear = DateTime.Now.AddYears(1).Year;
                        DateTime FStart = new DateTime(currentYear, 4, 1);
                        DateTime FEnd = new DateTime(nextYear, 3, 31);
                        int FYDir1 = currentYear % 100;
                        int FYDir2 = nextYear % 100;
                        var fyearTrans = new FYearTrans
                        {
                            CompanyId = company.CompanyId,
                            FYStart = FStart,
                            FYEnd = FEnd,
                            FYear = FStart.ToShortDateString() + " - " + FEnd.ToShortDateString(),
                            FYearDir = FYDir1.ToString() + FYDir2.ToString(),
                            DatabaseName = "NEDBILLDT",
                            ServerName = "SQL//2017",
                            YearEndStatus = "No",
                            CompanyCode = company.CompanyCode,
                        };
                        cmpDBContext.FYearTranss.Add(fyearTrans);
                        cmpDBContext.SaveChanges();
                        var empctrls = new EmpCtrl
                        {
                            EmpId = 1,
                            UserID = txtUserName.Text.Trim(),
                            Password = TxtPassword.Text.Trim(),
                            EmpName = "Administrator",
                            IsAdmin = true,
                            IsLocked = false,
                            RoleId = 1
                        };
                        cmpDBContext.EmpCtrl.Add(empctrls);
                        cmpDBContext.SaveChanges();
                        List<Customer> customer = cmpDBContext.Customers.ToList();
                        if (customer.Count == 1)
                        {
                            foreach (Customer cust in customer)
                            {
                                cust.StateCode = Convert.ToInt32(cmbState.SelectedValue);
                            }
                            cmpDBContext.SaveChanges();
                        }
                        List<Vendor> vendor = cmpDBContext.Vendor.ToList();
                        if (vendor.Count == 1)
                        {
                            foreach (Vendor vend in vendor)
                            {
                                vend.StateCode = Convert.ToInt32(cmbState.SelectedValue);
                            }
                            cmpDBContext.SaveChanges();
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                //Logger.Error(ex);
                return false;
                throw;
            }
        }
        private bool FieldValidation()
        {
            if (txtCompanyName.Text == "")
            {
                MessageBox.Show("Please enter Company Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompanyName.Focus();
                return false;
            }
            else if (cmbState.Text == "")
            {
                MessageBox.Show("Please select State", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbState.Focus();
                return false;
            }
            else if (txtPhoneNo.Text != "")
            {
                if (!MdlMain.IsPhoneNumber(txtPhoneNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Phone No. to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPhoneNo.Focus();
                    return false;
                }
            }
            else if (txtEmail.Text != "")
            {
                if (!MdlMain.IsEmailId(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Email Id. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmail.Focus();
                    return false;
                }
            }
            else if (txtGstNo.Text != "")
            {
                if (GstTypeId != 1)
                {
                    if (!MdlMain.IsGSTNo(txtGstNo.Text.Trim()))
                    {
                        MessageBox.Show("Please enter valid GST No. as you selected GST Type: " + cmbGstType.Text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtGstNo.Focus();
                        return false;
                    }
                }
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Please enter User Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return false;
            }
            else if (TxtPassword.Text == "")
            {
                MessageBox.Show("Please enter Password", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtPassword.Focus();
                return false;
            }
            return true;
        }
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            txtCountry.ReadOnly = true;
            txtCountry.Text = "India";
            GetStateList();
            GetCompanyGstType();
            cmbGstType.SelectedIndex = 0;
            txtGstNo.Enabled = false;
            cmbCompType.SelectedIndex = 0;
            cmbCompNature.SelectedIndex = 0;
            cmbBusinessType.SelectedIndex = 0;
            cmbPrintMode.SelectedIndex = 0;
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
        private string GetCompanyCode(string compName, string phoneNo)
        {
            try
            {
                string CompanyCode = "";
                Byte[] HashBytes;
                MD5 md5 = new MD5CryptoServiceProvider();
                string Temp = compName + ", " + phoneNo;
                HashBytes = md5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(Temp));
                CompanyCode = Convert.ToBase64String(HashBytes);
                CompanyCode = CompanyCode.Substring(0, 4);
                return CompanyCode;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event handling methods
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveCompany())
            {
                this.Hide();
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Closed += (s, args) => this.Close();
                frmLogin.ShowDialog();
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, txtPin.Text))
                e.Handled = true;
        }
        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress1.Focus();
            }
        }
        private void txtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress2.Focus();
            }
        }
        private void txtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPin.Focus();
            }
        }
        private void txtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbState.Focus();
            }
        }
        private void cmbState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCountry.Focus();
            }
        }
        private void txtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPhoneNo.Focus();
            }
        }
        private void txtPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtWeb.Focus();
            }
        }
        private void txtWeb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbGstType.Focus();
            }
        }
        private void cmbGstType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGstNo.Focus();
            }
        }
        private void txtGstNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUserName.Focus();
            }
        }
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPassword.Focus();
            }
        }
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
        private void BtnnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            if ((txtCompanyName.Text.Length) == 1)
            {
                txtCompanyName.Text = txtCompanyName.Text[0].ToString().ToUpper();
                txtCompanyName.Select(2, 1);
            }
        }
        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if ((txtAddress1.Text.Length) == 1)
            {
                txtAddress1.Text = txtAddress1.Text[0].ToString().ToUpper();
                txtAddress1.Select(2, 1);
            }
        }
        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if ((txtAddress2.Text.Length) == 1)
            {
                txtAddress2.Text = txtAddress2.Text[0].ToString().ToUpper();
                txtAddress2.Select(2, 1);
            }
        }
        private void CompanyProfiles_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
        }
        private void CompanyProfiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
        }
        private void cmbGstType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GstTypeId = Convert.ToInt32(cmbGstType.SelectedValue);
            if (GstTypeId == 1)
            {
                txtGstNo.Enabled = false;
            }
            else
            {
                txtGstNo.Enabled = true;
            }

        }

        #endregion

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
