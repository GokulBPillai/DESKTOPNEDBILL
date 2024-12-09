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

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesSummary : Form
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
        int CustomerId = 0;
        int StockId = 0;
        public FrmSalesSummary()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnRefresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnRefresh.Width, BtnRefresh.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));

        }
        #region Get & Set Methods
        private void GetSalesDetailAll(DateTime FromDate, DateTime ToDate)
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
                               join salesInvMst in cmpDBContext.SalesInvMasters on stkTr.InvNo equals salesInvMst.SalesInvNo
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate.Date >= fromDt && stkTr.TranDate.Date <= toDt)
                               && stkTr.TranType == "Sales"
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   cust.CustomerName,
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
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetSalesDetailByCustomerID(DateTime FromDate, DateTime ToDate, int CustId)
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
                               join salesInvMst in cmpDBContext.SalesInvMasters on stkTr.InvNo equals salesInvMst.SalesInvNo
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate.Date >= fromDt && stkTr.TranDate.Date <= toDt)
                               && cust.CustomerId == CustId && stkTr.TranType == "Sales"
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   cust.CustomerName,
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
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetSalesDetailByStockIDCustomerId()
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
                               join salesInvMst in cmpDBContext.SalesInvMasters on stkTr.InvNo equals salesInvMst.SalesInvNo
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate >= fromDt && stkTr.TranDate <= toDt) && stkTr.TranType == "Sales"
                               //where ((CustId != 0 && StkId != 0) ? (cust.CustomerId == CustId && stk.StockId == StkId) : false
                               //|| (StkId != 0) ? stk.StockId == StkId : false
                               //|| (CustId != 0) ? cust.CustomerId == CustId : true)
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   cust.CustomerName,
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
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count();
                    row.Cells[4].Value = QtyAmt;
                    row.Cells[5].Value = SalesAmt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetCustomerDetails(int CustID)
        {
            try
            {
                CustomerId = 0;
                var custList = (from cust in cmpDBContext.Customers
                                where cust.Status == true && cust.CustomerId == CustID
                                select new
                                {
                                    cust.CustomerName,
                                    cust.Address1,
                                    cust.Address2,
                                    cust.TotalBalance,
                                    cust.Location
                                }).ToList();
                foreach (var cus in custList)
                {
                    CustomerId = CustID;
                    txtSearch.Text = cus.CustomerName;
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

        #region Event methods
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
            GetSalesDetailByStockIDCustomerId();
        }
        private void TxtCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomerSelectList frmcustomerList = new FrmCustomerSelectList();
            frmcustomerList.ShowDialog();
            SetCustomerDetails(MdlMain.gCustomerId);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmSalesSummary_Load(object sender, EventArgs e)
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
                worksheet.Name = "Sales By Item Details";
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

        private void BtnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GrdSalesDetails.Rows.Count > 0)
            {
                ExportToExcel();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue = txtSearch.Text.Trim();
                decimal SalesAmt = 0;
                decimal QtyAmt = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var invList = (from stkTr in cmpDBContext.StkTrans
                               join salesInvMst in cmpDBContext.SalesInvMasters on stkTr.InvNo equals salesInvMst.SalesInvNo
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                               where (stkTr.TranDate >= fromDt && stkTr.TranDate <= toDt) && stkTr.TranType == "Sales"
                               where cust.CustomerName.Contains(textvalue) || stk.StockName.Contains(textvalue)
                               select new
                               {
                                   stkTr.InvNo,
                                   stkTr.TranDate,
                                   cust.CustomerName,
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
                    GrdSalesDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdSalesDetails.AutoGenerateColumns = false;
                    GrdSalesDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + invList.Count();
                    row.Cells[4].Value = QtyAmt;
                    row.Cells[5].Value = SalesAmt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmSalesSummary_KeyDown(object sender, KeyEventArgs e)
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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GrdSalesDetails.DataSource = null;
            GrdSummary.DataSource = null;
            GrdSummary.Rows.Clear();

        }

        private void FrmSalesSummary_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
