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

namespace DESKTOPNEDBILL.Forms.Sales
{
    public partial class FrmSalesReceiptList : Form
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
        public FrmSalesReceiptList()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
        }

        private void FrmSalesReceiptList_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MdlMain.gSalesRcpNo = 0;
            GetSalesReceiptDetails();
        }
        private void GetSalesReceiptDetails()
        {
            try
            {
                var salesRcptList = (from glBrk in cmpDBContext.GlBreaks
                                     join cust in cmpDBContext.Customers on glBrk.IDNo equals cust.CustomerId                                     
                                     where glBrk.GlModule == "Sales" && glBrk.TranType == "Receipt"
                                     && glBrk.Status == "Active" && glBrk.GnlId == 12000
                                     //from salesRcpt in cmpDBContext.SalesReceipt
                                     //join cust in cmpDBContext.Customers on salesRcpt.CustomerId equals cust.CustomerId
                                     //orderby salesRcpt.RcpNo descending
                                    select new
                                    {
                                        glBrk.TranDate,
                                        glBrk.TranRef,
                                        cust.CustomerName,
                                        glBrk.Amt2,
                                        
                                    }).ToList();
                if (salesRcptList.Count != 0)
                {
                    GrdSalesReceiptDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = salesRcptList;
                    GrdSalesReceiptDetails.AutoGenerateColumns = false;
                    GrdSalesReceiptDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdSalesReceiptDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MdlMain.gSalesRcpNo = Convert.ToInt32(GrdSalesReceiptDetails.SelectedRows[0].Cells["SalesRcpNo"].Value);
            this.Close();
        }

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSalesReceiptList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LblClose_Click(sender, e);
            }
        }
    }
}
