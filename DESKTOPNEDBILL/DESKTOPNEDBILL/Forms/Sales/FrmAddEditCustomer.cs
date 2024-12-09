using DESKTOPNEDBILL.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmAddEditCustomer : Form
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
        private readonly FrmCustomer frmCustomer;
        int EditcustID = 0;
        int NewCustID = 0;
        bool ADD_NEW_BOOL = true;

        public FrmAddEditCustomer(FrmCustomer frmCustomer)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmCustomer = frmCustomer;
            //load

        }

        #region Event Handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            MdlMain.gCustomerId = 0;
            this.Close();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveCustomer())
            {
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Customer saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Customer updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ADD_NEW_BOOL = true;
                frmCustomer.GetCustomerList();
                InitializingData();
                if (Application.OpenForms.OfType<FrmSalesInvoice>().Count() > 0)
                {
                    MdlMain.gCustomerId = NewCustID;
                    this.Close();
                }
            }
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TxtCustomerName_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtCustomerName.Text.Length) == 1)
            {
                TxtCustomerName.Text = TxtCustomerName.Text[0].ToString().ToUpper();
                TxtCustomerName.Select(2, 1);
            }
        }
        private void TxtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress1.Focus();
            }
        }
        private void TxtAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress2.Focus();
            }
        }
        private void TxtAddress2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtContactNo.Focus();
            }
        }
        private void TxtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtEmailID.Focus();
            }
        }
        private void TxtEmailID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbLocation.Focus();
            }
        }
        private void CmbLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtCreditLimit.Focus();
            }
        }
        private void TxtCreditLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtOpening.Focus();
            }
        }
        private void TxtOpening_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbTaxtype.Focus();
            }
        }
        private void CmbTaxtype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbPaymenttype.Focus();
            }
        }
        private void CmbPaymenttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtGSTNo.Focus();
            }
        }
        private void TxtGSTNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPaymentTerms.Focus();
            }
        }
        private void TxtPaymentTerms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbPriceGroup.Focus();
            }
        }
        private void CmbPriceGroup_KeyDown(object sender, KeyEventArgs e)
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
                CmbState.Focus();
            }
        }
        private void CmbState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();

        }
        #endregion

        #region Only Numeric allowed        
        private void TxtPaymentTerms_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtPaymentTerms.Text))
                e.Handled = true;
        }
        private void TxtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtCreditLimit.Text))
                e.Handled = true;
        }
        private void TxtOpening_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtOpening.Text))
                e.Handled = true;
        }

        #endregion

        #region Get & Set Methods        
        private void GetCustmoerDetailsByCustomerId(List<Customer> customerDetailsById)
        {
            try
            {
                if (customerDetailsById.Count > 0)
                {
                    foreach (var item in customerDetailsById)
                    {
                        TxtCustomerName.Text = item.CustomerName;
                        TxtAddress1.Text = item.Address1;
                        TxtAddress2.Text = item.Address2;
                        TxtContactNo.Text = item.ContactNo;
                        TxtEmailID.Text = item.EmailId;
                        TxtGSTNo.Text = item.GSTNo;
                        TxtPaymentTerms.Text = item.PaymentInTerms;
                        TxtCreditLimit.Text = item.Creditlimit.ToString();
                        TxtOpening.Text = item.OpeningBalance.ToString();
                        CmbPaymenttype.Text = item.PaymentType;
                        CmbTaxtype.Text = item.TaxType;
                        CmbPriceGroup.Text = item.PriceGroup;
                        CmbStatus.SelectedItem = "InActive";
                        if (item.Status == true)
                        {
                            CmbStatus.SelectedItem = "Active";
                        }
                        CmbState.SelectedValue = item.StateCode;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveCustomer()
        {
            try
            {
                if (FieldValidation())
                {
                    if (TxtCustomerName.Text == "")
                    {
                        TxtCustomerName.Text = TxtContactNo.Text.Trim();
                    }
                    if (EditcustID > 0)
                    {
                        ADD_NEW_BOOL = false;
                        List<Customer> custs = cmpDBContext.Customers.Where(m => m.CustomerId == EditcustID).ToList();
                        foreach (Customer cst in custs)
                        {
                            cst.CustomerName = TxtCustomerName.Text.Trim();
                            cst.Address1 = TxtAddress1.Text.Trim();
                            cst.Address2 = TxtAddress2.Text.Trim();
                            cst.ContactNo = TxtContactNo.Text.Trim();
                            cst.EmailId = TxtEmailID.Text.Trim();
                            cst.GSTNo = TxtGSTNo.Text.Trim();
                            cst.PaymentInTerms = TxtPaymentTerms.Text.Trim();
                            cst.Creditlimit = TxtCreditLimit.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtCreditLimit.Text);
                            cst.OpeningBalance = TxtOpening.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpening.Text);
                            cst.PaymentType = CmbPaymenttype.Text.Trim();
                            cst.TaxType = CmbTaxtype.Text.Trim();
                            cst.Location = CmbLocation.Text.Trim();
                            cst.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                            cst.PriceGroup = CmbPriceGroup.Text.Trim();
                            cst.StateCode = Convert.ToInt32(CmbState.SelectedValue);
                        }
                        //cmpDBContext.Customers.AddRange(custs);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        ADD_NEW_BOOL = true;
                        var customers = new Customer()
                        {
                            CustomerName = TxtCustomerName.Text.Trim(),
                            Address1 = TxtAddress1.Text.Trim(),
                            Address2 = TxtAddress2.Text.Trim(),
                            ContactNo = TxtContactNo.Text,
                            EmailId = TxtEmailID.Text.Trim(),
                            GSTNo = TxtGSTNo.Text.Trim(),
                            PaymentInTerms = TxtPaymentTerms.Text.Trim(),
                            Creditlimit = TxtCreditLimit.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtCreditLimit.Text),
                            OpeningBalance = TxtOpening.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpening.Text),
                            PaymentType = CmbPaymenttype.Text.Trim(),
                            TaxType = CmbTaxtype.Text.Trim(),
                            Location = CmbLocation.Text.Trim(),
                            Status = true,
                            PriceGroup = CmbPriceGroup.Text.Trim(),
                            StateCode = Convert.ToInt32(CmbState.SelectedValue),
                            TotalBalance= TxtOpening.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpening.Text)
                        };
                        cmpDBContext.Customers.Add(customers);
                        cmpDBContext.SaveChanges();
                        NewCustID = customers.CustomerId;
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
            if (TxtCustomerName.Text == "" && TxtContactNo.Text == "")
            {
                MessageBox.Show("Please enter Customer Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtCustomerName.Focus();
                return false;
            }
            if (TxtContactNo.Text != "")
            {
                if (!MdlMain.IsPhoneNumber(TxtContactNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Phone No. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtContactNo.Focus();
                    return false;
                }
            }
            if (TxtEmailID.Text != "")
            {
                if (!MdlMain.IsEmailId(TxtEmailID.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid Email Id. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtEmailID.Focus();
                    return false;
                }
            }
            if (TxtGSTNo.Text != "")
            {
                if (!MdlMain.IsGSTNo(TxtGSTNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter valid GST No. Otherwise keep the field blank", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtGSTNo.Focus();
                    return false;
                }
            }

            return true;
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
            ADD_NEW_BOOL = true;
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Customer";
            TxtGSTNo.Enabled = false;
            EditcustID = 0;
            CmbStatus.SelectedIndex = 0;
            CmbTaxtype.SelectedIndex = 0;
            CmbPaymenttype.SelectedIndex = 0;
            CmbLocation.SelectedIndex = 0;
            CmbPriceGroup.SelectedIndex = 0;
            GetStateList();
            CmbState.SelectedValue = MdlMain.gCompanyStateCodeId;
        }
        private void GetStateList()
        {
            try
            {
                List<StateCode> stat = cmpDBContext.StateCodes.ToList();
                if (stat.Count > 0)
                {
                    CmbState.DataSource = stat;
                }
                CmbState.DisplayMember = "State";
                CmbState.ValueMember = "StateCodeId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Set Uppercase for first letter in textbox
        private void TxtCustomerName_TextChanged(object sender, EventArgs e)
        {
            if ((TxtCustomerName.Text.Length) == 1)
            {
                TxtCustomerName.Text = TxtCustomerName.Text[0].ToString().ToUpper();
                TxtCustomerName.Select(2, 1);
            }
        }
        private void TxtAddress1_TextChanged(object sender, EventArgs e)
        {
            if ((TxtAddress1.Text.Length) == 1)
            {
                TxtAddress1.Text = TxtAddress1.Text[0].ToString().ToUpper();
                TxtAddress1.Select(2, 1);
            }
        }
        private void TxtAddress2_TextChanged(object sender, EventArgs e)
        {
            if ((TxtAddress2.Text.Length) == 1)
            {
                TxtAddress2.Text = TxtAddress2.Text[0].ToString().ToUpper();
                TxtAddress2.Select(2, 1);
            }
        }


        #endregion

        private void TxtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtContactNo.Text))
                e.Handled = true;
        }

        private void CmbTaxtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (CmbTaxtype.SelectedIndex == 0)
            {
                TxtGSTNo.Enabled = false;
            }
            else
            {
                TxtGSTNo.Enabled = true;
            }
        }

        private void FrmAddEditCustomer_Load(object sender, EventArgs e)
        {           
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gCustomerId > 0)
            {
                ADD_NEW_BOOL = false;
                EditcustID = MdlMain.gCustomerId;
                LblHeader.Text = "Edit Customer";
                List<Customer> customerDetailsById = cmpDBContext.Customers.Where(m => m.CustomerId == EditcustID).ToList();
                Customer cust = customerDetailsById.FirstOrDefault();
                GetCustmoerDetailsByCustomerId(customerDetailsById);
            }
        }

        private void FrmAddEditCustomer_KeyDown(object sender, KeyEventArgs e)
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
    }
}
