using DESKTOPNEDBILL.Forms.Banks;
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

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesSettlement : Form
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
        int BankGnlId = 0;
        int BankID = 0;
        int CustomerId = 0;
        int RcptNo = 0;
        bool ADD_NEW_BOOL = true;
        DataTable StatementTbl = new DataTable();
        public FrmSalesSettlement()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            btnCustomer.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnCustomer.Width, btnCustomer.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

        }
        private void FrmSalesSettlement_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStatementTbl();
            InitializingData();
        }

        #region Event handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtBank_Click(object sender, EventArgs e)
        {
            GetBankDetails();
        }
        private void TxtCustomerName_Click(object sender, EventArgs e)
        {
            FrmCustomerSelectList frmcustomerList = new FrmCustomerSelectList();
            frmcustomerList.ShowDialog();
            SetCustomerDetails(MdlMain.gCustomerId);
        }
        private void TxtCurrReceipt_TextChanged(object sender, EventArgs e)
        {
            decimal ytdBal = TxtYTDBalance.Text != "" ? Convert.ToDecimal(TxtYTDBalance.Text) : 0;
            if (ytdBal > 0)
            {
                TxtRevBalance.Text = (ytdBal - Convert.ToDecimal(TxtCurrReceipt.Text)).ToString();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation())
            {
                AddReceipt();
                AddGlBreak();
                AddCustomerDue();
                InitializingData();
                SetCustomerDetails(MdlMain.gCustomerId);
                MessageBox.Show("Receipt saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Set & Get Methods
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
                var bnkdtls = (from bnk in cmpDBContext.Bank
                               where bnk.BankId == BankId && bnk.Status == true
                               select new
                               {
                                   bnk.BankId,
                                   bnk.BankName,
                                   bnk.GnlId,
                               });
                if (bnkdtls.Count() > 0)
                {
                    TxtBank.Text = bnkdtls.First().BankName;
                    BankGnlId = bnkdtls.First().GnlId;
                    BankID = bnkdtls.First().BankId;
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
                if (custList.Count() > 0)
                {
                    foreach (var cus in custList)
                    {
                        CustomerId = CustID;
                        TxtCustomerName.Text = cus.CustomerName;
                        TxtAddress.Text = cus.Address1 + ", " + cus.Address2;
                    }
                }                
                decimal balance = Convert.ToDecimal(MdlMain.GetCustomerBalance(CustomerId));
                //decimal balance = MdlMain.GetCustomerBalance(CustomerId);
                TxtYTDBalance.Text = balance.ToString("0.00");
                TxtRevBalance.Text = balance.ToString("0.00");

                GetCustomerStatement(CustomerId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddReceipt()
        {
            try
            {
                IsGlbreakExist(TxtBillNo.Text.Trim());
                if (Convert.ToDecimal(TxtCurrReceipt.Text) > 0)
                {
                    if (RcptNo > 0)
                    {
                        List<SalesReceipt> salesRcpt = cmpDBContext.SalesReceipt.Where(m => m.RcpNo == RcptNo).ToList();
                        foreach (SalesReceipt slrpt in salesRcpt)
                        {
                            slrpt.TranDate = DtpRcptDate.Value;
                            slrpt.FinYear = MdlMain.gFinYear;
                            slrpt.CompanyCode = "";
                            slrpt.CustomerId = CustomerId;
                            slrpt.RcpAmount = Convert.ToDecimal(TxtCurrReceipt.Text);
                            slrpt.GnlId = BankGnlId;
                        }
                        //cmpDBContext.SalesReceipt.UpdateRange(salesRcpt);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        var saleRcpt = new SalesReceipt()
                        {
                            TranDate = DtpRcptDate.Value,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = "",
                            CustomerId = CustomerId,
                            RcpAmount = Convert.ToDecimal(TxtCurrReceipt.Text),
                            GnlId = BankGnlId
                        };
                        cmpDBContext.SalesReceipt.Add(saleRcpt);
                        cmpDBContext.SaveChanges();
                        RcptNo = saleRcpt.RcpNo;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void AddGlBreak()
        {
            try
            {
                var glBreak = new GlBreak()
                {
                    TranDate = DtpRcptDate.Value,
                    FinYear = MdlMain.gFinYear,
                    TranType = "Receipt",
                    GlModule = "Sales",
                    TranRef = RcptNo.ToString(),
                    IDNo = CustomerId,
                    DptId = 1,
                    Status = "Active",
                    DEnd = "No",
                    LedAc = CustomerId.ToString(),
                    AmtMode = TxtBank.Text.Trim() + " " + TxtComment.Text.Trim(),
                    GnlId = 12000,//---------------------------------------Accounts Payable Head
                    Amt1 = 0,
                    Amt2 = Convert.ToDecimal(TxtCurrReceipt.Text),
                };
                cmpDBContext.GlBreaks.Add(glBreak);
                cmpDBContext.SaveChanges();

                var glc = cmpDBContext.GlAccounts.Where(m => m.GnlId == BankGnlId).Select(m => m);
                var glBreak1 = new GlBreak();
                glBreak1.TranDate = DtpRcptDate.Value;
                glBreak1.FinYear = MdlMain.gFinYear;
                glBreak1.TranType = "Receipt";
                glBreak1.GlModule = "Sales";
                glBreak1.TranRef = RcptNo.ToString();
                glBreak1.IDNo = CustomerId;
                glBreak1.DptId = 1;
                glBreak1.Status = "Active";
                glBreak1.DEnd = "No";
                glBreak1.LedAc = CustomerId.ToString();//TxtBillNo.Text.Trim();
                glBreak1.AmtMode = TxtBank.Text.Trim();
                glBreak1.GnlId = BankGnlId;//---------------------------------------Accounts Payable Head
                if (glc.First().GnlCode == "DR")
                {
                    glBreak1.Amt1 = 0;
                    glBreak1.Amt2 = Convert.ToDecimal(TxtCurrReceipt.Text);
                }
                else
                {
                    glBreak1.Amt1 = Convert.ToDecimal(TxtCurrReceipt.Text);
                    glBreak1.Amt2 = 0;
                }
                cmpDBContext.GlBreaks.Add(glBreak1);
                cmpDBContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void IsGlbreakExist(string RcpNo)
        {
            try
            {
                List<GlBreak> vbreak = cmpDBContext.GlBreaks.Where(m => m.TranRef == RcpNo && m.GlModule == "Sales" && m.TranType == "Receipt").ToList();
                if (vbreak.Count > 0)
                {
                    cmpDBContext.GlBreaks.RemoveRange(vbreak);
                    cmpDBContext.SaveChanges();
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
                var cust = cmpDBContext.Customers.Where(m => m.CustomerId == CustomerId).Select(m => m);
                List<GlBreak> glBrk = cmpDBContext.GlBreaks.Where(m => m.IDNo == CustomerId && m.Status == "Active" && (m.GnlId == 35000 || m.GnlId == 12000) && (m.GlModule == "Sales" || m.GlModule == "Journal")).ToList();
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
        private void CreateStatementTbl()
        {
            StatementTbl.Columns.Add("Date", typeof(DateTime));
            StatementTbl.Columns.Add("Ref", typeof(string));
            StatementTbl.Columns.Add("Debit", typeof(decimal));
            StatementTbl.Columns.Add("Credit", typeof(decimal));
            StatementTbl.Columns.Add("Balance", typeof(decimal));
            StatementTbl.Columns.Add("LinkNo", typeof(int));
        }
        private void GetCustomerStatement(int custId)
        {
            try
            {
                decimal Dbt = 0;
                decimal Crdt = 0;
                StatementTbl.Clear();
                decimal CurrOpBal = GetCurrentCustOpBalTodate(custId, MdlMain.gFYStart);
                var glBrList = (from glBr in cmpDBContext.GlBreaks
                                where glBr.Status == "Active" && glBr.IDNo == custId && (glBr.GnlId == 12000 || glBr.GnlId == 35000)
                                select new
                                {
                                    glBr.GnlId,
                                    glBr.TranDate,
                                    glBr.TranType,
                                    glBr.GlModule,
                                    glBr.TranRef,
                                    glBr.IDNo,
                                    glBr.DptId,
                                    glBr.Amt1,
                                    glBr.Amt2,
                                    glBr.AmtMode,
                                    glBr.Status,
                                    glBr.DEnd,
                                    glBr.LedAc
                                });
                DataRow statFRow = StatementTbl.NewRow();
                statFRow["Date"] = MdlMain.gFYStart;
                statFRow["Ref"] = "Opening Balance";
                if (CurrOpBal < 0)
                {
                    statFRow["Credit"] = CurrOpBal;
                    statFRow["Debit"] = 0;
                    Crdt = Crdt + CurrOpBal;
                }
                else
                {
                    statFRow["Credit"] = 0;
                    statFRow["Debit"] = CurrOpBal;
                    Dbt = Dbt + CurrOpBal;
                }
                statFRow["Balance"] = CurrOpBal;
                statFRow["LinkNo"] = 1;
                StatementTbl.Rows.Add(statFRow);

                foreach (var glBr in glBrList)
                {
                    DataRow statRow = StatementTbl.NewRow();
                    if (glBr.GlModule == "Journal")
                    {
                        statRow["Date"] = glBr.TranDate;
                        statRow["Ref"] = "JNL" + glBr.TranRef;
                        statRow["LinkNo"] = 1;
                    }
                    else
                    {
                        statRow["Date"] = glBr.TranDate;
                        if (MdlMain.SerilNoStat == "Yes")
                        {
                            statRow["Ref"] = glBr.TranType + " - " + glBr.AmtMode;
                            if (glBr.TranType == "Invoice")
                            {
                                statRow["Ref"] = "INV-" + glBr.AmtMode;
                            }
                        }
                        else
                        {
                            statRow["Ref"] = glBr.TranType + " - " + glBr.TranRef;
                            if (glBr.TranType == "Invoice")
                            {
                                statRow["Ref"] = "INV" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "Advance")
                            {
                                statRow["Ref"] = "ADV" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "Receipt")
                            {
                                statRow["Ref"] = "RCP" + " - " + glBr.TranRef + " - " + glBr.AmtMode;
                            }
                            if (glBr.TranType == "Returns")
                            {
                                statRow["Ref"] = "RTN" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "Debit")
                            {
                                statRow["Ref"] = "DRN" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "Credit")
                            {
                                statRow["Ref"] = "CRN" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "CrNote")
                            {
                                statRow["Ref"] = "CRN" + " - " + glBr.TranRef;
                            }
                            if (glBr.TranType == "DrNote")
                            {
                                statRow["Ref"] = "DRN" + " - " + glBr.TranRef;
                            }
                        }
                        statRow["LinkNo"] = 1;
                    }
                    if (glBr.GlModule == "Sales" || glBr.GlModule == "Journal")
                    {
                        if ((glBr.TranType == "Invoice") || (glBr.TranType == "Refund") || (glBr.TranType == "Credit") || (glBr.TranType == "CrNote") || (glBr.TranType == "DrNote") || (glBr.TranType == "Debit") || (glBr.TranType == "AdvAdj"))
                        {
                            if (glBr.GnlId == 12000)
                            {
                                if (glBr.GlModule == "Journal")
                                {
                                    if (glBr.TranType == "Credit")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal -= glBr.Amt2;
                                        statRow["Ref"] = "RCP-" + glBr.TranRef;
                                    }
                                    else
                                    {
                                        statRow["Debit"] = glBr.Amt1;
                                        statRow["Credit"] = 0;
                                        CurrOpBal += glBr.Amt1;
                                        statRow["Ref"] = "RFD-" + glBr.TranRef;
                                    }
                                }
                                else
                                {
                                    if (glBr.TranType == "AdvAdj")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal -= glBr.Amt2;
                                        statRow["Ref"] = "ADV-" + glBr.TranRef;
                                    }
                                    else if (glBr.TranType == "CrNote")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal -= glBr.Amt2;
                                    }
                                    else if (glBr.TranType == "DrNote")
                                    {
                                        statRow["Credit"] = 0;
                                        statRow["Debit"] = glBr.Amt1;
                                        CurrOpBal += glBr.Amt1;
                                    }
                                    else
                                    {
                                        statRow["Credit"] = 0;
                                        statRow["Debit"] = glBr.Amt1;
                                        CurrOpBal += glBr.Amt1;
                                    }
                                }
                                statRow["Balance"] = CurrOpBal;
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                StatementTbl.Rows.Add(statRow);
                            }
                            else if (glBr.GnlId == 35000)
                            {
                                statRow["Debit"] = glBr.Amt1;
                                statRow["Credit"] = 0;
                                CurrOpBal += glBr.Amt1;
                                statRow["Balance"] = CurrOpBal;
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                StatementTbl.Rows.Add(statRow);
                            }
                        }
                        if (glBr.GlModule != "Journal")
                        {
                            if ((glBr.TranType == "Advance") || (glBr.TranType == "Receipt") || (glBr.TranType == "Returns") || (glBr.TranType == "Debit"))
                            {
                                if (glBr.GnlId == 12000)
                                {
                                    statRow["Credit"] = glBr.Amt2;
                                    statRow["Debit"] = 0;
                                    CurrOpBal -= glBr.Amt2;
                                    Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    statRow["Balance"] = CurrOpBal;
                                    StatementTbl.Rows.Add(statRow);
                                }
                                else if (glBr.GnlId == 35000)
                                {
                                    statRow["Credit"] = glBr.Amt2;
                                    statRow["Debit"] = 0;
                                    if (CurrOpBal < 0)
                                    {
                                        CurrOpBal = CurrOpBal + glBr.Amt2;
                                    }
                                    else
                                    {
                                        CurrOpBal = CurrOpBal - glBr.Amt2;
                                    }
                                    Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    statRow["Balance"] = CurrOpBal;
                                    StatementTbl.Rows.Add(statRow);
                                }
                            }
                        }
                    }
                }
                GrdStatementDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = StatementTbl;
                GrdStatementDetails.AutoGenerateColumns = false;
                GrdStatementDetails.DataSource = bindingSource;

                GrdSummary.DataSource = null;
                GrdSummary.Rows.Clear();
                int rowIndex = GrdSummary.Rows.Add();
                var row = GrdSummary.Rows[rowIndex];
                row.Cells[0].Value = "Total Records: " + StatementTbl.Rows.Count;
                row.Cells[2].Value = Dbt;
                row.Cells[3].Value = Crdt;
                row.Cells[4].Value = CurrOpBal;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal GetCurrentCustOpBalTodate(int custId, DateTime fyStart)
        {
            try
            {
                decimal Amt1 = 0;
                decimal Amt2 = 0;
                decimal OpBal = 0;
                //var glbrList = (from glBr in cmpDBContext.GlBreaks
                //                where glBr.GlModule == "Sales" && glBr.TranDate >= fyStart && glBr.TranDate <= DateTime.Now && glBr.Status == "Active" && glBr.IDNo == custId && (glBr.GnlId == 12000 || glBr.GnlId == 35000)
                //                group glBr by glBr.IDNo into grp
                //                select new
                //                {
                //                    TotAmt1 = grp.Sum(x => x.Amt1),
                //                    TotAmt2 = grp.Sum(x => x.Amt2),
                //                }).ToList();
                var custDet = (from cust in cmpDBContext.Customers
                               where cust.CustomerId == custId
                               select new
                               {
                                   cust.OpeningBalance
                               }).ToList();
                if (custDet.Count() > 0)
                {
                    OpBal = Convert.ToDecimal(custDet.First().OpeningBalance);
                }
                //if (glbrList.Count() > 0)
                //{
                //    Amt1 = Convert.ToDecimal(glbrList.First().TotAmt1);
                //    Amt2 = Convert.ToDecimal(glbrList.First().TotAmt2);
                //}

                decimal OpBAl = OpBal + Amt1 - Amt2;
                return OpBAl;
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
                if (CustomerId <= 0)
                {
                    MessageBox.Show("Please select a customer to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (BankID <= 0)
                {
                    MessageBox.Show("Please select a Bank/Cash details to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (Convert.ToDecimal(TxtCurrReceipt.Text) <= 0)
                {
                    MessageBox.Show("Please enter current receipt to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                return true;

            }
            catch (Exception)
            {

                throw;
            }
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
        private void InitializingData()
        {
            try
            {
                ADD_NEW_BOOL = true;
                BankGnlId = 0;
                BankID = 0;
                CustomerId = 0;
                RcptNo = 0;
                StatementTbl.Clear();
                ClearTextBoxes(this.Controls);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }

        private void TxtCurrReceipt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtCurrReceipt.Text))
                e.Handled = true;
        }

        private void PbSearchRcptNo_Click(object sender, EventArgs e)
        {
            GetSalesReceiptList();
        }
        private void GetSalesReceiptList()
        {
            try
            {
                FrmSalesReceiptList frmSalesReceiptList = new FrmSalesReceiptList();
                frmSalesReceiptList.ShowDialog();
                if (MdlMain.gSalesRcpNo > 0)
                {
                    ADD_NEW_BOOL = false;
                    //InitializingDataForEditing();
                    RcptNo = MdlMain.gSalesRcpNo;
                    string rcpNo = RcptNo.ToString();
                    MdlMain.gSalesRcpNo = 0;
                    var rcptList = (from glBrk in cmpDBContext.GlBreaks
                                    join cust in cmpDBContext.Customers on glBrk.IDNo equals cust.CustomerId
                                    join rcpt in cmpDBContext.SalesReceipt on glBrk.TranRef equals rcpt.RcpNo.ToString()
                                    join bnk in cmpDBContext.Bank on rcpt.GnlId equals bnk.GnlId
                                    where glBrk.TranRef == rcpNo && glBrk.GlModule == "Sales" && glBrk.TranType == "Receipt"
                                    && glBrk.Status == "Active" && glBrk.GnlId == 12000
                                    select new
                                    {
                                        glBrk.TranDate,
                                        glBrk.Amt2,
                                        glBrk.TranRef,
                                        glBrk.AmtMode,
                                        glBrk.GnlId,
                                        cust.CustomerName,
                                        cust.CustomerId,
                                        cust.Address1,
                                        cust.Address2,
                                        cust.TotalBalance,
                                        bnk.BankName,
                                        bnk.BankId
                                    }).ToList();
                    if (rcptList.Count() > 0)
                    {
                        foreach (var rcp in rcptList)
                        {
                            CustomerId = rcp.CustomerId;
                            TxtCustomerName.Text = rcp.CustomerName.ToString();
                            TxtAddress.Text = rcp.Address1;
                            TxtCurrReceipt.Text = rcp.Amt2.ToString();
                            BankGnlId = rcp.GnlId;
                            TxtComment.Text = rcp.AmtMode;
                            TxtBank.Text = rcp.BankName;
                            TxtBillNo.Text = rcp.TranRef.ToString();
                            BankID = rcp.BankId;
                        }
                        SetBankDetails(BankID);
                        SetCustomerDetails(CustomerId);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FrmSalesSettlement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }

        private void searchDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Dbt = 0;
                decimal Crdt = 0;
                StatementTbl.Clear();
                DateTime trDate = dtpSearchDate.Value.Date;
                decimal CurrOpBal = GetCurrentCustOpBalTodate(CustomerId, MdlMain.gFYStart);
                var glBrList = (from glBr in cmpDBContext.GlBreaks
                                where glBr.Status == "Active" && glBr.IDNo == CustomerId && (glBr.GnlId == 12000 || glBr.GnlId == 35000)
                                where glBr.TranDate == trDate
                                select new
                                {
                                    glBr.GnlId,
                                    glBr.TranDate,
                                    glBr.TranType,
                                    glBr.GlModule,
                                    glBr.TranRef,
                                    glBr.IDNo,
                                    glBr.DptId,
                                    glBr.Amt1,
                                    glBr.Amt2,
                                    glBr.AmtMode,
                                    glBr.Status,
                                    glBr.DEnd,
                                    glBr.LedAc
                                });
                DataRow statFRow = StatementTbl.NewRow();
                statFRow["Date"] = MdlMain.gFYStart;
                statFRow["Ref"] = "Opening Balance";
                if (CurrOpBal < 0)
                {
                    statFRow["Credit"] = CurrOpBal;
                    statFRow["Debit"] = 0;
                    Crdt = Crdt + CurrOpBal;
                }
                else
                {
                    statFRow["Credit"] = 0;
                    statFRow["Debit"] = CurrOpBal;
                    Dbt = Dbt + CurrOpBal;
                }
                statFRow["Balance"] = CurrOpBal;
                statFRow["LinkNo"] = 1;
                StatementTbl.Rows.Add(statFRow);

                if (glBrList.Count() > 0)
                {
                    foreach (var glBr in glBrList)
                    {
                        DataRow statRow = StatementTbl.NewRow();
                        if (glBr.GlModule == "Journal")
                        {
                            statRow["Date"] = glBr.TranDate;
                            statRow["Ref"] = "JNL" + glBr.TranRef;
                            statRow["LinkNo"] = 1;
                        }
                        else
                        {
                            statRow["Date"] = glBr.TranDate;
                            if (MdlMain.SerilNoStat == "Yes")
                            {
                                statRow["Ref"] = glBr.TranType + " - " + glBr.AmtMode;
                                if (glBr.TranType == "Invoice")
                                {
                                    statRow["Ref"] = "INV-" + glBr.AmtMode;
                                }
                            }
                            else
                            {
                                statRow["Ref"] = glBr.TranType + " - " + glBr.TranRef;
                                if (glBr.TranType == "Invoice")
                                {
                                    statRow["Ref"] = "INV" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "Advance")
                                {
                                    statRow["Ref"] = "ADV" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "Receipt")
                                {
                                    statRow["Ref"] = "RCP" + " - " + glBr.TranRef + " - " + glBr.AmtMode;
                                }
                                if (glBr.TranType == "Returns")
                                {
                                    statRow["Ref"] = "RTN" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "Debit")
                                {
                                    statRow["Ref"] = "DRN" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "Credit")
                                {
                                    statRow["Ref"] = "CRN" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "CrNote")
                                {
                                    statRow["Ref"] = "CRN" + " - " + glBr.TranRef;
                                }
                                if (glBr.TranType == "DrNote")
                                {
                                    statRow["Ref"] = "DRN" + " - " + glBr.TranRef;
                                }
                            }
                            statRow["LinkNo"] = 1;
                        }
                        if (glBr.GlModule == "Sales" || glBr.GlModule == "Journal")
                        {
                            if ((glBr.TranType == "Invoice") || (glBr.TranType == "Refund") || (glBr.TranType == "Credit") || (glBr.TranType == "CrNote") || (glBr.TranType == "DrNote") || (glBr.TranType == "Debit") || (glBr.TranType == "AdvAdj"))
                            {
                                if (glBr.GnlId == 12000)
                                {
                                    if (glBr.GlModule == "Journal")
                                    {
                                        if (glBr.TranType == "Credit")
                                        {
                                            statRow["Credit"] = glBr.Amt2;
                                            statRow["Debit"] = 0;
                                            CurrOpBal -= glBr.Amt2;
                                            statRow["Ref"] = "RCP-" + glBr.TranRef;
                                        }
                                        else
                                        {
                                            statRow["Debit"] = glBr.Amt1;
                                            statRow["Credit"] = 0;
                                            CurrOpBal += glBr.Amt1;
                                            statRow["Ref"] = "RFD-" + glBr.TranRef;
                                        }
                                    }
                                    else
                                    {
                                        if (glBr.TranType == "AdvAdj")
                                        {
                                            statRow["Credit"] = glBr.Amt2;
                                            statRow["Debit"] = 0;
                                            CurrOpBal -= glBr.Amt2;
                                            statRow["Ref"] = "ADV-" + glBr.TranRef;
                                        }
                                        else if (glBr.TranType == "CrNote")
                                        {
                                            statRow["Credit"] = glBr.Amt2;
                                            statRow["Debit"] = 0;
                                            CurrOpBal -= glBr.Amt2;
                                        }
                                        else if (glBr.TranType == "DrNote")
                                        {
                                            statRow["Credit"] = 0;
                                            statRow["Debit"] = glBr.Amt1;
                                            CurrOpBal += glBr.Amt1;
                                        }
                                        else
                                        {
                                            statRow["Credit"] = 0;
                                            statRow["Debit"] = glBr.Amt1;
                                            CurrOpBal += glBr.Amt1;
                                        }
                                    }
                                    statRow["Balance"] = CurrOpBal;
                                    Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    StatementTbl.Rows.Add(statRow);
                                }
                                else if (glBr.GnlId == 35000)
                                {
                                    statRow["Debit"] = glBr.Amt1;
                                    statRow["Credit"] = 0;
                                    CurrOpBal += glBr.Amt1;
                                    statRow["Balance"] = CurrOpBal;
                                    Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    StatementTbl.Rows.Add(statRow);
                                }
                            }
                            if (glBr.GlModule != "Journal")
                            {
                                if ((glBr.TranType == "Advance") || (glBr.TranType == "Receipt") || (glBr.TranType == "Returns") || (glBr.TranType == "Debit"))
                                {
                                    if (glBr.GnlId == 12000)
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal -= glBr.Amt2;
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                        statRow["Balance"] = CurrOpBal;
                                        StatementTbl.Rows.Add(statRow);
                                    }
                                    else if (glBr.GnlId == 35000)
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        if (CurrOpBal < 0)
                                        {
                                            CurrOpBal = CurrOpBal + glBr.Amt2;
                                        }
                                        else
                                        {
                                            CurrOpBal = CurrOpBal - glBr.Amt2;
                                        }
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                        statRow["Balance"] = CurrOpBal;
                                        StatementTbl.Rows.Add(statRow);
                                    }
                                }
                            }
                        }
                    }
                }
                if (StatementTbl.Rows.Count > 0)
                {
                    GrdStatementDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = StatementTbl;
                    GrdStatementDetails.AutoGenerateColumns = false;
                    GrdStatementDetails.DataSource = bindingSource;

                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                    int rowIndex = GrdSummary.Rows.Add();
                    var row = GrdSummary.Rows[rowIndex];
                    row.Cells[0].Value = "Total Items: " + StatementTbl.Rows.Count;
                    row.Cells[2].Value = Dbt;
                    row.Cells[3].Value = Crdt;
                    row.Cells[4].Value = CurrOpBal;
                }
                else
                {
                    GrdStatementDetails.DataSource = null;
                    GrdSummary.DataSource = null;
                    GrdSummary.Rows.Clear();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomerSelectList frmcustomerList = new FrmCustomerSelectList();
            frmcustomerList.ShowDialog();
            SetCustomerDetails(MdlMain.gCustomerId);
        }

        private void FrmSalesSettlement_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
