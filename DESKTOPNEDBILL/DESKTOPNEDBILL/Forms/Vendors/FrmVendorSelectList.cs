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
    public partial class FrmVendorSelectList : Form
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
        CMPDBContext cMPDBContext = new CMPDBContext();
        public FrmVendorSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void GetVendorList()
        {
            try
            {
                List<Vendor> vend = cMPDBContext.Vendor.ToList();
                if (vend.Count != 0)
                {
                    GrdVendorDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vend;
                    GrdVendorDetails.AutoGenerateColumns = false;
                    GrdVendorDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Event Handling Methods
        private void FrmVendorSelectList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetVendorList();
        }
        private void GrdVendorDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MdlMain.gVendorId = Convert.ToInt32(GrdVendorDetails.SelectedRows[0].Cells["VendorId"].Value);
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtVendorName_TextChanged(object sender, EventArgs e)
        {
            List<Vendor> vVendors = cMPDBContext.Vendor.Where(m => m.VendorName.Contains(TxtVendorName.Text)).ToList();
            if (vVendors.Count != 0)
            {
                GrdVendorDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = vVendors;
                GrdVendorDetails.AutoGenerateColumns = false;
                GrdVendorDetails.DataSource = bindingSource;
            }
        }
        private void TxtVendorName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    GrdVendorDetails.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdVendorDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MdlMain.gVendorId = Convert.ToInt32(GrdVendorDetails.SelectedRows[0].Cells["VendorId"].Value);
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void FrmVendorSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
