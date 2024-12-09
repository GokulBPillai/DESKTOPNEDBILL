using DESKTOPNEDBILL.Forms.Banks;
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
    public partial class FrmPurchaseSettlement : Form
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
        int VendorId = 0;
        int PayNo = 0;
        DataTable StatementTbl = new DataTable();
        bool ADD_NEW_BOOL = true;
        public FrmPurchaseSettlement()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            btnVendor.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnVendor.Width, btnVendor.Height, 5, 5));
            BtnRefresh.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnRefresh.Width, BtnRefresh.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            CreateStatementTbl();
            InitializingData();
        }

        #region Get & Set Methods
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
                TxtBank.Text = bnkdtls.First().BankName;
                BankGnlId = bnkdtls.First().GnlId;
                BankID = bnkdtls.First().BankId;
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
                    TxtVendorName.Text = vnd.VendorName;
                    TxtAddress.Text = vnd.VendorAdd1 + ", " + vnd.VendorAdd2;

                }
                decimal balance = MdlMain.GetCustomerBalance(VendorId);
                TxtYTDBalance.Text = balance.ToString();
                TxtRevBalance.Text = balance.ToString();
                GetVendorStatement(VendorId);
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
        private void AddPayment()
        {
            try
            {
                IsGlbreakExist(TxtPayNo.Text.Trim());
                if (Convert.ToDecimal(TxtCurrPayment.Text) > 0)
                {
                    if (PayNo > 0)
                    {
                        List<PurchasePayment> purpaynt = cmpDBContext.PurchasePayments.Where(m => m.PayNo == PayNo).ToList();
                        foreach (PurchasePayment purpmt in purpaynt)
                        {
                            purpmt.TranDate = DtpPayDate.Value;
                            purpmt.FinYear = MdlMain.gFinYear;
                            purpmt.CompanyCode = "";
                            purpmt.VendorId = VendorId;
                            purpmt.PayAmount = Convert.ToDecimal(TxtCurrPayment.Text);
                            purpmt.GnlId = BankGnlId;
                        }
                        //cmpDBContext.PurchasePayments.UpdateRange(purpaynt);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        var purPaymnt = new PurchasePayment()
                        {
                            TranDate = DtpPayDate.Value,
                            FinYear = MdlMain.gFinYear,
                            CompanyCode = "",
                            VendorId = VendorId,
                            PayAmount = Convert.ToDecimal(TxtCurrPayment.Text),
                            GnlId = BankGnlId
                        };
                        cmpDBContext.PurchasePayments.Add(purPaymnt);
                        cmpDBContext.SaveChanges();
                        PayNo = purPaymnt.PayNo;
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
                    TranDate = DtpPayDate.Value,
                    FinYear = MdlMain.gFinYear,
                    TranType = "Payment",
                    GlModule = "Purchase",
                    TranRef = PayNo.ToString(),
                    IDNo = VendorId,
                    DptId = 2001,
                    Status = "Active",
                    DEnd = "No",
                    LedAc = VendorId.ToString(),
                    AmtMode = TxtBank.Text.Trim() + " " + TxtComment.Text.Trim(),
                    GnlId = 32000,//---------------------------------------Accounts Payable Head
                    Amt1 = Convert.ToDecimal(TxtCurrPayment.Text),
                    Amt2 = 0
                };
                cmpDBContext.GlBreaks.Add(glBreak);
                cmpDBContext.SaveChanges();

                var glc = cmpDBContext.GlAccounts.Where(m => m.GnlId == BankGnlId).Select(m => m);
                var glBreak1 = new GlBreak();
                glBreak1.TranDate = DtpPayDate.Value;
                glBreak1.FinYear = MdlMain.gFinYear;
                glBreak1.TranType = "Payment";
                glBreak1.GlModule = "Purchase";
                glBreak1.TranRef = PayNo.ToString();
                glBreak1.IDNo = VendorId;
                glBreak1.DptId = 2001;
                glBreak1.Status = "Active";
                glBreak1.DEnd = "No";
                glBreak1.LedAc = TxtPayNo.Text.Trim();
                glBreak1.AmtMode = TxtBank.Text.Trim();
                if (BankID == 15000)
                {
                    glBreak1.Amt1 = 0;
                    glBreak1.Amt2 = Convert.ToDecimal(TxtCurrPayment.Text);
                }
                else
                {
                    if (glc.First().GnlCode == "DR")
                    {
                        glBreak1.Amt1 = Convert.ToDecimal(TxtCurrPayment.Text);
                        glBreak1.Amt2 = 0;
                    }
                    else
                    {
                        glBreak1.Amt1 = 0;
                        glBreak1.Amt2 = Convert.ToDecimal(TxtCurrPayment.Text);
                    }
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
                List<GlBreak> vbreak = cmpDBContext.GlBreaks.Where(m => m.TranRef == RcpNo && m.GlModule == "Purchase" && m.TranType == "Payment").ToList();
                if (vbreak.Count > 0)
                {
                    cmpDBContext.GlBreaks.RemoveRange(vbreak);
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
                if (VendorId <= 0)
                {
                    MessageBox.Show("Please select a vendor to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (BankID > 0)
                {
                    MessageBox.Show("Please select a Bank/Cash details to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                if (Convert.ToDecimal(TxtCurrPayment.Text) < 0)
                {
                    MessageBox.Show("Please enter current payment to continue.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                BankGnlId = 0;
                BankID = 0;
                VendorId = 0;
                PayNo = 0;
                StatementTbl.Clear();
                ClearTextBoxes(this.Controls);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private decimal GetCurrentVendOpBalTodate(int vendId, DateTime fyStart)
        {
            try
            {
                decimal Amt1 = 0;
                decimal Amt2 = 0;
                decimal OpBal = 0;
                var glbrList = (from glBr in cmpDBContext.GlBreaks
                                where glBr.GlModule == "Purchase" && glBr.TranDate >= fyStart && glBr.TranDate <= DateTime.Now && glBr.Status == "Active" && glBr.IDNo == vendId && glBr.GnlId == 32000
                                group glBr by glBr.IDNo into grp
                                select new
                                {
                                    TotAmt1 = grp.Sum(x => x.Amt1),
                                    TotAmt2 = grp.Sum(x => x.Amt2),
                                }).ToList();
                var vendDet = (from vend in cmpDBContext.Vendor
                               where vend.VendorId == vendId
                               select new
                               {
                                   vend.OpBal
                               }).ToList();
                if (vendDet.Count() > 0)
                {
                    OpBal = Convert.ToDecimal(vendDet.First().OpBal);
                }
                if (glbrList.Count() > 0)
                {
                    Amt1 = Convert.ToDecimal(glbrList.First().TotAmt1);
                    Amt2 = Convert.ToDecimal(glbrList.First().TotAmt2);
                }

                decimal OpBAl = OpBal + Amt2 - Amt1;
                return OpBAl;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetVendorStatement(int vendId)
        {
            try
            {
                decimal Dbt = 0;
                decimal Crdt = 0;
                StatementTbl.Clear();
                decimal CurrOpBal = GetCurrentVendOpBalTodate(vendId, MdlMain.gFYStart);
                var glBrList = (from glBr in cmpDBContext.GlBreaks
                                where glBr.Status == "Active" && glBr.IDNo == vendId && glBr.GnlId == 32000
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
                    statFRow["Credit"] = 0;
                    statFRow["Debit"] = CurrOpBal;
                    Dbt = Dbt + CurrOpBal;
                }
                else
                {
                    statFRow["Credit"] = CurrOpBal;
                    statFRow["Debit"] = 0;
                    Crdt = Crdt + CurrOpBal;
                }
                statFRow["Balance"] = CurrOpBal;
                statFRow["LinkNo"] = 1;
                StatementTbl.Rows.Add(statFRow);

                foreach (var glBr in glBrList)
                {
                    DataRow statRow = StatementTbl.NewRow();
                    statFRow["Date"] = glBr.TranDate;
                    if (glBr.GlModule == "Journal")
                    {
                        if (glBr.TranType == "Credit")
                        {
                            statRow["Ref"] = glBr.TranRef;
                        }
                        if (glBr.TranType == "Debit")
                        {
                            statRow["Ref"] = glBr.TranRef;
                        }
                    }
                    else
                    {
                        statRow["Ref"] = glBr.TranType + " - " + glBr.TranRef + "-" + glBr.AmtMode;
                        if (glBr.TranType == "Invoice")
                        {
                            statRow["Ref"] = "INV" + " - " + glBr.LedAc;
                        }
                        if (glBr.TranType == "Advance")
                        {
                            statRow["Ref"] = "ADV" + " - " + glBr.TranRef;
                        }
                        if (glBr.TranType == "Receipt")
                        {
                            statRow["Ref"] = "RCP" + " - " + glBr.TranRef;
                        }
                        if (glBr.TranType == "Payment")
                        {
                            statRow["Ref"] = "PMT" + " - " + glBr.TranRef + " - " + glBr.AmtMode;
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
                        statRow["LinkNo"] = 1;
                    }
                    if (glBr.GlModule == "Purchase" || glBr.GlModule == "Journal")
                    {
                        if ((glBr.TranType == "Invoice") || (glBr.TranType == "Journal") || (glBr.TranType == "Credit") || (glBr.TranType == "CrNote") || (glBr.TranType == "DrNote"))
                        {
                            if (glBr.GnlId == 32000)
                            {
                                if (glBr.GlModule == "Journal")
                                {
                                    if (glBr.TranType == "Credit")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else
                                    {
                                        statRow["Debit"] = glBr.Amt1;
                                        statRow["Credit"] = 0;
                                        CurrOpBal -= glBr.Amt1;
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    }
                                }
                                else
                                {
                                    if (glBr.TranType == "Invoice")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else if (glBr.TranType == "CrNote")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else if (glBr.TranType == "DrNote")
                                    {
                                        statRow["Credit"] = 0;
                                        statRow["Debit"] = glBr.Amt1;
                                        CurrOpBal -= glBr.Amt1;
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    }
                                }
                                statRow["Balance"] = CurrOpBal;
                                //Dbt += Convert.ToDecimal(statRow["Debit"]);
                                //Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
                            }
                            //else if (glBr.GnlId == 35000)
                            //{
                            //    statRow["Debit"] = glBr.Amt1;
                            //    statRow["Credit"] = 0;
                            //    CurrOpBal += glBr.Amt1;
                            //    statRow["Balance"] = CurrOpBal;
                            //    Dbt += Convert.ToDecimal(statRow["Debit"]);
                            //    Crdt += Convert.ToDecimal(statRow["Credit"]);
                            //    StatementTbl.Rows.Add(statRow);
                            //}
                        }
                        //Credit---------------------
                        if ((glBr.TranType == "Advance") || (glBr.TranType == "Payment") || (glBr.TranType == "Returns") || (glBr.TranType == "Debit"))
                        {
                            if (glBr.GnlId == 32000)
                            {
                                statRow["Credit"] = 0;
                                statRow["Debit"] = glBr.Amt1;
                                CurrOpBal -= glBr.Amt1;
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["Balance"] = CurrOpBal;
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
                            }
                            else if (glBr.GnlId == 12003)
                            {
                                statRow["Credit"] = 0;
                                statRow["Debit"] = glBr.Amt1;
                                if (CurrOpBal < 0)
                                {
                                    CurrOpBal = CurrOpBal + glBr.Amt1;
                                }
                                else
                                {
                                    CurrOpBal = CurrOpBal - glBr.Amt1;
                                }
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["Balance"] = CurrOpBal;
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
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
        #endregion

        #region Envent handling methods
        private void TxtVendorName_Click(object sender, EventArgs e)
        {
            FrmVendorSelectList frmvendorList = new FrmVendorSelectList();
            frmvendorList.ShowDialog();
            SetVendorDetails(MdlMain.gVendorId);
        }
        private void TxtBank_Click(object sender, EventArgs e)
        {
            GetBankDetails();
        }
        private void TxtCurrPayment_TextChanged(object sender, EventArgs e)
        {
            decimal ytdBal = Convert.ToDecimal(TxtYTDBalance.Text);
            if (ytdBal > 0)
            {
                TxtRevBalance.Text = (ytdBal - Convert.ToDecimal(TxtCurrPayment.Text)).ToString();
            }
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (FieldValidation())
            {
                AddPayment();
                AddGlBreak();
                InitializingData();
                SetVendorDetails(MdlMain.gVendorId);
                MessageBox.Show("Payment saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtCurrPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtCurrPayment.Text))
                e.Handled = true;
        }
        #endregion

        private void PbSearchRcptNo_Click(object sender, EventArgs e)
        {
            GetPurchasePaymentList();
        }
        private void GetPurchasePaymentList()
        {
            try
            {
                FrmPurchasePaymentList frmPurchasePaymentList = new FrmPurchasePaymentList();
                frmPurchasePaymentList.ShowDialog();
                if (MdlMain.gPurchasePayNo > 0)
                {
                    ADD_NEW_BOOL = false;
                    PayNo = MdlMain.gPurchasePayNo;
                    string payno = PayNo.ToString();
                    MdlMain.gPurchasePayNo = 0;
                    var payList = (from glBrk in cmpDBContext.GlBreaks
                                   join vend in cmpDBContext.Vendor on glBrk.IDNo equals vend.VendorId
                                   join pay in cmpDBContext.PurchasePayments on glBrk.TranRef equals pay.PayNo.ToString()
                                   join bnk in cmpDBContext.Bank on pay.GnlId equals bnk.GnlId
                                   where glBrk.TranRef == payno && glBrk.GlModule == "Purchase" && glBrk.TranType == "Payment"
                                   && glBrk.Status == "Active" && glBrk.GnlId == 32000
                                   select new
                                   {
                                       glBrk.TranDate,
                                       glBrk.Amt2,
                                       glBrk.TranRef,
                                       glBrk.AmtMode,
                                       glBrk.GnlId,
                                       vend.VendorName,
                                       vend.VendorId,
                                       vend.VendorAdd1,
                                       vend.VendorAdd2,
                                       vend.TotalBal,
                                       bnk.BankName,
                                       bnk.BankId
                                   }).ToList();
                    foreach (var pay in payList)
                    {
                        VendorId = pay.VendorId;
                        TxtVendorName.Text = pay.VendorName.ToString();
                        TxtAddress.Text = pay.VendorAdd1;
                        TxtCurrPayment.Text = pay.Amt2.ToString();
                        BankGnlId = pay.GnlId;
                        TxtComment.Text = pay.AmtMode;
                        TxtBank.Text = pay.BankName;
                        TxtPayNo.Text = pay.TranRef.ToString();
                        BankID = pay.BankId;
                    }
                    SetBankDetails(BankID);
                    SetVendorDetails(VendorId);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FrmPurchaseSettlement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnRefresh_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void btnVendor_Click(object sender, EventArgs e)
        {
            FrmVendorSelectList frmvendorList = new FrmVendorSelectList();
            frmvendorList.ShowDialog();
            SetVendorDetails(MdlMain.gVendorId);
        }

        private void dtpSearchDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Dbt = 0;
                decimal Crdt = 0;
                StatementTbl.Clear();
                decimal CurrOpBal = GetCurrentVendOpBalTodate(VendorId, MdlMain.gFYStart);
                var glBrList = (from glBr in cmpDBContext.GlBreaks
                                where glBr.Status == "Active" && glBr.IDNo == VendorId && glBr.GnlId == 32000
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
                    statFRow["Credit"] = 0;
                    statFRow["Debit"] = CurrOpBal;
                    Dbt = Dbt + CurrOpBal;
                }
                else
                {
                    statFRow["Credit"] = CurrOpBal;
                    statFRow["Debit"] = 0;
                    Crdt = Crdt + CurrOpBal;
                }
                statFRow["Balance"] = CurrOpBal;
                statFRow["LinkNo"] = 1;
                StatementTbl.Rows.Add(statFRow);

                foreach (var glBr in glBrList)
                {
                    DataRow statRow = StatementTbl.NewRow();
                    statFRow["Date"] = glBr.TranDate;
                    if (glBr.GlModule == "Journal")
                    {
                        if (glBr.TranType == "Credit")
                        {
                            statRow["Ref"] = glBr.TranRef;
                        }
                        if (glBr.TranType == "Debit")
                        {
                            statRow["Ref"] = glBr.TranRef;
                        }
                    }
                    else
                    {
                        statRow["Ref"] = glBr.TranType + " - " + glBr.TranRef + "-" + glBr.AmtMode;
                        if (glBr.TranType == "Invoice")
                        {
                            statRow["Ref"] = "INV" + " - " + glBr.LedAc;
                        }
                        if (glBr.TranType == "Advance")
                        {
                            statRow["Ref"] = "ADV" + " - " + glBr.TranRef;
                        }
                        if (glBr.TranType == "Receipt")
                        {
                            statRow["Ref"] = "RCP" + " - " + glBr.TranRef;
                        }
                        if (glBr.TranType == "Payment")
                        {
                            statRow["Ref"] = "PMT" + " - " + glBr.TranRef + " - " + glBr.AmtMode;
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
                        statRow["LinkNo"] = 1;
                    }
                    if (glBr.GlModule == "Purchase" || glBr.GlModule == "Journal")
                    {
                        if ((glBr.TranType == "Invoice") || (glBr.TranType == "Journal") || (glBr.TranType == "Credit") || (glBr.TranType == "CrNote") || (glBr.TranType == "DrNote"))
                        {
                            if (glBr.GnlId == 32000)
                            {
                                if (glBr.GlModule == "Journal")
                                {
                                    if (glBr.TranType == "Credit")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else
                                    {
                                        statRow["Debit"] = glBr.Amt1;
                                        statRow["Credit"] = 0;
                                        CurrOpBal -= glBr.Amt1;
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    }
                                }
                                else
                                {
                                    if (glBr.TranType == "Invoice")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else if (glBr.TranType == "CrNote")
                                    {
                                        statRow["Credit"] = glBr.Amt2;
                                        statRow["Debit"] = 0;
                                        CurrOpBal += glBr.Amt2;
                                        Crdt += Convert.ToDecimal(statRow["Credit"]);
                                    }
                                    else if (glBr.TranType == "DrNote")
                                    {
                                        statRow["Credit"] = 0;
                                        statRow["Debit"] = glBr.Amt1;
                                        CurrOpBal -= glBr.Amt1;
                                        Dbt += Convert.ToDecimal(statRow["Debit"]);
                                    }
                                }
                                statRow["Balance"] = CurrOpBal;
                                //Dbt += Convert.ToDecimal(statRow["Debit"]);
                                //Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
                            }
                            //else if (glBr.GnlId == 35000)
                            //{
                            //    statRow["Debit"] = glBr.Amt1;
                            //    statRow["Credit"] = 0;
                            //    CurrOpBal += glBr.Amt1;
                            //    statRow["Balance"] = CurrOpBal;
                            //    Dbt += Convert.ToDecimal(statRow["Debit"]);
                            //    Crdt += Convert.ToDecimal(statRow["Credit"]);
                            //    StatementTbl.Rows.Add(statRow);
                            //}
                        }
                        //Credit---------------------
                        if ((glBr.TranType == "Advance") || (glBr.TranType == "Payment") || (glBr.TranType == "Returns") || (glBr.TranType == "Debit"))
                        {
                            if (glBr.GnlId == 32000)
                            {
                                statRow["Credit"] = 0;
                                statRow["Debit"] = glBr.Amt1;
                                CurrOpBal -= glBr.Amt1;
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["Balance"] = CurrOpBal;
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
                            }
                            else if (glBr.GnlId == 12003)
                            {
                                statRow["Credit"] = 0;
                                statRow["Debit"] = glBr.Amt1;
                                if (CurrOpBal < 0)
                                {
                                    CurrOpBal = CurrOpBal + glBr.Amt1;
                                }
                                else
                                {
                                    CurrOpBal = CurrOpBal - glBr.Amt1;
                                }
                                Dbt += Convert.ToDecimal(statRow["Debit"]);
                                Crdt += Convert.ToDecimal(statRow["Credit"]);
                                statRow["Balance"] = CurrOpBal;
                                statRow["LinkNo"] = 1;
                                StatementTbl.Rows.Add(statRow);
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
                    row.Cells[0].Value = "Total Records: " + StatementTbl.Rows.Count;
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

        private void FrmPurchaseSettlement_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
