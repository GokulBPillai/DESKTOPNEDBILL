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

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesRetSelectList : Form
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
        public FrmSalesRetSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void FrmSalesRetSelectList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MdlMain.gSalesCrNoteNo = 0;
            GetSalesCrNoteDetails();
        }

        private void GetSalesCrNoteDetails()
        {
            try
            {
                var salesCrNoteList = (from salesRet in cmpDBContext.SalesRetMasters
                                       join cust in cmpDBContext.Customers on salesRet.CustID equals cust.CustomerId
                                       orderby salesRet.SalesRetNo descending
                                       select new
                                       {
                                           salesRet.RetDate,
                                           salesRet.SalesRetNo,
                                           salesRet.InvDate,
                                           salesRet.SalesInvNo,
                                           salesRet.InvRef,
                                           salesRet.InvAmount,
                                           cust.CustomerName,
                                       }).ToList();
                if (salesCrNoteList.Count != 0)
                {
                    GrdSalesInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = salesCrNoteList;
                    GrdSalesInvoiceDetails.AutoGenerateColumns = false;
                    GrdSalesInvoiceDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void TxtSalesRetRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GrdSalesInvoiceDetails.Focus();
            }
        }

        private void TxtSalesRetRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var salesCrNoteList = (from salesRet in cmpDBContext.SalesRetMasters
                                       join cust in cmpDBContext.Customers on salesRet.CustID equals cust.CustomerId
                                       where salesRet.InvRef.Contains(TxtSalesRetRef.Text.Trim())
                                       || cust.CustomerName.Contains(TxtSalesRetRef.Text.Trim())
                                       orderby salesRet.SalesInvNo
                                       select new
                                       {
                                           salesRet.RetDate,
                                           salesRet.SalesRetNo,
                                           salesRet.InvDate,
                                           salesRet.SalesInvNo,
                                           salesRet.InvRef,
                                           salesRet.InvAmount,
                                           cust.CustomerName,
                                       }).ToList();
                if (salesCrNoteList.Count != 0)
                {
                    GrdSalesInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = salesCrNoteList;
                    GrdSalesInvoiceDetails.AutoGenerateColumns = false;
                    GrdSalesInvoiceDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdSalesInvoiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gSalesCrNoteNo = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["salesretno"].Value);
            this.Close();
        }

        private void GrdSalesInvoiceDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MdlMain.gSalesCrNoteNo = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["salesretno"].Value);
                this.Close();
            }
        }

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSalesRetSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
