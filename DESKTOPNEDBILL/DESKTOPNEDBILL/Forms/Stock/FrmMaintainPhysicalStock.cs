using DESKTOPNEDBILL.Forms.Sales;
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
using DESKTOPNEDBILL.Forms.Main;
using System.Runtime.InteropServices;

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmMaintainPhysicalStock : Form
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
        DataTable PyAdjTbl = new DataTable();
        int StockID = 0;
        int BatchID = 0;
        public static string sendproducttext = "";
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmMaintainPhysicalStock()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSave.Width, btnSave.Height, 5, 5));
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));

        }
        #region Event Handling Methods
        private void FrmMaintainPhysicalStock_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreatePhysAdjTable();
        }
        private void TxtPhy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtDiff.Text = (Convert.ToDecimal(TxtPhy.Text) - Convert.ToDecimal(TxtOnHand.Text)).ToString();
                TxtDiff.Focus();
            }
        }
        private void TxtDiff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Convert.ToDecimal(TxtDiff.Text) != 0 && StockID > 0)
                {
                    AddStkDetToDataTbl();
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = PyAdjTbl;
                    GrdProductDetails.AutoGenerateColumns = false;
                    GrdProductDetails.DataSource = bindingSource;
                    ClearTextBoxes(this.Controls);
                    TxtProductName.Focus();
                }
            }
        }
        private void TxtProductName_Click(object sender, EventArgs e)
        {
            GetProductDetails();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveStkTran())
            {
                InitializingData();
                MessageBox.Show("Physical stock adjusted successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void TxtPhy_TextChanged(object sender, EventArgs e)
        {
            TxtDiff.Text = TxtPhy.Text != "" ? (Convert.ToDecimal(TxtPhy.Text) - Convert.ToDecimal(TxtOnHand.Text)).ToString() : "0";
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void TxtPhy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPhy.Text))
                e.Handled = true;
        }
        #endregion

        #region Get & Post Methods
        private void CreatePhysAdjTable()
        {
            PyAdjTbl.Columns.Add("StockId", typeof(int));
            PyAdjTbl.Columns.Add("StockName", typeof(string));
            PyAdjTbl.Columns.Add("Batch", typeof(string));
            PyAdjTbl.Columns.Add("BatchId", typeof(int));
            PyAdjTbl.Columns.Add("QtyIn", typeof(double));
            PyAdjTbl.Columns.Add("QtyOut", typeof(double));
            PyAdjTbl.Columns.Add("QtyDiff", typeof(double));
        }
        private void AddStkDetToDataTbl()
        {
            try
            {
                DataRow PhysRow = PyAdjTbl.NewRow();
                PhysRow["StockId"] = StockID;
                PhysRow["StockName"] = TxtProductName.Text.Trim();
                PhysRow["Batch"] = TxtBatch.Text.Trim();
                PhysRow["BatchId"] = BatchID;
                if (Convert.ToDecimal(TxtDiff.Text) > 0)
                {
                    PhysRow["QtyIn"] = Convert.ToDecimal(TxtDiff.Text);
                    PhysRow["QtyOut"] = 0;
                }
                else
                {
                    PhysRow["QtyIn"] = 0;
                    PhysRow["QtyOut"] = Convert.ToDecimal(TxtDiff.Text);
                }
                PhysRow["QtyDiff"] = Convert.ToDecimal(TxtDiff.Text);
                PyAdjTbl.Rows.Add(PhysRow);
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
                FrmProductSelectListSales frmproductList = new FrmProductSelectListSales();
                frmproductList.ShowDialog();
                if (MdlMain.gStockId > 0 && MdlMain.gBatchId > 0)
                {
                    SetStockDeatils(MdlMain.gStockId, MdlMain.gBatchId);
                    TxtPhy.Focus();
                    sendproducttext = "";
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
                var stkList = (from stk in cmpDBContext.Stock
                               join bth in cmpDBContext.Batch on stk.StockId equals bth.StockId
                               where stk.Status == true && bth.StockId == stockId && bth.BatchId == batchId
                               select new
                               {
                                   stk.StockId,
                                   stk.StockName,
                                   bth.BatchId,
                                   bth.BatchName,
                                   bth.StockOnHand,
                               });
                foreach (var stklst in stkList)
                {
                    StockID = stklst.StockId;
                    BatchID = stklst.BatchId;
                    TxtProductName.Text = stklst.StockName;
                    TxtBatch.Text = stklst.BatchName;
                    TxtOnHand.Text = stklst.StockOnHand.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private int SavePhysicalAdjust()
        {
            try
            {
                PhysicalAdjust phys = new PhysicalAdjust()
                {
                    Physdate = DateTime.Now.Date,
                    UserId = Convert.ToInt32(MdlMain.gUserID),
                    Narration = "",
                    Status = true
                };
                cmpDBContext.PhysicalAdjusts.Add(phys);
                cmpDBContext.SaveChanges();
                return phys.TranId;
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
                if (FieldValidation())
                {
                    DataRow[] FndTrRow = PyAdjTbl.Select();
                    foreach (DataRow item in PyAdjTbl.Rows)
                    {
                        var stkTran = new StkTran()
                        {
                            InvNo = SavePhysicalAdjust(),
                            TranDate = DateTime.Now.Date,
                            TranRef = "PhysAdj",
                            TranType = "PhysAdj",
                            StocksId = Convert.ToInt32(item["StockId"]),
                            DptId = 2001,
                            StkRetail = 0,
                            QtyIn = Convert.ToDecimal(item["QtyIn"]),
                            QtyOut = Convert.ToDecimal(item["QtyOut"]),
                            BatchId = Convert.ToInt32(item["BatchId"]),
                            GST = 0,
                            Discount = 0,
                            StkLocation = "0",
                            DiscountType = "",
                            HSNCode = "",
                            GSTAmount = 0,
                            CessAmount = 0,
                            CGSTAmount = 0,
                            SGSTAmount = 0,
                            IGSTAmount = 0,
                            AddlCess = 0,
                            TotalAmount = 0,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = MdlMain.gCompCode,
                        };
                        cmpDBContext.StkTrans.Add(stkTran);
                        cmpDBContext.SaveChanges();
                    }
                    return true;
                }
                return false;
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
                if (StockID <= 0 && TxtProductName.Text == "")
                {
                    MessageBox.Show("Please select Stock", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtProductName.Focus();
                    return false;
                }
                if (PyAdjTbl.Rows.Count <= 0)
                {
                    MessageBox.Show("Product is not added in table for saving Physical Adjust", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtProductName.Focus();
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            GrdProductDetails.DataSource = null;
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

        #endregion

        private void FrmMaintainPhysicalStock_Load_1(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
        }

        private void FrmMaintainPhysicalStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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

        private void FrmMaintainPhysicalStock_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
