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
    public partial class FrmAddEditInventoryType : Form
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

        int EditstockTypeId = 0;
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private readonly FrmInventoryType frmInventoryType;
        public FrmAddEditInventoryType(FrmInventoryType frmInventoryType)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmInventoryType = frmInventoryType;

        }

        #region Get & Set Methods
        private bool FieldValidation()
        {
            if (TxtInventoryType.Text == "")
            {
                MessageBox.Show("Please enter Stock Type", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtInventoryType.Focus();
                return false;
            }
            return true;
        }
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Stock Type";
            CmbStatus.SelectedIndex = 0;
            CmbIsStkDeduct.SelectedIndex = 0;
            EditstockTypeId = 0;
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
        public void GetStockTypeDetailsByTypeId(int TypeId)
        {
            try
            {
                var stkTYpeList = (from typ in cmpDBContext.InventoryTypes
                                   where typ.InventoryTypeId == TypeId
                                   select new
                                   {
                                       typ.InventoryTypeId,
                                       typ.TypeName,
                                       typ.IsStkDeduct,
                                       typ.Status,
                                   });

                if (stkTYpeList.Count() > 0)
                {
                    foreach (var item in stkTYpeList)
                    {
                        TxtInventoryType.Text = item.TypeName;
                        CmbIsStkDeduct.Text = item.IsStkDeduct ? "Yes" : "No";
                        CmbStatus.Text = item.Status ? "Active" : "InActive";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveInventoryType()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditstockTypeId > 0)
                    {
                        List<InventoryType> invType = cmpDBContext.InventoryTypes.Where(m => m.InventoryTypeId == EditstockTypeId).ToList();
                        foreach (InventoryType it in invType)
                        {
                            it.TypeName = TxtInventoryType.Text.Trim();
                            it.IsStkDeduct = CmbIsStkDeduct.Text.Trim() == "Yes" ? true : false;
                            it.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        }
                        //cmpDBContext.InventoryTypes.UpdateRange(invType);
                        cmpDBContext.SaveChanges();
                        ADD_NEW_BOOL = false;
                    }
                    else
                    {
                        var invType = new InventoryType()
                        {
                            TypeName = TxtInventoryType.Text.Trim(),
                            IsStkDeduct = CmbStatus.Text.Trim() == "Yes" ? true : false,
                            Status = CmbStatus.Text.Trim() == "Active" ? true : false,
                        };
                        cmpDBContext.InventoryTypes.Add(invType);
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

        #region Event handling methods
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveInventoryType())
            {
                frmInventoryType.GetStockTypeList();
                InitializingData();
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Stock Type saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Stock Type updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ADD_NEW_BOOL = true;
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        #endregion

        private void FrmAddEditInventoryType_Load(object sender, EventArgs e)
        {

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gInvTypeId > 0)
            {
                EditstockTypeId = MdlMain.gInvTypeId;
                LblHeader.Text = "Edit Stock Type";
                GetStockTypeDetailsByTypeId(EditstockTypeId);
            }
        }

        private void FrmAddEditInventoryType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
        }
    }
}
