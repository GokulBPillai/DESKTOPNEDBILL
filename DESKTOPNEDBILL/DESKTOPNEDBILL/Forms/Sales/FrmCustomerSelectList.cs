using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmCustomerSelectList : Form
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
        public FrmCustomerSelectList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }
        private void FrmCustomerSelectList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (FrmSalesInvoice.sendcustomertext != "")
            {
                TxtCustomer.Text = FrmSalesInvoice.sendcustomertext;
                int len = TxtCustomer.Text.Length;
                TxtCustomer.SelectionStart = len;
            }
            GetCustomerList();
        }
        private void GetCustomerList()
        {
            try
            {
                List<Customer> cust = cmpDBContext.Customers.ToList();
                if (cust.Count != 0)
                {
                    GrdCustomerDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = cust;
                    GrdCustomerDetails.AutoGenerateColumns = false;
                    GrdCustomerDetails.DataSource = bindingSource;
                }
                else
                {
                    //MdlMain.AddInitialCustomer();
                    //List<Customer> cust = cmpDBContext.Customers.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdCustomerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MdlMain.gCustomerId = Convert.ToInt32(GrdCustomerDetails.SelectedRows[0].Cells["CustomerId"].Value);
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Customer> vCust = cmpDBContext.Customers.Where(m => m.CustomerName.Contains(TxtCustomer.Text)).ToList();

                //var custList = (from cust in cmpDBContext.Customers
                //                where cust.CustomerName.Contains(TxtCustomer.Text)
                //               select new
                //               {
                //                   cust.CustomerName,
                //                   cust.Address1
                //               }).ToList();
                if (vCust.Count != 0)
                {
                    GrdCustomerDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = vCust;
                    GrdCustomerDetails.AutoGenerateColumns = false;
                    GrdCustomerDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    GrdCustomerDetails.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdCustomerDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MdlMain.gCustomerId = Convert.ToInt32(GrdCustomerDetails.SelectedRows[0].Cells["CustomerId"].Value);
                    this.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCustomerSelectList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
