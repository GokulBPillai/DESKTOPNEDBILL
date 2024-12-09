using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Forms.Stock;
using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
    public partial class FrmBank : Form
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
        public FrmBank()
        {
            InitializeComponent();
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));

        }

        #region Get & Set Methods
        public void GetBankList()
        {
            try
            {
                List<Bank> banks = cmpDBContext.Bank.ToList();
                if (banks.Count != 0)
                {
                    grdBankdetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = banks;
                    grdBankdetails.AutoGenerateColumns = false;
                    grdBankdetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InitializingData()
        {              
            GetBankList();
        }
        #endregion

        #region Click events methods
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void grdBankdetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdBankdetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gBankId = Convert.ToInt32(grdBankdetails.CurrentRow.Cells[0].Value);
                FrmAddEditBank frmAddEditBank = new FrmAddEditBank(this);
                frmAddEditBank.ShowDialog();
            }
        }       
        private void grdBankdetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int col = grdBankdetails.CurrentCell.ColumnIndex;
                int row = grdBankdetails.CurrentCell.RowIndex;
                if (col < grdBankdetails.Columns.Count - 1)
                {
                    grdBankdetails.CurrentCell = grdBankdetails.Rows[row].Cells[col + 1];
                    grdBankdetails.Focus();
                }
                else if (col == grdBankdetails.Columns.Count - 1)
                {
                    grdBankdetails.Rows.Add(1);
                    grdBankdetails.CurrentCell = grdBankdetails.Rows[row].Cells[1];
                    grdBankdetails.Focus();
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmBank_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
        }       
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditBank frmAddEditBank = new FrmAddEditBank(this);
                frmAddEditBank.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text;
                var banks = (from bnk in cmpDBContext.Bank                               
                               where
                               bnk.BankName.Contains(searchValue)
                               select bnk).ToList();

                if (banks.Count != 0)
                {
                    grdBankdetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = banks;
                    grdBankdetails.AutoGenerateColumns = false;
                    grdBankdetails.DataSource = bindingSource;
                }
                else
                {
                    grdBankdetails.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void FrmBank_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
