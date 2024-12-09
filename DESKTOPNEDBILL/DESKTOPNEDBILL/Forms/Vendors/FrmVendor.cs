using DESKTOPNEDBILL.Forms.Main;
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

namespace DESKTOPNEDBILL.Forms.Vendors
{
    public partial class FrmVendor : Form
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
        public FrmVendor()
        {
            InitializeComponent();
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));            
        }
        #region Get Methods
        public void GetVendorList()
        {
            try
            {
                var vendors = (from vend in cmpDBContext.Vendor
                               select new
                               {
                                   vend.VendorId,
                                   vend.VendorName,
                                   vend.VendorAdd1,
                                   vend.VendorAdd2,
                                   vend.PhoneNo,
                                   vend.PIN,
                                   vend.GSTNo,
                                   vend.VendorType,
                                   vend.OpBal,
                                   vend.TotalBal,
                                   vend.Status
                               }).ToList();

                if (vendors.Count != 0)
                {
                    GrdVendorDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vendors;
                    GrdVendorDetails.AutoGenerateColumns = false;
                    GrdVendorDetails.DataSource = bindingSource;
                }
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
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GrdVendorDetails.DataSource = null;
            InitializingData();                       
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditVendor frmAddEditVendor = new FrmAddEditVendor(this);
                frmAddEditVendor.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdVendorDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdVendorDetails.Columns[e.ColumnIndex].Name == "Edit")
                {
                    MdlMain.gVendorId = Convert.ToInt32(GrdVendorDetails.CurrentRow.Cells[0].Value);
                    FrmAddEditVendor frmAddEditVendor = new FrmAddEditVendor(this);
                    frmAddEditVendor.ShowDialog();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion      

        #region Validation and Initialization Methods   
        private void InitializingData()
        {
            GetVendorList();
        }
        #endregion

        private void FrmVendor_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                var vendors = (from vend in cmpDBContext.Vendor
                               where vend.VendorName.Contains(textvalue)
                               || vend.VendorAdd1.Contains(textvalue)
                               select new
                               {
                                   vend.VendorId,
                                   vend.VendorName,
                                   vend.VendorAdd1,
                                   vend.VendorAdd2,
                                   vend.PhoneNo,
                                   vend.PIN,
                                   vend.GSTNo,
                                   vend.VendorType,
                                   vend.OpBal,
                                   vend.TotalBal,
                                   vend.Status
                               }).ToList();

                if (vendors.Count != 0)
                {
                    GrdVendorDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vendors;
                    GrdVendorDetails.AutoGenerateColumns = false;
                    GrdVendorDetails.DataSource = bindingSource;
                }
                else
                {
                    GrdVendorDetails.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private void FrmVendor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
