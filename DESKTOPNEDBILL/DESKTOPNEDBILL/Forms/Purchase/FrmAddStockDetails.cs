using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models.Entities;
using TableDims.Models;
using DESKTOPNEDBILL.Forms.Purchase;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmAddStockDetails : Form
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
        int EditstockID = 0;
        //int newStkId = 0;
        int EditbatchID = 0;
        int BatchID = 0;
        bool ADD_NEW_BOOL = true;
        bool IsNewStock = false;
        bool IsNewBatch = false;
        string ExistingBatch = "";
        CMPDBContext cmpDBContext = new CMPDBContext();
        DataTable StockTranTbl = new DataTable();
        private FrmPurchase frmPurchase;
        public FrmAddStockDetails(FrmPurchase frmPurchase)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnAdd.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAdd.Width, BtnAdd.Height, 5, 5));

            this.frmPurchase = frmPurchase;
        }

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
        #endregion

        #region Get & Set Methods
        //private bool SaveStock()
        //{
        //    try
        //    {
        //        if (FieldValidation())
        //        {
        //            if (EditstockID > 0)
        //            {
        //                List<Stocks> stks = cmpDBContext.Stock.Where(m => m.StockId == EditstockID).ToList();
        //                foreach (Stocks st in stks)
        //                {
        //                    st.StockName = TxtProductName.Text.Trim();
        //                    st.BrandName = TxtBrand.Text.Trim();
        //                    st.UnitMeasure = Convert.ToInt32(CmbUnitMeasure.SelectedValue);
        //                    st.MinLevel = TxtMinLevel.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMinLevel.Text);
        //                    st.MaxLevel = TxtMaxLevel.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMaxLevel.Text);
        //                    st.VendId = Convert.ToInt32(CmbVendor.SelectedValue);
        //                    st.HSNCode = TxtHSNCode.Text.Trim();
        //                    st.Barcode = TxtBarcode.Text.Trim();
        //                    //st.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
        //                    st.CategoryId = Convert.ToInt32(CmbCategory.SelectedValue);
        //                    st.StkType = Convert.ToInt32(CmbInventoryType.SelectedValue);
        //                }
        //                //cmpDBContext.Stock.UpdateRange(stks);
        //                cmpDBContext.SaveChanges();
        //                ADD_NEW_BOOL = false;
        //            }
        //            else
        //            {
        //                var stocks = new Stocks()
        //                {
        //                    StockName = TxtProductName.Text.Trim(),
        //                    BrandName = TxtBrand.Text.Trim(),
        //                    UnitMeasure = Convert.ToInt32(CmbUnitMeasure.SelectedValue),
        //                    MinLevel = TxtMinLevel.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMinLevel.Text),
        //                    MaxLevel = TxtMaxLevel.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMaxLevel.Text),
        //                    VendId = Convert.ToInt32(CmbVendor.SelectedValue),
        //                    HSNCode = TxtHSNCode.Text.Trim(),
        //                    Barcode = TxtBarcode.Text.Trim(),
        //                    CategoryId = Convert.ToInt32(CmbCategory.SelectedValue),
        //                    StkType = Convert.ToInt32(CmbInventoryType.SelectedValue),
        //                    Status = true,
        //                };
        //                cmpDBContext.Stock.Add(stocks);
        //                cmpDBContext.SaveChanges();
        //                newStkId = stocks.StockId;
        //                ADD_NEW_BOOL = true;
        //            }
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
        //}
        //private bool SaveBatch()
        //{
        //    try
        //    {
        //        if (FieldValidation())
        //        {
        //            if (EditstockID > 0 && EditbatchID > 0)
        //            {
        //                List<Batch> batch = cmpDBContext.Batch.Where(m => m.StockId == EditstockID && m.BatchId == EditbatchID).ToList();
        //                foreach (Batch bt in batch)
        //                {
        //                    bt.StockId = EditstockID;
        //                    bt.BatchName = TxtBatch.Text == "" ? "GEN" : TxtBatch.Text.Trim();
        //                    bt.VendId = Convert.ToInt32(CmbVendor.SelectedValue);
        //                    bt.Expiry = DtpExpiry.Value.Date;
        //                    bt.StockOnHand = TxtOp.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOp.Text);
        //                    bt.OpeningStock = TxtOp.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOp.Text);
        //                    bt.PurchasePrice = TxtPurchasePrice.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtPurchasePrice.Text);
        //                    bt.OPCost = TxtOpCost.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpCost.Text);
        //                    bt.Mrp = TxtMrp.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMrp.Text);
        //                    bt.SellingPriceA = TxtSellingPriceA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceA.Text);
        //                    bt.SellingPriceB = TxtSellingPriceB.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceB.Text);
        //                    bt.SellingPriceC = TxtSellingPriceB.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceC.Text);
        //                    bt.GST = TxtGST.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGST.Text);
        //                    bt.Cess = TxtCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtCess.Text);
        //                    bt.AddlCess = TxtAddlCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtAddlCess.Text);
        //                    bt.Barcode = TxtBarcode.Text.Trim();
        //                    bt.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
        //                }
        //                //cmpDBContext.Batch.UpdateRange(batch);
        //                cmpDBContext.SaveChanges();
        //                ADD_NEW_BOOL = false;
        //            }
        //            else
        //            {
        //                var bts = new Batch()
        //                {
        //                    BatchName = TxtBatch.Text == "" ? "GEN" : TxtBatch.Text.Trim(),
        //                    StockId = newStkId,
        //                    VendId = Convert.ToInt32(CmbVendor.SelectedValue),
        //                    Expiry = DtpExpiry.Value.Date,
        //                    StockOnHand = Convert.ToDecimal(TxtOp.Text),
        //                    OpeningStock = Convert.ToDecimal(TxtOp.Text),
        //                    PurchasePrice = TxtPurchasePrice.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtPurchasePrice.Text),
        //                    OPCost = TxtOpCost.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpCost.Text),
        //                    Mrp = TxtMrp.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtMrp.Text),
        //                    SellingPriceA = TxtSellingPriceA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceA.Text),
        //                    SellingPriceB = TxtSellingPriceB.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceB.Text),
        //                    SellingPriceC = TxtSellingPriceC.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSellingPriceC.Text),
        //                    GST = TxtGST.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGST.Text),
        //                    Cess = TxtCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtCess.Text),
        //                    AddlCess = TxtAddlCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtAddlCess.Text),
        //                    Barcode = TxtBarcode.Text.Trim(),
        //                    Status = true
        //                };
        //                cmpDBContext.Batch.Add(bts);
        //                cmpDBContext.SaveChanges();
        //                newStkId = 0;
        //                ADD_NEW_BOOL = true;
        //            }
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //        throw;
        //    }
        //}
        //private bool FieldValidation()
        //{
        //    if (TxtProductName.Text == "")
        //    {
        //        MessageBox.Show("Please enter product name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        TxtProductName.Focus();
        //        return false;
        //    }
        //    if (TxtSellingPriceA.Text == "")
        //    {
        //        TxtSellingPriceA.Text = "0";
        //        //MessageBox.Show("Please enter selling price", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //TxtSellingPriceA.Focus();
        //        //return false;
        //    }
        //    if (TxtSellingPriceB.Text == "")
        //    {
        //        TxtSellingPriceB.Text = TxtSellingPriceA.Text;
        //    }
        //    if (TxtSellingPriceC.Text == "")
        //    {
        //        TxtSellingPriceC.Text = TxtSellingPriceA.Text;
        //    }
        //    if (TxtOp.Text == "")
        //    {
        //        TxtOp.Text = "0";
        //        //MessageBox.Show("Please enter quantity of product", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //TxtOp.Focus();
        //        //return false;
        //    }
        //    if (ChkBatch.Checked)
        //    {
        //        if (!ValidatingNewBatchName(EditstockID, TxtBatch.Text.Trim()))
        //        {
        //            MessageBox.Show("This Batch Name is already existing for the same product. Separate batch cannot be created in existing Batch Name. Please change the Batch Name for creating new batch.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            TxtBatch.Focus();
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        //private bool ValidatingNewBatchName(int stkID, string batchname)
        //{
        //    try
        //    {
        //        List<Batch> bth = cmpDBContext.Batch.Where(m => m.StockId == stkID && m.BatchName == batchname).ToList();
        //        if (bth.Count() > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Stock";
            ChkBatch.Visible = false;
            EditstockID = 0;
            EditbatchID = 0;
            //newStkId = 0;
            CmbStatus.SelectedIndex = 0;
            ExistingBatch = "";
            GetUnitMeasures();
            GetVendorsList();
            GetInventoryTypeList();
            GetInventoryCategoryList();
            GetDepartmentList();
        }
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = String.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }
        private void GetUnitMeasures()
        {
            try
            {
                List<UnitMeasure> unitMeasures = cmpDBContext.UnitMeasure.ToList();
                if (unitMeasures.Count <= 0)
                {
                    MdlMain.AddInitialUnitMeasures();
                    List<UnitMeasure> unitMeas = cmpDBContext.UnitMeasure.ToList();
                    CmbUnitMeasure.DataSource = unitMeas;
                }
                else
                {
                    CmbUnitMeasure.DataSource = unitMeasures;

                }
                CmbUnitMeasure.DisplayMember = "UnitMeasureDescription";
                CmbUnitMeasure.ValueMember = "Id";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetVendorsList()
        {
            try
            {
                List<Vendor> vendor = cmpDBContext.Vendor.ToList();
                if (vendor.Count <= 0)
                {
                    MdlMain.AddInitialVendor();
                    List<Vendor> vend = cmpDBContext.Vendor.ToList();
                    CmbVendor.DataSource = vend;
                }
                else
                {
                    CmbVendor.DataSource = vendor;
                }
                CmbVendor.DisplayMember = "VendorName";
                CmbVendor.ValueMember = "VendorId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetInventoryTypeList()
        {
            try
            {
                List<InventoryType> type = cmpDBContext.InventoryTypes.ToList();
                if (type.Count > 0)
                {
                    CmbInventoryType.DataSource = type;
                }
                CmbInventoryType.DisplayMember = "TypeName";
                CmbInventoryType.ValueMember = "InventoryTypeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetInventoryCategoryList()
        {
            try
            {
                List<InventoryCategory> cat = cmpDBContext.InventoryCategories.ToList();
                if (cat.Count > 0)
                {
                    CmbCategory.DataSource = cat;
                }
                CmbCategory.DisplayMember = "CategoryName";
                CmbCategory.ValueMember = "InventoryCategoryId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetDepartmentList()
        {
            try
            {
                List<Department> dpt = cmpDBContext.Department.ToList();
                if (dpt.Count > 0)
                {
                    CmbDepartment.DataSource = dpt;
                }
                CmbDepartment.DisplayMember = "DepartmentName";
                CmbDepartment.ValueMember = "DepartmentId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetStockDetailsByStockId(int stockId, int batchId)
        {
            try
            {
                var stkList = (from stk in cmpDBContext.Stock
                               join bth in cmpDBContext.Batch on stk.StockId equals bth.StockId
                               where bth.StockId == stockId && bth.BatchId == batchId
                               select new
                               {
                                   stk.StockId,
                                   stk.StockName,
                                   stk.BrandName,
                                   bth.BatchName,
                                   stk.UnitMeasure,
                                   bth.Expiry,
                                   bth.GST,
                                   bth.PurchasePrice,
                                   bth.OPCost,
                                   bth.Mrp,
                                   bth.Cess,
                                   bth.AddlCess,
                                   bth.StockOnHand,
                                   bth.SellingPriceA,
                                   bth.SellingPriceB,
                                   bth.SellingPriceC,
                                   stk.HSNCode,
                                   stk.MaxLevel,
                                   stk.MinLevel,
                                   stk.Barcode,
                                   stk.VendId,
                                   stk.Status,
                                   bth.OpeningStock,
                                   stk.CategoryId,
                                   stk.StkType,
                               });

                if (stkList.Count() > 0)
                {
                    foreach (var item in stkList)
                    {
                        TxtProductName.Text = item.StockName;
                        TxtBrand.Text = item.BrandName;
                        TxtPurchasePrice.Text = item.PurchasePrice.ToString();
                        //TxtOpCost.Text = item.OPCost.ToString();
                        TxtSellingPriceA.Text = item.SellingPriceA.ToString();
                        TxtSellingPriceB.Text = item.SellingPriceB.ToString();
                        TxtSellingPriceC.Text = item.SellingPriceC.ToString();
                        TxtMrp.Text = item.Mrp.ToString();
                        TxtGST.Text = item.GST.ToString();
                        TxtCess.Text = item.Cess.ToString();
                        TxtAddlCess.Text = item.AddlCess.ToString();
                        TxtBatch.Text = item.BatchName;
                        ExistingBatch = item.BatchName;
                        //TxtOp.Text = item.OpeningStock.ToString();
                        TxtHSNCode.Text = item.HSNCode;
                        TxtMaxLevel.Text = item.MaxLevel.ToString();
                        TxtMinLevel.Text = item.MinLevel.ToString();
                        TxtBarcode.Text = item.Barcode;
                        CmbUnitMeasure.SelectedValue = Convert.ToInt32(item.UnitMeasure);
                        CmbVendor.SelectedValue = item.VendId;
                        CmbCategory.SelectedValue = item.CategoryId;
                        CmbInventoryType.SelectedValue = item.StkType;
                        CmbStatus.SelectedItem = "InActive";
                        DtpExpiry.Text = item.Expiry.ToString();
                        if (item.Status == true)
                        {
                            CmbStatus.SelectedItem = "Active";
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddDataToDatatable()
        {
            try
            {
                decimal purchaseprice = TxtPurchasePrice.Text != "" ? Convert.ToDecimal(TxtPurchasePrice.Text) : 0;
                decimal quantity = txtQty.Text != "" ? Convert.ToDecimal(txtQty.Text) : 0;
                decimal gst = TxtGST.Text != "" ? Convert.ToDecimal(TxtGST.Text) : 0;
                decimal cess = TxtCess.Text != "" ? Convert.ToDecimal(TxtCess.Text) : 0;
                decimal discper = txtDiscPer.Text != "" ? Convert.ToDecimal(txtDiscPer.Text) : 0;
                decimal discamt = txtDiscAmt.Text != "" ? Convert.ToDecimal(txtDiscAmt.Text) : 0;

                Dictionary<string, decimal> PurchaseGSTlist = MdlMain.PurchaseGSTCalculation(purchaseprice, quantity, gst, cess, discper, discamt);
                DataRow stkTrRow = StockTranTbl.NewRow();
                //stkTrRow["TranId"] = EditSTkTranId == "" ? Convert.ToInt32(0) : Convert.ToInt32(EditSTkTranId);
                //stkTrRow["TranDate"] = DtpTranDate.Value;
                stkTrRow["TranRef"] = "Invoice";
                stkTrRow["TranType"] = "Purchase";
                stkTrRow["StockId"] = EditstockID;
                stkTrRow["StockName"] = TxtProductName.Text;
                //stkTrRow["DptId"] = CmbDtpId.SelectedValue;// Need to change
                stkTrRow["MRP"] = TxtMrp.Text;
                stkTrRow["StkCost"] = TxtPurchasePrice.Text;
                stkTrRow["StkRetail"] = TxtSellingPriceA.Text != "" ? Convert.ToDecimal(TxtSellingPriceA.Text) : 0;
                stkTrRow["QtyIn"] = quantity;
                stkTrRow["QtyOut"] = 0;
                stkTrRow["ExpDate"] = DtpExpiry.Text;
                stkTrRow["Batch"] = TxtBatch.Text;
                stkTrRow["BatchId"] = EditbatchID;
                stkTrRow["GST"] = gst;
                stkTrRow["CGSTPercentage"] = gst / 2;
                stkTrRow["SGSTPercentage"] = gst / 2;
                stkTrRow["Discount"] = discper;
                stkTrRow["DiscountPer"] = discamt;
                stkTrRow["StkLocation"] = 2001;// Need to change
                stkTrRow["DiscountType"] = "% per piece";// Need to change
                stkTrRow["HSNCode"] = TxtHSNCode.Text.Trim();
                stkTrRow["TotalDiscount"] = Convert.ToDecimal(PurchaseGSTlist["TotalDiscountAmount"]).ToString();
                stkTrRow["GSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalGSTAmount"]).ToString();
                stkTrRow["CessAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalCessAmount"]).ToString();
                stkTrRow["CGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalCGSTAmount"]).ToString();
                stkTrRow["SGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalSGSTAmount"]).ToString();
                stkTrRow["IGSTAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalIGSTAmount"]).ToString();
                stkTrRow["TotalAmount"] = Convert.ToDecimal(PurchaseGSTlist["TotalAmount"]).ToString();
                stkTrRow["SubTotal"] = Convert.ToDecimal(PurchaseGSTlist["SubTotalAmount"]).ToString();
                stkTrRow["Total"] = Convert.ToDecimal(PurchaseGSTlist["TotalAmount"]).ToString();
                stkTrRow["Cess"] = cess;
                stkTrRow["AddlCess"] = TxtAddlCess.Text != "" ? Convert.ToDecimal(TxtAddlCess.Text) : 0;
                stkTrRow["FinYear"] = MdlMain.gFinYear;
                stkTrRow["CompanyCode"] = MdlMain.gCompCode;
                stkTrRow["UnitMeas"] = Convert.ToInt32(CmbUnitMeasure.SelectedValue);
                stkTrRow["SellingPriceA"] = TxtSellingPriceA.Text != "" ? Convert.ToDecimal(TxtSellingPriceA.Text) : 0;
                stkTrRow["SellingPriceB"] = TxtSellingPriceB.Text != "" ? Convert.ToDecimal(TxtSellingPriceB.Text) : stkTrRow["SellingPriceA"];
                stkTrRow["SellingPriceC"] = TxtSellingPriceC.Text != "" ? Convert.ToDecimal(TxtSellingPriceC.Text) : stkTrRow["SellingPriceA"];
                stkTrRow["BarcodeNo"] = TxtBarcode.Text.Trim();
                stkTrRow["Brand"] = TxtBrand.Text.Trim();
                stkTrRow["MaxLevel"] = TxtMaxLevel.Text != "" ? Convert.ToDecimal(TxtMaxLevel.Text) : 0;
                stkTrRow["MinLevel"] = TxtMinLevel.Text != "" ? Convert.ToDecimal(TxtMinLevel.Text) : 0;
                stkTrRow["Vendor"] = Convert.ToInt32(CmbVendor.SelectedValue);
                stkTrRow["Category"] = Convert.ToInt32(CmbCategory.SelectedValue);
                stkTrRow["InventoryType"] = Convert.ToInt32(CmbInventoryType.SelectedValue);
                stkTrRow["Status"] = true;
                stkTrRow["IsNewStock"] = IsNewStock;
                stkTrRow["IsNewBatch"] = IsNewBatch;
                if (IsNewBatch)
                {
                    stkTrRow["BatchId"] = 0;
                }
                StockTranTbl.Rows.Add(stkTrRow);
                if (StockTranTbl.Rows.Count > 1)
                {
                    DataRow[] FndTrRow = StockTranTbl.Select();
                    StockTranTbl.Rows.Remove(FndTrRow[0]);
                }
                MdlMain.gStkTranDS.Tables.Clear();
                MdlMain.gStkTranDS.Tables.Add(StockTranTbl);
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetStockDetailsFromDatatable()
        {
            try
            {
                if (StockTranTbl.Rows.Count > 0)
                {
                    foreach (DataRow item in StockTranTbl.Rows)
                    {
                        EditstockID = Convert.ToInt32(item["StockId"]);
                        EditbatchID = Convert.ToInt32(item["BatchId"]);
                        TxtProductName.Text = item["StockName"].ToString();
                        TxtBrand.Text = item["Brand"].ToString();
                        CmbCategory.SelectedValue = Convert.ToInt32(item["Category"]);
                        CmbInventoryType.SelectedValue = Convert.ToInt32(item["InventoryType"]);
                        TxtHSNCode.Text = item["HSNCode"].ToString();
                        CmbUnitMeasure.SelectedValue = Convert.ToInt32(item["UnitMeas"]);
                        TxtMaxLevel.Text = item["MaxLevel"].ToString();
                        TxtMinLevel.Text = item["MinLevel"].ToString();
                        CmbVendor.SelectedValue = Convert.ToInt32(item["Vendor"]);                        
                        CmbStatus.SelectedItem = "Active";
                        TxtBarcode.Text = item["BarcodeNo"].ToString();
                        TxtBatch.Text = item["Batch"].ToString();
                        DtpExpiry.Text = item["ExpDate"].ToString();
                        txtQty.Text = item["QtyIn"].ToString();
                        TxtPurchasePrice.Text = item["StkCost"].ToString();
                        TxtMrp.Text = item["MRP"].ToString();
                        TxtSellingPriceA.Text = item["SellingPriceA"].ToString();
                        TxtSellingPriceB.Text = item["SellingPriceB"].ToString();
                        TxtSellingPriceC.Text = item["SellingPriceC"].ToString();
                        TxtGST.Text = item["GST"].ToString();
                        txtGSTAmt.Text = item["GSTAmount"].ToString();
                        TxtCess.Text = item["Cess"].ToString();
                        TxtAddlCess.Text = item["AddlCess"].ToString();
                        txtTotal.Text= item["Total"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ChangeTotalOnOtherValueChanged()
        {
            try
            {
                //GetDiscountTypeAmount();
                decimal Cost = TxtPurchasePrice.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtPurchasePrice.Text);
                decimal QuantityIn = txtQty.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtQty.Text);
                decimal GST = TxtGST.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtGST.Text);
                decimal Cess = TxtCess.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtCess.Text);
                decimal DiscountPer = txtDiscPer.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtDiscPer.Text);
                decimal DiscountAmt = txtDiscAmt.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(txtDiscAmt.Text);
                Dictionary<string, decimal> PurchaseGSTlist = MdlMain.PurchaseGSTCalculation(Cost, QuantityIn, GST, Cess, DiscountPer, DiscountAmt);
                txtTotal.Text = Convert.ToDecimal(PurchaseGSTlist["TotalAmount"]).ToString();
                txtGSTAmt.Text= Convert.ToDecimal(PurchaseGSTlist["TotalGSTAmount"]).ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event Handling Methods
        private void LblClose_Click(object sender, EventArgs e)
        {
            btnClose_Click(sender, e);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            AddDataToDatatable();           
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            MdlMain.gStkTranDS.Tables.Clear();
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void TxtGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtGST.Text))
                e.Handled = true;
        }
        private void TxtCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtCess.Text))
                e.Handled = true;
        }
        private void TxtAddlCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtAddlCess.Text))
                e.Handled = true;
        }
        private void TxtHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtHSNCode.Text))
                e.Handled = true;
        }
        private void TxtPurchasePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPurchasePrice.Text))
                e.Handled = true;
        }
        private void TxtMrp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtMrp.Text))
                e.Handled = true;
        }
        private void TxtSellingPriceA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtSellingPriceA.Text))
                e.Handled = true;
        }
        private void TxtSellingPriceB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtSellingPriceB.Text))
                e.Handled = true;
        }
        private void TxtSellingPriceC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtSellingPriceC.Text))
                e.Handled = true;
        }
        private void TxtMinLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtMinLevel.Text))
                e.Handled = true;
        }
        private void TxtMaxLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtMaxLevel.Text))
                e.Handled = true;
        }
        private void TxtSuggOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtSuggOrder.Text))
                e.Handled = true;
        }
        private void TxtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBrand.Focus();
            }
        }
        private void TxtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBarcode.Focus();
            }
        }
        private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBatch.Focus();
            }
        }
        private void TxtBatch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DtpExpiry.Focus();
            }
        }
        private void DtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbDepartment.Focus();
            }
        }
        private void CmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbInventoryType.Focus();
            }
        }
        private void CmbInventoryType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbCategory.Focus();
            }
        }
        private void CmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtGST.Focus();
            }
        }
        private void TxtGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtCess.Focus();
            }
        }
        private void TxtCess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddlCess.Focus();
            }
        }
        private void TxtAddlCess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtHSNCode.Focus();
            }
        }
        private void TxtHSNCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbUnitMeasure.Focus();
            }
        }
        private void CmbUnitMeasure_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TxtOp.Focus();
            }
        }
        private void TxtOp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TxtOpCost.Focus();
            }
        }
        private void TxtOpCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPurchasePrice.Focus();
            }
        }
        private void TxtPurchasePrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtMrp.Focus();
            }
        }
        private void TxtMrp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSellingPriceA.Focus();
            }
        }
        private void TxtSellingPriceA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSellingPriceB.Focus();
            }
        }
        private void TxtSellingPriceB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSellingPriceC.Focus();
            }
        }
        private void TxtSellingPriceC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbVendor.Focus();
            }
        }
        private void CmbVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtMinLevel.Focus();
            }
        }
        private void TxtMinLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtMaxLevel.Focus();
            }
        }
        private void TxtMaxLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSuggOrder.Focus();
            }
        }
        private void TxtSuggOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbIsWarranty.Focus();
            }
        }
        private void CmbIsWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbStatus.Focus();
            }
        }
        private void CmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnAdd.Focus();
            }
        }
        private void FrmAddEditStock_KeyDown(object sender, KeyEventArgs e)
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
        private void txtDiscPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, txtDiscPer.Text))
                e.Handled = true;
        }
        private void txtDiscAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, txtDiscAmt.Text))
                e.Handled = true;
        }
        private void TxtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }
        private void TxtGST_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }
        private void TxtCess_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }
        private void txtDiscPer_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }
        private void txtDiscAmt_TextChanged(object sender, EventArgs e)
        {
            ChangeTotalOnOtherValueChanged();
        }

        #endregion

        #region Set Uppercase for first letter in textbox
        private void TxtProductName_TextChanged(object sender, EventArgs e)
        {
            if ((TxtProductName.Text.Length) == 1)
            {
                TxtProductName.Text = TxtProductName.Text[0].ToString().ToUpper();
                TxtProductName.Select(2, 1);
            }
        }
        private void TxtBrand_TextChanged(object sender, EventArgs e)
        {
            if ((TxtBrand.Text.Length) == 1)
            {
                TxtBrand.Text = TxtBrand.Text[0].ToString().ToUpper();
                TxtBrand.Select(2, 1);
            }
        }
        #endregion

        private void FrmAddEditStock_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStockTranTbl();
            InitializingData();
            if (MdlMain.gStkTranDS.Tables.Count > 0)
            {
                StockTranTbl = MdlMain.gStkTranDS.Tables[0];
                GetStockDetailsFromDatatable();
                LblHeader.Text = "Edit existing stock for new purchase";
                IsNewStock = false;
                ChkBatch.Visible = true;
            }
            else
            {
                LblHeader.Text = "Add new stock for purchase";
                IsNewStock = true;
                TxtBatch.ReadOnly = false;
                ChkBatch.Visible = false;
            }
        }
        private void ChkBatch_CheckStateChanged(object sender, EventArgs e)
        {
            if (ChkBatch.Checked)
            {
                BatchID = 0;
                TxtBatch.ReadOnly = false;
                IsNewBatch = true;
            }
            else
            {
                BatchID = EditbatchID;
                TxtBatch.ReadOnly = true;
                IsNewBatch = false;
            }
        }
    }
}
