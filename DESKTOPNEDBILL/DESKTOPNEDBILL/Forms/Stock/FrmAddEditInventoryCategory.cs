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

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmAddEditInventoryCategory : Form
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
        int EditstockCategoryId = 0;
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private readonly FrmInventoryCategory frmInventoryCategory;
        public FrmAddEditInventoryCategory(FrmInventoryCategory frmInventoryCategory)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmInventoryCategory = frmInventoryCategory;

        }

        #region Get & Set Method
        private bool FieldValidation()
        {
            if (TxtCategory.Text == "")
            {
                MessageBox.Show("Please enter Category", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtCategory.Focus();
                return false;
            }
            return true;
        }
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Stock Category";
            CmbStatus.SelectedIndex = 0;
            EditstockCategoryId = 0;
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
        public void GetStockCategoryDetailsByCategoryId(int CategoryId)
        {
            try
            {
                var stkCatList = (from cat in cmpDBContext.InventoryCategories
                                  where cat.InventoryCategoryId == CategoryId
                                  select new
                                  {
                                      cat.InventoryCategoryId,
                                      cat.CategoryName,
                                      cat.Status,
                                  });

                if (stkCatList.Count() > 0)
                {
                    foreach (var item in stkCatList)
                    {
                        TxtCategory.Text = item.CategoryName;
                        CmbStatus.Text = item.Status ? "Active" : "InActive";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveInventoryCategory()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditstockCategoryId > 0)
                    {
                        List<InventoryCategory> invCat = cmpDBContext.InventoryCategories.Where(m => m.InventoryCategoryId == EditstockCategoryId).ToList();
                        foreach (InventoryCategory cat in invCat)
                        {
                            cat.CategoryName = TxtCategory.Text.Trim();
                            cat.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        }
                        //cmpDBContext.InventoryCategories.UpdateRange(invCat);
                        cmpDBContext.SaveChanges();
                        ADD_NEW_BOOL = false;
                    }
                    else
                    {
                        var invCat = new InventoryCategory()
                        {
                            CategoryName = TxtCategory.Text.Trim(),
                            Status = CmbStatus.Text.Trim() == "Active" ? true : false,
                        };
                        cmpDBContext.InventoryCategories.Add(invCat);
                        cmpDBContext.SaveChanges();
                        ADD_NEW_BOOL = true;
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
        #endregion

        #region Event handiling methods
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveInventoryCategory())
            {
                frmInventoryCategory.GetStockCategoryList();
                InitializingData();
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Stock Category saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Stock Category updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ADD_NEW_BOOL = true;
                this.Close();
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }

        #endregion

        private void FrmAddEditInventoryCategory_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gStkCategoryId > 0)
            {
                EditstockCategoryId = MdlMain.gStkCategoryId;
                LblHeader.Text = "Edit Stock Category";
                GetStockCategoryDetailsByCategoryId(EditstockCategoryId);
            }
        }

        private void FrmAddEditInventoryCategory_KeyDown(object sender, KeyEventArgs e)
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
