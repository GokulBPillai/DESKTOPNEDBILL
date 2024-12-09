using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Forms.Sales;
using DESKTOPNEDBILL.Forms.Vendors;
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
    public partial class FrmPurchaseSummary : Form
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
        int VendorId = 0;
        int StockId = 0;
        public FrmPurchaseSummary()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }
        #region Get & Set Methods
        private void GetSalesDetailAll()
        {
            try
            {
                decimal SalesAmt = 0;
                decimal QtyAmt = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from stkTr in cmpDBContext.StkTrans
                               join purInvMst in cmpDBContext.PurchaseInvMasters on stkTr.InvNo equals purInvMst.PurInvNo
                               join Vend in cmpDBContext.Vendor on purInvMst.VendID equals Vend.VendorId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate.Date >= fromDt && stkTr.TranDate.Date <= toDt)
                               && stkTr.TranType == "Sales"
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   Vend.VendorName,
                                   stk.StockName,
                                   stkTr.QtyOut,
                                   stkTr.TotalAmount,
                               }).ToList();
                if (invList.Count() != 0)
                {
                    foreach (var inv in invList)
                    {
                        SalesAmt += inv.TotalAmount;
                        QtyAmt += inv.QtyOut;
                    }
                    GrdPurchaseDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdPurchaseDetails.AutoGenerateColumns = false;
                    GrdPurchaseDetails.DataSource = bindingSource;                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }       
        private void GetPurchaseDetailByStockIDCustomerId(int StkId, int VendId)
        {
            try
            {
                decimal PurchaseAmt = 0;
                decimal QtyAmt = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from stkTr in cmpDBContext.StkTrans
                               join purInvMst in cmpDBContext.PurchaseInvMasters on stkTr.InvNo equals purInvMst.PurInvNo
                               join vend in cmpDBContext.Vendor on purInvMst.VendID equals vend.VendorId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate >= fromDt && stkTr.TranDate <= toDt) && stkTr.TranType == "Purchase"
                               where ((VendId != 0 && StkId != 0) ? (vend.VendorId == VendId && stk.StockId == StkId) : false
                               || (StkId != 0) ? stk.StockId == StkId : false
                               || (VendId != 0) ? vend.VendorId == VendId : true)
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   vend.VendorName,
                                   stk.StockName,
                                   stkTr.QtyIn,
                                   stkTr.TotalAmount,
                               }).ToList();
                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        PurchaseAmt += inv.TotalAmount;
                        QtyAmt += inv.QtyIn;
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
                    row.Cells[0].Value = "Total Records: " + invList.Count();
                    row.Cells[4].Value = QtyAmt;
                    row.Cells[5].Value = PurchaseAmt;
                }
                else
                {
                    GrdPurchaseDetails.DataSource = null;
                    MessageBox.Show("No Records Found!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetVendorDetails(int VendID)
        {
            try
            {
                VendorId = 0;
                var vendList = (from cust in cmpDBContext.Vendor
                                where cust.Status == true && cust.VendorId == VendID
                                select new
                                {
                                    cust.VendorName,
                                    cust.VendorAdd1,
                                    cust.VendorAdd2,
                                    cust.TotalBal
                                }).ToList();
                foreach (var vnd in vendList)
                {
                    VendorId = VendID;
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
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtItem_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProductSelectListSales frmproductList = new FrmProductSelectListSales();
                frmproductList.ShowDialog();
                if (MdlMain.gStockId > 0 && MdlMain.gBatchId > 0)
                {
                    SetStockDeatils(MdlMain.gStockId, MdlMain.gBatchId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            GetPurchaseDetailByStockIDCustomerId(StockId, VendorId);
        }

        private void TxtVendor_Click(object sender, EventArgs e)
        {
            FrmVendorSelectList frmVendorSelectList = new FrmVendorSelectList();
            frmVendorSelectList.ShowDialog();
            SetVendorDetails(MdlMain.gVendorId);
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
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
                for (int i = 1; i < GrdPurchaseDetails.Columns.Count + 1; i++)
                {
                    if (GrdPurchaseDetails.Columns[i - 1].Visible)
                    {
                        worksheet.Cells[1, i] = GrdPurchaseDetails.Columns[i - 1].HeaderText;
                        worksheet.Cells[1, i].Font.Bold = true;
                        //worksheet.Cells[0, i].Style.Font.Bold = true;
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

        private void BtnClear_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                decimal PurchaseAmt = 0;
                decimal QtyAmt = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from stkTr in cmpDBContext.StkTrans
                               join purInvMst in cmpDBContext.PurchaseInvMasters on stkTr.InvNo equals purInvMst.PurInvNo
                               join vend in cmpDBContext.Vendor on purInvMst.VendID equals vend.VendorId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate >= fromDt && stkTr.TranDate <= toDt) && stkTr.TranType == "Purchase"
                               where vend.VendorName.Contains(textvalue) || stk.StockName.Contains(textvalue)
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   vend.VendorName,
                                   stk.StockName,
                                   stkTr.QtyIn,
                                   stkTr.TotalAmount,
                               }).ToList();
                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        PurchaseAmt += inv.TotalAmount;
                        QtyAmt += inv.QtyIn;
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
                    row.Cells[0].Value = "Total Records: " + invList.Count();
                    row.Cells[4].Value = QtyAmt;
                    row.Cells[5].Value = PurchaseAmt;
                }
                else
                {
                    GrdPurchaseDetails.DataSource = null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmPurchaseSummary_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmPurchaseSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
