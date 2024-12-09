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

namespace DESKTOPNEDBILL.Forms.Banks
{
    public partial class FrmAddEditBank : Form
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
        int EditbnkID = 0;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private FrmBank frmBank;
        public FrmAddEditBank(FrmBank frmBank)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmBank = frmBank;
        }
        #region Get & Set Methods
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Bank";
            CmbStatus.SelectedIndex = 0;
            CmbAcType.SelectedIndex = 0;
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
        private int GetMainHead(string strGnlType)
        {
            if (strGnlType == "Current" || strGnlType == "Savings" || strGnlType == "Fixed Deposit")
            {
                return 12500;
            }
            else
            {
                return 30100;
            }
        }
        private int SetGnlId(int mainHead)
        {
            try
            {
                int result = 0;
                var NextVal = cmpDBContext.GlAccounts.Where(m => m.MainHead == mainHead).Max(x => x.GnlId);

                int NextGnlId = NextVal + 1;
                if (mainHead == 30100 && NextGnlId > 30150)
                {
                    result = -1;
                }
                if (mainHead == 12500 && NextGnlId > 12550)
                {
                    result = -1;
                }
                if (result == 0)
                {
                    var oGnl = new GlAccount()
                    {
                        GnlId = NextGnlId,
                        GnlDesc = TxtBankName.Text,
                        GnlCode = mainHead == 30100 ? "Cr" : "Dr",
                        GnlOpBal = 0,
                        GnlStat = "Active",
                        GroupOrder = 0,
                        GnlBudget = 0
                    };
                    cmpDBContext.GlAccounts.Add(oGnl);
                    cmpDBContext.SaveChanges();
                }
                return NextGnlId;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveBank()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditbnkID > 0)
                    {
                        List<Bank> bnks = cmpDBContext.Bank.Where(m => m.BankId == EditbnkID).ToList();
                        foreach (Bank bnk in bnks)
                        {
                            bnk.BankName = TxtBankName.Text.Trim();
                            bnk.BankAddress = TxtAddress.Text.Trim();
                            bnk.AccountNo = TxtAcNo.Text.Trim();
                            bnk.BankBranch = TxtBranch.Text.Trim();
                            bnk.PhoneNo = TxtPhone.Text.Trim();
                            bnk.PinCode = TxtPINCode.Text.Trim();
                            bnk.IFSC = TxtIFC.Text.Trim();
                            bnk.AccountType = CmbAcType.Text.Trim();
                            bnk.EmailId = TxtEmail.Text.Trim();
                            bnk.CreditLimit = TxtCreditlimit.Text.Trim();
                            bnk.ChqStNo = TxtChequefrom.Text.Trim();
                            bnk.ChqEdNo = TxtChequeto.Text.Trim();
                            bnk.BankAbbrv = TxtBankabbrivation.Text.Trim();
                            bnk.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        }
                        //cmpDBContext.Bank.UpdateRange(bnks);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        int NextGlId = SetGnlId(GetMainHead(CmbAcType.Text.Trim()));
                        var banks = new Bank()
                        {
                            BankName = TxtBankName.Text.Trim(),
                            BankAddress = TxtAddress.Text.Trim(),
                            AccountNo = TxtAcNo.Text,
                            BankBranch = TxtBranch.Text.Trim(),
                            PhoneNo = TxtPhone.Text,
                            PinCode = TxtPINCode.Text.Trim(),
                            IFSC = TxtIFC.Text,
                            AccountType = CmbAcType.Text,
                            EmailId = TxtEmail.Text,
                            CreditLimit = TxtCreditlimit.Text.Trim(),
                            ChqStNo = TxtChequefrom.Text.Trim(),
                            ChqEdNo = TxtChequeto.Text.Trim(),
                            BankAbbrv = TxtBankabbrivation.Text.Trim(),
                            GnlId = NextGlId,
                            Status = true,

                        };
                        cmpDBContext.Bank.Add(banks);
                        cmpDBContext.SaveChanges();
                    }
                    return true;
                }
                return false;
            }

            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private bool FieldValidation()
        {
            if (TxtBankName.Text == "")
            {
                MessageBox.Show("Please enter Bank name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtBankName.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region Event handling methods
        private void FrmAddEditBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
        }
        private void FrmAddEditBank_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gBankId > 00)
            {
                EditbnkID = MdlMain.gBankId;
                LblHeader.Text = "Edit Bank";
                List<Bank> bankDetailsById = cmpDBContext.Bank.Where(m => m.BankId == EditbnkID).ToList();
                Bank bnks = bankDetailsById.FirstOrDefault();
                GetBankDetailsByBankId(bankDetailsById);
                MdlMain.gBankId = 0;
            }
        }
        private void GetBankDetailsByBankId(List<Bank> bankDetailsById)
        {
            try
            {
                if (bankDetailsById.Count > 0)
                {
                    foreach (var item in bankDetailsById)
                    {
                        TxtBankName.Text = item.BankName;
                        TxtAddress.Text = item.BankAddress;
                        TxtAcNo.Text = item.AccountNo;
                        TxtBranch.Text = item.BankBranch;
                        TxtPhone.Text = item.PhoneNo;
                        TxtPINCode.Text = item.PinCode;
                        TxtIFC.Text = item.IFSC;
                        CmbAcType.Text = item.AccountType;
                        TxtEmail.Text = item.EmailId;
                        TxtCreditlimit.Text = item.CreditLimit;
                        TxtChequefrom.Text = item.ChqStNo;
                        TxtChequeto.Text = item.ChqEdNo;
                        TxtBankabbrivation.Text = item.BankAbbrv;
                        CmbStatus.SelectedItem = "InActive";
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
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveBank())
            {
                frmBank.GetBankList();
                InitializingData();
                MessageBox.Show("Bank saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtAcNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtAcNo.Text))
                e.Handled = true;
        }
        private void TxtPINCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPINCode.Text))
                e.Handled = true;
        }
        private void TxtCreditlimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtCreditlimit.Text))
                e.Handled = true;
        }
        private void TxtBankName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress.Focus();
            }
        }
        private void TxtAddress_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtAcNo.Focus();
            }
        }
        private void TxtAcNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtBranch.Focus();
            }
        }
        private void TxtBranch_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtPINCode.Focus();
            }
        }
        private void TxtPINCode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtPhone.Focus();
            }
        }
        private void TxtPhone_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtIFC.Focus();
            }
        }
        private void TxtIFC_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                CmbAcType.Focus();
            }
        }
        private void CmbType_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtEmail.Focus();
            }
        }
        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TxtCreditlimit.Focus();
            }
        }
        private void TxtCreditlimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtChequefrom.Focus();
            }
        }
        private void TxtChequefrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtChequeto.Focus();
            }
        }
        private void TxtChequeto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtBankabbrivation.Focus();
            }
        }
        private void TxtBankabbrivation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbStatus.Focus();
            }
        }
        private void TxtBankName_TextChanged(object sender, EventArgs e)
        {
            if ((TxtBankName.Text.Length) == 1)
            {
                TxtBankName.Text = TxtBankName.Text[0].ToString().ToUpper();
                TxtBankName.Select(2, 1);
            }
        }
        private void TxtAddress_TextChanged(object sender, EventArgs e)
        {
            if ((TxtAddress.Text.Length) == 1)
            {
                TxtAddress.Text = TxtAddress.Text[0].ToString().ToUpper();
                TxtAddress.Select(2, 1);
            }
        }
        private void TxtBranch_TextChanged(object sender, EventArgs e)
        {
            if ((TxtBranch.Text.Length) == 1)
            {
                TxtBranch.Text = TxtBranch.Text[0].ToString().ToUpper();
                TxtBranch.Select(2, 1);
            }
        }
        private void CmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
