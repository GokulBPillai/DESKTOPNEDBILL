using DESKTOPNEDBILL.Forms.Sales;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using TableDims.Models;
using DESKTOPNEDBILL.Forms.Main;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmStockOnHand : Form
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
        DataTable StockTbl = new DataTable();
        int StockId = 0;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmStockOnHand()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));

        }

        #region Get & Post Methods
        private void CreateStockTbl()
        {
            StockTbl.Columns.Add("StockId", typeof(int));
            StockTbl.Columns.Add("BatchId", typeof(int));
            StockTbl.Columns.Add("StockName", typeof(string));
            StockTbl.Columns.Add("BrandName", typeof(string));
            StockTbl.Columns.Add("BatchName", typeof(string));
            StockTbl.Columns.Add("OpeningStock", typeof(decimal));
            StockTbl.Columns.Add("GST", typeof(decimal));
            StockTbl.Columns.Add("PurchasePrice", typeof(decimal));
            StockTbl.Columns.Add("StockIn", typeof(decimal));
            StockTbl.Columns.Add("StockOut", typeof(decimal));
            StockTbl.Columns.Add("OnHand", typeof(decimal));
            StockTbl.Columns.Add("TotalAmount", typeof(decimal));
        }
        private void GetStockOnHand()
        {
            try
            {                
                GrdSalesDetails.DataSource = null;
                StockTbl.Clear();
                decimal TotAmt = 0;
                decimal QtyInAmt = 0;
                decimal QtyOutAmt = 0;
                decimal OPAmt = 0;
                decimal OHAmt = 0;
                decimal CostAmt = 0;
                DateTime UptoDt;
                UptoDt = DtpUpto.Value.Date;
                var OpList = (from bth in cmpDBContext.Batch
                              join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                              where stk.InventoryType.IsStkDeduct == true
                              select new
                              {
                                  stk.StockId,
                                  bth.BatchId,
                                  stk.StockName,
                                  stk.BrandName,
                                  bth.BatchName,
                                  bth.OpeningStock,
                                  bth.GST,
                                  bth.PurchasePrice,
                                  StkOut = bth.StockOut,
                                  StkIn = bth.StockIn
                              }).ToList();

                var invList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               join stkTr in cmpDBContext.StkTrans on bth.StockId equals stkTr.StocksId
                               where stkTr.TranDate <= UptoDt
                               && (StockId != 0) ? stk.StockId == StockId : true
                               && bth.BatchId == stkTr.BatchId
                               && stk.InventoryType.IsStkDeduct == true
                               group stkTr by
                               new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   stk.BrandName,
                                   bth.BatchName,
                                   bth.OpeningStock,
                                   bth.GST,
                                   bth.PurchasePrice,
                               } into g
                               select new
                               {
                                   g.Key.StockId,
                                   g.Key.BatchId,
                                   g.Key.StockName,
                                   g.Key.BrandName,
                                   g.Key.BatchName,
                                   g.Key.GST,
                                   g.Key.PurchasePrice,
                                   g.Key.OpeningStock,
                                   TotQtyOut = g.Sum(m => m.QtyOut),
                                   TotQtyIn = g.Sum(m => m.QtyIn),
                                   OnHand = (g.Key.OpeningStock + g.Sum(m => m.QtyIn)) - g.Sum(m => m.QtyOut),
                                   TotalAmount = ((g.Key.PurchasePrice * ((g.Key.OpeningStock + g.Sum(m => m.QtyIn)) - g.Sum(m => m.QtyOut))) * (1 + (g.Key.GST / 100))),
                               }).ToList();
                if (OpList.Count() != 0)
                {
                    foreach (var item in OpList)
                    {
                        DataRow stkRow = StockTbl.NewRow();
                        stkRow["StockId"] = item.StockId;
                        stkRow["BatchId"] = item.BatchId;
                        stkRow["StockName"] = item.StockName;
                        stkRow["BrandName"] = item.BrandName;
                        stkRow["BatchName"] = item.BatchName;
                        stkRow["OpeningStock"] = item.OpeningStock;
                        stkRow["GST"] = item.GST;
                        stkRow["PurchasePrice"] = item.PurchasePrice;
                        stkRow["StockIn"] = item.StkIn;
                        stkRow["StockOut"] = item.StkOut;
                        stkRow["OnHand"] = (item.OpeningStock + item.StkIn) - item.StkOut;
                        stkRow["TotalAmount"] = Convert.ToDecimal(stkRow["OnHand"]) * Convert.ToDecimal(stkRow["PurchasePrice"]);
                        foreach (var inv in invList)
                        {
                            if (inv.StockId == item.StockId && inv.BatchId == item.BatchId)
                            {
                                stkRow["StockIn"] = inv.TotQtyIn;
                                stkRow["StockOut"] = inv.TotQtyOut;
                                stkRow["OnHand"] = (item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut;
                                stkRow["TotalAmount"] = item.PurchasePrice * ((item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut);
                            }
                        }
                        StockTbl.Rows.Add(stkRow);
                    }
                }
                if (StockTbl.Rows.Count != 0)
                {
                    foreach (DataRow inv in StockTbl.Rows)
                    {
                        TotAmt += Convert.ToDecimal(inv["TotalAmount"]);
                        QtyInAmt += Convert.ToDecimal(inv["StockIn"]);
                        QtyOutAmt += Convert.ToDecimal(inv["StockOut"]);
                        OPAmt += Convert.ToDecimal(inv["OpeningStock"]);
                        OHAmt += Convert.ToDecimal(inv["OnHand"]);
                        CostAmt += Convert.ToDecimal(inv["PurchasePrice"]);
                    }
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StockTbl;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[2].Value = "Total Records: " + StockTbl.Rows.Count;
                    row.Cells[6].Value = OPAmt;
                    row.Cells[7].Value = QtyInAmt;
                    row.Cells[8].Value = QtyOutAmt;
                    row.Cells[9].Value = OHAmt;
                    row.Cells[10].Value = CostAmt;
                    row.Cells[11].Value = TotAmt;
                }
                else
                {
                    MessageBox.Show("No Records Found!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SetStockDeatils(int stockId, int batchId)
        {
            try
            {
                StockId = 0;
                var stkList = (from stk in cmpDBContext.Stock
                               join bth in cmpDBContext.Batch on stk.StockId equals bth.StockId
                               where stk.Status == true && bth.StockId == stockId && bth.BatchId == batchId
                               && bth.StockOnHand > 0
                               select new
                               {
                                   stk.StockId,
                                   stk.StockName,
                                   bth.BatchName,
                                   stk.UnitMeasure,
                                   bth.Expiry,
                                   bth.GST,
                                   bth.PurchasePrice,
                                   bth.Mrp,
                                   bth.Cess,
                                   bth.AddlCess,
                                   bth.StockOnHand,
                                   bth.SellingPriceA,
                                   stk.HSNCode
                               });
                foreach (var stklst in stkList)
                {
                    StockId = stklst.StockId;
                    //TxtItem.Text = stklst.StockName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }        
        private void ExportToExcel()
        {
            try
            {
                DateTime fromDt;
                //DateTime toDt;
                fromDt = DtpUpto.Value.Date;
                //toDt = DtpTo.Value.Date;
                // creating Excel Application  
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                // creating new WorkBook within Excel application  
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // creating new Excelsheet in workbook  
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                // see the excel sheet behind the program  
                app.Visible = true;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                // changing the name of active sheet  
                worksheet.Name = "Stock OnHand ";// + fromDt;
                // storing header part in Excel                
                for (int i = 1; i < GrdSalesDetails.Columns.Count + 1; i++)
                {
                    if (GrdSalesDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdSalesDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdSalesDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdSalesDetails.Columns.Count; j++)
                    {
                        if (GrdSalesDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdSalesDetails.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                Microsoft.Office.Interop.Excel.Range xlRange = worksheet.UsedRange;
                xlRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                xlRange.Columns.AutoFit();
                xlRange.Borders.Weight = 1d;

                // save the application  
                //workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                // Exit from the application  
                app.Quit();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event Handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetStockOnHand();
        }        
        private void FrmStockOnHand_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStockTbl();
            GetStockOnHand();
        }        
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdSalesDetails.Rows.Count > 0)
            {
                ExportToExcel();
            }
            else
            {
                MessageBox.Show("No Records Found!!!");
            }
        }
        private void FrmStockOnHand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnView_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                GrdSalesDetails.DataSource = null;
                StockTbl.Clear();
                decimal TotAmt = 0;
                decimal QtyInAmt = 0;
                decimal QtyOutAmt = 0;
                decimal OPAmt = 0;
                decimal OHAmt = 0;
                decimal CostAmt = 0;
                DateTime UptoDt;
                UptoDt = DtpUpto.Value.Date;
                var OpList = (from bth in cmpDBContext.Batch
                              join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                              where stk.StockName.Contains(textvalue) || stk.BrandName.Contains(textvalue)
                              && stk.InventoryType.IsStkDeduct == true
                              select new
                              {
                                  stk.StockId,
                                  bth.BatchId,
                                  stk.StockName,
                                  stk.BrandName,
                                  bth.BatchName,
                                  bth.OpeningStock,
                                  bth.GST,
                                  bth.PurchasePrice,
                                  StkOut = bth.StockOut,
                                  StkIn = bth.StockIn
                              }).ToList();

                var invList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               join stkTr in cmpDBContext.StkTrans on bth.StockId equals stkTr.StocksId
                               where stkTr.TranDate <= UptoDt
                               && stk.StockName.Contains(textvalue) || stk.BrandName.Contains(textvalue)
                               && bth.BatchId == stkTr.BatchId
                               && stk.InventoryType.IsStkDeduct == true
                               group stkTr by
                               new
                               {
                                   stk.StockId,
                                   bth.BatchId,
                                   stk.StockName,
                                   stk.BrandName,
                                   bth.BatchName,
                                   bth.OpeningStock,
                                   bth.GST,
                                   bth.PurchasePrice,
                               } into g
                               select new
                               {
                                   g.Key.StockId,
                                   g.Key.BatchId,
                                   g.Key.StockName,
                                   g.Key.BrandName,
                                   g.Key.BatchName,
                                   g.Key.GST,
                                   g.Key.PurchasePrice,
                                   g.Key.OpeningStock,
                                   TotQtyOut = g.Sum(m => m.QtyOut),
                                   TotQtyIn = g.Sum(m => m.QtyIn),
                                   OnHand = (g.Key.OpeningStock + g.Sum(m => m.QtyIn)) - g.Sum(m => m.QtyOut),
                                   TotalAmount = ((g.Key.PurchasePrice * ((g.Key.OpeningStock + g.Sum(m => m.QtyIn)) - g.Sum(m => m.QtyOut))) * (1 + (g.Key.GST / 100))),
                               }).ToList();
                if (OpList.Count() != 0)
                {
                    foreach (var item in OpList)
                    {
                        DataRow stkRow = StockTbl.NewRow();
                        stkRow["StockId"] = item.StockId;
                        stkRow["BatchId"] = item.BatchId;
                        stkRow["StockName"] = item.StockName;
                        stkRow["BrandName"] = item.BrandName;
                        stkRow["BatchName"] = item.BatchName;
                        stkRow["OpeningStock"] = item.OpeningStock;
                        stkRow["GST"] = item.GST;
                        stkRow["PurchasePrice"] = item.PurchasePrice;
                        stkRow["StockIn"] = item.StkIn;
                        stkRow["StockOut"] = item.StkOut;
                        stkRow["OnHand"] = (item.OpeningStock + item.StkIn) - item.StkOut;
                        stkRow["TotalAmount"] = Convert.ToDecimal(stkRow["OnHand"]) * Convert.ToDecimal(stkRow["PurchasePrice"]);
                        foreach (var inv in invList)
                        {
                            if (inv.StockId == item.StockId && inv.BatchId == item.BatchId)
                            {
                                stkRow["StockIn"] = inv.TotQtyIn;
                                stkRow["StockOut"] = inv.TotQtyOut;
                                stkRow["OnHand"] = (item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut;
                                stkRow["TotalAmount"] = item.PurchasePrice * ((item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut);
                            }
                        }
                        StockTbl.Rows.Add(stkRow);
                    }
                }
                if (StockTbl.Rows.Count != 0)
                {
                    foreach (DataRow inv in StockTbl.Rows)
                    {
                        TotAmt += Convert.ToDecimal(inv["TotalAmount"]);
                        QtyInAmt += Convert.ToDecimal(inv["StockIn"]);
                        QtyOutAmt += Convert.ToDecimal(inv["StockOut"]);
                        OPAmt += Convert.ToDecimal(inv["OpeningStock"]);
                        OHAmt += Convert.ToDecimal(inv["OnHand"]);
                        CostAmt += Convert.ToDecimal(inv["PurchasePrice"]);
                    }
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StockTbl;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[2].Value = "Total Records: " + StockTbl.Rows.Count;
                    row.Cells[6].Value = OPAmt;
                    row.Cells[7].Value = QtyInAmt;
                    row.Cells[8].Value = QtyOutAmt;
                    row.Cells[9].Value = OHAmt;
                    row.Cells[10].Value = CostAmt;
                    row.Cells[11].Value = TotAmt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GrdSalesDetails.DataSource = null;
            GrdSummary.DataSource = null;
            GrdSummary.Rows.Clear();
        }
        #endregion

        private void FrmStockOnHand_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
