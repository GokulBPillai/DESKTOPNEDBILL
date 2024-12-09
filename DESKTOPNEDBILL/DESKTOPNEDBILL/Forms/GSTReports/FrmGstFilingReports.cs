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

namespace DESKTOPNEDBILL.Forms.GSTReports
{
    public partial class FrmGstFilingReports : Form
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
        public FrmGstFilingReports()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnView.Width, BtnView.Height, 5, 5));

        }
        private void GetB2BView()
        {
            try
            {
                decimal Taxable = 0;
                decimal InvVal = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(01);
                toDt = DtpTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stcode in cmpDBContext.StateCodes on cust.StateCode equals stcode.StateCodeId
                               join stktr in cmpDBContext.StkTrans on salesInvMst.SalesInvNo equals stktr.InvNo
                               where salesInvMst.InvDate >= fromDt && salesInvMst.InvDate <= toDt
                               && salesInvMst.FinYear == MdlMain.gFinYear && stktr.TranType == "Sales" && stktr.TranRef != "Returns"
                               && cust.TaxType == "Registered"
                               select new
                               {
                                   salesInvMst.SalesInvNo,
                                   salesInvMst.InvDate,
                                   salesInvMst.Handling,
                                   salesInvMst.HandlingPer,
                                   salesInvMst.InvAmount,
                                   cust.CustomerName,
                                   cust.GSTNo,
                                   cust.TaxType,
                                   stcode.State,
                                   stktr.GST,
                                   stktr.StkRetail,
                                   stktr.QtyOut
                               } into x
                               group x by new { x.SalesInvNo, x.InvDate, x.Handling, x.HandlingPer, x.InvAmount, x.CustomerName, x.GSTNo, x.TaxType, x.State, x.GST } into grp
                               select new
                               {
                                   grp.Key.SalesInvNo,
                                   grp.Key.InvDate,
                                   grp.Key.Handling,
                                   grp.Key.HandlingPer,
                                   grp.Key.InvAmount,
                                   grp.Key.CustomerName,
                                   grp.Key.GSTNo,
                                   grp.Key.TaxType,
                                   grp.Key.State,
                                   grp.Key.GST,
                                   TaxableAmount = grp.Sum(x => x.QtyOut * x.StkRetail)
                               }).ToList();
                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        Taxable += inv.TaxableAmount;
                        InvVal += inv.InvAmount;
                    }
                    GrdB2B.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdB2B.AutoGenerateColumns = false;
                    GrdB2B.DataSource = bindingSource;

                    GrdB2BSummary.DataSource = null;
                    GrdB2BSummary.Rows.Clear();
                    int rowIndex = GrdB2BSummary.Rows.Add();
                    var row = GrdB2BSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count;
                    row.Cells[4].Value = InvVal;
                    row.Cells[8].Value = Taxable;
                }
                else
                {
                    GrdB2B.DataSource = null;
                    GrdB2BSummary.DataSource = null;
                    GrdB2BSummary.Rows.Clear();
                    //MessageBox.Show("No Records Found!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetB2CView()
        {
            try
            {
                decimal Taxable = 0;
                decimal cess = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(01);
                toDt = DtpTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join cust in cmpDBContext.Customers on salesInvMst.CustID equals cust.CustomerId
                               join stcode in cmpDBContext.StateCodes on cust.StateCode equals stcode.StateCodeId
                               join stktr in cmpDBContext.StkTrans on salesInvMst.SalesInvNo equals stktr.InvNo
                               where salesInvMst.InvDate >= fromDt && salesInvMst.InvDate <= toDt
                               && salesInvMst.FinYear == MdlMain.gFinYear && stktr.TranType == "Sales" && stktr.TranRef != "Returns"
                               && cust.TaxType == "UnRegistered"
                               select new
                               {
                                   Type = "OE",
                                   stcode.State,
                                   stktr.GST,
                                   salesInvMst.TotalCessAmount,
                                   stktr.StkRetail,
                                   stktr.QtyOut
                               } into x
                               group x by new { x.Type, x.State, x.GST, x.TotalCessAmount } into grp
                               select new
                               {
                                   grp.Key.Type,
                                   grp.Key.State,
                                   grp.Key.GST,
                                   grp.Key.TotalCessAmount,
                                   TaxableAmount = grp.Sum(x => x.QtyOut * x.StkRetail)
                               }).ToList();

                if (invList.Count != 0)
                {
                    foreach (var inv in invList)
                    {
                        Taxable += inv.TaxableAmount;
                        cess += inv.TotalCessAmount;
                    }
                    GrdB2C.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdB2C.AutoGenerateColumns = false;
                    GrdB2C.DataSource = bindingSource;

                    GrdB2CSummary.DataSource = null;
                    GrdB2CSummary.Rows.Clear();
                    int rowIndex = GrdB2CSummary.Rows.Add();
                    var row = GrdB2CSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count;
                    row.Cells[3].Value = Taxable;
                    row.Cells[4].Value = cess;

                }
                else
                {
                    GrdB2C.DataSource = null;
                    GrdB2CSummary.DataSource = null;
                    GrdB2CSummary.Rows.Clear();
                    //MessageBox.Show("No Records Found!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetHSNView()
        {
            try
            {
                decimal Taxable = 0;
                decimal Total = 0;
                decimal CGST = 0;
                decimal SGST = 0;
                decimal Cess = 0;
                decimal TotalCnt = 0;
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(01);
                toDt = DtpTo.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var invList = (from salesInvMst in cmpDBContext.SalesInvMasters
                               join stktr in cmpDBContext.StkTrans on salesInvMst.SalesInvNo equals stktr.InvNo
                               join stk in cmpDBContext.Stock on stktr.StocksId equals stk.StockId
                               join un in cmpDBContext.UnitMeasure on stktr.UnitMeasure equals un.Id
                               where salesInvMst.InvDate >= fromDt && salesInvMst.InvDate <= toDt
                               && salesInvMst.FinYear == MdlMain.gFinYear && stktr.TranType == "Sales" && stktr.TranRef != "Returns"
                               select new
                               {
                                   stktr.HSNCode,
                                   stk.StockName,
                                   un.UnitMeasureDescription,
                                   stktr.QtyOut,
                                   stktr.GrossTotal,
                                   stktr.NetTotal,
                                   stktr.TotalAmount,
                                   stktr.CessAmount,
                                   stktr.SGSTAmount,
                                   stktr.CGSTAmount
                               } into x
                               group x by new
                               {
                                   x.HSNCode,
                                   x.StockName,
                                   x.UnitMeasureDescription,
                                   //x.QtyOut,                                   
                                   //x.GrossTotal,
                                   //x.NetTotal,
                                   //x.TotalAmount,
                                   //x.CessAmount,
                                   //x.SGSTAmount,
                                   //x.CGSTAmount
                               } into grp
                               select new
                               {
                                   grp.Key.HSNCode,
                                   grp.Key.StockName,
                                   grp.Key.UnitMeasureDescription,
                                   QtyOut = grp.Sum(x => x.QtyOut),
                                   GrossTotal = grp.Sum(x => x.GrossTotal),
                                   NetTotal = grp.Sum(x => x.NetTotal),
                                   TotalAmount = grp.Sum(x => x.TotalAmount),
                                   CessAmount = grp.Sum(x => x.CessAmount),
                                   SGSTAmount = grp.Sum(x => x.SGSTAmount),
                                   CGSTAmount = grp.Sum(x => x.CGSTAmount)
                               }
                               ).ToList();

                if (invList.Count != 0)
                {
                    TotalCnt += invList.Count;
                    foreach (var inv in invList)
                    {
                        Taxable += inv.NetTotal;
                        CGST += inv.CGSTAmount;
                        SGST += inv.SGSTAmount;
                        Cess += inv.CessAmount;
                        Total += inv.TotalAmount;
                    }
                    GrdHSN.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = invList;
                    GrdHSN.AutoGenerateColumns = false;
                    GrdHSN.DataSource = bindingSource;

                    GrdHsnSummary.DataSource = null;
                    GrdHsnSummary.Rows.Clear();
                    int rowIndex = GrdHsnSummary.Rows.Add();
                    var row = GrdHsnSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Records: " + invList.Count;
                    row.Cells[4].Value = Total;
                    row.Cells[5].Value = Taxable;
                    row.Cells[6].Value = CGST;
                    row.Cells[7].Value = SGST;
                    row.Cells[8].Value = Cess;
                }
                else
                {
                    GrdHSN.DataSource = null;
                    GrdHsnSummary.DataSource = null;
                    GrdHsnSummary.Rows.Clear();
                    //MessageBox.Show("No Records Found!!!");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnView_Click(object sender, EventArgs e)
        {
            GetB2BView();
            GetB2CView();
            GetHSNView();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGstFilingReports_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void FrmGstFilingReports_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmGstFilingReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
