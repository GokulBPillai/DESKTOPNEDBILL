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
    public partial class FrmDayStock : Form
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
        DataTable DStkTbl = new DataTable();
        public FrmDayStock()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));
            //btnExportToExcel.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            //GetDayStockDetails();
            GetStockOnHand();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GetDayStockDetails()
        {
            try
            {
                decimal Total = 0;
                DateTime fromDt;
                fromDt = DtpFrom.Value.Date;
                //var daypreviousDet = cmpDBContext.StkTrans.GroupBy(x => x.BatchId).Select(g => new { Id = g.Key, Sum = g.Sum(x => x.QtyOut), sum = g.Sum(x => x.QtyIn) });
                //var det=daypreviousDet.Where(x=>x.tran)
                //    var daypreviousDet = (from c in cmpDBContext.StkTrans                                     
                //                          group c by c.BatchId
                //                          select new { Jenis = g.Key, Count = g.Count() }
                //).ToDictionary(x => x.Jenis, x => x.Count);
                var daystks = (from stktran in cmpDBContext.StkTrans
                               join stks in cmpDBContext.Stock on stktran.StocksId equals stks.StockId
                               join batch in cmpDBContext.Batch on stktran.BatchId equals batch.BatchId
                               where stktran.TranDate.Date == fromDt
                               select new
                               {
                                   stks.StockId,
                                   batch.BatchId,
                                   stks.StockName,
                                   batch.BatchName,
                                   batch.OpeningStock,
                                   stktran.QtyIn,
                                   stktran.QtyOut,
                                   batch.StockOnHand,
                                   batch.PurchasePrice,
                               }).ToList();
                decimal OpCase = 0;
                decimal StckIn = 0;
                decimal StckOut = 0;
                foreach (var item in daystks)
                {
                    DataRow stkRow = DStkTbl.NewRow();
                    stkRow["StockId"] = item.StockId;
                    stkRow["BatchId"] = item.BatchId;
                    stkRow["StockName"] = item.StockName;
                    stkRow["BatchName"] = item.BatchName;
                    OpCase = item.OpeningStock;
                    var stkList = (from stTr in cmpDBContext.StkTrans
                                   where stTr.TranDate.Date < fromDt
                                   group stTr by stTr.BatchId into grp
                                   select new
                                   {
                                       StkIn = grp.Sum(x => x.QtyIn),
                                       StkOut = grp.Sum(x => x.QtyOut),
                                   }).ToList();
                    if (stkList.Count() > 0)
                    {
                        StckIn = Convert.ToDecimal(stkList.First().StkIn);
                        StckOut = Convert.ToDecimal(stkList.First().StkOut);
                    }
                    stkRow["Opening"] = OpCase + StckIn - StckOut;
                    stkRow["StockIn"] = item.QtyIn;
                    stkRow["StockOut"] = item.QtyOut;
                    stkRow["Balance"] = OpCase + item.QtyIn - item.QtyOut;
                    stkRow["Cost"] = item.PurchasePrice;
                    stkRow["Amount"] = Convert.ToDecimal(stkRow["Balance"]) * item.PurchasePrice;
                    Total += Convert.ToDecimal(stkRow["Amount"]);
                    DStkTbl.Rows.Add(stkRow);
                }
                if (daystks.Count != 0)
                {
                    GrdDayStock.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = DStkTbl;
                    GrdDayStock.AutoGenerateColumns = false;
                    GrdDayStock.DataSource = bindingSource;

                }
                else
                {
                    MessageBox.Show("No Records Found!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CreateDayStockTbl()
        {
            DStkTbl.Columns.Add("StockId", typeof(int));
            DStkTbl.Columns.Add("BatchId", typeof(int));
            DStkTbl.Columns.Add("StockName", typeof(string));
            DStkTbl.Columns.Add("BatchName", typeof(string));
            DStkTbl.Columns.Add("PurchasePrice", typeof(decimal));
            DStkTbl.Columns.Add("Opening", typeof(decimal));
            DStkTbl.Columns.Add("StockIn", typeof(decimal));
            DStkTbl.Columns.Add("StockOut", typeof(decimal));
            DStkTbl.Columns.Add("Balance", typeof(decimal));
            DStkTbl.Columns.Add("Amount", typeof(decimal));
        }
        private void GetStockOnHand()
        {
            try
            {
                //if (CmbItem.Text == "")
                //{
                //    StockId = 0;
                //}
                GrdDayStock.DataSource = null;
                DStkTbl.Clear();
                decimal TotAmt = 0;
                decimal QtyInAmt = 0;
                decimal QtyOutAmt = 0;
                decimal OPAmt = 0;
                decimal OHAmt = 0;
                decimal CostAmt = 0;
                DateTime UptoDt;
                UptoDt = DtpFrom.Value.Date;
                var OpList = (from bth in cmpDBContext.Batch
                              join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                              where
                              //(StockId != 0) ? stk.StockId == StockId : true &&
                              stk.InventoryType.IsStkDeduct == true
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
                                  StkOut = 0,
                                  StkIn = 0
                              }).ToList();

                var invList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               join stkTr in cmpDBContext.StkTrans on bth.StockId equals stkTr.StocksId
                               where stkTr.TranDate <= UptoDt
                              //&& (StockId != 0) ? stk.StockId == StockId : true
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
                if (OpList.Count != 0)
                {
                    foreach (var item in OpList)
                    {
                        DataRow stkRow = DStkTbl.NewRow();
                        stkRow["StockId"] = item.StockId;
                        stkRow["BatchId"] = item.BatchId;
                        stkRow["StockName"] = item.StockName;
                        stkRow["BatchName"] = item.BatchName;
                        stkRow["Opening"] = item.OpeningStock;
                        stkRow["PurchasePrice"] = item.PurchasePrice;
                        stkRow["StockIn"] = item.StkIn;
                        stkRow["StockOut"] = item.StkOut;
                        stkRow["Balance"] = (item.OpeningStock + item.StkIn) - item.StkOut;
                        stkRow["Amount"] = Convert.ToDecimal(stkRow["Balance"]) * item.PurchasePrice;
                        foreach (var inv in invList)
                        {
                            if (inv.StockId == item.StockId && inv.BatchId == item.BatchId)
                            {
                                stkRow["StockIn"] = inv.TotQtyIn;
                                stkRow["StockOut"] = inv.TotQtyOut;
                                stkRow["Balance"] = (item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut;
                                stkRow["Amount"] = item.PurchasePrice * ((item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut);
                            }
                        }
                        DStkTbl.Rows.Add(stkRow);
                    }
                }
                if (DStkTbl.Rows.Count != 0)
                {
                    foreach (DataRow inv in DStkTbl.Rows)
                    {
                        TotAmt += Convert.ToDecimal(inv["Amount"]);
                        QtyInAmt += Convert.ToDecimal(inv["StockIn"]);
                        QtyOutAmt += Convert.ToDecimal(inv["StockOut"]);
                        OPAmt += Convert.ToDecimal(inv["Opening"]);
                        OHAmt += Convert.ToDecimal(inv["Balance"]);
                        CostAmt += Convert.ToDecimal(inv["PurchasePrice"]);
                    }
                    GrdDayStock.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = DStkTbl;
                    GrdDayStock.AutoGenerateColumns = false;
                    GrdDayStock.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + DStkTbl.Rows.Count;
                    row.Cells[2].Value = OPAmt;
                    row.Cells[3].Value = QtyInAmt;
                    row.Cells[4].Value = QtyOutAmt;
                    row.Cells[5].Value = OHAmt;
                    row.Cells[6].Value = CostAmt;
                    row.Cells[7].Value = TotAmt;
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

        private void FrmDayStock_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateDayStockTbl();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdDayStock.Rows.Count > 0)
            {
                ExportToExcel();
            }
        }
        private void ExportToExcel()
        {
            try
            {
                DateTime fromDt;
                //DateTime toDt;
                fromDt = DtpFrom.Value.Date;
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
                worksheet.Name = "Day Stock "; //+ fromDt;
                // storing header part in Excel                
                for (int i = 1; i < GrdDayStock.Columns.Count + 1; i++)
                {
                    if (GrdDayStock.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdDayStock.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdDayStock.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdDayStock.Columns.Count; j++)
                    {
                        if (GrdDayStock.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdDayStock.Rows[i].Cells[j].Value.ToString();
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

        private void FrmDayStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
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
                GrdDayStock.DataSource = null;
                DStkTbl.Clear();
                decimal TotAmt = 0;
                decimal QtyInAmt = 0;
                decimal QtyOutAmt = 0;
                decimal OPAmt = 0;
                decimal OHAmt = 0;
                decimal CostAmt = 0;
                DateTime UptoDt;
                UptoDt = DtpFrom.Value.Date;
                var OpList = (from bth in cmpDBContext.Batch
                              join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                              where stk.StockName.Contains(textvalue) &&
                              stk.InventoryType.IsStkDeduct == true
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
                                  StkOut = 0,
                                  StkIn = 0
                              }).ToList();

                var invList = (from bth in cmpDBContext.Batch
                               join stk in cmpDBContext.Stock on bth.StockId equals stk.StockId
                               join stkTr in cmpDBContext.StkTrans on bth.StockId equals stkTr.StocksId
                               where stkTr.TranDate <= UptoDt
                               where stk.StockName.Contains(textvalue)
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
                if (OpList.Count != 0)
                {
                    foreach (var item in OpList)
                    {
                        DataRow stkRow = DStkTbl.NewRow();
                        stkRow["StockId"] = item.StockId;
                        stkRow["BatchId"] = item.BatchId;
                        stkRow["StockName"] = item.StockName;
                        stkRow["BatchName"] = item.BatchName;
                        stkRow["Opening"] = item.OpeningStock;
                        stkRow["PurchasePrice"] = item.PurchasePrice;
                        stkRow["StockIn"] = item.StkIn;
                        stkRow["StockOut"] = item.StkOut;
                        stkRow["Balance"] = (item.OpeningStock + item.StkIn) - item.StkOut;
                        stkRow["Amount"] = Convert.ToDecimal(stkRow["Balance"]) * item.PurchasePrice;
                        foreach (var inv in invList)
                        {
                            if (inv.StockId == item.StockId && inv.BatchId == item.BatchId)
                            {
                                stkRow["StockIn"] = inv.TotQtyIn;
                                stkRow["StockOut"] = inv.TotQtyOut;
                                stkRow["Balance"] = (item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut;
                                stkRow["Amount"] = item.PurchasePrice * ((item.OpeningStock + inv.TotQtyIn) - inv.TotQtyOut);
                            }
                        }
                        DStkTbl.Rows.Add(stkRow);
                    }
                }
                if (DStkTbl.Rows.Count != 0)
                {
                    foreach (DataRow inv in DStkTbl.Rows)
                    {
                        TotAmt += Convert.ToDecimal(inv["Amount"]);
                        QtyInAmt += Convert.ToDecimal(inv["StockIn"]);
                        QtyOutAmt += Convert.ToDecimal(inv["StockOut"]);
                        OPAmt += Convert.ToDecimal(inv["Opening"]);
                        OHAmt += Convert.ToDecimal(inv["Balance"]);
                        CostAmt += Convert.ToDecimal(inv["PurchasePrice"]);
                    }
                    GrdDayStock.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = DStkTbl;
                    GrdDayStock.AutoGenerateColumns = false;
                    GrdDayStock.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + DStkTbl.Rows.Count;
                    row.Cells[2].Value = OPAmt;
                    row.Cells[3].Value = QtyInAmt;
                    row.Cells[4].Value = QtyOutAmt;
                    row.Cells[5].Value = OHAmt;
                    row.Cells[6].Value = CostAmt;
                    row.Cells[7].Value = TotAmt;
                }
                else
                {
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    GrdDayStock.DataSource = null;                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GrdDayStock.DataSource = null;
            GrdSummary.DataSource = null;
            GrdSummary.Rows.Clear();
        }

        private void FrmDayStock_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
