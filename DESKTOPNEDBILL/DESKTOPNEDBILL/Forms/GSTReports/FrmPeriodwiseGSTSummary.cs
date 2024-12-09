using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models.Entities;

namespace DESKTOPNEDBILL.Forms.GSTReports
{
    public partial class FrmPeriodwiseGSTSummary : Form
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
        public FrmPeriodwiseGSTSummary()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));

        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetGSTDetails();
        }
        private void PBExcel_Click(object sender, EventArgs e)
        {
            if (GrdGstDetails.Rows.Count > 0)
            {
                ExportToExcel();
            }
            else
            {
                MessageBox.Show("No Records Found!!!");
            }
        }
        private void GetGSTDetails()
        {
            try
            {
                decimal Amt0 = 0;
                decimal TaxAmt0 = 0;
                decimal Amt5 = 0;
                decimal TaxAmt5 = 0;
                decimal Amt12 = 0;
                decimal TaxAmt12 = 0;
                decimal Amt18 = 0;
                decimal TaxAmt18 = 0;
                decimal Amt28 = 0;
                decimal TaxAmt28 = 0;
                decimal Cess = 0;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;

                List<QryStkDaySummary> vwQryStkDaySummarylist =
                    cmpDBContext.Database.SqlQuery<QryStkDaySummary>(
                    "SELECT * FROM  vw_QryStkDaySummary").ToList();
                var gstList = vwQryStkDaySummarylist.Where(m => m.TranDate >= fromDt && m.TranDate <= toDt).ToList();

                if (gstList.Count != 0)
                {
                    foreach (var inv in gstList)
                    {
                        Amt0 += inv.Amount0Per;
                        TaxAmt0 += inv.Tax0Per;
                        Amt5 += inv.Amount5Per;
                        TaxAmt5 += inv.Tax5Per;
                        Amt12 += inv.Amount12Per;
                        TaxAmt12 += inv.Tax12Per;
                        Amt18 += inv.Amount18Per;
                        TaxAmt18 += inv.Tax18Per;
                        Amt28 += inv.Amount28Per;
                        TaxAmt28 += inv.Tax28Per;
                        Cess += inv.cess;
                        Total += inv.InvoiceAmt;
                    }
                    GrdGstDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = gstList;
                    GrdGstDetails.AutoGenerateColumns = false;
                    GrdGstDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + gstList.Count;
                    row.Cells[4].Value = TaxAmt0;
                    row.Cells[5].Value = Amt0;
                    row.Cells[6].Value = TaxAmt5;
                    row.Cells[7].Value = Amt5;
                    row.Cells[8].Value = TaxAmt12;
                    row.Cells[9].Value = Amt12;
                    row.Cells[10].Value = TaxAmt18;
                    row.Cells[11].Value = Amt18;
                    row.Cells[12].Value = TaxAmt28;
                    row.Cells[13].Value = Amt28;
                    row.Cells[14].Value = Cess;
                    row.Cells[15].Value = Total;
                }
                else
                {
                    GrdGstDetails.DataSource=null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmPeriodwiseGSTSummary_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
                worksheet.Name = "GST Summary Details";
                // storing header part in Excel                
                for (int i = 1; i < GrdGstDetails.Columns.Count + 1; i++)
                {
                    if (GrdGstDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdGstDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdGstDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdGstDetails.Columns.Count; j++)
                    {
                        if (GrdGstDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdGstDetails.Rows[i].Cells[j].Value.ToString();
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

        private void FrmPeriodwiseGSTSummary_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmPeriodwiseGSTSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
