using DESKTOPNEDBILL.Forms.Banks;
using DESKTOPNEDBILL.Forms.Vendors;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models.Entities;
using TableDims.Models;
using DESKTOPNEDBILL.Forms.Main;
using static System.Windows.Forms.AxHost;
using DESKTOPNEDBILL.Forms.Stock;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.Purchase
{
    public partial class FrmPurchase : Form
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
        bool ADD_NEW_BOOL = true;
        int StockID = 0;
        int BatchID = 0;
        int NewBatchID = 0;
        int OldBatchID = 0;
        string STOCKName = "";
        string HSNCode = "";
        decimal DiscountPer = 0;
        decimal DiscountAmt = 0;
        int EditStockId = 0;
        string EditSTkTranId = "";
        int PurchaseInvoiceNo = 0;
        int PurchasePayNo = 0;
        int VendorId = 0;
        DataTable StockTranTbl = new DataTable();
        DataTable StockTranGridTbl = new DataTable();
        DataTable StockTbl = new DataTable();
        DataTable BatchTbl = new DataTable();
        DataTable InvSummTbl = new DataTable();
        bool EditValue = false;
        decimal PrevSubTotal = 0;
        decimal PrevTotalDiscount = 0;
        decimal PrevHandling = 0;
        decimal PrevRoundOff = 0;
        decimal PrevTotalCess = 0;
        decimal PrevTotalGST = 0;
        decimal PrevGrandTotal = 0;
        decimal PrevPaidAmount = 0;
        int BankGnlId = 0;
        int BankID = 0;
        string VendorType = "Local";
        int NewStockId = 0;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmPurchase()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));
            btnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 5, 5));
            //btnCustomer.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCustomer.Width, btnCustomer.Height, 3, 3));
            btnAddNewStock.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAddNewStock.Width, btnAddNewStock.Height, 5, 5));
            btnExtStock.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExtStock.Width, btnExtStock.Height, 5, 5));

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
            StockTranTbl.Columns.Add("InvoiceRef", typeof(string));
            StockTranTbl.Columns.Add("TranDate", typeof(DateTime));
            StockTranTbl.Columns.Add("TranRef", typeof(string));
            StockTranTbl.Columns.Add("TranType", typeof(string));
            StockTranTbl.Columns.Add("StockId", typeof(int));
            StockTranTbl.Columns.Add("StockName", typeof(string));
            StockTranTbl.Columns.Add("DptId", typeof(int));
            StockTranTbl.Columns.Add("MRP", typeof(decimal));
            StockTranTbl.Columns.Add("StkCost", typeof(decimal));
            StockTranTbl.Columns.Add("StkRetail", typeof(decimal));
            StockTranTbl.Columns.Add("QtyIn", typeof(decimal));
            StockTranTbl.Columns.Add("QtyOut", typeof(decimal));
            StockTranTbl.Columns.Add("ExpDate", typeof(DateTime));
            StockTranTbl.Columns.Add("Batch", typeof(string));
            StockTranTbl.Columns.Add("BatchId", typeof(int));
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
            StockTranTbl.Columns.Add("SubTotal", typeof(decimal));
            StockTranTbl.Columns.Add("TotalAmount", typeof(decimal));
            StockTranTbl.Columns.Add("BarcodeNo", typeof(string));
            StockTranTbl.Columns.Add("UnitMeas", typeof(int));
            StockTranTbl.Columns.Add("FinYear", typeof(string));
            StockTranTbl.Columns.Add("CompanyCode", typeof(string));
            StockTranTbl.Columns.Add("IsDeduct", typeof(bool));

            StockTranTbl.Columns.Add("SellingPriceA", typeof(decimal));
            StockTranTbl.Columns.Add("SellingPriceB", typeof(decimal));
            StockTranTbl.Columns.Add("SellingPriceC", typeof(decimal));
            StockTranTbl.Columns.Add("Total", typeof(decimal));
            StockTranTbl.Columns.Add("Brand", typeof(string));
            StockTranTbl.Columns.Add("MaxLevel", typeof(decimal));
            StockTranTbl.Columns.Add("MinLevel", typeof(decimal));
            StockTranTbl.Columns.Add("SuggOrder", typeof(decimal));
            StockTranTbl.Columns.Add("Vendor", typeof(int));
            StockTranTbl.Columns.Add("Category", typeof(int));
            StockTranTbl.Columns.Add("InventoryType", typeof(int));
            StockTranTbl.Columns.Add("Status", typeof(bool));
            StockTranTbl.Columns.Add("IsNewStock", typeof(bool));
            StockTranTbl.Columns.Add("IsNewBatch", typeof(bool));
        }
        private void CreateStockTranGridTbl()
        {
            StockTranGridTbl.Columns.Add("TranId", typeof(int));
            StockTranGridTbl.Columns.Add("InvNo", typeof(int));
            StockTranGridTbl.Columns.Add("InvoiceRef", typeof(string));
            StockTranGridTbl.Columns.Add("TranDate", typeof(DateTime));
            StockTranGridTbl.Columns.Add("TranRef", typeof(string));
            StockTranGridTbl.Columns.Add("TranType", typeof(string));
            StockTranGridTbl.Columns.Add("StockId", typeof(int));
            StockTranGridTbl.Columns.Add("StockName", typeof(string));
            StockTranGridTbl.Columns.Add("DptId", typeof(int));
            StockTranGridTbl.Columns.Add("MRP", typeof(decimal));
            StockTranGridTbl.Columns.Add("StkCost", typeof(decimal));
            StockTranGridTbl.Columns.Add("StkRetail", typeof(decimal));
            StockTranGridTbl.Columns.Add("QtyIn", typeof(decimal));
            StockTranGridTbl.Columns.Add("QtyOut", typeof(decimal));
            StockTranGridTbl.Columns.Add("ExpDate", typeof(DateTime));
            StockTranGridTbl.Columns.Add("Batch", typeof(string));
            StockTranGridTbl.Columns.Add("BatchId", typeof(int));
            StockTranGridTbl.Columns.Add("GST", typeof(decimal));
            StockTranGridTbl.Columns.Add("CGSTPercentage", typeof(decimal));
            StockTranGridTbl.Columns.Add("SGSTPercentage", typeof(decimal));
            StockTranGridTbl.Columns.Add("Cess", typeof(decimal));
            StockTranGridTbl.Columns.Add("AddlCess", typeof(decimal));
            StockTranGridTbl.Columns.Add("Discount", typeof(decimal));
            StockTranGridTbl.Columns.Add("DiscountPer", typeof(decimal));
            StockTranGridTbl.Columns.Add("TotalDiscount", typeof(decimal));
            StockTranGridTbl.Columns.Add("StkLocation", typeof(string));
            StockTranGridTbl.Columns.Add("DiscountType", typeof(string));
            StockTranGridTbl.Columns.Add("HSNCode", typeof(string));
            StockTranGridTbl.Columns.Add("GSTAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("CessAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("CGSTAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("SGSTAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("IGSTAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("SubTotal", typeof(decimal));
            StockTranGridTbl.Columns.Add("TotalAmount", typeof(decimal));
            StockTranGridTbl.Columns.Add("BarcodeNo", typeof(string));
            StockTranGridTbl.Columns.Add("UnitMeas", typeof(int));
            StockTranGridTbl.Columns.Add("FinYear", typeof(string));
            StockTranGridTbl.Columns.Add("CompanyCode", typeof(string));
            StockTranGridTbl.Columns.Add("IsDeduct", typeof(bool));

            StockTranGridTbl.Columns.Add("SellingPriceA", typeof(decimal));
            StockTranGridTbl.Columns.Add("SellingPriceB", typeof(decimal));
            StockTranGridTbl.Columns.Add("SellingPriceC", typeof(decimal));
            StockTranGridTbl.Columns.Add("Total", typeof(decimal));
            StockTranGridTbl.Columns.Add("Brand", typeof(string));
            StockTranGridTbl.Columns.Add("MaxLevel", typeof(decimal));
            StockTranGridTbl.Columns.Add("MinLevel", typeof(decimal));
            StockTranGridTbl.Columns.Add("SuggOrder", typeof(decimal));
            StockTranGridTbl.Columns.Add("Vendor", typeof(int));
            StockTranGridTbl.Columns.Add("Category", typeof(int));
            StockTranGridTbl.Columns.Add("InventoryType", typeof(int));
            StockTranGridTbl.Columns.Add("Status", typeof(bool));
            StockTranGridTbl.Columns.Add("IsNewStock", typeof(bool));
            StockTranGridTbl.Columns.Add("IsNewBatch", typeof(bool));
        }
        private void CreateStockTbl()
        {
            StockTbl.Columns.Add("StockId", typeof(int));
            StockTbl.Columns.Add("StockName", typeof(string));
            StockTbl.Columns.Add("UnitMeasure", typeof(int));
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
        }
        private void CreateBatchStocktable()
        {
            BatchTbl.Columns.Add("StockId", typeof(int));
            BatchTbl.Columns.Add("Batch", typeof(string));
            BatchTbl.Columns.Add("BatchId", typeof(int));
            BatchTbl.Columns.Add("Quantity", typeof(decimal));
            BatchTbl.Columns.Add("StkCost", typeof(decimal));
            BatchTbl.Columns.Add("StkRetail", typeof(decimal));
            BatchTbl.Columns.Add("MRP", typeof(decimal));
            BatchTbl.Columns.Add("GST", typeof(decimal));
            BatchTbl.Columns.Add("Cess", typeof(decimal));
            BatchTbl.Columns.Add("AddlCess", typeof(decimal));
            BatchTbl.Columns.Add("IsDeduct", typeof(bool));
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
            InvSummTbl.Columns.Add("AddlCessTotal", typeof(decimal));
            InvSummTbl.Columns.Add("GrandTotal", typeof(decimal));
            InvSummTbl.Columns.Add("RoundTotal", typeof(decimal));
            InvSummTbl.Columns.Add("TotCountDescription", typeof(string));
        }
        #endregion

        #region Get & POST Methods
        public void SetStockDeatils(int stockId, int batchId)
        {
            try
            {
                var stkList = (from stk in cmpDBContext.Stock
                               join bth in cmpDBContext.Batch on stk.StockId equals bth.StockId
                               where stk.Status == true && bth.StockId == stockId && bth.BatchId == batchId
                               select new
                               {
                                   stk.StockId,
                                   stk.StockName,
                                   bth.BatchName,
                                   bth.BatchId,
                                   stk.UnitMeasure,
                                   bth.Expiry,
                                   bth.GST,
                                   bth.PurchasePrice,
                                   bth.Mrp,
                                   bth.Cess,
                                   bth.AddlCess,
                                   bth.StockOnHand,
                                   bth.SellingPriceA,
                                   stk.HSNCode,
                                   bth.SellingPriceB,
                                   bth.SellingPriceC,
                                   bth.Barcode,
                                   stk.BrandName,
                                   stk.MaxLevel,
                                   stk.MinLevel,
                                   stk.VendId,
                                   stk.CategoryId,
                                   stk.StkType,
                                   stk.Status,
                                   stk.InventoryType.IsStkDeduct
                               });
                foreach (var stklst in stkList)
                {
                    decimal NewQty = 1;
                    Dictionary<string, decimal> PurchaseGSTlist = MdlMain.PurchaseGSTCalculation(stklst.PurchasePrice, NewQty, stklst.GST, stklst.Cess, DiscountPer, DiscountAmt);
                    DataRow stkTrRow = StockTranTbl.NewRow();
                    stkTrRow["TranId"] = EditSTkTranId == "" ? Convert.ToInt32(0) : Convert.ToInt32(EditSTkTranId);
                    stkTrRow["TranDate"] = DtpTranDate.Value;
                    stkTrRow["TranRef"] = TxtVendRef.Text.Trim();
                    stkTrRow["TranType"] = "Purchase";
                    stkTrRow["StockId"] = stklst.StockId;
                    stkTrRow["StockName"] = stklst.StockName;
                    //stkTrRow["DptId"] = CmbDtpId.SelectedValue;// Need to change
                    stkTrRow["MRP"] = stklst.Mrp;
                    stkTrRow["StkCost"] = stklst.PurchasePrice;
                    stkTrRow["StkRetail"] = stklst.SellingPriceA;
                    stkTrRow["QtyIn"] = NewQty;
                    stkTrRow["QtyOut"] = 0;
                    stkTrRow["ExpDate"] = stklst.Expiry;
                    stkTrRow["Batch"] = stklst.BatchName;
                    stkTrRow["BatchId"] = stklst.BatchId;
                    stkTrRow["GST"] = stklst.GST;
                    stkTrRow["CGSTPercentage"] = stklst.GST / 2;
                    stkTrRow["SGSTPercentage"] = stklst.GST / 2;
                    stkTrRow["Discount"] = 0;
                    stkTrRow["DiscountPer"] = 0;
                    stkTrRow["StkLocation"] = 2001;// Need to change
                    stkTrRow["DiscountType"] = "% per piece";// Need to change
                    stkTrRow["HSNCode"] = stklst.HSNCode;
                    stkTrRow["TotalDiscount"] = 0;
                    stkTrRow["GSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalGSTAmount"]).ToString();
                    stkTrRow["CessAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalCessAmount"]).ToString();
                    stkTrRow["CGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalCGSTAmount"]).ToString();
                    stkTrRow["SGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalSGSTAmount"]).ToString();
                    stkTrRow["IGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalIGSTAmount"]).ToString();
                    stkTrRow["TotalAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalAmount"]).ToString();
                    stkTrRow["SubTotal"] = Convert.ToDecimal(PurchaseGSTlist["SubTotalAmount"]).ToString();
                    stkTrRow["Total"] = Convert.ToDecimal(PurchaseGSTlist["TotalAmount"]).ToString();
                    stkTrRow["Cess"] = stklst.Cess;
                    stkTrRow["AddlCess"] = stklst.AddlCess;
                    stkTrRow["BarcodeNo"] = stklst.Barcode;
                    stkTrRow["FinYear"] = MdlMain.gFinYear;
                    stkTrRow["CompanyCode"] = MdlMain.gCompCode;
                    stkTrRow["IsDeduct"] = stklst.IsStkDeduct;

                    stkTrRow["UnitMeas"] = stklst.UnitMeasure;
                    stkTrRow["SellingPriceA"] = stklst.SellingPriceA;
                    stkTrRow["SellingPriceB"] = stklst.SellingPriceB;
                    stkTrRow["SellingPriceC"] = stklst.SellingPriceC;
                    stkTrRow["Brand"] = stklst.BrandName;
                    stkTrRow["MaxLevel"] = stklst.MaxLevel;
                    stkTrRow["MinLevel"] = stklst.MinLevel;
                    stkTrRow["Vendor"] = stklst.VendId;
                    stkTrRow["Category"] = stklst.CategoryId;
                    stkTrRow["InventoryType"] = stklst.StkType;
                    stkTrRow["Status"] = stklst.Status;

                    StockTranTbl.Rows.Add(stkTrRow);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetVendorDetails(int vendID)
        {
            try
            {
                var vendList = (from vnd in cmpDBContext.Vendor
                                where vnd.Status == true && vnd.VendorId == vendID
                                select new
                                {
                                    vnd.VendorName,
                                    vnd.VendorAdd1,
                                    vnd.VendorAdd2,
                                    vnd.TotalBal,
                                    vnd.VendorType
                                }).ToList();
                foreach (var vnd in vendList)
                {
                    VendorId = vendID;
                    TxtVendBalance.Text = vnd.TotalBal.ToString();
                    VendorType = vnd.VendorType;
                    lblVendorName.Text = vnd.VendorName.Trim();
                    lblAddress.Text = vnd.VendorAdd1 + ", " + vnd.VendorAdd2;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void MoveDataFromGrdProductDetails()
        {
            try
            {
                EditValue = true;
                int i = GrdProductDetails.CurrentRow.Index;
                DataRow[] FndTrRow = StockTranGridTbl.Select();
                EditSTkTranId = FndTrRow[i]["TranId"].ToString();
                StockID = Convert.ToInt32(FndTrRow[i]["StockId"]);
                BatchID = Convert.ToInt32(FndTrRow[i]["BatchId"]);
                StockTranTbl.Clear();
                StockTranTbl.ImportRow(StockTranGridTbl.Rows[i]);
                StockTranGridTbl.Rows.Remove(FndTrRow[i]);
                CalculateTotalSummaryToGrid();
                //ChkIsNewBatch.Visible = true;
                MdlMain.gStkTranDS.Tables.Clear();
                MdlMain.gStkTranDS.Tables.Add(StockTranTbl);
                FrmAddStockDetails frmAddStockDetails = new FrmAddStockDetails(this);
                frmAddStockDetails.ShowDialog();
                if (MdlMain.gStkTranDS.Tables.Count > 0)
                {
                    foreach (DataRow dr in (MdlMain.gStkTranDS.Tables[0].Rows))
                    {
                        StockTranGridTbl.Rows.Add(dr.ItemArray);
                    }
                }
                CalculateTotalSummaryToGrid();
                GrdProductDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StockTranGridTbl;
                GrdProductDetails.AutoGenerateColumns = false;
                GrdProductDetails.DataSource = bindingSource;
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
                decimal GSTTotal = 0;
                decimal CGSTTotal = 0;
                decimal SGSTTotal = 0;
                decimal IGSTTotal = 0;
                decimal DiscountTotal = 0;
                decimal GrandTotal = 0;
                decimal CessTotal = 0;
                decimal RoundTotal = 0;
                decimal AddlCessTotal = 0;
                foreach (DataRow item in StockTranGridTbl.Rows)
                {
                    Dictionary<string, decimal> PurchaseGSTlist = MdlMain.PurchaseGSTCalculation(Convert.ToDecimal(item["StkCost"]), Convert.ToDecimal(item["QtyIn"]), Convert.ToDecimal(item["GST"]), Convert.ToDecimal(item["Cess"]), Convert.ToDecimal(item["DiscountPer"]), Convert.ToDecimal(item["Discount"]));
                    NetTotal += (PurchaseGSTlist["SubTotalAmount"]);
                    GSTTotal += (PurchaseGSTlist["TotalGSTAmount"]);
                    CGSTTotal += (PurchaseGSTlist["TotalCGSTAmount"]);
                    SGSTTotal += (PurchaseGSTlist["TotalSGSTAmount"]);
                    if (VendorType == "Local")
                    { IGSTTotal = 0; }
                    else
                    {
                        IGSTTotal += (PurchaseGSTlist["TotalIGSTAmount"]);
                    }
                    DiscountTotal += (PurchaseGSTlist["TotalDiscountAmount"]);
                    CessTotal += (PurchaseGSTlist["TotalCessAmount"]);
                    GrandTotal += (PurchaseGSTlist["TotalAmount"]);
                    RoundTotal = Math.Round(GrandTotal);
                    AddlCessTotal += Convert.ToDecimal(item["AddlCess"]);
                    TxtSubTotal.Text = NetTotal.ToString("0.00");
                    TxtTotalCGSTAmt.Text = CGSTTotal.ToString("0.00");
                    TxtTotalSGSTAmount.Text = SGSTTotal.ToString("0.00");
                    TxtTotalIGSTAmt.Text = IGSTTotal.ToString("0.00");
                    TxtGrandTotal.Text = GrandTotal.ToString("0.00");
                }
                InvSummTbl.Clear();
                DataRow summRow = InvSummTbl.NewRow();
                summRow["NetTotal"] = NetTotal;
                summRow["GSTTotal"] = GSTTotal;
                summRow["CGSTTotal"] = CGSTTotal;
                summRow["SGSTTotal"] = SGSTTotal;
                summRow["IGSTTotal"] = IGSTTotal;
                summRow["DiscountTotal"] = DiscountTotal;
                summRow["CessTotal"] = CessTotal;
                summRow["AddlCessTotal"] = AddlCessTotal;
                summRow["GrandTotal"] = GrandTotal;
                summRow["RoundTotal"] = RoundTotal;
                summRow["TotCountDescription"] = "Total Items: " + StockTranTbl.Rows.Count;
                InvSummTbl.Rows.Add(summRow);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = InvSummTbl;
                GrdSummary.AutoGenerateColumns = false;
                GrdSummary.DataSource = bindingSource;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetProductDetails()
        {
            try
            {
                MdlMain.gStkTranDS.Tables.Clear();
                FrmProductSelectList frmproductList = new FrmProductSelectList();
                frmproductList.ShowDialog();
                if (MdlMain.gStockId > 0 && MdlMain.gBatchId > 0)
                {
                    SetStockDeatils(MdlMain.gStockId, MdlMain.gBatchId);
                    MdlMain.gStkTranDS.Tables.Add(StockTranTbl);
                    FrmAddStockDetails frmAddStockDetails = new FrmAddStockDetails(this);
                    frmAddStockDetails.ShowDialog();
                    //=========================================                   
                    if (MdlMain.gStkTranDS.Tables.Count > 0)
                    {
                        foreach (DataRow dr in (MdlMain.gStkTranDS.Tables[0].Rows))
                        {
                            StockTranGridTbl.Rows.Add(dr.ItemArray);
                        }
                    }
                    StockTranTbl.Clear();
                    CalculateTotalSummaryToGrid();
                    EditValue = false;
                    EditSTkTranId = "";
                    //ChkIsNewBatch.Visible = false;
                    //=============================================
                    //EditValue = true;
                    MdlMain.gStockId = 0;
                    MdlMain.gBatchId = 0;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StockTranGridTbl;
                    GrdProductDetails.AutoGenerateColumns = false;
                    GrdProductDetails.DataSource = bindingSource;
                    EditValue = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetPurInvoiceSummaryDetails(int purInvNo)
        {
            try
            {
                var invList = (from pInvMst in cmpDBContext.PurchaseInvMasters
                               join vnd in cmpDBContext.Vendor on pInvMst.VendID equals vnd.VendorId
                               where pInvMst.PurInvNo == purInvNo
                               select new
                               {
                                   pInvMst.InvDate,
                                   pInvMst.VendID,
                                   pInvMst.InvAmount,
                                   pInvMst.PayBankIdNo,
                                   pInvMst.InvPaymode,
                                   pInvMst.PayNo,
                                   pInvMst.Handling,
                                   pInvMst.HandlingPer,
                                   pInvMst.TotDiscount,
                                   pInvMst.RoundAmount,
                                   pInvMst.InvRef,
                                   pInvMst.TotalCessAmount,
                                   pInvMst.TotalCGSTAmount,
                                   pInvMst.TotalSGSTAmount,
                                   pInvMst.TotalIGSTAmount,
                                   pInvMst.AddlCess,
                                   pInvMst.AddlDiscount,
                                   pInvMst.SubTotal,
                                   vnd.VendorName,
                                   vnd.VendorId,
                                   vnd.VendorAdd1,
                                   vnd.VendorAdd2,
                                   vnd.TotalBal
                               }).ToList();
                foreach (var inv in invList)
                {
                    PurchaseInvoiceNo = purInvNo;
                    PurchasePayNo = inv.PayNo;
                    VendorId = inv.VendorId;
                    lblVendorName.Text = inv.VendorName.Trim();
                    lblAddress.Text = inv.VendorAdd1 + ", " + inv.VendorAdd2;
                    MdlMain.gVendorId = inv.VendorId;
                    DtpTranDate.Text = inv.InvDate.ToString("dd/MMM/yyyy");
                    CmbPaymentMode.Text = inv.InvPaymode;
                    TxtRoundOff.Text = inv.RoundAmount.ToString();
                    TxtVendRef.Text = inv.InvRef;
                    BankID = inv.PayBankIdNo;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SetPurInvoiceGridDetails(int purInvNo)
        {
            try
            {
                StockTranGridTbl.Clear();
                var stkTranList = (from stkTran in cmpDBContext.StkTrans
                                   join stk in cmpDBContext.Stock on stkTran.StocksId equals stk.StockId
                                   join bth in cmpDBContext.Batch on stkTran.BatchId equals bth.BatchId
                                   where stkTran.InvNo == purInvNo && stkTran.TranRef == "Invoice" && stkTran.TranType == "Purchase"
                                   select new
                                   {
                                       stkTran.TranId,
                                       stkTran.InvNo,
                                       stkTran.InvoiceRef,
                                       stkTran.TranDate,
                                       stkTran.StocksId,
                                       stk.StockName,
                                       stkTran.DptId,
                                       bth.Mrp,
                                       stkTran.StkCost,
                                       stkTran.StkRetail,
                                       stkTran.QtyIn,
                                       stkTran.QtyOut,
                                       bth.Expiry,
                                       bth.BatchName,
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
                                       stkTran.FinYear,
                                       stkTran.CompanyCode,
                                       bth.Barcode,
                                       stk.InventoryType.IsStkDeduct,
                                       stk.UnitMeasure,
                                       bth.SellingPriceA,
                                       bth.SellingPriceB,
                                       bth.SellingPriceC,
                                       stk.BrandName,
                                       stk.MaxLevel,
                                       stk.MinLevel,
                                       stk.VendId,
                                       stk.CategoryId,
                                       stk.StkType,
                                       stk.Status,
                                   }).ToList();
                if (stkTranList.Count() != 0)
                {
                    //GrdProductDetails.DataSource = null;
                    //BindingSource bindingSource = new BindingSource();
                    //bindingSource.DataSource = stkTranList;
                    //GrdProductDetails.AutoGenerateColumns = false;
                    //GrdProductDetails.DataSource = bindingSource;
                    //int ItemCnt = 0;
                    foreach (var item in stkTranList)
                    {
                        DataRow stkTrRow = StockTranGridTbl.NewRow();
                        stkTrRow["TranId"] = item.TranId;
                        stkTrRow["InvNo"] = item.InvNo;
                        stkTrRow["InvoiceRef"] = item.InvoiceRef;
                        stkTrRow["TranDate"] = item.TranDate;
                        stkTrRow["TranRef"] = TxtVendRef.Text.Trim();
                        stkTrRow["TranType"] = "Purchase";
                        stkTrRow["StockId"] = item.StocksId;
                        stkTrRow["StockName"] = item.StockName;
                        //stkTrRow["DptId"] = CmbDtpId.SelectedValue;// Need to change
                        stkTrRow["MRP"] = item.Mrp;
                        stkTrRow["StkCost"] = item.StkCost;
                        stkTrRow["StkRetail"] = item.StkRetail;
                        stkTrRow["QtyIn"] = item.QtyIn;
                        stkTrRow["QtyOut"] = item.QtyOut;
                        stkTrRow["ExpDate"] = item.Expiry;
                        stkTrRow["Batch"] = item.BatchName;
                        stkTrRow["BatchId"] = item.BatchId;
                        stkTrRow["GST"] = item.GST;
                        stkTrRow["CGSTPercentage"] = item.CGSTPercentage;
                        stkTrRow["SGSTPercentage"] = item.SGSTPercentage;
                        stkTrRow["DiscountType"] = item.DiscountType;
                        stkTrRow["DiscountPer"] = item.DiscPer;
                        stkTrRow["Discount"] = item.Discount;
                        stkTrRow["TotalDiscount"] = item.TotalDiscount;
                        stkTrRow["StkLocation"] = 2001;// Need to change
                        stkTrRow["HSNCode"] = HSNCode;
                        stkTrRow["GSTAmount"] = item.GSTAmount;
                        stkTrRow["CessAmount"] = item.CessAmount;
                        stkTrRow["CGSTAmount"] = item.CGSTAmount;
                        stkTrRow["SGSTAmount"] = item.SGSTAmount;
                        stkTrRow["IGSTAmount"] = item.IGSTAmount;
                        stkTrRow["TotalAmount"] = item.TotalAmount;
                        stkTrRow["Cess"] = item.CessAmount;
                        stkTrRow["AddlCess"] = item.AddlCess;
                        stkTrRow["BarcodeNo"] = item.Barcode;
                        stkTrRow["FinYear"] = item.FinYear;
                        stkTrRow["CompanyCode"] = item.CompanyCode;

                        stkTrRow["UnitMeas"] = item.UnitMeasure;
                        stkTrRow["SellingPriceA"] = item.SellingPriceA;
                        stkTrRow["SellingPriceB"] = item.SellingPriceB;
                        stkTrRow["SellingPriceC"] = item.SellingPriceC;
                        stkTrRow["Brand"] = item.BrandName;
                        stkTrRow["MaxLevel"] = item.MaxLevel;
                        stkTrRow["MinLevel"] = item.MinLevel;
                        stkTrRow["Vendor"] = item.VendId;
                        stkTrRow["Category"] = item.CategoryId;
                        stkTrRow["InventoryType"] = item.StkType;
                        stkTrRow["Status"] = true;
                        stkTrRow["IsNewStock"] = false;
                        stkTrRow["IsNewBatch"] = false;
                        StockTranGridTbl.Rows.Add(stkTrRow);

                        DataRow btRow = BatchTbl.NewRow();
                        btRow["StockId"] = item.StocksId;
                        btRow["BatchId"] = item.BatchId;
                        btRow["Batch"] = item.BatchName;
                        btRow["Quantity"] = item.QtyIn;
                        btRow["IsDeduct"] = item.IsStkDeduct;
                        BatchTbl.Rows.Add(btRow);
                    }
                }

                if (StockTranGridTbl.Rows.Count > 0)
                {
                    GrdProductDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StockTranGridTbl;
                    GrdProductDetails.AutoGenerateColumns = false;
                    GrdProductDetails.DataSource = bindingSource;
                }
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
                DataRow[] FndTrRow = StockTranGridTbl.Select();
                DataRow[] BatchRow = BatchTbl.Select();
                int count1 = 0;
                if (StockTranGridTbl.Rows.Count > 0)
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
        private void GetPurInvoiceList()
        {
            try
            {
                FrmPurchaseInvSelectList frmpurInvoiceList = new FrmPurchaseInvSelectList();
                frmpurInvoiceList.ShowDialog();
                if (MdlMain.gPurchaseInvNo > 0)
                {
                    StockTranTbl.Clear();
                    StockTranGridTbl.Clear();
                    SetPurInvoiceSummaryDetails(MdlMain.gPurchaseInvNo);
                    SetPurInvoiceGridDetails(MdlMain.gPurchaseInvNo);
                    SetBankDetails(BankID);
                    ADD_NEW_BOOL = false;
                    PrevSubTotal = Convert.ToDecimal(TxtSubTotal.Text);
                    //PrevTotalDiscount = Convert.ToDecimal(TxtTotalDiscount.Text);
                    //PrevHandling = Convert.ToDecimal(TxtHandlingAmt.Text);
                    PrevRoundOff = Convert.ToDecimal(TxtRoundOff.Text);
                    //PrevTotalCess = Convert.ToDecimal(TxtTotalCessAmt.Text);
                    PrevTotalGST = Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2;
                    PrevGrandTotal = Convert.ToDecimal(TxtGrandTotal.Text);
                    PrevPaidAmount = Convert.ToDecimal(TxtPaidAmount.Text);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InitializingData()
        {
            ADD_NEW_BOOL = true;
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel6);
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel2);
            MdlMain.ClearTextBoxesInPanel(this.tableLayoutPanel8);
            lblVendorName.Text = "";
            lblAddress.Text = "";            
            StockTranTbl.Clear();
            InvSummTbl.Clear();
            StockTranGridTbl.Clear();
            MdlMain.gStkTranDS.Tables.Clear();
            //ChkIsNewBatch.Visible = false;
            GrdProductDetails.DataSource = null;
            EditStockId = 0;
            PurchaseInvoiceNo = 0;
            PurchasePayNo = 0;
            MdlMain.gPurchaseInvNo = 0;
            MdlMain.gVendorId = 0;
            CmbPaymentMode.SelectedIndex = 0;
            BankGnlId = 15000;
            BankID = 1;
            //CmbDiscType.SelectedIndex = 0;
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
            NewStockId = 0;
            VendorId = 1;
            SetVendorDetails(VendorId);
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
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool FieldValidation()
        {
            try
            {

                if (VendorId <= 0 && lblVendorName.Text == "")
                {
                    MessageBox.Show("Please select Vendor", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCustomer.Focus();
                    return false;
                }
                if (StockTranGridTbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Product is not added in table for saving purchase", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (TxtVendRef.Text == "")
                {
                    MessageBox.Show("Please enter Purchase Bill No", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtVendRef.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event Handling methods
        private void GrdProductDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrdProductDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                EditStockId = Convert.ToInt32(GrdProductDetails.CurrentRow.Cells[0].Value);
                if (EditStockId > 0)
                {
                    MoveDataFromGrdProductDetails();
                }
            }
            if (GrdProductDetails.Columns[e.ColumnIndex].Name == "Delete")
            {
                int i = GrdProductDetails.CurrentRow.Index;
                DataRow[] FndTrRow = StockTranGridTbl.Select();
                StockTranTbl.Rows.Remove(FndTrRow[i]);
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StockTranGridTbl;
                GrdProductDetails.AutoGenerateColumns = false;
                GrdProductDetails.DataSource = bindingSource;
                CalculateTotalSummaryToGrid();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation())
            {
                if (ADD_NEW_BOOL == false && PurchaseInvoiceNo > 0)
                {
                    List<StkTran> stkTr = cmpDBContext.StkTrans.Where(m => m.InvNo == PurchaseInvoiceNo && m.TranType == "Purchase" && m.TranRef == "Invoice").ToList();
                    if (stkTr.Count > 0)
                    {
                        cmpDBContext.StkTrans.RemoveRange(stkTr);
                        cmpDBContext.SaveChanges();
                    }
                    IsGlbreakExist(PurchaseInvoiceNo.ToString(), TxtVendRef.Text.Trim());
                }
                decimal paidAmount = TxtPaidAmount.Text != "" ? Convert.ToDecimal(TxtPaidAmount.Text) : Convert.ToDecimal(0);
                if (paidAmount > 0)
                {
                    if (PurchasePayNo <= 0)
                    {
                        SavePurchasePayment();
                    }
                }
                else
                {
                    PurchasePayNo = 0;
                }
                SavePurchaseInvMaster();
                SaveStock();
                SaveBatch();
                SaveStkTran();
                SetGlBreak();
                UpdateStockBatch();
                AddVendorDue();
                InitializingData();
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Purchase saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Purchase updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void PbSearchBillNo_Click(object sender, EventArgs e)
        {
            GetPurInvoiceList();
        }
        private void TxtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            if (CmbPaymentMode.Text == "Credit")
            {
                TxtPaidAmount.Text = "0";
            }
            else { TxtPaidAmount.Text = TxtGrandTotal.Text; }
        }
        private void CmbPaymentMode_TextChanged(object sender, EventArgs e)
        {
            if (CmbPaymentMode.Text == "Cash")
            {
                TxtBankName.Text = "Cash";
                TxtPaidAmount.Text = TxtGrandTotal.Text;
                TxtPaidAmount.Enabled = true;
                BankGnlId = 15000;
                BankID = 1;
            }
            else
            {
                TxtBankName.Text = "Credit";
                TxtPaidAmount.Text = "0";
                TxtPaidAmount.Enabled = false;
                BankGnlId = 0;
                BankID = 0;
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
                if (row.Cells[GrdProductDetails.Columns["cost"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["cost"].Index].Value = 0;
                }
                if (row.Cells[GrdProductDetails.Columns["qty"].Index].Value.ToString() == "")
                {
                    row.Cells[GrdProductDetails.Columns["qty"].Index].Value = 0;
                }

                decimal DiscPer = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["discPer"].Index].Value);
                decimal DiscAmt = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["DiscAmt"].Index].Value);
                DiscountPer = DiscPer;
                DiscountAmt = DiscAmt;
                decimal Cost = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["cost"].Index].Value);
                decimal QuantityIn = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["qty"].Index].Value);
                decimal GST = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["gstper"].Index].Value);
                decimal Cess = Convert.ToDecimal(row.Cells[GrdProductDetails.Columns["cess"].Index].Value);
                Dictionary<string, decimal> SalesGSTlist = MdlMain.PurchaseGSTCalculation(Cost, QuantityIn, GST, Cess, DiscountPer, DiscountAmt);

                row.Cells[GrdProductDetails.Columns["totdisc"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalDiscountAmount"]);
                row.Cells[GrdProductDetails.Columns["totalamount"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalAmount"]);
                row.Cells[GrdProductDetails.Columns["grdSGST"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalSGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["grdCGST"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalCGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["gstamt"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalGSTAmount"]);
                row.Cells[GrdProductDetails.Columns["cess"].Index].Value = Convert.ToDecimal(SalesGSTlist["TotalCessAmount"]);
                //row.Cells[GrdProductDetails.Columns["nettotal"].Index].Value = Convert.ToDecimal(SalesGSTlist["NetTotalAmount"]);
                CalculateTotalSummaryToGrid();
            }
        }
        private void FrmPurchase_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
        private void FrmPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(sender, e);
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
        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmVendorSelectList frmvendorList = new FrmVendorSelectList();
            frmvendorList.ShowDialog();
            SetVendorDetails(MdlMain.gVendorId);
        }
        private void btnAddNewStock_Click(object sender, EventArgs e)
        {
            MdlMain.gStkTranDS.Tables.Clear();
            FrmAddStockDetails frmAddStockDetails = new FrmAddStockDetails(this);
            frmAddStockDetails.ShowDialog();
            if (MdlMain.gStkTranDS.Tables.Count > 0)
            {
                foreach (DataRow dr in (MdlMain.gStkTranDS.Tables[0].Rows))
                {
                    StockTranGridTbl.Rows.Add(dr.ItemArray);
                }
            }
            CalculateTotalSummaryToGrid();
            GrdProductDetails.DataSource = null;
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = StockTranGridTbl;
            GrdProductDetails.AutoGenerateColumns = false;
            GrdProductDetails.DataSource = bindingSource;
        }
        private void btnExtStock_Click(object sender, EventArgs e)
        {
            GetProductDetails();
        }

        #endregion

        #region Save and Update methods
        private bool SavePurchaseInvMaster()
        {
            try
            {
                if (MdlMain.gPurchaseInvNo > 0)
                {
                    List<PurchaseInvMaster> pInvMaster = cmpDBContext.PurchaseInvMasters.Where(m => m.PurInvNo == MdlMain.gPurchaseInvNo).ToList();
                    foreach (PurchaseInvMaster pinv in pInvMaster)
                    {
                        foreach (DataRow item in InvSummTbl.Rows)
                        {
                            pinv.InvDate = DtpTranDate.Value.Date;
                            pinv.VendID = VendorId;
                            pinv.InvAmount = TxtGrandTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGrandTotal.Text);
                            pinv.InvPaymode = CmbPaymentMode.Text;
                            //pinv.Handling = TxtHandlingAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandlingAmt.Text);
                            //pinv.HandlingPer = TxtHandlingPer.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandlingPer.Text);
                            pinv.TotDiscount = Convert.ToDecimal(item["DiscountTotal"]);//TxtTotalDiscount.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalDiscount.Text);
                            pinv.RoundAmount = TxtRoundOff.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtRoundOff.Text);
                            pinv.VendStatus = "Active";
                            pinv.InvRef = TxtVendRef.Text.Trim();
                            pinv.SubTotal = TxtSubTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSubTotal.Text);
                            pinv.TotalCessAmount = Convert.ToDecimal(item["CessTotal"]);//TxtTotalCessAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCessAmt.Text);
                            pinv.TotalCGSTAmount = Convert.ToDecimal(item["CGSTTotal"]);//TxtTotalCGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCGSTAmt.Text);
                            pinv.TotalSGSTAmount = Convert.ToDecimal(item["SGSTTotal"]);//TxtTotalSGSTAmount.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalSGSTAmount.Text);
                            pinv.TotalIGSTAmount = Convert.ToDecimal(item["IGSTTotal"]);//TxtTotalIGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalIGSTAmt.Text);
                            pinv.AddlCess = Convert.ToDecimal(item["AddlCessTotal"]);//TxtTotalAddlCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalAddlCess.Text);
                            pinv.AddlDiscount = 0;//TxtAddlDisc.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtAddlDisc.Text);
                            pinv.PayBankIdNo = BankID;//-Need to change
                            pinv.PayNo = PurchasePayNo;//TxtPaidAmount.Text != "" ? SavePurchasePayment() : 0;   
                            pinv.BillType = "GST";//Need to change                       
                            pinv.FinYear = MdlMain.gFinYear;
                            pinv.CompanyCode = MdlMain.gCompCode;
                        }

                    }
                    //cmpDBContext.PurchaseInvMasters.UpdateRange(pInvMaster);
                    cmpDBContext.SaveChanges();
                }
                else
                {
                    foreach (DataRow item in InvSummTbl.Rows)
                    {
                        var purchaseInvMasters = new PurchaseInvMaster()
                        {
                            InvDate = DtpTranDate.Value.Date,
                            VendID = VendorId,
                            InvAmount = TxtGrandTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGrandTotal.Text),
                            InvPaymode = CmbPaymentMode.Text,
                            Handling = 0,//TxtHandlingAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandlingAmt.Text),
                            HandlingPer = 0,//TxtHandlingPer.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHandlingPer.Text),
                            TotDiscount = Convert.ToDecimal(item["DiscountTotal"]),//TxtTotalDiscount.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalDiscount.Text),
                            RoundAmount = TxtRoundOff.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtRoundOff.Text),
                            VendStatus = "Active",
                            InvRef = TxtVendRef.Text.Trim(),
                            SubTotal = TxtSubTotal.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSubTotal.Text),
                            TotalCessAmount = Convert.ToDecimal(item["CessTotal"]),//TxtTotalCessAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCessAmt.Text),
                            TotalCGSTAmount = Convert.ToDecimal(item["CGSTTotal"]),//TxtTotalCGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalCGSTAmt.Text),
                            TotalSGSTAmount = Convert.ToDecimal(item["SGSTTotal"]),//TxtTotalSGSTAmount.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalSGSTAmount.Text),
                            TotalIGSTAmount = Convert.ToDecimal(item["IGSTTotal"]),//TxtTotalIGSTAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalIGSTAmt.Text),
                            AddlCess = Convert.ToDecimal(item["AddlCessTotal"]),//TxtTotalAddlCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTotalAddlCess.Text),
                            AddlDiscount = 0,//TxtAddlDisc.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtAddlDisc.Text),
                            PayBankIdNo = BankID,
                            PayNo = PurchasePayNo,//TxtPaidAmount.Text != "" ? SavePurchasePayment() : 0,//-------Need to change  
                            BillType = "GST",//Need to change                        
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                        };
                        cmpDBContext.PurchaseInvMasters.Add(purchaseInvMasters);
                        cmpDBContext.SaveChanges();
                        PurchaseInvoiceNo = purchaseInvMasters.PurInvNo;
                    }
                    if (PurchaseInvoiceNo > 0)
                    {
                        return true;
                    }
                    else return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int SavePurchasePayment()
        {
            try
            {
                var purPay = new PurchasePayment()
                {
                    TranDate = DtpTranDate.Value.Date,
                    PayAmount = Convert.ToDecimal(TxtPaidAmount.Text),
                    VendorId = VendorId,
                    GnlId = BankGnlId,
                    FinYear = MdlMain.gFinYear,
                    CompanyCode = MdlMain.gCompCode
                };
                cmpDBContext.PurchasePayments.Add(purPay);
                cmpDBContext.SaveChanges();
                PurchasePayNo = purPay.PayNo;
                return purPay.PayNo;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveStkTran()
        {
            try
            {                
                foreach (DataRow item in StockTranGridTbl.Rows)
                {
                    var stkTran = new StkTran()
                    {
                        //Stocks = (Stocks)cmpDBContext.Stock.Where(m => m.StockId == Convert.ToInt32(item["StockId"])),
                        InvNo = PurchaseInvoiceNo,
                        TranDate = DtpTranDate.Value.Date,
                        TranRef = "Invoice",
                        TranType = "Purchase",
                        StocksId = Convert.ToInt32(item["StockId"]),
                        DptId = 2001,
                        //mrp = Convert.ToDecimal(item["MRP"]),
                        StkCost = Convert.ToDecimal(item["StkCost"]),
                        StkRetail = Convert.ToDecimal(item["StkRetail"]),
                        QtyIn = Convert.ToDecimal(item["QtyIn"]),
                        QtyOut = Convert.ToDecimal(item["QtyOut"]),
                        Expiry = Convert.ToDateTime(item["ExpDate"]),//----------------------Need to change
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
                        TotalAmount = Convert.ToDecimal(item["TotalAmount"]),
                        FinYear = item["FinYear"].ToString(),
                        CompanyCode = item["CompanyCode"].ToString(),
                        InvoiceRef = TxtVendRef.Text.Trim(),
                    };
                    cmpDBContext.StkTrans.Add(stkTran);
                    cmpDBContext.SaveChanges();
                    item["TranId"] = stkTran.TranId;
                    item["InvNo"] = PurchaseInvoiceNo;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private void UpdateStockBatch()
        {
            try
            {
                SetAllStockDetailsForUpdatinginBatch();
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
        private void SetGlBreak()
        {
            try
            {
                foreach (DataRow item in InvSummTbl.Rows)
                {


                    if (Convert.ToDecimal(TxtGrandTotal.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranRef = PurchaseInvoiceNo.ToString(),
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,//-----------------Need to Change
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            IDNo = VendorId,
                            DptId = 2001,//-----------------Need to Change
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 32000, //---------------------------------------Accounts Payable Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(TxtGrandTotal.Text),
                        };

                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32000).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(TxtGrandTotal.Text) - PrevGrandTotal);
                            }
                            else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(TxtGrandTotal.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(TxtSubTotal.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            TranRef = PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 50001, //---------------------------------------Cost of Goods Head
                            Amt1 = Convert.ToDecimal(TxtSubTotal.Text),
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50001).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtSubTotal.Text) - PrevSubTotal);
                            }
                            else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(TxtSubTotal.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["DiscountTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranRef = PurchaseInvoiceNo.ToString(),
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 45025,//---------------------------------------Discounts Received Head
                            Amt1 = 0,
                            Amt2 = Convert.ToDecimal(item["DiscountTotal"]),
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 45025).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt2 = gl.Amt2 + (Convert.ToDecimal(item["DiscountTotal"]) - PrevTotalDiscount);
                            }
                            else { gl.Amt2 = gl.Amt2 + Convert.ToDecimal(item["DiscountTotal"]); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    //if (TxtHandlingAmt.Text != "" ? Convert.ToDecimal(TxtHandlingAmt.Text) > 0 : Convert.ToDecimal(0) > 0)
                    //{
                    //    var glBreak = new GlBreak()
                    //    {
                    //        TranDate = DtpTranDate.Value,
                    //        FinYear = MdlMain.gFinYear,
                    //        TranType = "Invoice",
                    //        GlModule = "Purchase",
                    //        TranRef = PurchaseInvoiceNo.ToString(),
                    //        IDNo = VendorId,
                    //        DptId = 2001,
                    //        AmtMode = TxtBankName.Text.Trim(),
                    //        Status = "Active",
                    //        DEnd = "No",
                    //        LedAc = TxtVendRef.Text.Trim(),
                    //        GnlId = 50011,//---------------------------------------Carriage In Head
                    //        Amt1 = Convert.ToDecimal(TxtHandlingAmt.Text),
                    //        Amt2 = 0,
                    //    };
                    //    cmpDBContext.GlBreaks.Add(glBreak);
                    //    cmpDBContext.SaveChanges();
                    //    List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50011).ToList();
                    //    foreach (GlAccount gl in glAcc)
                    //    {
                    //        if (ADD_NEW_BOOL == false)
                    //        {
                    //            gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtHandlingAmt.Text) - PrevHandling);
                    //        }
                    //        else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(TxtHandlingAmt.Text); }
                    //    }
                    //    //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                    //    cmpDBContext.SaveChanges();
                    //}
                    if (TxtRoundOff.Text != "" ? Convert.ToDecimal(TxtRoundOff.Text) > 0 : Convert.ToDecimal(0) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            TranRef = PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 50051,//---------------------------------------Printing and Stationery Head
                            Amt1 = Convert.ToDecimal(TxtRoundOff.Text),
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50051).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtRoundOff.Text) - PrevRoundOff);
                            }
                            else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(TxtRoundOff.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(item["CessTotal"]) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            TranRef = PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 50010,//---------------------------------------Cess Input Head
                            Amt1 = Convert.ToDecimal(Convert.ToDecimal(item["CessTotal"])),
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 50010).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(item["CessTotal"]) - PrevTotalCess);
                            }
                            else { gl.Amt1 = gl.Amt1 + Convert.ToDecimal(item["CessTotal"]); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(TxtTotalCGSTAmt.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            TranRef = PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 12001,//---------------------------------------GST Paid Head
                            Amt1 = Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2,//----Adding CGST & SGST
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 12001).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + ((Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2) - PrevTotalGST);
                            }
                            else { gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(TxtTotalIGSTAmt.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Invoice",
                            GlModule = "Purchase",
                            TranRef = PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            AmtMode = TxtBankName.Text.Trim(),
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            GnlId = 12001,//---------------------------------------GST Paid Head
                            Amt1 = Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2,//----Adding CGST & SGST
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 12001).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 = gl.Amt1 + ((Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2) - PrevTotalGST);
                            }
                            else { gl.Amt1 = gl.Amt1 + (Convert.ToDecimal(TxtTotalCGSTAmt.Text) * 2); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                    }
                    if (Convert.ToDecimal(TxtPaidAmount.Text) > 0)
                    {
                        var glBreak = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Payment",
                            GlModule = "Purchase",
                            TranRef = PurchasePayNo.ToString(),//PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            AmtMode = TxtBankName.Text.Trim(),
                            GnlId = 32000,//---------------------------------------Accounts Payable Head
                            Amt1 = Convert.ToDecimal(TxtPaidAmount.Text),
                            Amt2 = 0,
                        };
                        cmpDBContext.GlBreaks.Add(glBreak);
                        cmpDBContext.SaveChanges();
                        List<GlAccount> glAcc = cmpDBContext.GlAccounts.Where(m => m.GnlId == 32000).ToList();
                        foreach (GlAccount gl in glAcc)
                        {
                            if (ADD_NEW_BOOL == false)
                            {
                                gl.Amt1 += (Convert.ToDecimal(TxtPaidAmount.Text) - PrevPaidAmount);
                            }
                            else { gl.Amt1 += Convert.ToDecimal(TxtPaidAmount.Text); }
                        }
                        //cmpDBContext.GlAccounts.UpdateRange(glAcc);
                        cmpDBContext.SaveChanges();
                        var glBreak1 = new GlBreak()
                        {
                            TranDate = DtpTranDate.Value.Date,
                            FinYear = MdlMain.gFinYear,
                            TranType = "Payment",
                            GlModule = "Purchase",
                            TranRef = PurchasePayNo.ToString(),//PurchaseInvoiceNo.ToString(),
                            IDNo = VendorId,
                            DptId = 2001,
                            Status = "Active",
                            DEnd = "No",
                            LedAc = TxtVendRef.Text.Trim(),
                            AmtMode = TxtBankName.Text.Trim(),
                            GnlId = BankGnlId,//---------------------------------------Accounts Payable Head
                                              //Amt1 = Convert.ToDecimal(TxtPaidAmount.Text),
                                              //Amt2 = 0,
                        };
                        var glc = cmpDBContext.GlAccounts.Where(m => m.GnlId == BankGnlId).Select(m => m);
                        if (glc.Count() > 0)
                        {
                            if (BankGnlId == 15000)//-------------------------------Cash In Hand Head
                            {
                                glBreak1.Amt1 = 0;
                                glBreak1.Amt2 = Convert.ToDecimal(TxtPaidAmount.Text);
                                //List<GlAccount> glAccnt = cmpDBContext.GlAccounts.Where(m => m.GnlId == 15000).ToList();
                                foreach (GlAccount gl in glc)
                                {
                                    if (ADD_NEW_BOOL == false)
                                    {
                                        gl.Amt2 += (Convert.ToDecimal(TxtPaidAmount.Text) - PrevPaidAmount);
                                    }
                                    else { gl.Amt2 += Convert.ToDecimal(TxtPaidAmount.Text); }
                                }
                                //cmpDBContext.GlAccounts.UpdateRange(glc);
                                cmpDBContext.SaveChanges();
                            }
                            else
                            {
                                if (glc.First().GnlCode == "DR")
                                {
                                    glBreak1.Amt1 = Convert.ToDecimal(TxtPaidAmount.Text);
                                    glBreak1.Amt2 = 0;
                                    List<GlAccount> glAccnt = cmpDBContext.GlAccounts.Where(m => m.GnlId == 15000).ToList();
                                    foreach (GlAccount gl in glAccnt)
                                    {
                                        if (ADD_NEW_BOOL == false)
                                        {
                                            gl.Amt1 += (Convert.ToDecimal(TxtPaidAmount.Text) - PrevPaidAmount);
                                        }
                                        else { gl.Amt1 += Convert.ToDecimal(TxtPaidAmount.Text); }
                                    }
                                    //cmpDBContext.GlAccounts.UpdateRange(glAccnt);
                                    cmpDBContext.SaveChanges();
                                }
                                else
                                {
                                    glBreak1.Amt1 = 0;
                                    glBreak1.Amt2 = Convert.ToDecimal(TxtPaidAmount.Text);
                                    List<GlAccount> glAccnt = cmpDBContext.GlAccounts.Where(m => m.GnlId == 15000).ToList();
                                    foreach (GlAccount gl in glAccnt)
                                    {
                                        if (ADD_NEW_BOOL == false)
                                        {
                                            gl.Amt2 += (Convert.ToDecimal(TxtPaidAmount.Text) - PrevPaidAmount);
                                        }
                                        else { gl.Amt2 += Convert.ToDecimal(TxtPaidAmount.Text); }
                                    }
                                    //cmpDBContext.GlAccounts.UpdateRange(glAccnt);
                                    cmpDBContext.SaveChanges();
                                }
                            }
                        }
                        cmpDBContext.GlBreaks.Add(glBreak1);
                        cmpDBContext.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddVendorDue()
        {
            try
            {
                decimal Amt1 = 0;
                decimal Amt2 = 0;
                var vend = cmpDBContext.Vendor.Where(m => m.VendorId == VendorId).Select(m => m);
                List<GlBreak> glBrk = cmpDBContext.GlBreaks.Where(m => m.GnlId == 32000 && m.IDNo == VendorId && m.Status == "Active" && (m.GlModule == "Purchase" || m.GlModule == "Journal")).ToList();
                foreach (GlBreak glb in glBrk)
                {
                    Amt1 += glb.Amt1;
                    Amt2 += glb.Amt2;
                }
                vend.First().TotalBal = (vend.First().OpBal + Amt2) - Amt1;
                //cmpDBContext.Vendor.UpdateRange(vend);
                cmpDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void IsGlbreakExist(string InvNo, string VendRef)
        {
            try
            {
                List<GlBreak> vbreak = cmpDBContext.GlBreaks.Where(m => m.TranRef == InvNo && m.LedAc == VendRef && m.GlModule == "Purchase" && m.TranType=="Invoice").ToList();
                if (vbreak.Count > 0)
                {
                    //foreach (var item in vbreak)
                    //{
                    //    if (item.TranType == "Payment")
                    //    {
                    //        int trnRef = Convert.ToInt32(item.TranRef);
                    //        List<PurchasePayment> vpymt = cmpDBContext.PurchasePayments.Where(m => m.PayNo == trnRef).ToList();
                    //        if (vpymt.Count > 0)
                    //        {
                    //            cmpDBContext.PurchasePayments.RemoveRange(vpymt);
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
        private bool SaveStock()
        {
            try
            {
                foreach (DataRow item in StockTranGridTbl.Rows)
                {
                    if ((bool)item["IsNewStock"] == true && ADD_NEW_BOOL)
                    {
                        NewStockId = 0;
                        var stocks = new Stocks()
                        {
                            StockName = item["StockName"].ToString(),
                            BrandName = item["Brand"].ToString(),
                            UnitMeasure = Convert.ToInt32(item["UnitMeas"]),
                            MinLevel = Convert.ToInt32(item["MinLevel"]),
                            MaxLevel = Convert.ToInt32(item["MaxLevel"]),
                            VendId = Convert.ToInt32(Convert.ToInt32(item["Vendor"])),
                            HSNCode = item["HSNCode"].ToString(),
                            Barcode = item["BarcodeNo"].ToString(),
                            CategoryId = Convert.ToInt32(Convert.ToInt32(item["Category"])),
                            StkType = Convert.ToInt32(Convert.ToInt32(item["InventoryType"])),
                            Status = true,
                        };
                        cmpDBContext.Stock.Add(stocks);
                        cmpDBContext.SaveChanges();
                        NewStockId = stocks.StockId;
                        item["StockId"] = stocks.StockId;                        
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private bool SaveBatch()
        {
            try
            {
                foreach (DataRow item in StockTranGridTbl.Rows)
                {
                    if (((bool)item["IsNewStock"] == true || (bool)item["IsNewBatch"] == true) && ADD_NEW_BOOL)
                    {
                        var bts = new Batch()
                        {
                            BatchName = item["Batch"].ToString(),
                            StockId = Convert.ToInt32(Convert.ToInt32(item["StockId"])),
                            VendId = Convert.ToInt32(Convert.ToInt32(item["Vendor"])),
                            Expiry = Convert.ToDateTime(item["ExpDate"]),
                            StockOnHand = Convert.ToDecimal(item["QtyIn"]),
                            StockIn = Convert.ToDecimal(item["QtyIn"]),
                            OpeningStock = 0,
                            PurchasePrice = Convert.ToDecimal(item["StkCost"]),
                            OPCost = 0,
                            Mrp = Convert.ToDecimal(item["MRP"]),
                            SellingPriceA = Convert.ToDecimal(item["SellingPriceA"]),
                            SellingPriceB = Convert.ToDecimal(item["SellingPriceB"]),
                            SellingPriceC = Convert.ToDecimal(item["SellingPriceC"]),
                            GST = Convert.ToDecimal(item["GST"]),
                            Cess = Convert.ToDecimal(item["Cess"]),
                            AddlCess = Convert.ToDecimal(item["AddlCess"]),
                            Barcode = item["BarcodeNo"].ToString(),
                            Status = true
                        };
                        cmpDBContext.Batch.Add(bts);
                        cmpDBContext.SaveChanges();
                        item["BatchId"] = bts.BatchId;
                        //return true;
                    }
                    else
                    {

                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        #endregion

        #region Only Numeric allowed in textbox
        private void txtGst_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, txtGst.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void txtCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, txtCess.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void txtAddlCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, txtAddlCess.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtMrp_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtMrp.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void txtPurchasePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, txtPurchasePrice.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void txtSellingPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, txtSellingPrice.Text))
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
        private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtQty.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtHandlingPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtHandlingPer.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void TxtHandlingAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!MdlMain.isNumber(e.KeyChar, TxtHandlingAmt.Text))
            //    e.Handled = true;
            var regex = new Regex(@"[^0-9.\b]");
            if (regex.IsMatch(e.KeyChar.ToString()))
            {
                e.Handled = true;
            }
        }
        private void ChkIsNewBatch_CheckStateChanged(object sender, EventArgs e)
        {
            //if (ChkIsNewBatch.Checked)
            //{
            //    BatchID = 0;
            //    txtBatch.Enabled = true;
            //}
            //else
            //{
            //    BatchID = OldBatchID;
            //    txtBatch.Enabled = false;
            //}
        }

        #endregion

        private void FrmPurchase_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStockTranTbl();
            CreateStockTbl();
            CreateBatchStocktable();
            CreateInvoiceSummaryTbl();
            CreateStockTranGridTbl();
            InitializingData();
            GrdProductDetails.Columns["gstper"].ReadOnly = true;
            MdlMain.gStkTranDS.Tables.Clear();
        }
        
    }
}
