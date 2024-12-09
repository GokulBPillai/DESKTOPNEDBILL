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
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Purchase
{
    public partial class FrmPurchaseLedger : Form
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
        public FrmPurchaseLedger()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetPurchaseDetails(DtpFrom.Value, DtpTo.Value);
        }
        private void GetPurchaseDetails(DateTime FromDate, DateTime ToDate)
        {
            try
            {
                decimal SubTot = 0;
                decimal Discount = 0;
                decimal CGST = 0;
                decimal SGST = 0;
                decimal Cess = 0;
                decimal AddlCess = 0;
                decimal AddlDisc = 0;
                decimal Handling = 0;
                decimal RoundOff = 0;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from pInvMst in cmpDBContext.PurchaseInvMasters
                               join vnd in cmpDBContext.Vendor on pInvMst.VendID equals vnd.VendorId
                               where pInvMst.InvDate >= fromDt && pInvMst.InvDate <= toDt
                               select new
                               {
                                   pInvMst.PurInvNo,
                                   pInvMst.InvDate,
                                   vnd.VendorName,
                                   pInvMst.SubTotal,
                                   pInvMst.TotDiscount,
                                   pInvMst.TotalCGSTAmount,
                                   pInvMst.TotalSGSTAmount,
                                   pInvMst.TotalIGSTAmount,
                                   pInvMst.TotalCessAmount,
                                   pInvMst.AddlCess,
                                   pInvMst.AddlDiscount,
                                   pInvMst.Handling,
                                   pInvMst.RoundAmount,
                                   pInvMst.InvAmount,
                               }).ToList();
                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        SubTot += inv.SubTotal;
                        Discount += inv.TotDiscount;
                        CGST += inv.TotalCGSTAmount;
                        SGST += inv.TotalSGSTAmount;
                        Cess += inv.TotalCessAmount;
                        AddlCess += inv.AddlCess;
                        AddlDisc += inv.AddlDiscount;
                        Handling += inv.Handling;
                        RoundOff += inv.RoundAmount;
                        Total += inv.InvAmount;
                    }
                    GrdPurchaseDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdPurchaseDetails.AutoGenerateColumns = false;
                    GrdPurchaseDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count;
                    row.Cells[3].Value = SubTot;
                    row.Cells[4].Value = Discount;
                    row.Cells[5].Value = CGST;
                    row.Cells[6].Value = SGST;
                    row.Cells[7].Value = Cess;
                    row.Cells[8].Value = AddlCess;
                    row.Cells[9].Value = AddlDisc;
                    row.Cells[10].Value = RoundOff;
                    row.Cells[11].Value = Total;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetPurchaseDetails(DtpFrom.Value, DtpTo.Value);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PBExcel_Click(object sender, EventArgs e)
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
                worksheet.Name = "Purchase Ledger";
                // storing header part in Excel  
                for (int i = 1; i < GrdPurchaseDetails.Columns.Count + 1; i++)
                {
                    if (GrdPurchaseDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdPurchaseDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                    }
                }
                // storing Each row and column value to excel sheet  
                for (int i = 0; i < GrdPurchaseDetails.Rows.Count; i++)
                {
                    for (int j = 0; j < GrdPurchaseDetails.Columns.Count; j++)
                    {
                        if (GrdPurchaseDetails.Columns[j].Visible)
                        {
                            worksheet.Cells[i + 2, j + 1] = GrdPurchaseDetails.Rows[i].Cells[j].Value.ToString();
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

        private void FrmPurchaseLedger_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text= string.Empty;
            GrdPurchaseDetails.DataSource = null;
            GrdSummary.DataSource = null;
            GrdSummary.Rows.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                decimal SubTot = 0;
                decimal Discount = 0;
                decimal CGST = 0;
                decimal SGST = 0;
                decimal Cess = 0;
                decimal AddlCess = 0;
                decimal AddlDisc = 0;
                decimal Handling = 0;
                decimal RoundOff = 0;
                decimal Total = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from pInvMst in cmpDBContext.PurchaseInvMasters
                               join vnd in cmpDBContext.Vendor on pInvMst.VendID equals vnd.VendorId
                               where pInvMst.InvDate >= fromDt && pInvMst.InvDate <= toDt
                               where vnd.VendorName.Contains(textvalue)
                               select new
                               {
                                   pInvMst.PurInvNo,
                                   pInvMst.InvDate,
                                   vnd.VendorName,
                                   pInvMst.SubTotal,
                                   pInvMst.TotDiscount,
                                   pInvMst.TotalCGSTAmount,
                                   pInvMst.TotalSGSTAmount,
                                   pInvMst.TotalIGSTAmount,
                                   pInvMst.TotalCessAmount,
                                   pInvMst.AddlCess,
                                   pInvMst.AddlDiscount,
                                   pInvMst.Handling,
                                   pInvMst.RoundAmount,
                                   pInvMst.InvAmount,
                               }).ToList();
                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        SubTot += inv.SubTotal;
                        Discount += inv.TotDiscount;
                        CGST += inv.TotalCGSTAmount;
                        SGST += inv.TotalSGSTAmount;
                        Cess += inv.TotalCessAmount;
                        AddlCess += inv.AddlCess;
                        AddlDisc += inv.AddlDiscount;
                        Handling += inv.Handling;
                        RoundOff += inv.RoundAmount;
                        Total += inv.InvAmount;
                    }
                    GrdPurchaseDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdPurchaseDetails.AutoGenerateColumns = false;
                    GrdPurchaseDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count;
                    row.Cells[3].Value = SubTot;
                    row.Cells[4].Value = Discount;
                    row.Cells[5].Value = CGST;
                    row.Cells[6].Value = SGST;
                    row.Cells[7].Value = Cess;
                    row.Cells[8].Value = AddlCess;
                    row.Cells[9].Value = AddlDisc;
                    row.Cells[10].Value = RoundOff;
                    row.Cells[11].Value = Total;                   
                }
                else
                {
                    GrdPurchaseDetails.DataSource=null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmPurchaseLedger_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
