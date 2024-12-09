using DESKTOPNEDBILL.Module;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesInvSelectList : Form
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
        public FrmSalesInvSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void FrmSalesInvSelectList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MdlMain.gSalesInvNo = 0;
            GetSalesInvoiceDetails();
        }
        #region Get methods
        private void GetSalesInvoiceDetails()
        {
            try
            {
                var salesInvList = (from salesInv in cmpDBContext.SalesInvMasters
                                    join cust in cmpDBContext.Customers on salesInv.CustID equals cust.CustomerId
                                    orderby salesInv.SalesInvNo descending
                                    select new
                                    {
                                        salesInv.InvDate,
                                        salesInv.SalesInvNo,
                                        salesInv.InvRef,
                                        salesInv.InvAmount,
                                        cust.CustomerName,
                                    }).ToList();
                if (salesInvList.Count != 0)
                {
                    GrdSalesInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = salesInvList;
                    GrdSalesInvoiceDetails.AutoGenerateColumns = false;
                    GrdSalesInvoiceDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Click event methods
        private void TxtSalesInvRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GrdSalesInvoiceDetails.Focus();
            }
        }
        private void TxtSalesInvRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var salesInvList = (from salesInv in cmpDBContext.SalesInvMasters
                                    join cust in cmpDBContext.Customers on salesInv.CustID equals cust.CustomerId
                                    where salesInv.InvRef.Contains(TxtSalesInvRef.Text.Trim())
                                    || cust.CustomerName.Contains(TxtSalesInvRef.Text.Trim())
                                    orderby salesInv.SalesInvNo
                                    select new
                                    {
                                        salesInv.InvDate,
                                        salesInv.SalesInvNo,
                                        salesInv.InvRef,
                                        salesInv.InvAmount,
                                        cust.CustomerName,
                                    }).ToList();
                if (salesInvList.Count != 0)
                {
                    GrdSalesInvoiceDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = salesInvList;
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
            MdlMain.gSalesInvNo = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["SalesInvNo"].Value);
            MdlMain.gStkTranId = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["StkTranId"].Value);
            this.Close();
        }
        private void GrdSalesInvoiceDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MdlMain.gSalesInvNo = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["SalesInvNo"].Value);
                MdlMain.gStkTranId = Convert.ToInt32(GrdSalesInvoiceDetails.SelectedRows[0].Cells["StkTranId"].Value);
                this.Close();
            }
        }
        #endregion

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSalesInvSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }

    }
}
