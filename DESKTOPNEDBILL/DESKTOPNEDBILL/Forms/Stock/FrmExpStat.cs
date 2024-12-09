using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmExpStat : Form
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
        DateTime todayDt = DateTime.Now.Date;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmExpStat()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnView.Width, btnView.Height, 5, 5));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));

        }

        private void FrmExpStat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                btnView_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            GetStockListBySearch(txtSearch.Text.Trim());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            grdStockDetails.DataSource = null;
        }
        private void GetStockListBySearch(string searchValue)
        {
            try
            {
                DateTime fromDt;
                DateTime toDt;
                fromDt = dtpFrom.Value.Date;
                toDt = dtpTo.Value.Date;
                var stkList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               where stk.StockName.Contains(searchValue)
                               || bth.BatchName.Contains(searchValue) && stk.Status == true
                               //&& (fromDt == toDt ? bth.Expiry < fromDt : (bth.Expiry >= fromDt && bth.Expiry <= toDt))
                               where chkDateFilter.Checked == false ? (fromDt == toDt ? bth.Expiry < fromDt : (bth.Expiry >= fromDt && bth.Expiry <= toDt)) : true
                               orderby bth.Status descending
                               select new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   bth.BatchName,
                                   bth.Expiry,
                                   bth.StockOnHand,
                                   Days = SqlFunctions.DateDiff("day", todayDt, bth.Expiry)
                               }).ToList();
                if (stkList.Count() != 0)
                {
                    grdStockDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkList.ToList();
                    grdStockDetails.AutoGenerateColumns = false;
                    grdStockDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void grdStockDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in grdStockDetails.Rows)
            {            //Here 2 cell is target value and 1 cell is Volume
                if (Convert.ToInt32(row.Cells[6].Value) <= 0)
                {
                    row.Cells["days"].Style.BackColor = Color.Red;
                }
                else if(Convert.ToInt32(row.Cells[6].Value) > 0 && Convert.ToInt32(row.Cells[6].Value) <= 3)// Or your condition 
                {
                    row.Cells["days"].Style.BackColor = Color.LightCoral;
                }
                else if (Convert.ToInt32(row.Cells[6].Value) > 3 && Convert.ToInt32(row.Cells[6].Value) < 10)
                {
                    row.Cells["days"].Style.BackColor = Color.LightSalmon;
                }
                
            }
        }

        private void FrmExpStat_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }

    }
}
