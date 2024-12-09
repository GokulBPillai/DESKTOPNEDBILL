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

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmStock : Form
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
        public FrmStock()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;           
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
            InitializingData();
        }

        #region Events methods
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void grdStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdStockDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gStockId = Convert.ToInt32(grdStockDetails.CurrentRow.Cells[0].Value);
                MdlMain.gBatchId = Convert.ToInt32(grdStockDetails.CurrentRow.Cells[1].Value);
                FrmAddEditStock frmAddEditStock = new FrmAddEditStock(this);
                frmAddEditStock.ShowDialog();
            }
        }
        private void grdStockDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int col = grdStockDetails.CurrentCell.ColumnIndex;
                int row = grdStockDetails.CurrentCell.RowIndex;
                if (col < grdStockDetails.Columns.Count - 1)
                {
                    grdStockDetails.CurrentCell = grdStockDetails.Rows[row].Cells[col + 1];
                    grdStockDetails.Focus();
                }
                else if (col == grdStockDetails.Columns.Count - 1)
                {
                    grdStockDetails.Rows.Add(1);
                    grdStockDetails.CurrentCell = grdStockDetails.Rows[row].Cells[1];
                    grdStockDetails.Focus();
                }
            }
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditStock frmAddEditStock = new FrmAddEditStock(this);
                frmAddEditStock.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get Methods
        private void InitializingData()
        {
            GetStockList();
        }
        public void GetStockList()
        {
            try
            {
                var stkList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               orderby bth.Status descending
                               select new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   bth.BatchName,
                                   bth.SellingPriceA,
                                   bth.PurchasePrice,
                                   bth.Mrp,
                                   bth.Status,
                               }).ToList();
                if (stkList.Count() != 0)
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
        public void GetStockListBySearch(string searchValue)
        {
            try
            {
                var stkList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               where 
                               stk.StockName.Contains(searchValue)
                               || bth.BatchName.Contains(searchValue)
                               orderby bth.Status descending
                               select new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   bth.BatchName,
                                   bth.SellingPriceA,
                                   bth.PurchasePrice,
                                   bth.Mrp,
                                   bth.Status,
                               }).ToList();
                if (stkList.Count() != 0)
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
        #endregion

        private void grdStockDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in grdStockDetails.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (row.Cells[7].Value.ToString() == "False")// Or your condition 
                {                   
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {                   
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void FrmStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetStockListBySearch(txtSearch.Text.Trim());
        }

        private void FrmStock_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
