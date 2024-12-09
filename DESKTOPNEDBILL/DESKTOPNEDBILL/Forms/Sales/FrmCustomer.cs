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
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmCustomer : Form
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
        public int pageSize = 40;
        public int pageNumber = 1;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmCustomer()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));

        }
        #region Save Customer methods       
        private void InitializingData()
        {
            MdlMain.gCustomerId = 0;
            GetCustomerList();
        }
        #endregion 

        #region Get & Set Methods
        public void GetCustomerList()
        {
            try
            {
                var customers = (from cust in cmpDBContext.Customers
                                 select new
                                 {
                                     cust.CustomerId,
                                     cust.CustomerName,
                                     cust.Address1,
                                     cust.Address2,
                                     cust.ContactNo,
                                     cust.EmailId,
                                     cust.GSTNo,
                                     cust.PaymentInTerms,
                                     cust.Creditlimit,
                                     cust.PaymentType,
                                     cust.TaxType,
                                     cust.Location,
                                     cust.Status,
                                     cust.TotalBalance//GetCustomerDue(cust.CustomerId)
                                 }).ToList();
                if (customers.Count != 0)
                {
                    grdCustomerDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = customers;
                    grdCustomerDetails.AutoGenerateColumns = false;
                    grdCustomerDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Event Handling methods      
        private void grdCustomerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdCustomerDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gCustomerId = Convert.ToInt32(grdCustomerDetails.CurrentRow.Cells[0].Value);
                FrmAddEditCustomer frmAddEditCustomer = new FrmAddEditCustomer(this);
                frmAddEditCustomer.ShowDialog();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditCustomer frmAddEditCustomer = new FrmAddEditCustomer(this);
                frmAddEditCustomer.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
        }

        private void FrmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<Customer> vCust = cmpDBContext.Customers.
                Where(m => m.CustomerName.Contains(txtSearch.Text)
                || m.Address1.Contains(txtSearch.Text)
                || m.Location.Contains(txtSearch.Text)
                || m.PaymentType.Contains(txtSearch.Text)
                ).ToList();
            if (vCust.Count != 0)
            {
                grdCustomerDetails.DataSource = null;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = vCust;
                grdCustomerDetails.AutoGenerateColumns = false;
                grdCustomerDetails.DataSource = bindingSource;
            }
        }

        private void FrmCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
