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

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmAddEditUserAccess : Form
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
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        int EditEmployeeId = 0;
        private FrmUserAccess frmUserAccess;
        public FrmAddEditUserAccess(FrmUserAccess frmUserAccess)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmUserAccess = frmUserAccess;
        }
        private void FrmAddEditUserAccess_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetEmpList();
            if (MdlMain.gEmployeeId > 0)
            {
                ADD_NEW_BOOL = false;
                CmbIsAdmin.SelectedIndex = 1;
                CmbIsLocked.SelectedIndex = 1;
                EditEmployeeId = MdlMain.gEmployeeId;
                LblHeader.Text = "Edit User Access";
                GetUserAccessDet(EditEmployeeId);
                MdlMain.gEmployeeId = 0;                
            }
        }
        public void GetUserAccessDet(int empId)
        {
            try
            {
                var stkList = (from ctrl in cmpDBContext.EmpCtrl
                               where ctrl.EmpId == empId
                               select new
                               {
                                   ctrl.EmpId,
                                   ctrl.UserID,
                                   ctrl.Password,
                                   ctrl.IsAdmin,
                                   ctrl.IsLocked,
                               });

                if (stkList.Count() > 0)
                {
                    ADD_NEW_BOOL = false;
                    foreach (var item in stkList)
                    {
                        CmbEmployee.SelectedValue = item.EmpId;
                        TxtUserName.Text = item.UserID;
                        TxtPassword.Text = item.Password;
                        CmbIsAdmin.Text = item.IsAdmin == true ? "Yes" : "No";
                        CmbIsLocked.Text = item.IsLocked == true ? "Yes" : "No";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool FieldValidation()
        {
            if (CmbEmployee.Text == "")
            {
                MessageBox.Show("Please select an Employee", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CmbEmployee.Focus();
                return false;
            }
            else if (TxtUserName.Text == "")
            {
                MessageBox.Show("Please enter UserName", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtUserName.Focus();
                return false;
            }
            else if (TxtPassword.Text == "")
            {
                MessageBox.Show("Please enter Password", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtPassword.Focus();
                return false;
            }
            return true;
        }
        private bool SaveUserAccess()
        {
            try
            {
                if (FieldValidation())
                {
                    if (ADD_NEW_BOOL)
                    {
                        int empid = Convert.ToInt32(CmbEmployee.SelectedValue);
                        var role = cmpDBContext.RoleMaster.Where(m => m.RoleId == empid).Select(m => m);

                        var empCtrl = new EmpCtrl()
                        {
                            EmpId = empid,
                            EmpName = CmbEmployee.Text,
                            UserID = TxtUserName.Text.Trim(),
                            Password = TxtPassword.Text.Trim(),
                            RoleId = role.First().RoleId,
                            IsAdmin = CmbIsAdmin.Text.Trim() == "Yes" ? true : false,
                            IsLocked = CmbIsAdmin.Text.Trim() == "Yes" ? true : false,
                        };
                        //cmpDBContext.EmpCtrl.Update(empCtrl);
                        cmpDBContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        List<EmpCtrl> empctrl = cmpDBContext.EmpCtrl.Where(m => m.EmpId == EditEmployeeId).ToList();
                        foreach (EmpCtrl ctrl in empctrl)
                        {
                            ctrl.UserID = TxtUserName.Text.Trim();
                            ctrl.Password = TxtPassword.Text.Trim();
                            ctrl.IsAdmin = CmbIsAdmin.Text.Trim() == "Yes" ? true : false;
                            ctrl.IsLocked = CmbIsAdmin.Text.Trim() == "Yes" ? true : false;
                        }
                        //cmpDBContext.EmpCtrl.UpdateRange(empctrl);
                        cmpDBContext.SaveChanges();
                        return true;
                    }

                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        private void GetEmpList()
        {
            try
            {
                this.CmbEmployee.SelectedIndexChanged -= new EventHandler(CmbEmployee_SelectedIndexChanged);
                List<Employee> emp = cmpDBContext.Employee.ToList();
                if (emp.Count > 0)
                {
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = emp;
                    CmbEmployee.DataSource = bindingSource;
                }
                CmbEmployee.DisplayMember = "EmployeeName";
                CmbEmployee.ValueMember = "EmployeeId";
                CmbEmployee.SelectedIndex = -1;
                this.CmbEmployee.SelectedIndexChanged += new EventHandler(CmbEmployee_SelectedIndexChanged);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void InitializingData()
        {
            ADD_NEW_BOOL = true;
            ClearTextBoxes(this.Controls);
            CmbEmployee.SelectedIndex = -1;
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
        private void CmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CmbEmployee.SelectedValue != null && ADD_NEW_BOOL)
            {
                GetUserAccessDet(Convert.ToInt32(CmbEmployee.SelectedValue));
            }

        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (SaveUserAccess())
            {
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("User Access saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User Access updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ADD_NEW_BOOL = true;
                frmUserAccess.GetUserAccessDetailList();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPassword.Focus();
            }
        }
        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbIsAdmin.Focus();
            }
        }
        private void CmbIsAdmin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbIsLocked.Focus();
            }
        }
        private void CmbIsLocked_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void FrmAddEditUserAccess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                BtnClear_Click(sender, e);
            }
        }
    }
}
