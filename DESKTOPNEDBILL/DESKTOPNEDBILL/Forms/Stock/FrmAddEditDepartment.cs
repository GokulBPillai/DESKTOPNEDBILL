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
    public partial class FrmAddEditDepartment : Form
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
        int EditDepartmentId = 0;
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private readonly FrmDepartment frmDepartment;
        public FrmAddEditDepartment(FrmDepartment frmDepartment)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmDepartment = frmDepartment;

        }
        #region Get & Post methods
        private void InitializingData()
        {
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Department";
            CmbStatus.SelectedIndex = 0;
            EditDepartmentId = 0;
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
        public void GetDepartmentDetailsByDepartmentId(int DepartmentId)
        {
            try
            {
                var dptList = (from dpt in cmpDBContext.Department
                               where dpt.DepartmentId == DepartmentId
                               select new
                               {
                                   dpt.DepartmentId,
                                   dpt.DepartmentName,
                                   dpt.Status,
                               });

                if (dptList.Count() > 0)
                {
                    foreach (var item in dptList)
                    {
                        TxtDepartment.Text = item.DepartmentName;
                        CmbStatus.Text = item.Status ? "Active" : "InActive";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool SaveDepartment()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditDepartmentId > 0)
                    {
                        List<Department> dpt = cmpDBContext.Department.Where(m => m.DepartmentId == EditDepartmentId).ToList();
                        foreach (Department dt in dpt)
                        {
                            dt.DepartmentName = TxtDepartment.Text.Trim();
                            dt.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        }
                        //cmpDBContext.Department.UpdateRange(dpt);
                        cmpDBContext.SaveChanges();
                        ADD_NEW_BOOL = false;
                    }
                    else
                    {
                        var dpt = new Department()
                        {
                            DepartmentName = TxtDepartment.Text.Trim(),
                            Status = CmbStatus.Text.Trim() == "Active" ? true : false,
                        };
                        cmpDBContext.Department.Add(dpt);
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
        private bool FieldValidation()
        {
            if (TxtDepartment.Text == "")
            {
                MessageBox.Show("Please enter Department Name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtDepartment.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region Event handling methods
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
            if (SaveDepartment())
            {
                frmDepartment.GetDepartmentList();
                InitializingData();
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Department saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Department updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void FrmAddEditDepartment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gDepartmentId > 0)
            {
                EditDepartmentId = MdlMain.gDepartmentId;
                LblHeader.Text = "Edit Department";
                GetDepartmentDetailsByDepartmentId(EditDepartmentId);
            }
        }

        private void FrmAddEditDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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
