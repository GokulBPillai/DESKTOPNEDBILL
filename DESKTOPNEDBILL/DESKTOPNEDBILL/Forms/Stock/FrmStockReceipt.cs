using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections;
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
    public partial class FrmStockReceipt : Form
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
        public FrmStockReceipt()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));

        }
        #region Event Handling Methods
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetStockDetails();
        }
        private void GetStockDetails()
        {
            try
            {
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var stkrept = (from stktran in cmpDBContext.StkTrans
                               join stks in cmpDBContext.Stock on stktran.StocksId equals stks.StockId
                               join batch in cmpDBContext.Batch on stktran.BatchId equals batch.BatchId
                               where stktran.TranType == "Purchase" && (stktran.TranDate >= fromDt && stktran.TranDate <= toDt)
                               select new
                               {
                                   stks.StockId,
                                   stks.StockName,
                                   batch.BatchId,
                                   batch.BatchName,
                                   stktran.QtyIn,
                                   stktran.StkCost,
                                   //TotalAmount = Convert.ToDouble(stktran.QtyIn * stktran.StkCost),
                               } into x
                               group x by new { x.StockId, x.StockName, x.BatchId, x.BatchName, x.StkCost } into grp
                               select new
                               {
                                   grp.Key.StockId,
                                   grp.Key.StockName,
                                   grp.Key.BatchId,
                                   grp.Key.BatchName,
                                   grp.Key.StkCost,
                                   QtyIn = grp.Sum(x => x.QtyIn),
                                   //StkCost = grp.Sum(x => x.StkCost),
                                   TotalAmount = grp.Sum(x => x.QtyIn * x.StkCost)
                               }).ToList();
                if (stkrept.Count() != 0)
                {
                    foreach (var stkret in stkrept)
                    {
                        Total += Convert.ToDecimal(stkret.TotalAmount);
                    }
                    GrdStockReceipt.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkrept;
                    GrdStockReceipt.AutoGenerateColumns = false;
                    GrdStockReceipt.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + stkrept.Count();
                    row.Cells[4].Value = Total;
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
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void FrmStockReceipt_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdStockReceipt.Rows.Count > 0)
            {
                ExportToExcel();
            }
            else
            {
                MessageBox.Show("No Records Found!!!");
            }
        }
        private void ExportToExcel()
        {
            try
            {
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
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
                worksheet.Name = "Stock Receipt";
                // storing header part in Excel                
                for (int i = 1; i < GrdStockReceipt.Columns.Count + 1; i++)
                {
                    if (GrdStockReceipt.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdStockReceipt.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdStockReceipt.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdStockReceipt.Columns.Count; j++)
                    {
                        if (GrdStockReceipt.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdStockReceipt.Rows[i].Cells[j].Value.ToString();
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

        private void FrmStockReceipt_KeyDown(object sender, KeyEventArgs e)
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
                string textvalue = txtSearch.Text;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var stkrept = (from stktran in cmpDBContext.StkTrans
                               join stks in cmpDBContext.Stock on stktran.StocksId equals stks.StockId
                               join batch in cmpDBContext.Batch on stktran.BatchId equals batch.BatchId
                               where stktran.TranType == "Purchase" && (stktran.TranDate >= fromDt && stktran.TranDate <= toDt)
                               where stks.StockName.Contains(textvalue)
                               select new
                               {
                                   stks.StockId,
                                   stks.StockName,
                                   batch.BatchId,
                                   batch.BatchName,
                                   stktran.QtyIn,
                                   stktran.StkCost,
                                   //TotalAmount = Convert.ToDouble(stktran.QtyIn * stktran.StkCost),
                               } into x
                               group x by new { x.StockId, x.StockName, x.BatchId, x.BatchName, x.StkCost } into grp
                               select new
                               {
                                   grp.Key.StockId,
                                   grp.Key.StockName,
                                   grp.Key.BatchId,
                                   grp.Key.BatchName,
                                   grp.Key.StkCost,
                                   QtyIn = grp.Sum(x => x.QtyIn),
                                   //StkCost = grp.Sum(x => x.StkCost),
                                   TotalAmount = grp.Sum(x => x.QtyIn * x.StkCost)
                               }).ToList();
                if (stkrept.Count() != 0)
                {
                    foreach (var stkret in stkrept)
                    {
                        Total += Convert.ToDecimal(stkret.TotalAmount);
                    }
                    GrdStockReceipt.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkrept;
                    GrdStockReceipt.AutoGenerateColumns = false;
                    GrdStockReceipt.DataSource = bindingSource;


                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + stkrept.Count();
                    row.Cells[4].Value = Total;
                }
                else
                {
                    GrdStockReceipt.DataSource = null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmStockReceipt_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
