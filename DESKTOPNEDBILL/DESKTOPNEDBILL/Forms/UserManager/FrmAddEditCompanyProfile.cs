using DESKTOPNEDBILL.Forms.Permission;
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
using TableDims.Models.Entities;
using TableDims.Models;
using DESKTOPNEDBILL.Forms.Main;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmAddEditCompanyProfile : Form
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
        int EditCompanyId = 0;
        private FrmCompanyProfile frmcompanyProfile;
        public FrmAddEditCompanyProfile(FrmCompanyProfile frmcompanyProfile)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmcompanyProfile = frmcompanyProfile;
        }

        #region Get & Post Methods
        private void GetStateList()
        {
            try
            {
                List<StateCode> stat = cmpDBContext.StateCodes.ToList();
                if (stat.Count > 0)
                {
                    CmbState.DataSource = stat;
                }
                CmbState.DisplayMember = "State";
                CmbState.ValueMember = "StateCodeId";
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
                    CmbGstType.DataSource = gst;
                }
                CmbGstType.DisplayMember = "Type";
                CmbGstType.ValueMember = "CompanyGstTypeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetCompanyDetailsByStockId(int compId)
        {
            try
            {
                var stkList = (from cmp in cmpDBContext.CompanyProfiles
                               where cmp.CompanyId == compId
                               select new
                               {
                                   cmp.CompanyId,
                                   cmp.CompanyCode,
                                   cmp.CompanyName,
                                   cmp.Address1,
                                   cmp.Address2,
                                   cmp.City,
                                   cmp.EmailId,
                                   cmp.Country,
                                   cmp.GSTNo,
                                   cmp.GSTType,
                                   cmp.PhoneNo,
                                   cmp.PIN,
                                   cmp.State,
                                   cmp.Web,
                                   cmp.CompanyType,
                                   cmp.CompanyNature,
                                   cmp.BusinessType,
                                   cmp.PrintMode,
                               });

                if (stkList.Count() > 0)
                {
                    foreach (var item in stkList)
                    {
                        TxtCompanyName.Text = item.CompanyName;
                        TxtAddress1.Text = item.Address1;
                        TxtAddress2.Text = item.Address2;
                        TxtPhoneNo.Text = item.PhoneNo;
                        TxtEmail.Text = item.EmailId;
                        TxtPin.Text = item.PIN;
                        TxtCountry.Text = item.Country;
                        CmbState.SelectedValue = item.State;
                        CmbGstType.SelectedValue = item.GSTType;
                        TxtWeb.Text = item.Web;
                        cmbCompType.Text = item.CompanyType;
                        cmbCompNature.Text = item.CompanyNature;
                        cmbBusinessType.Text = item.BusinessType;
                        cmbPrintMode.Text = item.PrintMode;
                    }
                }
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
                    List<CompanyProfile> companyProfiles = cmpDBContext.CompanyProfiles.Where(m => m.CompanyId == EditCompanyId).ToList();
                    foreach (CompanyProfile profles in companyProfiles)
                    {
                        profles.CompanyName = TxtCompanyName.Text.Trim();
                        profles.Address1 = TxtAddress1.Text.Trim();
                        profles.Address2 = TxtAddress2.Text.Trim();
                        profles.PhoneNo = TxtPhoneNo.Text;
                        profles.PIN = TxtPin.Text.Trim();
                        profles.State = Convert.ToInt32(CmbState.SelectedValue);
                        profles.Country = TxtCountry.Text.Trim();
                        profles.EmailId = TxtEmail.Text.Trim();
                        profles.Web = TxtWeb.Text.Trim();
                        profles.GSTNo = TxtGstNo.Text.Trim();
                        profles.GSTType = Convert.ToInt32(CmbGstType.SelectedValue);
                        profles.CompanyType = cmbCompType.Text.Trim();
                        profles.CompanyNature = cmbCompNature.Text.Trim();
                        profles.BusinessType = cmbBusinessType.Text.Trim();
                        profles.PrintMode = cmbPrintMode.Text.Trim();
                    };
                    //cmpDBContext.CompanyProfiles.Update(company);
                    cmpDBContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private bool FieldValidation()
        {
            if (TxtCompanyName.Text == "")
            {
                MessageBox.Show("Please enter Company Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtCompanyName.Focus();
                return false;
            }
            else if (TxtPin.Text != "")
            {
                if (!MdlMain.IsPINCode(TxtPin.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Pincode. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtPin.Focus();
                    return false;
                }
            }
            else if (CmbState.Text == "")
            {
                MessageBox.Show("Please select State", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CmbState.Focus();
                return false;
            }
            else if (TxtPhoneNo.Text != "")
            {
                if (!MdlMain.IsPhoneNumber(TxtPhoneNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Phone No. to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtPhoneNo.Focus();
                    return false;
                }
            }
            else if (TxtEmail.Text != "")
            {
                if (!MdlMain.IsEmailId(TxtEmail.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Email Id. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtEmail.Focus();
                    return false;
                }
            }
            else if (TxtGstNo.Text != "")
            {
                if (!MdlMain.IsGSTNo(TxtGstNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid GST No. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtGstNo.Focus();
                    return false;
                }
            }

            return true;
        }
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            GetStateList();
            GetCompanyGstType();
            CmbGstType.SelectedIndex = 0;
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
        #endregion

        #region Event Handling Methods
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure want to update the company details? Application will restart after the updation.", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                if (SaveCompany())
                {
                    //this.Close();
                    ////this.Hide();
                    //FrmLogin frmLogin = new FrmLogin();
                    //frmLogin.Closed += (s, args) => this.Close();
                    //frmLogin.ShowDialog();
                    Application.Restart();
                    Environment.Exit(0);
                }
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void TxtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress1.Focus();
            }
        }
        private void TxtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress2.Focus();
            }
        }
        private void TxtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPin.Focus();
            }
        }
        private void TxtPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbState.Focus();
            }
        }
        private void CmbState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtCountry.Focus();
            }
        }
        private void TxtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPhoneNo.Focus();
            }
        }
        private void TxtPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtEmail.Focus();
            }
        }
        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtWeb.Focus();
            }
        }
        private void TxtWeb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbGstType.Focus();
            }
        }
        private void CmbGstType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtGstNo.Focus();
            }
        }
        private void TxtGstNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCompType.Focus();
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmAddEditCompanyProfile_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gCompanyId > 0)
            {
                EditCompanyId = MdlMain.gCompanyId;
                LblHeader.Text = "Edit Comapny";
                GetCompanyDetailsByStockId(EditCompanyId);
                MdlMain.gCompanyId = 0;
            }

        }
        private void TxtAddress1_TextChanged(object sender, EventArgs e)
        {
            if ((TxtAddress1.Text.Length) == 1)
            {
                TxtAddress1.Text = TxtAddress1.Text[0].ToString().ToUpper();
                TxtAddress1.Select(2, 1);
            }
        }
        private void TxtAddress2_TextChanged(object sender, EventArgs e)
        {
            if ((TxtAddress2.Text.Length) == 1)
            {
                TxtAddress2.Text = TxtAddress2.Text[0].ToString().ToUpper();
                TxtAddress2.Select(2, 1);
            }
        }
        private void FrmAddEditCompanyProfile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
        }
        private void cmbCompType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCompNature.Focus();
            }
        }
        private void cmbCompNature_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBusinessType.Focus();
            }
        }
        private void cmbBusinessType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbPrintMode.Focus();
            }
        }
        private void cmbPrintMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void FrmAddEditCompanyProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
