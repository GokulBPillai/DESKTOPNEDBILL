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
    public partial class FrmAddEditEmployee : Form
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
        int EditEmpID = 0;
        public int pageSize = 40;
        public int pageNumber = 1;
        bool ADD_NEW_BOOL = true;
        CMPDBContext cmpDBContext = new CMPDBContext();
        private FrmMaintainEmployee frmMaintainEmployee;
        public FrmAddEditEmployee(FrmMaintainEmployee frmMaintainEmployee)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 10, 10));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClear.Width, BtnClear.Height, 5, 5));
            BtnSave.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnSave.Width, BtnSave.Height, 5, 5));

            this.frmMaintainEmployee = frmMaintainEmployee;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }        
              
        #region Save Employee methods
        private bool SaveEmployee()
        {
            try
            {
                if (FieldValidation())
                {
                    if (EditEmpID > 0)
                    {
                        ADD_NEW_BOOL = false;
                        List<Employee> emps = cmpDBContext.Employee.Where(m => m.EmployeeId == EditEmpID).ToList();
                        foreach (Employee emp in emps)
                        {
                            emp.EmployeeName = TxtEmpName.Text.Trim();
                            emp.Address1 = TxtAddress1.Text.Trim();
                            emp.Address2 = TxtAddress2.Text.Trim();
                            emp.PhoneNo = TxtPhoneNo.Text.Trim();
                            emp.EmailId = TxtEmail.Text.Trim();
                            emp.Gender = CmbGender.Text.Trim();
                            emp.DOB = DateTime.Now;
                            emp.Religion = TxtReligion.Text.Trim();
                            emp.Nationality = TxtNationality.Text.Trim();
                            emp.Education = TxtEducation.Text.Trim();
                            emp.PANNO = TxtPanNo.Text.Trim();
                            emp.UANNO = TxtUANNo.Text.Trim();
                            emp.DepartmentId = Convert.ToInt32(CmbDepartment.SelectedValue);
                            emp.EmpPositionId = Convert.ToInt32(CmbPosition.SelectedValue);
                            emp.DateofJoined = DateTime.Now;
                            emp.EmpType = TxtType.Text.Trim();
                            emp.EmpStatus = CmbStatus.Text.Trim() == "Active" ? true : false;
                            emp.BasicSalary = TxtBasicsalary.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtBasicsalary.Text);
                            emp.TAllowance = TxtTA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTA.Text);
                            emp.DAllowance = TxtDA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtDA.Text);
                            emp.HRAllowance = TxtHRA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHRA.Text);
                            emp.Others = TxtOthers.Text.Trim();
                            emp.OpeningBalance = TxtOpBalance.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpBalance.Text);
                            emp.SalaryAdvance = TxtSalaryadvance.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSalaryadvance.Text);
                            emp.Nominee = TxtNominee.Text.Trim();
                            emp.Relation = TxtRelation.Text.Trim();
                        }
                        //cmpDBContext.Employee.UpdateRange(emps);
                        cmpDBContext.SaveChanges();
                        List<RoleMaster> roleMast = cmpDBContext.RoleMaster.Where(m => m.RoleName == EditEmpID.ToString()).ToList();
                        foreach (RoleMaster rm in roleMast)
                        {
                            rm.Status = CmbStatus.Text.Trim() == "Active" ? true : false;
                        };
                        //cmpDBContext.RoleMaster.UpdateRange(roleMast);
                        cmpDBContext.SaveChanges();
                    }
                    else
                    {
                        ADD_NEW_BOOL = true;
                        var employees = new Employee()
                        {
                            EmployeeName = TxtEmpName.Text.Trim(),
                            Address1 = TxtAddress1.Text.Trim(),
                            Address2 = TxtAddress2.Text.Trim(),
                            PhoneNo = TxtPhoneNo.Text,
                            EmailId = TxtEmail.Text.Trim(),
                            Gender = CmbGender.Text.Trim(),
                            DOB = DateTime.Now,
                            Religion = TxtReligion.Text.Trim(),
                            Nationality = TxtNationality.Text.Trim(),
                            Education = TxtEducation.Text.Trim(),
                            PANNO = TxtPanNo.Text.Trim(),
                            UANNO = TxtUANNo.Text.Trim(),
                            DepartmentId = Convert.ToInt32(CmbDepartment.SelectedValue),
                            EmpPositionId = Convert.ToInt32(CmbPosition.SelectedValue),
                            DateofJoined = DateTime.Now,
                            EmpType = TxtType.Text.Trim(),
                            EmpStatus = true,
                            BasicSalary = TxtBasicsalary.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtBasicsalary.Text),
                            TAllowance = TxtTA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtTA.Text),
                            DAllowance = TxtDA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtDA.Text),
                            HRAllowance = TxtHRA.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtHRA.Text),
                            Others = TxtOthers.Text.Trim(),
                            OpeningBalance = TxtOpBalance.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtOpBalance.Text),
                            SalaryAdvance = TxtSalaryadvance.Text == "" ? Convert.ToDecimal(0) : Convert.ToDecimal(TxtSalaryadvance.Text),
                            Nominee = TxtNominee.Text.Trim(),
                            Relation = TxtRelation.Text.Trim(),
                        };
                        cmpDBContext.Employee.Add(employees);
                        cmpDBContext.SaveChanges();
                        var roleMas = new RoleMaster()
                        {
                            RoleName = employees.EmployeeId.ToString(),
                            Status = true
                        };
                        cmpDBContext.RoleMaster.Add(roleMas);
                        cmpDBContext.SaveChanges();
                        List<ModMaster> mm = cmpDBContext.ModMaster.Where(m => m.IsActive == true).ToList();
                        List<RoleModule> rmd = new List<RoleModule>();
                        foreach (var item in mm)
                        {
                            rmd.Add(new RoleModule { RoleId = employees.EmployeeId, ModId = item.ModId, Status = false });
                        }
                        cmpDBContext.RoleModule.AddRange(rmd);
                        cmpDBContext.SaveChanges();
                        List<SubModMaster> sm = cmpDBContext.SubModMaster.Where(m => m.Status == true).ToList();
                        List<RoleSubModule> rsmd = new List<RoleSubModule>();
                        foreach (var item1 in mm)
                        {
                            List<SubModMaster> subModMast = cmpDBContext.SubModMaster.Where(m => m.ModId == item1.ModId).ToList();
                            foreach (var item2 in subModMast)
                            {
                                rsmd.Add(new RoleSubModule { RoleId = employees.EmployeeId, ModId = item1.ModId, SubModId = item2.SubModId, Status = false });
                            }
                        }
                        cmpDBContext.RoleSubModule.AddRange(rsmd);
                        cmpDBContext.SaveChanges();
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
            if (TxtEmpName.Text == "")
            {
                MessageBox.Show("Please enter Employee name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtEmpName.Focus();
                return false;
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
            GetDepartmentList();
            GetEmpPositionList();
            EditEmpID = 0;
            CmbGender.SelectedIndex = 0;
            CmbDepartment.SelectedIndex = 0;
            CmbPosition.SelectedIndex = 0;
            CmbStatus.SelectedIndex = 0;

        }
        #endregion 
        #region GetMethodes       
        private void GetEmployeeDetailsByEmployeeId(int empId)
        {
            try
            {
                var empList = (from emp in cmpDBContext.Employee
                               where emp.EmployeeId == empId
                               select new
                               {
                                   emp.EmployeeId,
                                   emp.EmployeeName,
                                   emp.Address1,
                                   emp.Address2,
                                   emp.PhoneNo,
                                   emp.EmailId,
                                   emp.Gender,
                                   emp.DOB,
                                   emp.Religion,
                                   emp.Nationality,
                                   emp.Education,
                                   emp.PANNO,
                                   emp.UANNO,
                                   emp.Department,
                                   emp.EmployeePosition,
                                   emp.EmpType,
                                   emp.EmpStatus,
                                   emp.DateofJoined,
                                   emp.BasicSalary,
                                   emp.TAllowance,
                                   emp.DAllowance,
                                   emp.HRAllowance,
                                   emp.SalaryAdvance,
                                   emp.Others,
                                   emp.OpeningBalance,
                                   emp.Nominee,
                                   emp.Relation
                               });
                if (empList.Count() > 0)
                {
                    foreach (var item in empList)
                    {
                        TxtEmpName.Text = item.EmployeeName;
                        TxtAddress1.Text = item.Address1;
                        TxtAddress2.Text = item.Address2;
                        TxtPhoneNo.Text = item.PhoneNo;
                        TxtEmail.Text = item.EmailId;
                        CmbGender.Text = item.Gender;
                        DtpDOB.Text = item.DOB.ToString();
                        TxtReligion.Text = item.Religion;
                        TxtNationality.Text = item.Nationality;
                        TxtEducation.Text = item.Education;
                        TxtPanNo.Text = item.PANNO;
                        TxtUANNo.Text = item.UANNO;
                        CmbDepartment.Text = item.Department?.DepartmentName.ToString();// Convert.ToInt32(cmbVendor.SelectedValue),
                        CmbPosition.Text = item.EmployeePosition?.EmpPositionName.ToString(); //item.EmpPositionId.ToString();
                        DtpDtjoined.Text = item.DateofJoined.ToString();
                        TxtType.Text = item.EmpType;
                        CmbStatus.SelectedItem = "InActive";
                        if (item.EmpStatus == true)
                        {
                            CmbStatus.SelectedItem = "Active";
                        }
                        TxtBasicsalary.Text = item.BasicSalary.ToString();
                        TxtTA.Text = item.TAllowance.ToString();
                        TxtDA.Text = item.DAllowance.ToString();
                        TxtHRA.Text = item.HRAllowance.ToString();
                        TxtOthers.Text = item.Others;
                        TxtOpBalance.Text = item.OpeningBalance.ToString();
                        TxtSalaryadvance.Text = item.SalaryAdvance.ToString();
                        TxtNominee.Text = item.Nominee;
                        TxtRelation.Text = item.Relation;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetDepartmentList()
        {
            try
            {
                List<Department> department = cmpDBContext.Department.ToList();
                if (department.Count <= 0)
                {
                    MdlMain.AddInitialDepartment();
                    List<Department> dept = cmpDBContext.Department.ToList();
                    CmbDepartment.DataSource = dept;
                }
                else
                {
                    CmbDepartment.DataSource = department;
                }
                CmbDepartment.DisplayMember = "DepartmentName";
                CmbDepartment.ValueMember = "DepartmentId";
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GetEmpPositionList()
        {
            try
            {
                List<EmployeePosition> empPos = cmpDBContext.EmployeePosition.ToList();
                if (empPos.Count > 0)
                {

                    CmbPosition.DataSource = empPos;
                    CmbPosition.DisplayMember = "EmpPositionName";
                    CmbPosition.ValueMember = "EmpPositionId";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Click events methods
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveEmployee())
            {
                if (ADD_NEW_BOOL)
                {
                    MessageBox.Show("Employee saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Employee updated successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ADD_NEW_BOOL = true;
                frmMaintainEmployee.GetEmployeeList();
                InitializingData();
                //MessageBox.Show("Employee saved successfully!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            InitializingData();
        }
        private void TxtEmpName_Click(object sender, EventArgs e)
        {
            //List<Employee> employees = cMPDBContext.Employees.Where(m => m.EmployeeName.Contains(TxtEmpName.Text)).ToList();
            //if (employees.Count != 0)
            //{
            //    grdEmployeedetails.DataSource = null;
            //    BindingSource bindingSource = new BindingSource();
            //    bindingSource.DataSource = employees;
            //    grdEmployeedetails.AutoGenerateColumns = false;
            //    grdEmployeedetails.DataSource = bindingSource;
            //}
        }
        #endregion
        #region Only Numeric allowed
        private void TxtBasicsalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtBasicsalary.Text))
                e.Handled = true;
        }

        private void TxtTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtTA.Text))
                e.Handled = true;
        }

        private void TxtDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtDA.Text))
                e.Handled = true;
        }

        private void TxtHRA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtHRA.Text))
                e.Handled = true;
        }

        private void TxtSalaryadvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!MdlMain.isNumber(e.KeyChar, TxtSalaryadvance.Text))
                e.Handled = true;
        }
        #endregion
        #region Focus change on Enter button click
        private void TxtEmpName_KeyDown(object sender, KeyEventArgs e)
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
                TxtPhoneNo.Focus();
            }
        }

        private void TxtPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtEmail.Focus();
            }
        }

        private void TxtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbGender.Focus();
            }
        }

        private void CmbGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DtpDOB.Focus();
            }
        }

        private void DtpDOB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtReligion.Focus();
            }
        }

        private void TxtReligion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtNationality.Focus();
            }
        }

        private void TxtNationality_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtEducation.Focus();
            }
        }

        private void TxtEducation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtPanNo.Focus();
            }
        }

        private void TxtPanNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtUANNo.Focus();
            }
        }

        private void TxtUANNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbDepartment.Focus();
            }
        }

        private void CmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CmbPosition.Focus();
            }
        }

        private void CmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DtpDtjoined.Focus();
            }
        }

        private void DtpDtjoined_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtType.Focus();
            }
        }

        private void TxtType_KeyDown(object sender, KeyEventArgs e)
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
                TxtBasicsalary.Focus();
            }
        }

        private void TxtBasicsalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtTA.Focus();
            }
        }

        private void TxtTA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtDA.Focus();
            }
        }

        private void TxtDA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtHRA.Focus();
            }
        }

        private void TxtHRA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtOthers.Focus();
            }
        }

        private void TxtOthers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtOpBalance.Focus();
            }
        }

        private void TxtOpBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSalaryadvance.Focus();
            }
        }

        private void TxtSalaryadvance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtNominee.Focus();
            }
        }

        private void TxtNominee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtRelation.Focus();
            }
        }
        #endregion
        #region Set Uppercase for first letter in textbox
        private void TxtEmpName_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtEmpName.Text.Length) == 1)
            {
                TxtEmpName.Text = TxtEmpName.Text[0].ToString().ToUpper();
                TxtEmpName.Select(2, 1);

            }
        }
        #endregion


        private void LblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtAddress2_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtAddress2.Text.Length) == 1)
            {
                TxtAddress2.Text = TxtAddress2.Text[0].ToString().ToUpper();
                TxtAddress2.Select(2, 1);

            }
        }

        private void TxtAddress1_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtAddress1.Text.Length) == 1)
            {
                TxtAddress1.Text = TxtAddress1.Text[0].ToString().ToUpper();
                TxtAddress1.Select(2, 1);

            }
        }
        private void TxtNationality_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtNationality.Text.Length) == 1)
            {
                TxtNationality.Text = TxtNationality.Text[0].ToString().ToUpper();
                TxtNationality.Select(2, 1);

            }
        }
        private void TxtType_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtType.Text.Length) == 1)
            {
                TxtType.Text = TxtType.Text[0].ToString().ToUpper();
                TxtType.Select(2, 1);

            }
        }
        private void TxtNominee_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtNominee.Text.Length) == 1)
            {
                TxtNominee.Text = TxtNominee.Text[0].ToString().ToUpper();
                TxtNominee.Select(2, 1);

            }
        }
        private void TxtRelation_TabIndexChanged(object sender, EventArgs e)
        {
            if ((TxtRelation.Text.Length) == 1)
            {
                TxtRelation.Text = TxtRelation.Text[0].ToString().ToUpper();
                TxtRelation.Select(2, 1);

            }
        }

        private void CmbPosition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtType.Focus();
            }
        }

        private void FrmAddEditEmployee_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializingData();
            if (MdlMain.gEmployeeId > 0)
            {
                ADD_NEW_BOOL = false;
                EditEmpID = MdlMain.gEmployeeId;
                LblHeader.Text = "Edit Employee";
                GetEmployeeDetailsByEmployeeId(EditEmpID);
                MdlMain.gEmployeeId = 0;
            }
        }

        private void FrmAddEditEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSave_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                btnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }
    }
}
