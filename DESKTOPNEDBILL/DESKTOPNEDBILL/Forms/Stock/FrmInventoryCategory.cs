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
    public partial class FrmInventoryCategory : Form
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
        public FrmInventoryCategory()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
            
        }

        #region Get & Set methods
        public void GetStockCategoryList()
        {
            try
            {
                var catList = (from cat in cmpDBContext.InventoryCategories
                               select new
                               {
                                   cat.InventoryCategoryId,
                                   cat.CategoryName,
                                   cat.Status
                               }).ToList();
                if (catList.Count() != 0)
                {
                    GrdInventoryCategory.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = catList;
                    GrdInventoryCategory.AutoGenerateColumns = false;
                    GrdInventoryCategory.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event Handling methods
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditInventoryCategory frmAddEditInventoryCategory = new FrmAddEditInventoryCategory(this);
                frmAddEditInventoryCategory.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GrdInventoryCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdInventoryCategory.Columns[e.ColumnIndex].Name == "Edit")
                {
                    MdlMain.gStkCategoryId = Convert.ToInt32(GrdInventoryCategory.CurrentRow.Cells[0].Value);
                    FrmAddEditInventoryCategory frmAddEditInventoryCategory = new FrmAddEditInventoryCategory(this);
                    frmAddEditInventoryCategory.ShowDialog();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        private void FrmInventoryCategory_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetStockCategoryList();
        }

        private void FrmInventoryCategory_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmInventoryCategory_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
