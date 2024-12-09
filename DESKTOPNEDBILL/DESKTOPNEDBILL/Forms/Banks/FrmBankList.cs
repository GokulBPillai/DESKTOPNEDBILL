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
    public partial class FrmBankList : Form
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
        public FrmBankList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void GetBankList()
        {
            try
            {
                List<Bank> bnk = cmpDBContext.Bank.Where(m => m.Status == true).ToList();
                //Bank bk = new Bank() { BankId = 15000, BankName = "Cash" };
                //bnk.Add(bk);
                if (bnk.Count != 0)
                {
                    GrdBankDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = bnk;
                    GrdBankDetails.AutoGenerateColumns = false;
                    GrdBankDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FrmBankList_Load(object sender, EventArgs e)
        {
            try
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                MdlMain.gBankId = 0;
                GetBankList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtBankName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Bank> vbank = cmpDBContext.Bank.Where(m => m.BankName.Contains(TxtBankName.Text)).ToList();
                if (vbank.Count != 0)
                {
                    GrdBankDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vbank;
                    GrdBankDetails.AutoGenerateColumns = false;
                    GrdBankDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                GrdBankDetails.Focus();
            }
        }
        private void GrdBankDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MdlMain.gBankId = Convert.ToInt32(GrdBankDetails.SelectedRows[0].Cells["BankId"].Value);
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdBankDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gBankId = Convert.ToInt32(GrdBankDetails.SelectedRows[0].Cells["BnkId"].Value);
            this.Close();
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBankList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
