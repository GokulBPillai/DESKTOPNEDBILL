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
    public partial class FrmPurchaseRetSelectList : Form
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
        public FrmPurchaseRetSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        private void GetPurchaseDrNoteDetails()
        {
            try
            {
                var purchaseDrNoteList = (from purchaseRet in cmpDBContext.PurchaseRetMaster
                                          join vend in cmpDBContext.Vendor on purchaseRet.VendID equals vend.VendorId
                                          orderby purchaseRet.PurRetNo descending
                                          select new
                                          {
                                              purchaseRet.RetDate,
                                              purchaseRet.PurRetNo,
                                              purchaseRet.InvDate,
                                              purchaseRet.PurInvNo,
                                              purchaseRet.InvRef,
                                              purchaseRet.InvAmount,
                                              vend.VendorName,
                                          }).ToList();
                if (purchaseDrNoteList.Count != 0)
                {
                    GrdPurchaseInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = purchaseDrNoteList;
                    GrdPurchaseInvoiceDetails.AutoGenerateColumns = false;
                    GrdPurchaseInvoiceDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmPurchaseRetSelectList_Load(object sender, EventArgs e)
        {
            MdlMain.gPurchaseDrNoteNo = 0;
            GetPurchaseDrNoteDetails();
        }

        private void GrdPurchaseInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gPurchaseDrNoteNo = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["purretno"].Value);
            this.Close();
        }

        private void GrdPurchaseInvoiceDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MdlMain.gPurchaseDrNoteNo = Convert.ToInt32(GrdPurchaseInvoiceDetails.SelectedRows[0].Cells["purretno"].Value);
                this.Close();
            }
        }

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPurchaseRetSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }

        private void TxtPurRetRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var purchaseDrNoteList = (from purchaseRet in cmpDBContext.PurchaseRetMaster
                                          join vend in cmpDBContext.Vendor on purchaseRet.VendID equals vend.VendorId
                                          where purchaseRet.InvRef.Contains(TxtPurRetRef.Text.Trim())
                                       || vend.VendorName.Contains(TxtPurRetRef.Text.Trim())
                                          orderby purchaseRet.PurRetNo descending
                                          select new
                                          {
                                              purchaseRet.RetDate,
                                              purchaseRet.PurRetNo,
                                              purchaseRet.InvDate,
                                              purchaseRet.PurInvNo,
                                              purchaseRet.InvRef,
                                              purchaseRet.InvAmount,
                                              vend.VendorName,
                                          }).ToList();
                if (purchaseDrNoteList.Count != 0)
                {
                    GrdPurchaseInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = purchaseDrNoteList;
                    GrdPurchaseInvoiceDetails.AutoGenerateColumns = false;
                    GrdPurchaseInvoiceDetails.DataSource = bindingSource;
                }
                else
                {
                    GrdPurchaseInvoiceDetails.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void TxtPurRetRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GrdPurchaseInvoiceDetails.Focus();
            }
        }
    }
}
