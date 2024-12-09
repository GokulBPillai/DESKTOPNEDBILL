﻿using DESKTOPNEDBILL.Forms.Main;
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

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmUserAccess : Form
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
        public FrmUserAccess()
        {
            InitializeComponent();
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditUserAccess frmAddEditUserAccess = new FrmAddEditUserAccess(this);
                frmAddEditUserAccess.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void FrmUserAccess_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetUserAccessDetailList();
        }
        public void GetUserAccessDetailList()
        {
            try
            {
                var ModuleList = (from empctrl in cmpDBContext.EmpCtrl
                                  join emp in cmpDBContext.Employee on empctrl.EmpId equals emp.EmployeeId
                                  select new
                                  {
                                      empctrl.EmpId,
                                      emp.EmployeeName,
                                      empctrl.UserID,
                                      empctrl.Password,
                                      empctrl.IsLocked,
                                      empctrl.IsAdmin,
                                      emp.PhoneNo,
                                      emp.EmailId
                                  }).ToList();
                if (ModuleList.Count() != 0)
                {
                    GrdUserDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = ModuleList;
                    GrdUserDetails.AutoGenerateColumns = false;
                    GrdUserDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdUserDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrdUserDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gEmployeeId = Convert.ToInt32(GrdUserDetails.CurrentRow.Cells[0].Value);
                FrmAddEditUserAccess frmAddEditUserAccess = new FrmAddEditUserAccess(this);
                frmAddEditUserAccess.ShowDialog();
            }
        }

        private void FrmUserAccess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            } 
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
        }

        private void FrmUserAccess_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}