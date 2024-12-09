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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmCustomerOpBalance : Form
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
        public FrmCustomerOpBalance()
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
            GetCustomerList();
        }
        public void GetCustomerList()
        {
            try
            {
                decimal OpBal = 0;
                var customers = (from cust in cmpDBContext.Customers
                                 where cust.OpeningBalance > 0
                                 select new
                                 {
                                     cust.CustomerId,
                                     cust.CustomerName,
                                     Address = cust.Address1 + ", " + cust.Address2,
                                     cust.OpeningBalance,
                                     cust.TotalBalance,
                                 }).ToList();
                if (customers.Count != 0)
                {
                    foreach (var cst in customers)
                    {
                        OpBal += cst.OpeningBalance;
                    }
                    GrdCustomerDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = customers;
                    GrdCustomerDetails.AutoGenerateColumns = false;
                    GrdCustomerDetails.DataSource = bindingSource;
                }
                else
                {
                    MessageBox.Show("No Records Found!!!");
                }
                this.GrdSummary.DataSource = null;
                this.GrdSummary.Rows.Clear();
                int rowIndex = GrdSummary.Rows.Add();
                var row = GrdSummary.Rows[rowIndex];
                row.Cells[0].Value = "Total Records: " + customers.Count;
                row.Cells[1].Value = "";
                row.Cells[2].Value = OpBal;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmCustomerOpBalance_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //GetCustomerList();
        }
        private void ExportToExcel()
        {
            try
            {

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
                worksheet.Name = "Customer OP Balance Details";
                // storing header part in Excel                
                for (int i = 1; i < GrdCustomerDetails.Columns.Count + 1; i++)
                {
                    if (GrdCustomerDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdCustomerDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdCustomerDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdCustomerDetails.Columns.Count; j++)
                    {
                        if (GrdCustomerDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdCustomerDetails.Rows[i].Cells[j].Value.ToString();
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
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdCustomerDetails.Rows.Count > 0)
            {
                ExportToExcel();
            }
            else
            {
                MessageBox.Show("No Records Found!!!");
            }
        }

        private void FrmCustomerOpBalance_KeyDown(object sender, KeyEventArgs e)
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
                decimal OpBal = 0;
                var customers = (from cust in cmpDBContext.Customers
                                 where cust.OpeningBalance > 0
                                 where cust.CustomerName.Contains(textvalue) || cust.Address1.Contains(textvalue)
                                 select new
                                 {
                                     cust.CustomerId,
                                     cust.CustomerName,
                                     Address = cust.Address1 + ", " + cust.Address2,
                                     cust.OpeningBalance,
                                     cust.TotalBalance,
                                 }).ToList();
                if (customers.Count != 0)
                {
                    foreach (var cst in customers)
                    {
                        OpBal += cst.OpeningBalance;
                    }
                    GrdCustomerDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = customers;
                    GrdCustomerDetails.AutoGenerateColumns = false;
                    GrdCustomerDetails.DataSource = bindingSource;

                    this.GrdSummary.DataSource = null;
                    this.GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + customers.Count;
                    row.Cells[1].Value = "";
                    row.Cells[2].Value = OpBal;
                }
                else
                {
                    GrdCustomerDetails.DataSource = null;
                }
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmCustomerOpBalance_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
