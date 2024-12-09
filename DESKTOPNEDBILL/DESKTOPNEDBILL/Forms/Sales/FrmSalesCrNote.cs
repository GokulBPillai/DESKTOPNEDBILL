using DESKTOPNEDBILL.Forms.Banks;
using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using DESKTOPNEDBILL.Reports.Sales;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;
using TableDims.Models.Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesCrNote : Form
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
        DataTable StockTranTbl = new DataTable();
        DataTable BatchTbl = new DataTable();
        DataTable TaxSummTbl = new DataTable();
        DataTable InvSummTbl = new DataTable();
        DataTable StockTbl = new DataTable();
        DataTable CompTbl = new DataTable();
        DataTable CompTblRpt = new DataTable();
        DataTable StockTranTblRpt = new DataTable();
        DataTable TaxSummTblRpt = new DataTable();
        DS_SalesInvoice ds_SalesInvoice = new DS_SalesInvoice();
        bool EditValue = false;
        bool ADD_NEW_BOOL = true;
        int StockID = 0;
        int EditStockId = 0;
        int BatchID = 0;
        string STOCKName = "";
        string HSNCode = "";
        bool IsStockDeduct;
        decimal DiscountPer = 0;
        decimal DiscountAmt = 0;
        string EditSTkTranId = "";
        string CustomerType = "Local";
        int CustomerID = 0;
        decimal PrevSubTotal = 0;
        decimal PrevTotalDiscount = 0;
        decimal PrevHandling = 0;
        decimal PrevRoundOff = 0;
        decimal PrevTotalCess = 0;
        decimal PrevTotalGST = 0;
        decimal PrevGrandTotal = 0;
        decimal PrevPaidAmount = 0;
        int SalesInvoiceNo = 0;
        int SalesReceiptNo = 0;
        int SalesCrNoteNo = 0;
        int BankGnlId = 0;
        int BankID = 0;
        decimal Mrp = 0;
        decimal stkcost = 0;
        public static string sendcustomertext = "";
        public static string sendproducttext = "";
        string priceGroup = "";
        string unitMeasure = "";
        int unitMeasureInt = 0;
        public FrmSalesCrNote()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));           
            btnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 5, 5));          
            BtnPrint.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnPrint.Width, BtnPrint.Height, 5, 5));
            btnSelectCrNote.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSelectCrNote.Width, btnSelectCrNote.Height, 5, 5));
            btnSelectInvoice.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSelectInvoice.Width, btnSelectInvoice.Height, 5, 5));

        }
        private void FrmSalesInvoice_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStockTranTbl();
            CreateStockTbl();
            CreateCompanyDetailsTbl();
            CreateTaxSummaryTbl();
            CreateBatchStocktable();
            CreateInvoiceSummaryTbl();
            InitializingData();
        }

        #region Method for enter key press enabaling in Datagrid view
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((GrdProductDetails.Focused || GrdProductDetails.EditingControl != null)
                && keyData == Keys.Enter)
            {
                SelectNextCell();
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }
        private void SelectNextCell()
        {
            var col = GrdProductDetails.CurrentCell.ColumnIndex;
            var row = GrdProductDetails.CurrentCell.RowIndex;

            if (col < GrdProductDetails.ColumnCount - 1)
            {
                col++;
            }
            else
            {
                col = 0;
                row++;
            }
            // Optional, select the first cell if the current cell is the last one.
            // You can return; here instead or add new row.
            if (row == GrdProductDetails.RowCount) row = 0;

            for (int i = col; i < GrdProductDetails.ColumnCount - 1; i++)
            {
                if (GrdProductDetails[i, row].Visible == false)
                {
                    continue;
                }
                else
                {
                    GrdProductDetails.CurrentCell = GrdProductDetails[i, row];
                    break;
                }
            }
        }
        #endregion        

        #region Create DataTables
        private void CreateStockTranTbl()
        {
            StockTranTbl.Columns.Add("TranId", typeof(int));
            StockTranTbl.Columns.Add("InvNo", typeof(int));
            StockTranTbl.Columns.Add("TranDate", typeof(DateTime));
            StockTranTbl.Columns.Add("TranRef", typeof(string));
            StockTranTbl.Columns.Add("TranType", typeof(string));
            StockTranTbl.Columns.Add("StockId", typeof(int));
            StockTranTbl.Columns.Add("StockName", typeof(string));
            StockTranTbl.Columns.Add("Batch", typeof(string));
            StockTranTbl.Columns.Add("BatchId", typeof(int));
            StockTranTbl.Columns.Add("DptId", typeof(int));
            StockTranTbl.Columns.Add("MRP", typeof(decimal));
            StockTranTbl.Columns.Add("StkCost", typeof(decimal));
            StockTranTbl.Columns.Add("StkRetail", typeof(decimal));
            StockTranTbl.Columns.Add("QtyIn", typeof(decimal));
            StockTranTbl.Columns.Add("QtyOut", typeof(decimal));
            StockTranTbl.Columns.Add("StockOnHand", typeof(decimal));
            StockTranTbl.Columns.Add("UnitMeasure", typeof(int));
            StockTranTbl.Columns.Add("Expiry", typeof(DateTime));
            StockTranTbl.Columns.Add("GST", typeof(decimal));
            StockTranTbl.Columns.Add("CGSTPercentage", typeof(decimal));
            StockTranTbl.Columns.Add("SGSTPercentage", typeof(decimal));
            StockTranTbl.Columns.Add("Cess", typeof(decimal));
            StockTranTbl.Columns.Add("AddlCess", typeof(decimal));
            StockTranTbl.Columns.Add("Discount", typeof(decimal));
            StockTranTbl.Columns.Add("DiscountPer", typeof(decimal));
            StockTranTbl.Columns.Add("TotalDiscount", typeof(decimal));
            StockTranTbl.Columns.Add("StkLocation", typeof(string));
            StockTranTbl.Columns.Add("DiscountType", typeof(string));
            StockTranTbl.Columns.Add("HSNCode", typeof(string));
            StockTranTbl.Columns.Add("GSTAmount", typeof(decimal));
            StockTranTbl.Columns.Add("CessAmount", typeof(decimal));
            StockTranTbl.Columns.Add("CGSTAmount", typeof(decimal));
            StockTranTbl.Columns.Add("SGSTAmount", typeof(decimal));
            StockTranTbl.Columns.Add("IGSTAmount", typeof(decimal));
            //StockTranTbl.Columns.Add("SubTotal", typeof(decimal));
            StockTranTbl.Columns.Add("GrossTotal", typeof(decimal));
            StockTranTbl.Columns.Add("NetTotal", typeof(decimal));//NetTotal and SubTotal are same (Value without tax and discounts)
            StockTranTbl.Columns.Add("TotalAmount", typeof(decimal));
            StockTranTbl.Columns.Add("BarcodeNo", typeof(string));
            StockTranTbl.Columns.Add("UnitMeas", typeof(string));
            StockTranTbl.Columns.Add("FinYear", typeof(string));
            StockTranTbl.Columns.Add("CompanyCode", typeof(string));
            StockTranTbl.Columns.Add("IsDeduct", typeof(bool));

            StockTranTbl.Columns.Add("SGST", typeof(string));
            StockTranTbl.Columns.Add("CGST", typeof(string));
        }
        private void CreateBatchStocktable()
        {
            BatchTbl.Columns.Add("StockId", typeof(int));
            BatchTbl.Columns.Add("Batch", typeof(string));
            BatchTbl.Columns.Add("BatchId", typeof(int));
            BatchTbl.Columns.Add("Quantity", typeof(decimal));
            BatchTbl.Columns.Add("ItemCount", typeof(decimal));
            BatchTbl.Columns.Add("StockChange", typeof(decimal));
            BatchTbl.Columns.Add("IsDeduct", typeof(bool));
        }
        private void CreateStockTbl()
        {
            StockTbl.Columns.Add("StockId", typeof(int));
            StockTbl.Columns.Add("StockName", typeof(string));
            StockTbl.Columns.Add("UnitMeasure", typeof(string));
            StockTbl.Columns.Add("UnitMeasureInt", typeof(int));
            StockTbl.Columns.Add("BatchName", typeof(string));
            StockTbl.Columns.Add("BatchId", typeof(int));
            StockTbl.Columns.Add("Expiry", typeof(DateTime));
            StockTbl.Columns.Add("GST", typeof(decimal));
            StockTbl.Columns.Add("PurchasePrice", typeof(decimal));
            StockTbl.Columns.Add("Mrp", typeof(decimal));
            StockTbl.Columns.Add("Cess", typeof(decimal));
            StockTbl.Columns.Add("AddlCess", typeof(decimal));
            StockTbl.Columns.Add("StockOnHand", typeof(decimal));
            StockTbl.Columns.Add("SellingPrice", typeof(decimal));
            StockTbl.Columns.Add("Total", typeof(decimal));
            StockTbl.Columns.Add("Qty", typeof(decimal));
            StockTbl.Columns.Add("IsDeduct", typeof(bool));
        }
        private void CreateCompanyDetailsTbl()
        {
            CompTbl.Columns.Add("CompanyName", typeof(string));
            CompTbl.Columns.Add("CompAdd1", typeof(string));
            CompTbl.Columns.Add("CompAdd2", typeof(string));
            CompTbl.Columns.Add("CompStateCode", typeof(string));
            CompTbl.Columns.Add("CompPhone", typeof(string));
            CompTbl.Columns.Add("CompGstNo", typeof(string));
            CompTbl.Columns.Add("CustomerName", typeof(string));
            CompTbl.Columns.Add("CustAdd1", typeof(string));
            CompTbl.Columns.Add("CustAdd2", typeof(string));
            CompTbl.Columns.Add("CustState", typeof(string));
            CompTbl.Columns.Add("CustPhone", typeof(string));
            CompTbl.Columns.Add("CompEmail", typeof(string));
            CompTbl.Columns.Add("CustGst", typeof(string));
            CompTbl.Columns.Add("InvoiceNo", typeof(int));
            CompTbl.Columns.Add("InvDate", typeof(DateTime));
            CompTbl.Columns.Add("TotalDiscount", typeof(decimal));
            CompTbl.Columns.Add("CGSTTotal", typeof(decimal));
            CompTbl.Columns.Add("SGSTTotal", typeof(decimal));
            CompTbl.Columns.Add("IGSTTotal", typeof(decimal));
            CompTbl.Columns.Add("SubTotal", typeof(decimal));
            CompTbl.Columns.Add("GrossTotal", typeof(decimal));
            CompTbl.Columns.Add("NetTotal", typeof(decimal));
            CompTbl.Columns.Add("CessTotal", typeof(decimal));
            CompTbl.Columns.Add("RoundOff", typeof(decimal));
            CompTbl.Columns.Add("HandlingPer", typeof(decimal));
            CompTbl.Columns.Add("HandlingAmount", typeof(decimal));
            CompTbl.Columns.Add("TransportationCharge", typeof(decimal));
            CompTbl.Columns.Add("GrandTotal", typeof(decimal));

            CompTbl.Columns.Add("InvComents", typeof(string));
            CompTbl.Columns.Add("Rswords", typeof(string));
        }
        private void CreateTaxSummaryTbl()
        {
            TaxSummTbl.Columns.Add("SalesAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("TaxAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("CGSTAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("SGSTAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("CessAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("AddlCessAmount", typeof(decimal));
            TaxSummTbl.Columns.Add("Tax", typeof(decimal));

        }
        private void CreateInvoiceSummaryTbl()
        {
            InvSummTbl.Columns.Add("NetTotal", typeof(decimal));
            InvSummTbl.Columns.Add("GSTTotal", typeof(decimal));
            InvSummTbl.Columns.Add("CGSTTotal", typeof(decimal));
            InvSummTbl.Columns.Add("SGSTTotal", typeof(decimal));
            InvSummTbl.Columns.Add("IGSTTotal", typeof(decimal));
            InvSummTbl.Columns.Add("DiscountTotal", typeof(decimal));
            InvSummTbl.Columns.Add("CessTotal", typeof(decimal));
            InvSummTbl.Columns.Add("GrandTotal", typeof(decimal));
            InvSummTbl.Columns.Add("RoundTotal", typeof(decimal));
            InvSummTbl.Columns.Add("TotCountDescription", typeof(string));
        }
        #endregion

        #region Get & POST methods
        private void GetProductDetails()
        {
            try
            {
                if (CustomerID <= 0 && TxtCustomerName.Text == "")
                {
                    MessageBox.Show("Before selecting stock, please select a Customer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCustomerName.Focus();
                    return;
                }
                List<Stocks> stk = cmpDBContext.Stock.ToList();
                if (stk.Count() > 0)
                {
                    FrmProductSelectListSales frmproductList = new FrmProductSelectListSales();
                    frmproductList.ShowDialog();
                    if (MdlMain.gStockId > 0 && MdlMain.gBatchId > 0)
                    {
                        //SetStockDeatils(MdlMain.gStockId, MdlMain.gBatchId);
                        SetStockDeatilsToGrid(MdlMain.gStockId, MdlMain.gBatchId);
                        EditValue = true;
                        //TxtQuantity.Focus();
                        sendproducttext = "";
                        MdlMain.gStockId = 0;
                        MdlMain.gBatchId = 0;
                    }
                }
                else
                {
                    MessageBox.Show("There is no Stock to select.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SetStockDeatilsToGrid(int stockId, int batchId)
        {
            try
            {
                decimal rate;
                var stkList = (from stk in cmpDBContext.Stock
                               join bth in cmpDBContext.Batch on stk.StockId equals bth.StockId
                               join uni in cmpDBContext.UnitMeasure on stk.UnitMeasure equals uni.Id
                               where stk.Status == true && bth.StockId == stockId && bth.BatchId == batchId
                               && bth.StockOnHand > 0
                               select new
                               {
                                   stk.StockId,
                                   stk.StockName,
                                   bth.BatchId,
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
                                   bth.SellingPriceB,
                                   bth.SellingPriceC,
                                   stk.HSNCode,
                                   uni.UnitMeasureDescription,
                                   unitmeasureId = uni.Id,
                                   stk.InventoryType.IsStkDeduct
                               });
                foreach (var stklst in stkList)
                {
                    decimal NewQty = 0;
                    DataRow[] FndRow = StockTranTbl.Select();
                    for (int i = 0; i <= FndRow.GetUpperBound(0); i++)
                    {
                        if (FndRow[i]["StockId"].Equals(stockId) && FndRow[i]["BatchId"].Equals(batchId))
                        {
                            NewQty = (decimal)FndRow[i]["QtyOut"];
                            StockTranTbl.Rows[i].Delete();
                        }
                    }
                    NewQty += 1;
                    if (priceGroup == "Selling Price A")
                    {
                        rate = stklst.SellingPriceA;
                    }
                    else if (priceGroup == "Selling Price B")
                    {
                        rate = stklst.SellingPriceB;
                    }
                    else { rate = stklst.SellingPriceC; }
                    Dictionary<string, decimal> SalesGSTlist = MdlMain.SalesGSTCalculation(rate, NewQty, stklst.GST, stklst.Cess, DiscountPer, DiscountAmt);
                    DataRow stkTrRow = StockTranTbl.NewRow();
                    stkTrRow["TranId"] = EditSTkTranId == "" ? Convert.ToInt32(0) : Convert.ToInt32(EditSTkTranId);
                    stkTrRow["TranDate"] = DtpInvDate.Value;
                    stkTrRow["TranRef"] = SalesInvoiceNo.ToString();//TxtBillNo.Text.Trim();
                    stkTrRow["TranType"] = "Sales";
                    stkTrRow["StockId"] = stockId;
                    stkTrRow["StockName"] = stklst.StockName;
                    stkTrRow["Batch"] = stklst.BatchName;
                    stkTrRow["BatchId"] = batchId;
                    //stkTrRow["DptId"] = CmbDtpId.SelectedValue;// Need to change
                    stkTrRow["MRP"] = stklst.Mrp;
                    stkTrRow["StkCost"] = stklst.PurchasePrice;
                    stkTrRow["StkRetail"] = rate;
                    stkTrRow["QtyIn"] = 0;
                    stkTrRow["QtyOut"] = NewQty;
                    stkTrRow["StockOnHand"] = stklst.StockOnHand;
                    stkTrRow["Expiry"] = stklst.Expiry;
                    stkTrRow["GST"] = stklst.GST;
                    stkTrRow["CGSTPercentage"] = stklst.GST / 2;
                    stkTrRow["SGSTPercentage"] = stklst.GST / 2;
                    stkTrRow["Discount"] = 0;
                    stkTrRow["DiscountPer"] = 0;
                    stkTrRow["TotalDiscount"] = 0;
                    stkTrRow["StkLocation"] = 2001;// Need to change
                    stkTrRow["DiscountType"] = "% per piece";
                    stkTrRow["HSNCode"] = stklst.HSNCode;
                    stkTrRow["GSTAmount"] = Convert.ToDecimal(SalesGSTlist["TotalGSTAmount"]).ToString();
                    stkTrRow["CessAmount"] = Convert.ToDecimal(SalesGSTlist["TotalCessAmount"]).ToString();
                    stkTrRow["CGSTAmount"] = Convert.ToDecimal(SalesGSTlist["TotalCGSTAmount"]).ToString();
                    stkTrRow["SGSTAmount"] = Convert.ToDecimal(SalesGSTlist["TotalSGSTAmount"]).ToString();
                    stkTrRow["IGSTAmount"] = Convert.ToDecimal(SalesGSTlist["TotalIGSTAmount"]).ToString();
                    stkTrRow["NetTotal"] = Convert.ToDecimal(SalesGSTlist["NetTotalAmount"]).ToString();
                    //stkTrRow["SubTotal"] = Convert.ToDecimal(SalesGSTlist["SubTotalAmount"]).ToString();
                    stkTrRow["GrossTotal"] = Convert.ToDecimal(SalesGSTlist["TotalAmount"]).ToString();
                    stkTrRow["TotalAmount"] = Convert.ToDecimal(SalesGSTlist["TotalAmount"]).ToString();
                    stkTrRow["Cess"] = stklst.Cess;
                    stkTrRow["AddlCess"] = stklst.AddlCess;
                    stkTrRow["BarcodeNo"] = "";
                    stkTrRow["UnitMeas"] = stklst.UnitMeasureDescription;
                    stkTrRow["UnitMeasure"] = stklst.unitmeasureId;
                    stkTrRow["FinYear"] = MdlMain.gFinYear;
                    stkTrRow["CompanyCode"] = MdlMain.gCompCode;
                    stkTrRow["IsDeduct"] = stklst.IsStkDeduct;
                    StockTranTbl.Rows.Add(stkTrRow);
                    //CalculateTotalSummary();
                    CalculateTotalSummaryToGrid();
                    EditValue = false;
                    EditSTkTranId = "";
                    //TxtTotalItem.Text = StockTranTbl.Rows.Count.ToString();
                    decimal Qtycount = 0;
                    foreach (DataRow item in StockTranTbl.Rows)
                    {
                        Qtycount += Convert.ToDecimal(item["QtyOut"]);
                    }
                    //TxtTotalQty.Text = Qtycount.ToString();
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StockTranTbl;
                    GrdProductDetails.AutoGenerateColumns = false;
                    GrdProductDetails.DataSource = bindingSource;
                    EditValue = false;
                    //MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel4);
                    MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel7);
                    //TxtProductName.Focus();
                    //TxtProductName.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CalculateTotalSummaryToGrid()
        {
            try
            {
                decimal NetTotal = 0;
                decimal CGSTTotal = 0;
                decimal SGSTTotal = 0;
                decimal IGSTTotal = 0;
                decimal GSTTotal = 0;
                decimal DiscountTotal = 0;
                decimal GrandTotal = 0;
                decimal CessTotal = 0;
                decimal RoundTotal = 0;
                decimal Qty = 0;
                foreach (DataRow item in StockTranTbl.Rows)
                {
                    if (ADD_NEW_BOOL)
                    {
                        Qty = Convert.ToDecimal(item["QtyOut"]);// data from stktran and TranType = Invoice where qty is in QtyOut
                    }
                    else Qty = Convert.ToDecimal(item["QtyIn"]);// data from stktran and TranType = Returns where qty is in QtyIn

                    Dictionary<string, decimal> SalesGSTlist = MdlMain.SalesGSTCalculation(Convert.ToDecimal(item["StkRetail"]), Qty, Convert.ToDecimal(item["GST"]), Convert.ToDecimal(item["Cess"]), Convert.ToDecimal(item["DiscountPer"]), Convert.ToDecimal(item["Discount"]));
                    NetTotal += (SalesGSTlist["NetTotalAmount"]);
                    GSTTotal += (SalesGSTlist["TotalGSTAmount"]);
                    CGSTTotal += (SalesGSTlist["TotalCGSTAmount"]);
                    SGSTTotal += (SalesGSTlist["TotalSGSTAmount"]);
                    if (CustomerType == "Local")
                    { IGSTTotal = 0; }
                    else { IGSTTotal += (SalesGSTlist["TotalIGSTAmount"]); }
                    DiscountTotal += (SalesGSTlist["TotalDiscountAmount"]);
                    CessTotal += (SalesGSTlist["TotalCessAmount"]);
                    GrandTotal += (SalesGSTlist["TotalAmount"]);
                    RoundTotal = Math.Round(GrandTotal);
                }
                TxtRoundOff.Text = (RoundTotal - GrandTotal).ToString("0.00");
                TxtGrossTotal.Text = RoundTotal.ToString("0.00");

                InvSummTbl.Clear();
                DataRow summRow = InvSummTbl.NewRow();
                summRow["NetTotal"] = NetTotal;
                summRow["GSTTotal"] = GSTTotal;
                summRow["CGSTTotal"] = CGSTTotal;
                summRow["SGSTTotal"] = SGSTTotal;
                summRow["IGSTTotal"] = IGSTTotal;
                summRow["DiscountTotal"] = DiscountTotal;
                summRow["CessTotal"] = CessTotal;
                summRow["GrandTotal"] = GrandTotal;
                summRow["RoundTotal"] = RoundTotal;
                summRow["TotCountDescription"] = "Total Items: " + StockTranTbl.Rows.Count;
                InvSummTbl.Rows.Add(summRow);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = InvSummTbl;
                GrdProductSummary.AutoGenerateColumns = false;
                GrdProductSummary.DataSource = bindingSource;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void SetCustomerDetails(int CustID)
        {
            try
            {
                var custList = (from cust in cmpDBContext.Customers
                                where cust.Status == true && cust.CustomerId == CustID
                                select new
                                {
                                    cust.CustomerName,
                                    cust.Address1,
                                    cust.Address2,
                                    cust.TotalBalance,
                                    cust.Location,
                                    cust.PriceGroup,
                                    cust.PaymentType,
                                    cust.StateCode
                                }).ToList();
                foreach (var cus in custList)
                {
                    CustomerID = CustID;
                    TxtCustBal.Text = cus.TotalBalance.ToString();
                    CustomerType = cus.Location;
                    CmbTaxType.Text = CustomerType;
                    priceGroup = cus.PriceGroup;
                    CmbPayType.Text = cus.PaymentType;
                    CmbStateCode.SelectedValue = cus.StateCode;
                    //TxtProductName.Focus();
                    TxtCustomerName.Text = "";
                    TxtCustomerName.AppendText(cus.CustomerName.Trim() + Environment.NewLine + cus.Address1 + Environment.NewLine + cus.Address2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetInitialCustomer()
        {
            try
            {
                CustomerID = 1;
                var custList = (from cust in cmpDBContext.Customers
                                where cust.Status == true && cust.CustomerId == 1
                                select new
                                {
                                    cust.CustomerName,
                                    cust.Address1,
                                    cust.Address2,
                                    cust.TotalBalance,
                                    cust.Location,
                                    cust.PriceGroup,
                                    cust.PaymentType,
                                    cust.StateCode
                                }).ToList();
                foreach (var cus in custList)
                {
                    TxtCustBal.Text = cus.TotalBalance.ToString();
                    CustomerType = cus.Location;
                    CmbTaxType.Text = CustomerType;
                    priceGroup = cus.PriceGroup;
                    CmbPayType.Text = cus.PaymentType;
                    CmbStateCode.SelectedValue = cus.StateCode;
                    //TxtProductName.Focus();
                    TxtCustomerName.Text = "";
                    TxtCustomerName.AppendText(cus.CustomerName.Trim() + Environment.NewLine + cus.Address1 + Environment.NewLine + cus.Address2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetSalesInvoiceList()
        {
            try
            {
                InitializingData();
                FrmSalesInvSelectList frmsalesInvoiceList = new FrmSalesInvSelectList();
                frmsalesInvoiceList.ShowDialog();
                if (MdlMain.gSalesInvNo > 0)
                {
                    ADD_NEW_BOOL = true;
                    SalesInvoiceNo = MdlMain.gSalesInvNo;
                    MdlMain.gSalesInvNo = 0;
                    SetSalesInvoiceSummaryDetails(SalesInvoiceNo);
                    SetSalesInvoiceGridDetails(SalesInvoiceNo);
                    SetBankDetails(BankID);
                    foreach (DataRow item in InvSummTbl.Rows)
                    {
                        PrevSubTotal = Convert.ToDecimal(item["NetTotal"]);
                        PrevTotalDiscount = Convert.ToDecimal(item["DiscountTotal"]);
                        PrevTotalCess = Convert.ToDecimal(item["CessTotal"]);
                        PrevTotalGST = Convert.ToDecimal(item["CGSTTotal"]) * 2;
                        PrevRoundOff = Convert.ToDecimal(TxtRoundOff.Text);
                        PrevGrandTotal = Convert.ToDecimal(TxtGrossTotal.Text);
                        PrevPaidAmount = Convert.ToDecimal(TxtPaidAmt.Text);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetSalesInvoiceSummaryDetails(int salesInvNo)
        {
            try
            {
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               where salesInvMst.SalesInvNo == salesInvNo
                               select new
                               {
                                   salesInvMst.InvDate,
                                   salesInvMst.CustID,
                                   salesInvMst.InvAmount,
                                   salesInvMst.PayBankIdNo,
                                   salesInvMst.InvPaymode,
                                   salesInvMst.Handling,
                                   salesInvMst.HandlingPer,
                                   salesInvMst.TotDiscount,
                                   salesInvMst.RoundAmount,
                                   salesInvMst.InvRef,
                                   salesInvMst.TotalCessAmount,
                                   salesInvMst.TotalCGSTAmount,
                                   salesInvMst.TotalSGSTAmount,
                                   salesInvMst.TotalIGSTAmount,
                                   salesInvMst.AddlCess,
                                   salesInvMst.AddlDiscount,
                                   salesInvMst.RcpNo,
                                   salesInvMst.NetTotal,
                                   cust.CustomerName,
                                   cust.CustomerId,
                                   cust.Address1,
                                   cust.Address2,
                                   cust.TotalBalance,
                                   cust.StateCode,
                                   cust.Location
                               }).ToList();
                foreach (var inv in invList)
                {
                    SalesInvoiceNo = salesInvNo;
                    SalesReceiptNo = inv.RcpNo;
                    CustomerID = inv.CustomerId;
                    TxtCustBal.Text = inv.TotalBalance.ToString();
                    MdlMain.gCustomerId = inv.CustomerId;
                    DtpInvDate.Value = inv.InvDate;
                    CmbPayType.Text = inv.InvPaymode;
                    TxtRoundOff.Text = inv.RoundAmount.ToString();
                    //TxtBillNo.Text = salesInvNo.ToString();
                    BankID = inv.PayBankIdNo;
                    CmbStateCode.SelectedValue = inv.StateCode;
                    CmbTaxType.Text = inv.Location;
                    TxtCustomerName.Text = "";
                    TxtCustomerName.AppendText(inv.CustomerName.Trim() + Environment.NewLine + inv.Address1 + Environment.NewLine + inv.Address2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetSalesInvoiceGridDetails(int salesInvNo)
        {
            try
            {
                var stkTranList = (from stkTran in cmpDBContext.StkTrans
                                   join stk in cmpDBContext.Stock on stkTran.StocksId equals stk.StockId
                                   join bth in cmpDBContext.Batch on stkTran.BatchId equals bth.BatchId
                                   join uni in cmpDBContext.UnitMeasure on stkTran.UnitMeasure equals uni.Id
                                   where stkTran.InvNo == salesInvNo && stkTran.TranRef == "Invoice" && stkTran.TranType == "Sales"
                                   select new
                                   {
                                       stkTran.TranId,
                                       stkTran.InvNo,
                                       stkTran.TranDate,
                                       stkTran.StocksId,
                                       stk.StockName,
                                       stkTran.DptId,
                                       bth.Mrp,
                                       bth.PurchasePrice,
                                       bth.StockOnHand,
                                       stkTran.StkRetail,
                                       stkTran.QtyIn,
                                       stkTran.QtyOut,
                                       stkTran.Expiry,
                                       Batch = bth.BatchName,
                                       stkTran.BatchId,
                                       stkTran.GST,
                                       CGSTPercentage = (stkTran.GST / 2),
                                       SGSTPercentage = (stkTran.GST / 2),
                                       stkTran.Discount,
                                       stkTran.DiscPer,
                                       stkTran.TotalDiscount,
                                       //TotalDiscount = (stkTran.Discount * stkTran.QtyOut),
                                       stkTran.StkLocation,
                                       stkTran.DiscountType,
                                       stkTran.HSNCode,
                                       stkTran.GSTAmount,
                                       stkTran.CessAmount,
                                       stkTran.CGSTAmount,
                                       stkTran.SGSTAmount,
                                       stkTran.IGSTAmount,
                                       stkTran.TotalAmount,
                                       stkTran.AddlCess,
                                       stkTran.NetTotal,
                                       stkTran.GrossTotal,
                                       stkTran.FinYear,
                                       stkTran.CompanyCode,
                                       stkTran.BarcodeNo,
                                       UnitMeas = uni.UnitMeasureDescription,
                                       UnitMeasint = uni.Id,
                                       stk.InventoryType.IsStkDeduct,
                                   }).ToList();
                if (stkTranList.Count() != 0)
                {
                    foreach (var item in stkTranList)
                    {
                        DataRow stkTrRow = StockTranTbl.NewRow();
                        stkTrRow["TranId"] = item.TranId;
                        stkTrRow["InvNo"] = item.InvNo;
                        stkTrRow["TranDate"] = item.TranDate;
                        stkTrRow["TranRef"] = SalesInvoiceNo.ToString();//TxtBillNo.Text.Trim();
                        stkTrRow["TranType"] = "Sales";
                        stkTrRow["StockId"] = item.StocksId;
                        stkTrRow["StockName"] = item.StockName;
                        stkTrRow["DptId"] = 0;// Need to change
                        stkTrRow["MRP"] = item.Mrp;
                        stkTrRow["StkCost"] = item.PurchasePrice;
                        stkTrRow["StkRetail"] = item.StkRetail;
                        stkTrRow["QtyIn"] = item.QtyIn;
                        stkTrRow["QtyOut"] = item.QtyOut;
                        stkTrRow["StockOnHand"] = item.StockOnHand;
                        stkTrRow["UnitMeas"] = item.UnitMeas;
                        stkTrRow["UnitMeasure"] = item.UnitMeasint;
                        stkTrRow["Expiry"] = item.Expiry;
                        stkTrRow["BatchId"] = item.BatchId;
                        stkTrRow["Batch"] = item.Batch;
                        stkTrRow["GST"] = item.GST;
                        stkTrRow["CGSTPercentage"] = item.CGSTPercentage;
                        stkTrRow["SGSTPercentage"] = item.SGSTPercentage;
                        stkTrRow["DiscountType"] = item.DiscountType;
                        stkTrRow["Discount"] = item.Discount;
                        stkTrRow["DiscountPer"] = item.DiscPer;
                        stkTrRow["TotalDiscount"] = item.TotalDiscount;
                        stkTrRow["StkLocation"] = item.StkLocation;
                        stkTrRow["HSNCode"] = item.HSNCode;
                        stkTrRow["GSTAmount"] = item.GSTAmount;
                        stkTrRow["CessAmount"] = item.CessAmount;
                        stkTrRow["CGSTAmount"] = item.CGSTAmount;
                        stkTrRow["SGSTAmount"] = item.SGSTAmount;
                        stkTrRow["IGSTAmount"] = item.IGSTAmount;
                        stkTrRow["TotalAmount"] = item.TotalAmount;
                        //stkTrRow["SubTotal"] = item.SubTotal;
                        stkTrRow["GrossTotal"] = item.GrossTotal;
                        stkTrRow["NetTotal"] = item.NetTotal;
                        stkTrRow["Cess"] = item.CessAmount;
                        stkTrRow["AddlCess"] = item.AddlCess;
                        stkTrRow["BarcodeNo"] = item.BarcodeNo;
                        stkTrRow["FinYear"] = item.FinYear;
                        stkTrRow["CompanyCode"] = item.CompanyCode;
                        stkTrRow["IsDeduct"] = item.IsStkDeduct;
                        StockTranTbl.Rows.Add(stkTrRow);
                    }
                }
                GrdProductDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StockTranTbl;
                GrdProductDetails.AutoGenerateColumns = false;
                GrdProductDetails.DataSource = bindingSource;
                CalculateTotalSummaryToGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetAllStockDetailsForUpdatinginBatch()
        {
            try
            {
                DataRow[] FndTrRow = StockTranTbl.Select();
                DataRow[] BatchRow = BatchTbl.Select();
                int count1 = 0;
                if (StockTranTbl.Rows.Count > 0)
                {
                    for (int i = 0; i <= FndTrRow.GetUpperBound(0); i++)
                    {
                        count1 = 0;
                        if (BatchTbl.Rows.Count > 0)
                        {
                            for (int j = 0; j <= BatchRow.GetUpperBound(0); j++)
                            {
                                if (BatchRow[j]["StockId"].Equals(FndTrRow[i]["StockId"]) && BatchRow[j]["BatchId"].Equals(FndTrRow[i]["BatchId"]))
                                {
                                    //Do Nothing
                                    count1++;
                                    continue;
                                }
                            }
                            // Add to BatchTbl
                            if (count1 == 0)
                            {
                                DataRow btRow = BatchTbl.NewRow();
                                btRow["StockId"] = FndTrRow[i]["StockId"];
                                btRow["BatchId"] = FndTrRow[i]["BatchId"];
                                btRow["Batch"] = FndTrRow[i]["Batch"];
                                btRow["Quantity"] = FndTrRow[i]["QtyOut"];
                                btRow["IsDeduct"] = FndTrRow[i]["IsDeduct"];
                                BatchTbl.Rows.Add(btRow);
                            }
                        }
                        else
                        {
                            DataRow btRow = BatchTbl.NewRow();
                            btRow["StockId"] = FndTrRow[i]["StockId"];
                            btRow["BatchId"] = FndTrRow[i]["BatchId"];
                            btRow["Batch"] = FndTrRow[i]["Batch"];
                            btRow["Quantity"] = FndTrRow[i]["QtyOut"];
                            btRow["IsDeduct"] = FndTrRow[i]["IsDeduct"];
                            BatchTbl.Rows.Add(btRow);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetBankDetails(int BankId)
        {
            try
            {
                if (BankId == 0)
                {
                    TxtBankName.Text = "Credit";
                    BankGnlId = 0;
                }
                else
                {
                    var bnkdtls = (from bnk in cmpDBContext.Bank
                                   where bnk.BankId == BankId && bnk.Status == true
                                   select new
                                   {
                                       bnk.BankId,
                                       bnk.BankName,
                                       bnk.GnlId,
                                   });
                    TxtBankName.Text = bnkdtls.First().BankName;
                    BankGnlId = bnkdtls.First().GnlId;
                    BankID = bnkdtls.First().BankId;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetBankDetails()
        {
            try
            {
                FrmBankList frmBanklist = new FrmBankList();
                frmBanklist.ShowDialog();
                if (MdlMain.gBankId > 0)
                {
                    SetBankDetails(MdlMain.gBankId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveSalesRetMaster()
        {
            try
            {
                if (ADD_NEW_BOOL == false)
                {
                    if (SalesCrNoteNo > 0)
                    {
                        List<SalesRetMaster> salesRetMaster = cmpDBContext.SalesRetMasters.Where(m => m.SalesRetNo == SalesCrNoteNo).ToList();
                        foreach (SalesRetMaster salesret in salesRetMaster)
                        {
                            foreach (DataRow item in InvSummTbl.Rows)
                            {
                                salesret.SalesInvNo = SalesInvoiceNo;//Convert.ToInt32(TxtBillNo.Text);
                                salesret.InvDate = DtpInvDate.Value.Date;
                                salesret.RetDate = DtpCrNoteDt.Value.Date;
                                salesret.CustID = CustomerID;
                                salesret.InvAmount = TxtGrossTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGrossTotal.Text);
                                salesret.InvPaymode = CmbPayType.Text;
                                salesret.TotDiscount = Convert.ToDecimal(item["DiscountTotal"]);
                                salesret.RoundAmount = TxtRoundOff.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtRoundOff.Text);
                                salesret.CustStatus = "Active"; salesret.NetTotal = Convert.ToDecimal(item["NetTotal"]);
                                salesret.TotalCessAmount = Convert.ToDecimal(item["CessTotal"]);
                                salesret.TotalCGSTAmount = Convert.ToDecimal(item["CGSTTotal"]);
                                salesret.TotalSGSTAmount = Convert.ToDecimal(item["SGSTTotal"]);
                                salesret.TotalIGSTAmount = Convert.ToDecimal(item["IGSTTotal"]);
                                salesret.EmpId = Convert.ToInt32(MdlMain.gLogEmpId);
                                salesret.PayBankIdNo = BankID;
                                salesret.RcpNo = SalesReceiptNo;
                                salesret.BillType = MdlMain.gInvCategory;
                                salesret.FinYear = MdlMain.gFinYear;
                                salesret.CompanyCode = MdlMain.gCompCode;
                            }
                        }
                        cmpDBContext.SaveChanges();
                    }
                }
                else
                {
                    foreach (DataRow item in InvSummTbl.Rows)
                    {
                        var salesRetMasters = new SalesRetMaster()
                        {
                            InvDate = DtpInvDate.Value.Date,
                            RetDate = DtpCrNoteDt.Value.Date,
                            SalesInvNo = SalesInvoiceNo,//Convert.ToInt32(TxtBillNo.Text),
                            CustID = CustomerID,
                            CustStatus = "Active",
                            InvRef = txtCrNoteNo.Text.Trim(),
                            EmpId = Convert.ToInt32(MdlMain.gLogEmpId),
                            BillType = MdlMain.gInvCategory,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            InvPaymode = CmbPayType.Text,
                            PayBankIdNo = BankID,
                            RcpNo = SalesReceiptNo,
                            TotDiscount = Convert.ToDecimal(item["DiscountTotal"]),
                            RoundAmount = TxtRoundOff.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtRoundOff.Text),
                            SubTotal = Convert.ToDecimal(item["NetTotal"]),
                            NetTotal = Convert.ToDecimal(item["NetTotal"]),
                            TotalCessAmount = Convert.ToDecimal(item["CessTotal"]),
                            TotalCGSTAmount = Convert.ToDecimal(item["CGSTTotal"]),
                            TotalSGSTAmount = Convert.ToDecimal(item["SGSTTotal"]),
                            TotalIGSTAmount = Convert.ToDecimal(item["IGSTTotal"]),
                            InvAmount = TxtGrossTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGrossTotal.Text),

                        };
                        cmpDBContext.SalesRetMasters.Add(salesRetMasters);
                        cmpDBContext.SaveChanges();
                        SalesCrNoteNo = salesRetMasters.SalesRetNo;
                    }

                }
                if (SalesCrNoteNo > 0)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //private int SaveSalesReceipt()
        //{
        //    try
        //    {
        //        var salesReceipt = new SalesReceipt()
        //        {
        //            TranDate = DtpInvDate.Value.Date,
        //            RcpAmount = Convert.ToDecimal(TxtPaidAmt.Text),
        //            CustomerId = CustomerID,
        //            GnlId = BankGnlId,
        //            FinYear = MdlMain.gFinYear,
        //            CompanyCode = MdlMain.gCompCode
        //        };
        //        cmpDBContext.SalesReceipt.Add(salesReceipt);
        //        cmpDBContext.SaveChanges();
        //        SalesReceiptNo = salesReceipt.RcpNo;
        //        return salesReceipt.RcpNo;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        private bool SaveStkRetTran()
        {
            try
            {
                SetAllStockDetailsForUpdatinginBatch();
                foreach (DataRow item in StockTranTbl.Rows)
                {
                    decimal QuantityIn = 0;
                    decimal QuantityOut = 0;
                    if (ADD_NEW_BOOL)
                    {
                        QuantityIn = Convert.ToDecimal(item["QtyOut"]);
                        QuantityOut = Convert.ToDecimal(item["QtyIn"]);
                    }
                    else
                    {
                        QuantityIn = Convert.ToDecimal(item["QtyIn"]);
                        QuantityOut = Convert.ToDecimal(item["QtyOut"]);
                    }
                    var stkTran = new StkTran()
                    {
                        InvNo = SalesCrNoteNo,
                        TranDate = DtpCrNoteDt.Value.Date,
                        TranRef = "Returns",
                        TranType = "Sales",
                        StocksId = Convert.ToInt32(item["StockId"]),
                        DptId = 2001,
                        //MRP = Convert.ToDecimal(item["MRP"]),
                        //StkCost = Convert.ToDecimal(item["StkCost"]),
                        StkRetail = Convert.ToDecimal(item["StkRetail"]),
                        QtyIn = QuantityIn,
                        QtyOut = QuantityOut,
                        UnitMeasure = Convert.ToInt32(item["UnitMeasure"]),
                        Expiry = Convert.ToDateTime(item["Expiry"]),
                        BatchId = Convert.ToInt32(item["BatchId"]),
                        GST = Convert.ToDecimal(item["GST"]),
                        Discount = Convert.ToDecimal(item["Discount"]),
                        DiscPer = Convert.ToDecimal(item["DiscountPer"]),
                        TotalDiscount = Convert.ToDecimal(item["TotalDiscount"]),
                        StkLocation = item["StkLocation"].ToString(),
                        DiscountType = item["DiscountType"].ToString(),
                        HSNCode = item["HSNCode"].ToString(),
                        GSTAmount = Convert.ToDecimal(item["GSTAmount"]),
                        CessAmount = Convert.ToDecimal(item["CessAmount"]),
                        CGSTAmount = Convert.ToDecimal(item["CGSTAmount"]),
                        SGSTAmount = Convert.ToDecimal(item["SGSTAmount"]),
                        IGSTAmount = Convert.ToDecimal(item["IGSTAmount"]),
                        AddlCess = Convert.ToDecimal(item["AddlCess"]),
                        GrossTotal = Convert.ToDecimal(item["GrossTotal"]),
                        NetTotal = Convert.ToDecimal(item["NetTotal"]),
                        TotalAmount = Convert.ToDecimal(item["TotalAmount"]),
                        FinYear = item["FinYear"].ToString(),
                        CompanyCode = item["CompanyCode"].ToString(),
                    };
                    cmpDBContext.StkTrans.Add(stkTran);
                    cmpDBContext.SaveChanges();
                    item["TranId"] = stkTran.TranId;
                    item["InvNo"] = SalesCrNoteNo;
                }

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //private void SetRetGlBreak()
        //{
        //    try
        //    {
        //        foreach (DataRow item in InvSummTbl.Rows)
        //        {
        //            if (Convert.ToDecimal(TxtGrossTotal.Text) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    IDNo = CustomerID,
        //                    DptId = 2001,//-----------------Need to Change
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 12000, //---------------------------------------Accounts Payable Head
        //                    Amt1 = Convert.ToDecimal(TxtGrossTotal.Text),
        //                    Amt2 = 0,
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 12000).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtGrossTotal.Text) - PrevGrandTotal);
        //                    }
        //                    else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(TxtGrossTotal.Text); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //            if (Convert.ToDecimal(item["NetTotal"]) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    IDNo = CustomerID,
        //                    DptId = 2001,
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 40010, //---------------------------------------Sales Return
        //                    Amt1 = 0,
        //                    Amt2 = Convert.ToDecimal(item["NetTotal"]),
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 40010).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["NetTotal"]) - PrevSubTotal);
        //                    }
        //                    else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(item["NetTotal"]); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //            if (Convert.ToDecimal(item["DiscountTotal"]) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    IDNo = CustomerID,
        //                    DptId = 2001,
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 50012,//---------------------------------------Discounts Received Head
        //                    Amt1 = Convert.ToDecimal(item["DiscountTotal"]),
        //                    Amt2 = 0,
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50012).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(item["DiscountTotal"]) - PrevTotalDiscount);
        //                    }
        //                    else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(item["DiscountTotal"]); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //            if (TxtRoundOff.Text != "" ? Convert.ToDecimal(TxtRoundOff.Text) > 0 : Convert.ToDecimal(0) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    IDNo = CustomerID,
        //                    DptId = 2001,
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 50051,//---------------------------------------Printing and Stationery Head
        //                    Amt1 = 0,
        //                    Amt2 = Convert.ToDecimal(TxtRoundOff.Text),
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50051).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(TxtRoundOff.Text) - PrevRoundOff);
        //                    }
        //                    else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(TxtRoundOff.Text); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //            if (Convert.ToDecimal(item["CessTotal"]) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    IDNo = CustomerID,
        //                    DptId = 2001,
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 32010,//---------------------------------------Cess Input Head
        //                    Amt1 = 0,
        //                    Amt2 = Convert.ToDecimal(item["CessTotal"]),
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32010).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["CessTotal"]) - PrevTotalCess);
        //                    }
        //                    else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(item["CessTotal"]); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //            if (Convert.ToDecimal(item["CGSTTotal"]) > 0)
        //            {
        //                var glBreak = new GlBreak()
        //                {
        //                    TranDate = DtpCrNoteDt.Value.Date,
        //                    FinYear = MdlMain.gFinYear,
        //                    CompanyCode = MdlMain.gCompCode,
        //                    TranType = "Returns",
        //                    GlModule = "Sales",
        //                    TranRef = SalesCrNoteNo.ToString(),
        //                    IDNo = CustomerID,
        //                    DptId = 2001,
        //                    AmtMode = TxtBankName.Text.Trim(),
        //                    Status = "Active",
        //                    DEnd = "No",
        //                    LedAc = SalesCrNoteNo.ToString(),
        //                    GnlId = 32001,//---------------------------------------GST Paid Head
        //                    Amt1 = 0,
        //                    Amt2 = Convert.ToDecimal(item["CGSTTotal"]) * 2,//----Adding CGST & SGST
        //                    InvoiceNo = SalesInvoiceNo
        //                };
        //                cmpDBContext.GlBreaks.Add(glBreak);
        //                cmpDBContext.SaveChanges();
        //                List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32001).ToList();
        //                foreach (GlAccount gl in glAcc)
        //                {
        //                    if (ADD_NEW_BOOL == false)
        //                    {
        //                        gl.Amt2 = gl.Amt2 + ((Convert.ToDecimal(item["CGSTTotal"]) * 2) - PrevTotalGST);
        //                    }
        //                    else { gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["CGSTTotal"]) * 2); }
        //                }
        //                //cmpDBContext.GlAccounts.UpdateRange(glAcc);
        //                cmpDBContext.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        private void SetRetGlBreak()
        {
            try
            {
                foreach (DataRow item in InvSummTbl.Rows)
                {
                    if (Convert.ToDecimal(TxtGrossTotal.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranRef = SalesCrNoteNo.ToString(),
                            TranDate = DtpInvDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            IDNo = CustomerID,
                            DptId = 2001,//-----------------Need to Change
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 12000, //---------------------------------------Accounts Payable Head
                            Amt1 = Convert.ToDecimal(TxtGrossTotal.Text),
                            Amt2 = 0,
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 12000).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtGrossTotal.Text) - PrevGrandTotal);
                            }
                            else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(TxtGrossTotal.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["NetTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpCrNoteDt.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            TranRef = SalesCrNoteNo.ToString(),
                            IDNo = CustomerID,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 40010, //---------------------------------------Sales Return
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(item["NetTotal"]),
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 40010).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["NetTotal"]) - PrevSubTotal);
                            }
                            else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(item["NetTotal"]); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["DiscountTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranRef = SalesCrNoteNo.ToString(),
                            TranDate = DtpInvDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            IDNo = CustomerID,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 50012,//---------------------------------------Discounts Received Head
                            Amt1 = Convert.ToDecimal(item["DiscountTotal"]),
                            Amt2 = 0,
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50012).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(item["DiscountTotal"]) - PrevTotalDiscount);
                            }
                            else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(item["DiscountTotal"]); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (TxtRoundOff.Text != "" ? Convert.ToDecimal(TxtRoundOff.Text) > 0 : Convert.ToDecimal(0) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpCrNoteDt.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            TranRef = SalesCrNoteNo.ToString(),
                            IDNo = CustomerID,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 50051,//---------------------------------------Printing and Stationery Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(TxtRoundOff.Text),
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50051).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(TxtRoundOff.Text) - PrevRoundOff);
                            }
                            else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(TxtRoundOff.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["CessTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpCrNoteDt.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            TranRef = SalesCrNoteNo.ToString(),
                            IDNo = CustomerID,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 32010,//---------------------------------------Cess Input Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(item["CessTotal"]),
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32010).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["CessTotal"]) - PrevTotalCess);
                            }
                            else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(item["CessTotal"]); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["CGSTTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpCrNoteDt.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            TranRef = SalesCrNoteNo.ToString(),
                            IDNo = CustomerID,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            GnlId = 32001,//---------------------------------------GST Paid Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(item["CGSTTotal"]) * 2,//----Adding CGST & SGST
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32001).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + ((Convert.ToDecimal(item["CGSTTotal"]) * 2) - PrevTotalGST);
                            }
                            else { gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["CGSTTotal"]) * 2); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(TxtPaidAmt.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpCrNoteDt.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                            TranType = "Returns",
                            GlModule = "Sales",
                            TranRef = SalesReceiptNo.ToString(),//SalesInvoiceNo.ToString(),
                            IDNo = CustomerID,
                            DptId = 2001,
                            Status = "Active",
                            DEnd = "No",
                            LedAc = SalesCrNoteNo.ToString(),
                            AmtMode = TxtBankName.Text.Trim(),
                            GnlId = 12000,//---------------------------------------Accounts Payable Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(TxtPaidAmt.Text),
                            InvoiceNo = SalesInvoiceNo
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 12000).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 += (Convert.ToDecimal(TxtPaidAmt.Text) - PrevPaidAmount);
                            }
                            else { gl.Amt2 += Convert.ToDecimal(TxtPaidAmt.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                        var glc = cmpDBContext.GlAccounts.Where(m => m.GnlId == BankGnlId).Select(m => m);
                        if (BankGnlId == 15000)//-------------------------------Cash In Hand Head
                        {
                            var glBreak1 = new GlBreak();
                            glBreak1.TranDate = DtpCrNoteDt.Value.Date;
                            glBreak1.FinYear = MdlMain.gFinYear;
                            glBreak1.CompanyCode = MdlMain.gCompCode;
                            glBreak1.TranType = "Returns";
                            glBreak1.GlModule = "Sales";
                            glBreak1.TranRef = SalesReceiptNo.ToString();//SalesInvoiceNo.ToString();
                            glBreak1.IDNo = CustomerID;
                            glBreak1.DptId = 2001;
                            glBreak1.Status = "Active";
                            glBreak1.DEnd = "No";
                            glBreak1.LedAc = SalesCrNoteNo.ToString();
                            glBreak1.AmtMode = TxtBankName.Text.Trim();
                            glBreak1.GnlId = 15000;//---------------------------------------Accounts Payable Head
                            glBreak1.InvoiceNo = SalesInvoiceNo;
                            if (glc.First().GnlCode == "DR")
                            {
                                glBreak1.Amt1 = 0;
                                glBreak1.Amt2 = Convert.ToDecimal(TxtPaidAmt.Text);
                                List<GlAccount> glAccnt = cmpDBContext.GlAccounts.Where(m => m.GnlId == 15000).ToList();
                                foreach (GlAccount gl in glAccnt)
                                {
                                    if (ADD_NEW_BOOL == false)
                                    {
                                        gl.Amt2 += (Convert.ToDecimal(TxtPaidAmt.Text) - PrevPaidAmount);
                                    }
                                    else { gl.Amt2 += Convert.ToDecimal(TxtPaidAmt.Text); }
                                    //cmpDBContext.GlAccounts.Update(gl);
                                    cmpDBContext.SaveChanges();
                                }
                            }
                            else
                            {
                                glBreak1.Amt1 = Convert.ToDecimal(TxtPaidAmt.Text);
                                glBreak1.Amt2 = 0;
                                List<GlAccount> glAccnt = cmpDBContext.GlAccounts.Where(m => m.GnlId == 15000).ToList();
                                foreach (GlAccount gl in glAccnt)
                                {
                                    if (ADD_NEW_BOOL == false)
                                    {
                                        gl.Amt1 += (Convert.ToDecimal(TxtPaidAmt.Text) - PrevPaidAmount);
                                    }
                                    else { gl.Amt1 += Convert.ToDecimal(TxtPaidAmt.Text); }
                                    //cmpDBContext.GlAccounts.Update(gl);
                                    cmpDBContext.SaveChanges();
                                }
                            }
                            cmpDBContext.GlBreaks.Add(glBreak1);
                            cmpDBContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void IsGlbreakExist(int CrNoteNo)
        {
            try
            {
                List<GlBreak> vbreak = cmpDBContext.GlBreaks.Where(m => m.InvoiceNo == CrNoteNo && m.TranType == "Returns" && m.GlModule == "Sales").ToList();
                if (vbreak.Count > 0)
                {
                    //foreach (var item in vbreak)
                    //{
                    //    if (item.TranType == "Receipt")
                    //    {
                    //        int tranref = Convert.ToInt32(item.TranRef);
                    //        List<SalesReceipt> vRcpt = cmpDBContext.SalesReceipt.Where(m => m.RcpNo == tranref).ToList();
                    //        if (vRcpt.Count > 0)
                    //        {
                    //            cmpDBContext.SalesReceipt.RemoveRange(vRcpt);
                    //            cmpDBContext.SaveChanges();
                    //        }
                    //    }
                    //}
                    cmpDBContext.GlBreaks.RemoveRange(vbreak);
                    cmpDBContext.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void UpdateStockBatch()
        {
            try
            {
                DataRow[] FndTrRow = BatchTbl.Select();
                foreach (DataRow item in BatchTbl.Rows)
                {
                    int stkId = (int)item["StockId"];
                    int batchId = (int)item["BatchId"];
                    var stkTranList = (from stkTr in cmpDBContext.StkTrans
                                       join stk in cmpDBContext.Stock on stkTr.StocksId equals stk.StockId
                                       join bth in cmpDBContext.Batch on stkTr.BatchId equals bth.BatchId
                                       where stkTr.StocksId == stkId && stkTr.BatchId == batchId && stk.InventoryType.IsStkDeduct == true
                                       select new
                                       {
                                           stk.StockId,
                                           bth.BatchId,
                                           stkTr.QtyOut,
                                           stkTr.QtyIn
                                       } into x
                                       group x by new { x.StockId, x.BatchId } into grp
                                       select new
                                       {
                                           grp.Key.StockId,
                                           grp.Key.BatchId,
                                           QtyOut = grp.Sum(x => x.QtyOut),
                                           QtyIn = grp.Sum(x => x.QtyIn)
                                       }).ToList();
                    decimal stkOut = 0;
                    decimal stkIn = 0;
                    foreach (var stk in stkTranList)
                    {
                        stkOut += stk.QtyOut;
                        stkIn += stk.QtyIn;
                        var batch = cmpDBContext.Batch.Where(m => m.StockId == stkId && m.BatchId == batchId).ToList();
                        if (batch.Count > 0)
                        {
                            batch.First().StockOut = stkOut;
                            batch.First().StockIn = stkIn;
                            batch.First().StockOnHand = (batch.First().OpeningStock + batch.First().StockIn) - batch.First().StockOut;
                            cmpDBContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddCustomerDue()
        {
            try
            {
                decimal Amt1 = 0;
                decimal Amt2 = 0;
                var cust = cmpDBContext.Customers.Where(m => m.CustomerId == CustomerID).Select(m => m);
                List<GlBreak> glBrk = cmpDBContext.GlBreaks.Where(m => m.IDNo == CustomerID && m.Status == "Active" && (m.GnlId == 35000 || m.GnlId == 12000) && (m.GlModule == "Sales" || m.GlModule == "Journal")).ToList();
                foreach (GlBreak glb in glBrk)
                {
                    Amt1 += glb.Amt1;
                    Amt2 += glb.Amt2;
                }
                cust.First().TotalBalance = cust.First().OpeningBalance + (Amt1 - Amt2);
                //cmpDBContext.Customers.UpdateRange(cust);
                cmpDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InitializingData()
        {
            ADD_NEW_BOOL = true;
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel2);
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel4);
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel6);
            TxtCustomerName.Text = "";
            StockTranTbl.Clear();
            CompTbl.Clear();
            StockTbl.Clear();
            BatchTbl.Clear();
            TaxSummTbl.Clear();
            InvSummTbl.Clear();
            GrdProductDetails.DataSource = null;
            EditStockId = 0;
            SalesInvoiceNo = 0;
            SalesReceiptNo = 0;
            MdlMain.gSalesInvNo = 0;
            MdlMain.gCustomerId = 0;
            CmbPayType.SelectedIndex = 0;
            TxtBankName.Text = "Cash";
            BankGnlId = 15000;
            BankID = 1;
            CustomerID = 0;
            StockID = 0;
            STOCKName = "";
            HSNCode = "";
            DiscountPer = 0;
            DiscountAmt = 0;
            EditSTkTranId = "";
            PrevSubTotal = 0;
            PrevTotalDiscount = 0;
            PrevHandling = 0;
            PrevRoundOff = 0;
            PrevTotalCess = 0;
            PrevTotalGST = 0;
            PrevGrandTotal = 0;
            PrevPaidAmount = 0;
            GetStateList();
            CmbStateCode.SelectedValue = MdlMain.gCompanyStateCodeId;
            SetInitialCustomer();
        }
        private void InitializingDataForEditing()
        {
            ADD_NEW_BOOL = false;
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel2);
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel6);
            StockTranTbl.Clear();
            CompTbl.Clear();
            StockTbl.Clear();
            BatchTbl.Clear();
            TaxSummTbl.Clear();
            InvSummTbl.Clear();
            GrdProductDetails.DataSource = null;
            EditStockId = 0;
            SalesInvoiceNo = 0;
            SalesReceiptNo = 0;
            MdlMain.gCustomerId = 0;
            CmbPayType.SelectedIndex = 0;
            BankGnlId = 15000;
            BankID = 1;
            CustomerID = 0;
            StockID = 0;
            STOCKName = "";
            HSNCode = "";
            DiscountPer = 0;
            DiscountAmt = 0;
            EditSTkTranId = "";
            PrevSubTotal = 0;
            PrevTotalDiscount = 0;
            PrevHandling = 0;
            PrevRoundOff = 0;
            PrevTotalCess = 0;
            PrevTotalGST = 0;
            PrevGrandTotal = 0;
            PrevPaidAmount = 0;
            GetStateList();
            CmbStateCode.SelectedValue = MdlMain.gCompanyStateCodeId;
        }
        private bool FieldValidation()
        {
            try
            {
                //if (TxtProductName.Text != "" && TxtTotal.Text != "")
                //{
                //    MessageBox.Show("The selected product " + "'" + TxtProductName.Text.Trim() + "'" + " is still not added to table for saving!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    TxtQuantity.Focus();
                //    return false;
                //}

                if (CustomerID <= 0 && TxtCustomerName.Text == "")
                {
                    MessageBox.Show("Please select Customer", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtCustomerName.Focus();
                    return false;
                }
                if (StockTranTbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Product is not added in table for saving Sales Return", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //TxtProductName.Focus();
                    return false;
                }
                if (TxtBankName.Text == "")
                {
                    MessageBox.Show("Settlement mode field is blank. Please select an appropriate payment mode to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CmbPayType.Focus();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetCustomer()
        {
            try
            {
                List<Customer> cust = cmpDBContext.Customers.ToList();
                if (cust.Count <= 0)
                {
                    DialogResult result = MessageBox.Show("There is no Customer to select. Do you want to add a new Customer to create Invoice?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        FrmCustomer frmCustomer = new FrmCustomer();
                        FrmAddEditCustomer frmAddEditCustomer = new FrmAddEditCustomer(frmCustomer);
                        frmAddEditCustomer.ShowDialog();
                        SetCustomerDetails(MdlMain.gCustomerId);
                        MdlMain.gCustomerId = 0;
                    }
                }
                else
                {
                    FrmCustomerSelectList frmcustomerList = new FrmCustomerSelectList();
                    frmcustomerList.ShowDialog();
                    int TempCustomerId = MdlMain.gCustomerId;
                    InitializingData();
                    CustomerID = TempCustomerId;
                    SetCustomerDetails(CustomerID);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void GetStateList()
        {
            try
            {
                List<StateCode> stat = cmpDBContext.StateCodes.ToList();
                if (stat.Count > 0)
                {
                    CmbStateCode.DataSource = stat;
                }
                //string str = StatecodeNo + "/" + "State";
                CmbStateCode.DisplayMember = "State";
                CmbStateCode.ValueMember = "StateCodeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetCompanyDetails()
        {
            try
            {
                CompTbl.Clear();
                if (CustomerID > 0 && StockTranTbl.Rows.Count > 0)
                {
                    foreach (DataRow item in InvSummTbl.Rows)
                    {
                        var cust = cmpDBContext.Customers.Where(m => m.CustomerId == CustomerID).Select(m => m);
                        var comp = cmpDBContext.CompanyProfiles.Where(m => m.CompanyCode == MdlMain.gCompCode).Select(m => m);
                        DataRow compRow = CompTbl.NewRow();
                        compRow["CompanyName"] = comp.First().CompanyName;
                        compRow["CompAdd1"] = comp.First().Address1;
                        compRow["CompAdd2"] = comp.First().Address2;
                        compRow["CompStateCode"] = MdlMain.gCompanyState + "-" + MdlMain.gCompanyStateCode;
                        compRow["CompGstNo"] = comp.First().GSTNo;
                        compRow["CompPhone"] = comp.First().PhoneNo;
                        compRow["CustGst"] = cust.First().GSTNo;
                        compRow["CustomerName"] = cust.First().CustomerName;
                        compRow["CustAdd1"] = cust.First().Address1;
                        compRow["CustAdd2"] = cust.First().Address2;
                        compRow["CustState"] = MdlMain.gCompanyState;
                        compRow["CustPhone"] = cust.First().ContactNo;
                        compRow["CompEmail"] = comp.First().EmailId;
                        compRow["InvoiceNo"] = SalesCrNoteNo;
                        compRow["InvDate"] = DtpCrNoteDt.Value;
                        compRow["GrossTotal"] = Convert.ToDecimal(item["NetTotal"]);// TxtNetTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtNetTotal.Text);
                        compRow["TotalDiscount"] = Convert.ToDecimal(item["DiscountTotal"]);// TxtTotalDiscount.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalDiscount.Text);
                        compRow["CGSTTotal"] = Convert.ToDecimal(item["CGSTTotal"]);// TxtTotalCGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCGSTAmt.Text);
                        compRow["SGSTTotal"] = Convert.ToDecimal(item["SGSTTotal"]);// TxtTotalSGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalSGSTAmt.Text);
                        compRow["IGSTTotal"] = Convert.ToDecimal(item["IGSTTotal"]);// TxtTotalIGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalIGSTAmt.Text);
                        compRow["NetTotal"] = Convert.ToDecimal(item["NetTotal"]);//TxtNetTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtNetTotal.Text);
                        compRow["CessTotal"] = Convert.ToDecimal(item["CessTotal"]);// TxtTotalCessAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCessAmt.Text);
                        compRow["RoundOff"] = TxtRoundOff.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtRoundOff.Text);
                        compRow["GrandTotal"] = TxtGrossTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGrossTotal.Text);
                        compRow["Rswords"] = MdlMain.NumberToWords(Convert.ToDecimal(compRow["GrandTotal"].ToString()));
                        //compRow["SubTotal"] = TxtSubTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSubTotal.Text);
                        //compRow["HandlingPer"] = TxtHandlingPer.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandlingPer.Text);
                        //compRow["HandlingAmount"] = TxtHandling.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandling.Text);
                        //compRow["TransportationCharge"] = TxtTransCharge.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTransCharge.Text);

                        CompTbl.Rows.Add(compRow);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetTaxSummary(int tranId)
        {
            try
            {
                if (tranId > 0)
                {
                    TaxSummTbl.Clear();
                    CMPDBContext myDBContext = new CMPDBContext();
                    var Summary = (from stkTran in myDBContext.StkTrans
                                   where stkTran.InvNo == tranId && stkTran.TranType == "Sales"
                                   select new
                                   {
                                       stkTran.CessAmount,
                                       stkTran.GST,
                                       stkTran.QtyOut,
                                       stkTran.StkRetail,
                                       stkTran.Discount,
                                       stkTran.GSTAmount,
                                       stkTran.AddlCess,
                                   } into x
                                   group x by new { x.GST } into grp
                                   select new
                                   {
                                       grp.Key.GST,
                                       SalesAmount = grp.Sum(x => (x.QtyOut * x.StkRetail) - x.Discount),
                                       TaxAmount = grp.Sum(x => x.GSTAmount),
                                       CessAmount = grp.Sum(x => x.CessAmount),
                                       AddlCessAmount = grp.Sum(x => x.AddlCess)
                                   }).ToList();
                    foreach (var x in Summary)
                    {
                        DataRow summRow = TaxSummTbl.NewRow();
                        summRow["Tax"] = x.GST;
                        summRow["SalesAmount"] = Math.Round(x.SalesAmount, 2);
                        summRow["TaxAmount"] = x.TaxAmount;
                        summRow["CGSTAmount"] = x.TaxAmount <= 0 ? Convert.ToDecimal(0) : x.TaxAmount / 2;
                        summRow["SGSTAmount"] = x.TaxAmount <= 0 ? Convert.ToDecimal(0) : x.TaxAmount / 2;
                        summRow["CessAmount"] = x.CessAmount;
                        summRow["AddlCessAmount"] = x.AddlCessAmount;
                        TaxSummTbl.Rows.Add(summRow);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void PrintInvoice()
        {
            SetCompanyDetails();
            GetTaxSummary(SalesCrNoteNo);
            CompTblRpt.Clear();
            StockTranTblRpt.Clear();
            CompTblRpt = CompTbl.Copy();
            TaxSummTblRpt.Clear();
            StockTranTblRpt = StockTranTbl.Copy();
            TaxSummTblRpt = TaxSummTbl.Copy();
            ds_SalesInvoice.EnforceConstraints = true;
            ds_SalesInvoice.Tables["StkTranDetails"].Clear();
            ds_SalesInvoice.Tables["CompanyDetails"].Clear();
            ds_SalesInvoice.Tables["TaxSummary"].Clear();
            ds_SalesInvoice.Tables["StkTranDetails"].Merge(StockTranTblRpt);
            ds_SalesInvoice.Tables["CompanyDetails"].Merge(CompTblRpt);
            ds_SalesInvoice.Tables["TaxSummary"].Merge(TaxSummTblRpt);
            MdlMain.gDs_SalesInv1 = ds_SalesInvoice.Copy();
            if (MdlMain.gPrintMode == "A4")
            {
                if (MdlMain.gCompanyGSTTypeId == 2)
                {
                    if (MdlMain.gInvCategory == "GST")
                    {
                        FrmPrintSalesReturn frmPrintSalesReturn = new FrmPrintSalesReturn();
                        frmPrintSalesReturn.ShowDialog();
                    }
                    if (MdlMain.gInvCategory == "IGST")
                    {
                        FrmPrintIGSTInvoice frmPrintIGSTInvoice = new FrmPrintIGSTInvoice();
                        frmPrintIGSTInvoice.ShowDialog();
                    }
                }
                else if (MdlMain.gCompanyGSTTypeId == 3)
                {
                    FrmPrintGSTComposite frmPrintGSTComposite = new FrmPrintGSTComposite();
                    frmPrintGSTComposite.ShowDialog();
                }
                else if (MdlMain.gCompanyGSTTypeId == 1)
                {
                    FrmNonGSTInvoice frmNonGSTInvoice = new FrmNonGSTInvoice();
                    frmNonGSTInvoice.ShowDialog();
                }

            }
            else if (MdlMain.gPrintMode == "Thermal")
            {
                if (MdlMain.gCompanyGSTTypeId == 2)
                {
                    if (MdlMain.gInvCategory == "GST")
                    {
                        FrmPrintGSTThermal frmPrintGSTThermal = new FrmPrintGSTThermal();
                        frmPrintGSTThermal.ShowDialog();
                    }

                }
            }


        }
        #endregion

        #region Event handling methods
        private void GrdProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrdProductDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                int i = GrdProductDetails.CurrentRow.Index;
                DataRow[] FndTrRow = StockTranTbl.Select();
                StockTranTbl.Rows.Remove(FndTrRow[i]);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StockTranTbl;
                GrdProductDetails.AutoGenerateColumns = false;
                GrdProductDetails.DataSource = bindingSource;
                CalculateTotalSummaryToGrid();
                decimal Qtycount = 0;
                foreach (DataRow item in StockTranTbl.Rows)
                {
                    Qtycount += Convert.ToDecimal(item["QtyOut"]);
                }
            }
        }
        private void CmbPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbPayType.Text == "Cash")
            {
                TxtBankName.Text = "Cash";
                TxtPaidAmt.Text = TxtGrossTotal.Text;
                TxtPaidAmt.Enabled = true;
                BankGnlId = 15000;
                BankID = 1;
            }
            else if (CmbPayType.Text == "Credit")
            {
                TxtBankName.Text = "Credit";
                TxtPaidAmt.Text = "0";
                TxtPaidAmt.Enabled = false;
                BankGnlId = 0;
                BankID = 0;
            }
            else if (CmbPayType.Text == "UPI" || CmbPayType.Text == "Debit Card" || CmbPayType.Text == "Credit Card")
            {
                TxtPaidAmt.Text = TxtGrossTotal.Text;
                TxtPaidAmt.Enabled = true;
                BankGnlId = 0;
                BankID = 0;
                //TxtBankName_Click(sender, e);
                //Step1: TxtBankName_Click(sender, e);
                if (BankGnlId == 0 || BankGnlId == 15000)
                {
                    TxtBankName.Text = "";
                    TxtPaidAmt.Text = "0";
                    BankGnlId = 0;
                    BankID = 0;
                    MessageBox.Show("Please select a bank connected with select payment method to continue", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtBankName_Click(sender, e);
                    //goto Step1;

                }

            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    if (ADD_NEW_BOOL == false && SalesCrNoteNo > 0)
                    {
                        List<StkTran> stkTr = cmpDBContext.StkTrans.Where(m => m.InvNo == SalesCrNoteNo && m.TranType == "Sales" && m.TranRef == "Returns").ToList();
                        if (stkTr.Count > 0)
                        {
                            cmpDBContext.StkTrans.RemoveRange(stkTr);
                            cmpDBContext.SaveChanges();
                        }
                        IsGlbreakExist(SalesCrNoteNo);
                    }
                    SaveSalesRetMaster();
                    SaveStkRetTran();
                    SetRetGlBreak();
                    UpdateStockBatch();
                    //AddCustomerDue();
                    InitializingData();
                    if (ADD_NEW_BOOL)
                    {
                        MessageBox.Show("Sales Return saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Sales Return updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (FieldValidation())
                {
                    if (ADD_NEW_BOOL == false && SalesCrNoteNo > 0)
                    {
                        List<StkTran> stkTr = cmpDBContext.StkTrans.Where(m => m.InvNo == SalesCrNoteNo && m.TranType == "Sales" && m.TranRef == "Returns").ToList();
                        if (stkTr.Count > 0)
                        {
                            cmpDBContext.StkTrans.RemoveRange(stkTr);
                            cmpDBContext.SaveChanges();
                        }
                        IsGlbreakExist(SalesCrNoteNo);
                    }
                    SaveSalesRetMaster();
                    SaveStkRetTran();
                    SetRetGlBreak();
                    UpdateStockBatch();
                    //AddCustomerDue();
                    PrintInvoice();
                    InitializingData();
                    //MessageBox.Show("Sales Return saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtBankName_Click(object sender, EventArgs e)
        {
            try
            {
                GetBankDetails();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void CmbPayType_TextChanged(object sender, EventArgs e)
        {
            if (CmbPayType.Text == "Cash")
            {
                TxtBankName.Text = "Cash";
                TxtPaidAmt.Text = TxtGrossTotal.Text;
                TxtPaidAmt.Enabled = true;
                BankGnlId = 15000;
                BankID = 1;
            }
            else
            {
                TxtBankName.Text = "Credit";
                TxtPaidAmt.Text = "0";
                TxtPaidAmt.Enabled = false;
                BankGnlId = 0;
                BankID = 0;
            }
        }
        private void TxtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            if (CmbPayType.Text == "Credit")
            {
                TxtPaidAmt.Text = "0";
            }
            else { TxtPaidAmt.Text = TxtGrossTotal.Text; }
            if (TxtGrossTotal.Text != "")
            {
                label3.Text = MdlMain.NumberToWords(Convert.ToDecimal(TxtGrossTotal.Text));
            }

        }
        private void CmbStateCode_SelectedValueChanged(object sender, EventArgs e)
        {
            int str = CmbStateCode.SelectedIndex;
            if (CmbStateCode.SelectedValue?.ToString() != MdlMain.gCompanyStateCodeId.ToString())
            {
                GrdProductDetails.Columns["grdGSTper"].Visible = true;
                GrdProductDetails.Columns["grdGSTper"].HeaderText = "IGST%";
                GrdProductDetails.Columns["grdGSTAmt"].HeaderText = "IGST";
                MdlMain.gInvCategory = "IGST";
            }
            else
            {
                GrdProductDetails.Columns["grdGSTper"].Visible = true;
                GrdProductDetails.Columns["grdGSTper"].HeaderText = "GST%";
                GrdProductDetails.Columns["grdGSTAmt"].HeaderText = "GST";
                MdlMain.gInvCategory = "GST";
            }
        }
        private void DtpInvDate_ValueChanged(object sender, EventArgs e)
        {
            if (DtpInvDate.Value > MdlMain.gFYEnd)
            {
                DtpInvDate.Value = MdlMain.gFYEnd;
            }
            if (DtpInvDate.Value < MdlMain.gFYStart)
            {
                DtpInvDate.Value = MdlMain.gFYStart;
            }
        }
        private void FrmSalesInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Alt)
            {
                BtnPrint_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                btnClear_Click(sender, e);
            }
        }
        private void GrdProductDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in GrdProductDetails.Rows)
            {
                if (row.Cells[GrdProductDetails.Columns["discPer"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["discPer"].Index].Value = 0;
                }
                if (row.Cells[GrdProductDetails.Columns["discAmt"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["discAmt"].Index].Value = 0;
                }
                if (row.Cells[GrdProductDetails.Columns["retail"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["retail"].Index].Value = 0;
                }
                if (row.Cells[GrdProductDetails.Columns["qty"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["qty"].Index].Value = 0;
                }

                if (Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["qty"].Index].Value) > Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["stockonhand"].Index].Value))
                {
                    row.Cells[GrdProductDetails.Columns["qty"].Index].Value = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["stockonhand"].Index].Value);
                }
                decimal DiscPer = row.Cells[GrdProductDetails.Columns["discPer"].Index].Value.ToString() == "" ? 0 : Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["discPer"].Index].Value);
                decimal DiscAmt = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["discAmt"].Index].Value);
                DiscountPer = DiscPer;
                DiscountAmt = DiscAmt;
                decimal RetailPrice = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["retail"].Index].Value);
                decimal QuantityOut = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["qty"].Index].Value);
                decimal GST = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["grdGSTper"].Index].Value);
                decimal Cess = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["grdCess"].Index].Value);
                Dictionary<string, decimal> SalesGSTlist = MdlMain.SalesGSTCalculation(RetailPrice, QuantityOut, GST, Cess, DiscountPer, DiscountAmt);
                row.Cells[GrdProductDetails.Columns["totdisc"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalDiscountAmount"]);
                row.Cells[GrdProductDetails.Columns["totalamount"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalAmount"]);
                row.Cells[GrdProductDetails.Columns["grdSGST"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalSGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["grdCGST"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalCGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["grdGstAmt"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["grdCess"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalCessAmount"]);
                row.Cells[GrdProductDetails.Columns["nettotal"].Index].Value = Convert.ToDecimal(SalesGSTlist["NetTotalAmount"]);
                CalculateTotalSummaryToGrid();
            }
        }

        #endregion

        #region Only Numeric allowed in textbox
        private void TxtRetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtRetail.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtGST.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtCess.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtAddlCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtAddlCess.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtDiscount.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtQuantity.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtManulRetail_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }

        #endregion

        private void FrmSalesInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
        private void GetSalesCreditNoteList()
        {
            try
            {
                FrmSalesRetSelectList frmSalesRetSelectList = new FrmSalesRetSelectList();
                frmSalesRetSelectList.ShowDialog();
                if (MdlMain.gSalesCrNoteNo > 0)
                {
                    InitializingData();
                    ADD_NEW_BOOL = false;
                    SalesCrNoteNo = MdlMain.gSalesCrNoteNo;
                    MdlMain.gSalesCrNoteNo = 0;
                    SetSalesReturnSummaryDetails(SalesCrNoteNo);
                    SetSalesRetGridDetails(SalesCrNoteNo);
                    //SetBankDetails(BankID);                   
                    foreach (DataRow item in InvSummTbl.Rows)
                    {
                        PrevSubTotal = Convert.ToDecimal(item["NetTotal"]);
                        PrevTotalDiscount = Convert.ToDecimal(item["DiscountTotal"]);
                        PrevTotalCess = Convert.ToDecimal(item["CessTotal"]);
                        PrevTotalGST = Convert.ToDecimal(item["CGSTTotal"]) * 2;
                        PrevRoundOff = Convert.ToDecimal(TxtRoundOff.Text);
                        PrevGrandTotal = Convert.ToDecimal(TxtGrossTotal.Text);
                        PrevPaidAmount = Convert.ToDecimal(TxtPaidAmt.Text);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetSalesReturnSummaryDetails(int salesRetNo)
        {
            try
            {
                var retList = (from salesRetMst in cmpDBContext.SalesRetMasters
                               join cust in cmpDBContext.Customers on salesRetMst.CustID equals cust.CustomerId
                               where salesRetMst.SalesRetNo == salesRetNo
                               select new
                               {
                                   salesRetMst.RetDate,
                                   //salesRetMst.SalesRetNo,
                                   salesRetMst.SalesInvNo,
                                   salesRetMst.InvDate,
                                   salesRetMst.CustID,
                                   salesRetMst.InvAmount,
                                   salesRetMst.PayBankIdNo,
                                   salesRetMst.InvPaymode,
                                   salesRetMst.Handling,
                                   salesRetMst.HandlingPer,
                                   salesRetMst.TotDiscount,
                                   salesRetMst.RoundAmount,
                                   salesRetMst.InvRef,
                                   salesRetMst.TotalCessAmount,
                                   salesRetMst.TotalCGSTAmount,
                                   salesRetMst.TotalSGSTAmount,
                                   salesRetMst.TotalIGSTAmount,
                                   salesRetMst.AddlCess,
                                   salesRetMst.AddlDiscount,
                                   salesRetMst.RcpNo,
                                   salesRetMst.NetTotal,
                                   cust.CustomerName,
                                   cust.CustomerId,
                                   cust.Address1,
                                   cust.Address2,
                                   cust.TotalBalance,
                                   cust.StateCode,
                                   cust.Location
                               }).ToList();
                foreach (var inv in retList)
                {
                    SalesCrNoteNo = salesRetNo;
                    SalesInvoiceNo = inv.SalesInvNo;
                    SalesReceiptNo = inv.RcpNo;
                    CustomerID = inv.CustomerId;
                    TxtCustBal.Text = inv.TotalBalance.ToString();
                    MdlMain.gCustomerId = inv.CustomerId;
                    DtpInvDate.Value = inv.InvDate;
                    CmbPayType.Text = inv.InvPaymode;
                    TxtRoundOff.Text = inv.RoundAmount.ToString();
                    txtCrNoteNo.Text = salesRetNo.ToString();
                    //TxtBillNo.Text = SalesInvoiceNo.ToString();
                    BankID = inv.PayBankIdNo;
                    CmbStateCode.SelectedValue = inv.StateCode;
                    CmbTaxType.Text = inv.Location;
                    TxtCustomerName.Text = "";
                    TxtCustomerName.AppendText(inv.CustomerName.Trim() + Environment.NewLine + inv.Address1 + Environment.NewLine + inv.Address2);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetSalesRetGridDetails(int salesRetNo)
        {
            try
            {
                var stkTranList = (from stkTran in cmpDBContext.StkTrans
                                   join stk in cmpDBContext.Stock on stkTran.StocksId equals stk.StockId
                                   join bth in cmpDBContext.Batch on stkTran.BatchId equals bth.BatchId
                                   join uni in cmpDBContext.UnitMeasure on stkTran.UnitMeasure equals uni.Id
                                   where stkTran.InvNo == salesRetNo && stkTran.TranRef == "Returns" && stkTran.TranType == "Sales"
                                   select new
                                   {
                                       stkTran.TranId,
                                       stkTran.InvNo,
                                       stkTran.TranDate,
                                       stkTran.StocksId,
                                       stk.StockName,
                                       stkTran.DptId,
                                       bth.Mrp,
                                       bth.PurchasePrice,
                                       bth.StockOnHand,
                                       stkTran.StkRetail,
                                       stkTran.QtyIn,
                                       stkTran.QtyOut,
                                       stkTran.Expiry,
                                       Batch = bth.BatchName,
                                       stkTran.BatchId,
                                       stkTran.GST,
                                       CGSTPercentage = (stkTran.GST / 2),
                                       SGSTPercentage = (stkTran.GST / 2),
                                       stkTran.Discount,
                                       stkTran.DiscPer,
                                       stkTran.TotalDiscount,
                                       stkTran.StkLocation,
                                       stkTran.DiscountType,
                                       stkTran.HSNCode,
                                       stkTran.GSTAmount,
                                       stkTran.CessAmount,
                                       stkTran.CGSTAmount,
                                       stkTran.SGSTAmount,
                                       stkTran.IGSTAmount,
                                       stkTran.TotalAmount,
                                       stkTran.AddlCess,
                                       stkTran.NetTotal,
                                       stkTran.GrossTotal,
                                       stkTran.FinYear,
                                       stkTran.CompanyCode,
                                       stkTran.BarcodeNo,
                                       UnitMeas = uni.UnitMeasureDescription,
                                       UnitMeasint = uni.Id,
                                       stk.InventoryType.IsStkDeduct,
                                   }).ToList();
                if (stkTranList.Count() != 0)
                {
                    int ItemCnt = 0;
                    foreach (var item in stkTranList)
                    {
                        DataRow stkTrRow = StockTranTbl.NewRow();
                        stkTrRow["TranId"] = item.TranId;
                        stkTrRow["InvNo"] = item.InvNo;
                        stkTrRow["TranDate"] = item.TranDate;
                        stkTrRow["TranRef"] = SalesInvoiceNo.ToString();
                        stkTrRow["TranType"] = "Sales";
                        stkTrRow["StockId"] = item.StocksId;
                        stkTrRow["StockName"] = item.StockName;
                        stkTrRow["DptId"] = 0;// Need to change
                        stkTrRow["MRP"] = item.Mrp;
                        stkTrRow["StkCost"] = item.PurchasePrice;
                        stkTrRow["StkRetail"] = item.StkRetail;
                        stkTrRow["QtyIn"] = item.QtyIn;
                        stkTrRow["QtyOut"] = item.QtyOut;
                        stkTrRow["StockOnHand"] = item.StockOnHand;
                        stkTrRow["UnitMeas"] = item.UnitMeas;
                        stkTrRow["UnitMeasure"] = item.UnitMeasint;
                        stkTrRow["Expiry"] = item.Expiry;
                        stkTrRow["BatchId"] = item.BatchId;
                        stkTrRow["Batch"] = item.Batch;
                        stkTrRow["GST"] = item.GST;
                        stkTrRow["CGSTPercentage"] = item.CGSTPercentage;
                        stkTrRow["SGSTPercentage"] = item.SGSTPercentage;
                        stkTrRow["DiscountType"] = item.DiscountType;
                        stkTrRow["Discount"] = item.Discount;
                        stkTrRow["DiscountPer"] = item.DiscPer;
                        stkTrRow["TotalDiscount"] = item.TotalDiscount;
                        stkTrRow["StkLocation"] = item.StkLocation;
                        stkTrRow["HSNCode"] = item.HSNCode;
                        stkTrRow["GSTAmount"] = item.GSTAmount;
                        stkTrRow["CessAmount"] = item.CessAmount;
                        stkTrRow["CGSTAmount"] = item.CGSTAmount;
                        stkTrRow["SGSTAmount"] = item.SGSTAmount;
                        stkTrRow["IGSTAmount"] = item.IGSTAmount;
                        stkTrRow["TotalAmount"] = item.TotalAmount;
                        //stkTrRow["SubTotal"] = item.SubTotal;
                        stkTrRow["GrossTotal"] = item.GrossTotal;
                        stkTrRow["NetTotal"] = item.NetTotal;
                        stkTrRow["Cess"] = item.CessAmount;
                        stkTrRow["AddlCess"] = item.AddlCess;
                        stkTrRow["BarcodeNo"] = item.BarcodeNo;
                        stkTrRow["FinYear"] = item.FinYear;
                        stkTrRow["CompanyCode"] = item.CompanyCode;
                        stkTrRow["IsDeduct"] = item.IsStkDeduct;
                        StockTranTbl.Rows.Add(stkTrRow);

                        DataRow btRow = BatchTbl.NewRow();
                        btRow["StockId"] = item.StocksId;
                        btRow["BatchId"] = item.BatchId;
                        btRow["Batch"] = item.Batch;
                        btRow["Quantity"] = item.QtyOut;
                        btRow["ItemCount"] = ItemCnt++;
                        btRow["IsDeduct"] = item.IsStkDeduct;
                        btRow["StockChange"] = 0;
                        BatchTbl.Rows.Add(btRow);
                    }
                }
                GrdProductDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StockTranTbl;
                GrdProductDetails.AutoGenerateColumns = false;
                GrdProductDetails.DataSource = bindingSource;
                CalculateTotalSummaryToGrid();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnSelectInvoice_Click(object sender, EventArgs e)
        {
            GetSalesInvoiceList();
        }
        private void btnSelectCrNote_Click(object sender, EventArgs e)
        {
            GetSalesCreditNoteList();
        }
    }
}
