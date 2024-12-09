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

namespace DESKTOPNEDBILL.Forms.HR
{
    public partial class FrmMaintainEmployee : Form
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
        //int EditEmpID = 0;
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmMaintainEmployee()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
           
        }

        #region Get & Set methods
        public void GetEmployeeList()
        {
            try
            {
                var emplist = (from emp in cmpDBContext.Employee
                               join dpt in cmpDBContext.Department on emp.DepartmentId equals dpt.DepartmentId
                               select new
                               {
                                   emp.EmployeeId,
                                   emp.EmployeeName,
                                   Addr = emp.Address1 + ", " + emp.Address2,
                                   emp.PhoneNo,
                                   dpt.DepartmentName,
                                   emp.OpeningBalance
                               }).ToList();
                if (emplist.Count() > 0)
                {
                    GrdEmployeeDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = emplist;
                    GrdEmployeeDetails.AutoGenerateColumns = false;
                    GrdEmployeeDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event handling methods
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditEmployee frmAddEditEmployee = new FrmAddEditEmployee(this);
                frmAddEditEmployee.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void GrdEmployeeDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrdEmployeeDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gEmployeeId = Convert.ToInt32(GrdEmployeeDetails.CurrentRow.Cells[0].Value);
                FrmAddEditEmployee frmAddEditEmployee = new FrmAddEditEmployee(this);
                frmAddEditEmployee.ShowDialog();
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmMaintainEmployee_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetEmployeeList();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text;
                var emplist = (from emp in cmpDBContext.Employee
                               join dpt in cmpDBContext.Department on emp.DepartmentId equals dpt.DepartmentId
                               where emp.EmployeeName.Contains(searchValue)
                               || emp.Address1.Contains(searchValue) || emp.Address2.Contains(searchValue)
                               select new
                               {
                                   emp.EmployeeId,
                                   emp.EmployeeName,
                                   Addr = emp.Address1 + ", " + emp.Address2,
                                   emp.PhoneNo,
                                   dpt.DepartmentName
                               }).ToList();
                if (emplist.Count() > 0)
                {
                    GrdEmployeeDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = emplist;
                    GrdEmployeeDetails.AutoGenerateColumns = false;
                    GrdEmployeeDetails.DataSource = bindingSource;
                }
                else
                {
                    GrdEmployeeDetails.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmMaintainEmployee_KeyDown(object sender, KeyEventArgs e)
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

        private void FrmMaintainEmployee_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
