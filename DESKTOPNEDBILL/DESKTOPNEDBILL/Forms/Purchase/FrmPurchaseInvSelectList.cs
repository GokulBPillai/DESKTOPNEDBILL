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

namespace DESKTOPNEDBILL.Forms.Purchase
{
    public partial class FrmPurchaseInvSelectList : Form
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
        public FrmPurchaseInvSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        #region Event handling methods
        private void TxtPurInvRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var pInvList = (from pInv in cmpDBContext.PurchaseInvMasters
                                join vnd in cmpDBContext.Vendor on pInv.VendID equals vnd.VendorId
                                where pInv.InvRef.Contains(TxtPurInvRef.Text)
                                select new
                                {
                                    pInv.InvDate,
                                    pInv.PurInvNo,
                                    pInv.InvRef,
                                    pInv.InvAmount,
                                    vnd.VendorName,
                                }).ToList();
                if (pInvList.Count != 0)
                {
                    GrdPurchaseInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = pInvList;
                    GrdPurchaseInvoiceDetails.AutoGenerateColumns = false;
                    GrdPurchaseInvoiceDetails.DataSource = bindingSource;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtPurInvRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GrdPurchaseInvoiceDetails.Focus();
            }
        }
        private void GrdPurchaseInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gPurchaseInvNo = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["PurchaseInvNo"].Value);
            MdlMain.gStkTranId = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["StkTranId"].Value);
            this.Close();
        }
        private void GrdPurchaseInvoiceDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MdlMain.gPurchaseInvNo = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["PurInvNo"].Value);
                MdlMain.gStkTranId = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["StkTranId"].Value);
                this.Close();
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Get & Set methods
        private void GetPurcahseInvoiceDetails()
        {
            try
            {
                var pInvList = (from pInv in cmpDBContext.PurchaseInvMasters
                                join vnd in cmpDBContext.Vendor on pInv.VendID equals vnd.VendorId
                                orderby pInv.PurInvNo descending
                                select new
                                {
                                    pInv.InvDate,
                                    pInv.PurInvNo,
                                    pInv.InvRef,
                                    pInv.InvAmount,
                                    vnd.VendorName,
                                }).ToList();
                if (pInvList.Count != 0)
                {
                    GrdPurchaseInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = pInvList;
                    GrdPurchaseInvoiceDetails.AutoGenerateColumns = false;
                    GrdPurchaseInvoiceDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void FrmPurchaseInvSelectList_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MdlMain.gPurchaseInvNo = 0;
            GetPurcahseInvoiceDetails();
        }

        private void FrmPurchaseInvSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
