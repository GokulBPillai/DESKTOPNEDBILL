
using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.Sales
{   
    public partial class FrmSalesLedger : Form
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
        //private readonly FrmSalesInvoice frmSalesInvoice;
        DataTable InvSummTbl = new DataTable();
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmSalesLedger()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));
           
        }
        private void FrmSalesLedger_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateInvoiceSummaryTbl();
            GetSalesDetails();
        }
        private void CreateInvoiceSummaryTbl()
        {            
            InvSummTbl.Columns.Add("SubTot", typeof(decimal));
            InvSummTbl.Columns.Add("Discount", typeof(decimal));
            InvSummTbl.Columns.Add("CGST", typeof(decimal));
            InvSummTbl.Columns.Add("SGST", typeof(decimal));
            InvSummTbl.Columns.Add("IGST", typeof(decimal));
            InvSummTbl.Columns.Add("Cess", typeof(decimal));
            InvSummTbl.Columns.Add("AddlCess", typeof(decimal));
            InvSummTbl.Columns.Add("RoundOff", typeof(decimal));
            InvSummTbl.Columns.Add("Total", typeof(decimal));
            InvSummTbl.Columns.Add("RoundTotal", typeof(decimal));
            InvSummTbl.Columns.Add("TotCountDescription", typeof(string));            
        }
        private void GetSalesDetails()
        {
            try
            {
                decimal SubTot = 0;
                decimal Discount = 0;
                decimal CGST = 0;
                decimal SGST = 0;
                decimal IGST = 0;
                decimal Cess = 0;
                decimal AddlCess = 0;
                decimal Handling = 0;
                decimal RoundOff = 0;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(01);
                toDt = DtpTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               where salesInvMst.InvDate >= fromDt && salesInvMst.InvDate <= toDt
                               select new
                               {
                                   salesInvMst.SalesInvNo,
                                   salesInvMst.InvDate,
                                   cust.CustomerName,
                                   salesInvMst.SubTotal,
                                   salesInvMst.TotDiscount,
                                   salesInvMst.TotalCGSTAmount,
                                   salesInvMst.TotalSGSTAmount,
                                   salesInvMst.TotalIGSTAmount,
                                   salesInvMst.TotalCessAmount,
                                   salesInvMst.AddlCess,
                                   salesInvMst.Handling,
                                   salesInvMst.RoundAmount,
                                   salesInvMst.InvAmount,
                               }).ToList();
                if (invList.Count() != 0)
                {
                    foreach (var inv in invList)
                    {
                        SubTot += inv.SubTotal;
                        Discount += inv.TotDiscount;
                        CGST += inv.TotalCGSTAmount;
                        SGST += inv.TotalSGSTAmount;
                        IGST += inv.TotalIGSTAmount;
                        Cess += inv.TotalCessAmount;
                        AddlCess += inv.AddlCess;
                        Handling += inv.Handling;
                        RoundOff += inv.RoundAmount;
                        Total += inv.InvAmount;
                    }
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;                   

                    InvSummTbl.Clear();
                    DataRow summRow = InvSummTbl.NewRow();
                    summRow["SubTot"] = SubTot;
                    summRow["Discount"] = Discount;
                    summRow["CGST"] = CGST;
                    summRow["SGST"] = SGST;
                    summRow["IGST"] = IGST;
                    summRow["Cess"] = Cess;
                    summRow["AddlCess"] = AddlCess;
                    summRow["RoundOff"] = RoundOff;
                    summRow["Total"] = Total;                    
                    summRow["TotCountDescription"] = "Total Records: " + invList.Count;
                    InvSummTbl.Rows.Add(summRow);
                    BindingSource SummbindingSource = new BindingSource();
                    SummbindingSource.DataSource = InvSummTbl;
                    GrdSummary.AutoGenerateColumns = false;
                    GrdSummary.DataSource = SummbindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void PBExcel_Click(object sender, EventArgs e)
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
                worksheet.Name = "Sales Ledger";
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
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetSalesDetails();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSalesLedger_KeyDown(object sender, KeyEventArgs e)
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
                string textvalue= txtSearch.Text;
                decimal SubTot = 0;
                decimal Discount = 0;
                decimal CGST = 0;
                decimal SGST = 0;
                decimal IGST = 0;
                decimal Cess = 0;
                decimal AddlCess = 0;
                decimal Handling = 0;
                decimal RoundOff = 0;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(01);
                toDt = DtpTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               where salesInvMst.InvDate >= fromDt && salesInvMst.InvDate <= toDt
                               where cust.CustomerName.Contains(textvalue)
                               select new
                               {
                                   salesInvMst.SalesInvNo,
                                   salesInvMst.InvDate,
                                   cust.CustomerName,
                                   salesInvMst.SubTotal,
                                   salesInvMst.TotDiscount,
                                   salesInvMst.TotalCGSTAmount,
                                   salesInvMst.TotalSGSTAmount,
                                   salesInvMst.TotalIGSTAmount,
                                   salesInvMst.TotalCessAmount,
                                   salesInvMst.AddlCess,
                                   salesInvMst.Handling,
                                   salesInvMst.RoundAmount,
                                   salesInvMst.InvAmount,
                               }).ToList();
                if (invList.Count() != 0)
                {
                    foreach (var inv in invList)
                    {
                        SubTot += inv.SubTotal;
                        Discount += inv.TotDiscount;
                        CGST += inv.TotalCGSTAmount;
                        SGST += inv.TotalSGSTAmount;
                        IGST += inv.TotalIGSTAmount;
                        Cess += inv.TotalCessAmount;
                        AddlCess += inv.AddlCess;
                        Handling += inv.Handling;
                        RoundOff += inv.RoundAmount;
                        Total += inv.InvAmount;
                    }
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;

                    InvSummTbl.Clear();
                    DataRow summRow = InvSummTbl.NewRow();
                    summRow["SubTot"] = SubTot;
                    summRow["Discount"] = Discount;
                    summRow["CGST"] = CGST;
                    summRow["SGST"] = SGST;
                    summRow["IGST"] = IGST;
                    summRow["Cess"] = Cess;
                    summRow["AddlCess"] = AddlCess;
                    summRow["RoundOff"] = RoundOff;
                    summRow["Total"] = Total;
                    summRow["TotCountDescription"] = "Total Items: " + invList.Count();
                    InvSummTbl.Rows.Add(summRow);
                    BindingSource SummbindingSource = new BindingSource();
                    SummbindingSource.DataSource = InvSummTbl;
                    GrdSummary.AutoGenerateColumns = false;
                    GrdSummary.DataSource = SummbindingSource;
                }
                else
                {
                    GrdSalesDetails.DataSource = null;
                    GrdSummary.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdSalesDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            //FrmSalesInvoice frmSalesInvoice = new FrmSalesInvoice();
            //if (GrdSalesDetails.Columns[e.ColumnIndex].Name == "salesinvno")
            //{
            //    MdlMain.gSalesInvNo= Convert.ToInt32(GrdSalesDetails.CurrentRow.Cells[2].Value);
                
            //    frmSalesInvoice.Show();
            //    frmSalesInvoice.GetSalesInvoiceDetails();
            //    // FrmAddEditCustomer frmAddEditCustomer = new FrmAddEditCustomer(frmCustomer);
            //    //frmSalesInvoice.ShowDialog();
            //}
        }

        private void FrmSalesLedger_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
