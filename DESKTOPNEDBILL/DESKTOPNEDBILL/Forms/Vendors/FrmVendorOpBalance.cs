using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Forms.Stock;
using DESKTOPNEDBILL.Module;
using Microsoft.Office.Interop.Excel;
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

namespace DESKTOPNEDBILL.Forms.Vendors
{
    public partial class FrmVendorOpBalance : Form
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
        public FrmVendorOpBalance()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0,BtnClear.Width, BtnClear.Height, 5, 5));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));            
        }
        #region Get & Post Methods
        public void GetVendorList()
        {
            try
            {
                decimal OpBal = 0;
                var vendors = (from vend in cmpDBContext.Vendor
                               where vend.OpBal > 0
                               select new
                               {
                                   vend.VendorId,
                                   vend.VendorName,
                                   Address = vend.VendorAdd1 + "," + vend.VendorAdd2,
                                   vend.OpBal,
                                   vend.TotalBal,
                               }
                             ).ToList();
                if (vendors.Count != 0)
                {
                    foreach (var vnd in vendors)
                    {
                        OpBal += vnd.OpBal;
                    }
                    GrdVendorDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vendors;
                    GrdVendorDetails.AutoGenerateColumns = false;
                    GrdVendorDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + vendors.Count;
                    row.Cells[2].Value = OpBal;
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

        #region Event Handing Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetVendorList();
        }
        #endregion

        private void FrmVendorOpBalance_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdVendorDetails.Rows.Count > 0)
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
                worksheet.Name = "Vendor OpBalance";
                // storing header part in Excel                
                for (int i = 1; i < GrdVendorDetails.Columns.Count + 1; i++)
                {
                    if (GrdVendorDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdVendorDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdVendorDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdVendorDetails.Columns.Count; j++)
                    {
                        if (GrdVendorDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdVendorDetails.Rows[i].Cells[j].Value.ToString();
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                decimal OpBal = 0;
                var vendors = (from vend in cmpDBContext.Vendor
                               where vend.OpBal > 0
                               where vend.VendorName.Contains(textvalue)
                               && vend.VendorAdd1.Contains(textvalue)
                               select new
                               {
                                   vend.VendorId,
                                   vend.VendorName,
                                   Address = vend.VendorAdd1 + "," + vend.VendorAdd2,
                                   vend.OpBal,
                                   vend.TotalBal,
                               }
                             ).ToList();
                if (vendors.Count != 0)
                {
                    foreach (var vnd in vendors)
                    {
                        OpBal += vnd.OpBal;
                    }
                    GrdVendorDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vendors;
                    GrdVendorDetails.AutoGenerateColumns = false;
                    GrdVendorDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + vendors.Count;
                    row.Cells[2].Value = OpBal;                    
                }
                else
                {
                    GrdVendorDetails.DataSource = null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
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
            GrdVendorDetails.DataSource = null;
            GrdSummary.DataSource= null;
            GrdSummary.Rows.Clear();    
        }

        private void FrmVendorOpBalance_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmVendorOpBalance_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
