using DESKTOPNEDBILL.Module;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmProductSelectListSales : Form
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
        public FrmProductSelectListSales()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void FrmProductSelectList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (FrmSalesInvoice.sendproducttext != "")
            {
                TxtProductName.Text = FrmSalesInvoice.sendproducttext;
                int len = TxtProductName.Text.Length;
                TxtProductName.SelectionStart = len;
            }
            MdlMain.gStockId = 0;
            MdlMain.gBatchId = 0;
            GetStockList();
        }
        private void GetStockList()
        {
            try
            {
                var stkList = (from stk in cMPDBContext.Stock
                               join bth in cMPDBContext.Batch on stk.StockId equals bth.StockId
                               where stk.Status == true && bth.StockOnHand > 0
                               select new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   Batch = bth.BatchName,
                                   Unit = stk.UnitMeasure,
                                   bth.Expiry,
                                   bth.GST,
                                   Cost = bth.PurchasePrice,
                                   MRP = bth.Mrp,
                                   bth.Cess,
                                   bth.AddlCess,
                                   OnHand = bth.StockOnHand,
                                   SellingPrice = bth.SellingPriceA,
                               }).ToList();
                if (stkList.Count != 0)
                {
                    grdStockDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkList;
                    grdStockDetails.AutoGenerateColumns = false;
                    grdStockDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void grdStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gStockId = Convert.ToInt32(grdStockDetails.SelectedRows[0].Cells["StockId"].Value);
            MdlMain.gBatchId = Convert.ToInt32(grdStockDetails.SelectedRows[0].Cells["BatchId"].Value);
            this.Close();
        }
        private void TxtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                var stkList = (from stk in cMPDBContext.Stock
                               join bth in cMPDBContext.Batch on stk.StockId equals bth.StockId
                               where stk.Status == true && bth.StockOnHand > 0 &&
                               stk.StockName.Contains(TxtProductName.Text) || stk.Barcode.Contains(TxtProductName.Text)
                               select new
                               {
                                   StockId = stk.StockId,
                                   BatchId = bth.BatchId,
                                   StockName = stk.StockName,
                                   Batch = bth.BatchName,
                                   Unit = stk.UnitMeasure,
                                   Expiry = bth.Expiry,
                                   GST = bth.GST,
                                   Cost = bth.PurchasePrice,
                                   MRP = bth.Mrp,
                                   Cess = bth.Cess,
                                   AddlCess = bth.AddlCess,
                                   OnHand = bth.StockOnHand,
                                   SellingPrice = bth.SellingPriceA,
                               }).ToList();
                if (stkList.Count != 0)
                {
                    grdStockDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkList;
                    grdStockDetails.AutoGenerateColumns = false;
                    grdStockDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    grdStockDetails.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grdStockDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MdlMain.gStockId = Convert.ToInt32(grdStockDetails.SelectedRows[0].Cells["StockId"].Value);
                    MdlMain.gBatchId = Convert.ToInt32(grdStockDetails.SelectedRows[0].Cells["BatchId"].Value);
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

        private void FrmProductSelectListSales_KeyDown(object sender, KeyEventArgs e)
        {           
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
