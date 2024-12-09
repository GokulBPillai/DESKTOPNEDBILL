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

namespace DESKTOPNEDBILL.Forms.HR
{
    public partial class FrmAddEditEmployeePosition : Form
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
        int EditEmpPositionId = 0;
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private FrmEmployeePosition frmEmployeePosition;
        public FrmAddEditEmployeePosition(FrmEmployeePosition frmEmployeePosition)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmEmployeePosition = frmEmployeePosition;
        }
        private void InitializingData()
        {
            ADD_NEW_BOOL = true;
            ClearTextBoxes(this.Controls);
            LblHeader.Text = "Add New Employee Position";
            CmbStatus.SelectedIndex = 0;
            EditEmpPositionId = 0;
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
        private bool FieldValidation()
        {
            if (TxtPosition.Text == "")
            {
                MessageBox.Show("Please enter Position", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtPosition.Focus();
                return false;
            }
            return true;
        }
        public void GetEmployeePositionDetailsByPositionId(int PositionId)
        {
            try
            {
                var empPositionList = (from pos in cmpDBContext.EmployeePosition
                                       where pos.EmpPositionId == PositionId
                                       select new
                                       {
                                           pos.EmpPositionId,
                                           pos.EmpPositionName,
                                           pos.Status,
                                       });

                if (empPositionList.Count() > 0)
                {
                    foreach (var item in empPositionList)
                    {
                        TxtPosition.Text = item.EmpPositionName;
                        CmbStatus.Text = item.Status ? "Active" : "InActive";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FrmAddEditEmployeePosition_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gEmpPositionId > 0)
            {
                ADD_NEW_BOOL = false;
                EditEmpPositionId = MdlMain.gEmpPositionId;
                LblHeader.Text = "Edit Employee Position";
                GetEmployeePositionDetailsByPositionId(EditEmpPositionId);
                MdlMain.gEmpPositionId = 0;
            }
        }
        private bool SaveEmployeePosition()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditEmpPositionId > 0)
                    {
                        List<EmployeePosition> empPos = cmpDBContext.EmployeePosition.Where(m => m.EmpPositionId == EditEmpPositionId).ToList();
                        foreach (EmployeePosition pos in empPos)
                        {
                            pos.EmpPositionName = TxtPosition.Text.Trim();
                            pos.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        }
                        //cmpDBContext.EmployeePosition.UpdateRange(empPos);
                        cmpDBContext.SaveChanges();
                        ADD_NEW_BOOL = false;
                    }
                    else
                    {
                        var empPos = new EmployeePosition()
                        {
                            EmpPositionName = TxtPosition.Text.Trim(),
                            Status = CmbStatus.Text.Trim() == "Active" ? true : false,
                        };
                        cmpDBContext.EmployeePosition.Add(empPos);
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
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveEmployeePosition())
            {
                frmEmployeePosition.GetEmpPositionList();
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Employee Position saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Employee Position updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                InitializingData();
                this.Close();
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddEditEmployeePosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }
    }
}
