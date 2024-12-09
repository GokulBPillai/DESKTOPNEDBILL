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

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmInventoryType : Form
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
        public FrmInventoryType()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
           
        }
        public void GetStockTypeList()
        {
            try
            {
                var stkTypeList = (from ivT in cmpDBContext.InventoryTypes
                                   select new
                                   {
                                       ivT.InventoryTypeId,
                                       ivT.TypeName,
                                       IsStkDeduct = ivT.IsStkDeduct == true ? "Yes" : "No",
                                       Status = ivT.Status == true ? "Active" : "InActive",
                                   }).ToList();
                if (stkTypeList.Count() != 0)
                {
                    GrdInventoryType.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = stkTypeList;
                    GrdInventoryType.AutoGenerateColumns = false;
                    GrdInventoryType.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Event Handling Methods
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditInventoryType frmAddEditInventoryType = new FrmAddEditInventoryType(this);
                frmAddEditInventoryType.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdInventoryType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdInventoryType.Columns[e.ColumnIndex].Name == "Edit")
                {
                    if ((int)GrdInventoryType.CurrentRow.Cells[0].Value <= 2)
                    {
                        MessageBox.Show("Unable to edit/delete first two Inventory types", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    MdlMain.gInvTypeId = Convert.ToInt32(GrdInventoryType.CurrentRow.Cells[0].Value);
                    FrmAddEditInventoryType frmAddEditInventoryType = new FrmAddEditInventoryType(this);
                    frmAddEditInventoryType.ShowDialog();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        private void FrmInventoryType_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetStockTypeList();
        }

        private void FrmInventoryType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }

        private void FrmInventoryType_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
