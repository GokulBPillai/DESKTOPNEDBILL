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
    public partial class FrmOpeningStock : Form
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
        public FrmOpeningStock()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));
            BtnRefresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnRefresh.Width, BtnRefresh.Height, 5, 5));

        }

        #region Event Handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetStockList();
        }

        #endregion

        #region Get & Post Methods
        public void GetStockList()
        {
            try
            {
                //string textvalue = txtSearch.Text;
                decimal TotCost = 0;
                var batch = (from batc in cmpDBContext.Batch
                             join stk in cmpDBContext.Stock on batc.StockId equals stk.StockId
                             where batc.OpeningStock > 0
                             //where stk.StockName.Contains(textvalue)
                             select new
                             {
                                 batc.StockId,
                                 stk.StockName,
                                 batc.BatchName,
                                 batc.OpeningStock,
                                 batc.PurchasePrice,
                             }).ToList();
                if (batch.Count != 0)
                {
                    foreach (var bat in batch)
                    {
                        TotCost += bat.PurchasePrice;
                    }
                    GrdStockDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = batch;
                    GrdStockDetails.AutoGenerateColumns = false;
                    GrdStockDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + batch.Count;
                    row.Cells[3].Value = TotCost;
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
        #endregion

        private void FrmOpeningStock_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdStockDetails.Rows.Count > 0)
            {
                ExportToExcel();
            }
        }
        private void ExportToExcel()
        {
            try
            {
                //DateTime fromDt;
                //DateTime toDt;
                //fromDt = DtpFrom.Value.Date;
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
                worksheet.Name = "Opening Stock";
                // storing header part in Excel                
                for (int i = 1; i < GrdStockDetails.Columns.Count + 1; i++)
                {
                    if (GrdStockDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdStockDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdStockDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdStockDetails.Columns.Count; j++)
                    {
                        if (GrdStockDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdStockDetails.Rows[i].Cells[j].Value.ToString();
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

        private void FrmOpeningStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                BtnRefresh_Click(sender, e);
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
                string textvalue = txtSearch.Text;
                decimal TotCost = 0;
                var batch = (from batc in cmpDBContext.Batch
                             join stk in cmpDBContext.Stock on batc.StockId equals stk.StockId
                             where batc.OpeningStock > 0
                             where stk.StockName.Contains(textvalue)
                             select new
                             {
                                 batc.StockId,
                                 stk.StockName,
                                 batc.BatchName,
                                 batc.OpeningStock,
                                 batc.PurchasePrice,
                             }).ToList();
                if (batch.Count != 0)
                {
                    foreach (var bat in batch)
                    {
                        TotCost += bat.PurchasePrice;
                    }
                    GrdStockDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = batch;
                    GrdStockDetails.AutoGenerateColumns = false;
                    GrdStockDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + batch.Count;
                    row.Cells[3].Value = TotCost;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GrdStockDetails.DataSource = null;
            GrdSummary.DataSource=null;
            GrdSummary.Rows.Clear();
        }

        private void FrmOpeningStock_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
