using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Vendors
{
    public partial class FrmAddEditVendor : Form
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
        private readonly FrmVendor frmVendor;
        int EditvendID = 0;
        public FrmAddEditVendor(FrmVendor frmVendor)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            BtnRefresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnRefresh.Width, BtnRefresh.Height, 5, 5));
            btnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 5, 5));
            
            this.frmVendor = frmVendor;

        }
        #region Event Handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            MdlMain.gVendorId = 0;
            this.Close();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveVendor())
            {
                frmVendor.GetVendorList();
                InitializingData();
                MessageBox.Show("Vendor saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtVendor_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtVendor.Text.Length) == 1)
            {
                TxtVendor.Text = TxtVendor.Text[0].ToString().ToUpper();
                TxtVendor.Select(2, 1);
            }
        }
        private void TxtVendor_KeyDown(object sender, KeyEventArgs e)
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
                TxtPIN.Focus();
            }
        }
        private void TxtPIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtContactNo.Focus();
            }
        }
        private void TxtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbVendorType.Focus();
            }
        }
        private void CmbVendorType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtGSTNo.Focus();
            }
        }
        private void TxtGSTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtOpBal.Focus();
            }
        }
        private void TxtOpBal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtTotBal.Focus();
            }
        }
        private void TxtTotBal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbStatus.Focus();
            }
        }
        private void CmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }
        #endregion

        #region Only Numeric allowed        

        private void TxtOpBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtOpBal.Text))
                e.Handled = true;
        }
        private void TxtTotBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtTotBal.Text))
                e.Handled = true;
        }

        #endregion

        #region Get & Set Methods        
        private void GetVendorDetailsByCustomerId(List<Vendor> vendorDetailsById)
        {
            try
            {
                if (vendorDetailsById.Count > 0)
                {
                    foreach (var item in vendorDetailsById)
                    {
                        TxtVendor.Text = item.VendorName;
                        TxtAddress1.Text = item.VendorAdd1;
                        TxtAddress2.Text = item.VendorAdd2;
                        TxtPIN.Text = item.PIN.ToString();
                        TxtContactNo.Text = item.PhoneNo;
                        TxtTotBal.Text = item.TotalBal.ToString();
                        TxtGSTNo.Text = item.GSTNo;
                        TxtOpBal.Text = item.OpBal.ToString();
                        CmbVendorType.Text = item.VendorType;
                        CmbStatus.SelectedItem = "InActive";
                        if (item.Status == true)
                        {
                            CmbStatus.SelectedItem = "Active";
                        }
                        CmbState.SelectedValue = item.StateCode;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveVendor()
        {
            try
            {
                if (FieldValidation())
                {
                    if (TxtVendor.Text == "")
                    {
                        TxtVendor.Text = TxtPIN.Text.Trim();
                    }
                    if (EditvendID > 0)
                    {
                        List<Vendor> vend = cmpDBContext.Vendor.Where(m => m.VendorId == EditvendID).ToList();
                        foreach (Vendor vd in vend)
                        {
                            vd.VendorName = TxtVendor.Text.Trim();
                            vd.VendorAdd1 = TxtAddress1.Text.Trim();
                            vd.VendorAdd2 = TxtAddress2.Text.Trim();
                            vd.PIN = Convert.ToInt32(TxtPIN.Text);
                            vd.PhoneNo = TxtContactNo.Text.Trim();
                            vd.TotalBal = TxtTotBal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotBal.Text);
                            vd.GSTNo = TxtGSTNo.Text.Trim();
                            vd.OpBal = TxtOpBal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpBal.Text);
                            vd.VendorType = CmbVendorType.Text.Trim();
                            vd.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                            vd.StateCode = Convert.ToInt32(CmbState.SelectedValue);
                        }
                        //cmpDBContext.Vendor.UpdateRange(vend);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        var vendor = new Vendor()
                        {
                            VendorName = TxtVendor.Text.Trim(),
                            VendorAdd1 = TxtAddress1.Text.Trim(),
                            VendorAdd2 = TxtAddress2.Text.Trim(),
                            PIN = TxtPIN.Text == "" ? Convert.ToInt32(0) : Convert.ToInt32(TxtPIN.Text),
                            PhoneNo = TxtContactNo.Text.Trim(),
                            TotalBal = TxtTotBal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotBal.Text),
                            GSTNo = TxtGSTNo.Text.Trim(),
                            OpBal = TxtOpBal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpBal.Text),
                            VendorType = CmbVendorType.Text.Trim(),
                            Status = true,
                            StateCode = Convert.ToInt32(CmbState.SelectedValue)
                        };
                        cmpDBContext.Vendor.Add(vendor);
                        cmpDBContext.SaveChanges();
                    }
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

            if (TxtVendor.Text == "")
            {
                MessageBox.Show("Please enter Vendor Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtVendor.Focus();
                return false;
            }
            else if (TxtContactNo.Text.Trim() != "")
            {
                if (!MdlMain.IsPhoneNumber(TxtContactNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid phone no. otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtContactNo.Focus();
                    return false;
                }
            }
            else if (TxtGSTNo.Text.Trim() != "")
            {
                if (!MdlMain.IsGSTNo(TxtGSTNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid GST No. otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtGSTNo.Focus();
                    return false;
                }
            }
            else if (CmbState.Text == "")
            {
                MessageBox.Show("Please enter State Code", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CmbState.Focus();
                return false;
            }
            return true;
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
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Vendor";
            EditvendID = 0;
            CmbStatus.SelectedIndex = 0;
            CmbVendorType.SelectedIndex = 0;
            GetStateList();
            CmbState.SelectedValue = MdlMain.gCompanyStateCodeId;
        }
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
        #endregion

        #region Set Uppercase for first letter in textbox
        private void TxtVendor_TextChanged(object sender, EventArgs e)
        {
            if ((TxtVendor.Text.Length) == 1)
            {
                TxtVendor.Text = TxtVendor.Text[0].ToString().ToUpper();
                TxtVendor.Select(2, 1);
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
        #endregion

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            InitializingData();
        }

        private void FrmAddEditVendor_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gVendorId > 0)
            {
                EditvendID = MdlMain.gVendorId;
                LblHeader.Text = "Edit Vendor";
                List<Vendor> vendorDetailsById = cmpDBContext.Vendor.Where(m => m.VendorId == EditvendID).ToList();
                Vendor vend = vendorDetailsById.FirstOrDefault();
                GetVendorDetailsByCustomerId(vendorDetailsById);
            }
        }

        private void FrmAddEditVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnRefresh_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }

        private void TxtPIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPIN.Text))
                e.Handled = true;
        }

        private void TxtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPIN.Text))
                e.Handled = true;
        }
    }
}
