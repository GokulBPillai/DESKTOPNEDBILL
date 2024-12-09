using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmUserAccessReports : Form
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
        public FrmUserAccessReports()
        {
            InitializeComponent();
            btnClear.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 5, 5));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            btnView.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnView.Width, btnView.Height, 5, 5));
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            GetUserAccesDetails();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string textvalue=txtSearch.Text.Trim();
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var userDetails = (from user in cmpDBContext.UserAccessDetails
                                   join emp in cmpDBContext.Employee on user.RoleId equals emp.EmployeeId
                                   join empctrl in cmpDBContext.EmpCtrl on user.RoleId equals empctrl.RoleId
                                   join submod in cmpDBContext.SubModMaster on user.SubModIdAccessed equals submod.SubModId
                                   where (user.LoginDate >= fromDt && user.LogOutDate <= toDt)
                                   where emp.EmployeeName.Contains(textvalue) || submod.SubModName.Contains(textvalue)  
                                   select new
                                   {
                                       emp.EmployeeName,
                                       empctrl.UserID,
                                       submod.SubModName,
                                       user.LoginDate,
                                       user.LoginTime,
                                       user.LogOutDate,
                                       user.LogOutTime
                                   }).ToList();
                if (userDetails.Count > 0)
                {
                    GrdUserDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = userDetails;
                    GrdUserDetails.AutoGenerateColumns = false;
                    GrdUserDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmUserAccessReports_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GrdUserDetails.DataSource = null;
        }
        private void GetUserAccesDetails()
        {
            try
            {
                DateTime fromDt;
                DateTime toDt;
                fromDt = DtpFrom.Value.Date;
                toDt = DtpTo.Value.Date;
                var userDetails = (from user in cmpDBContext.UserAccessDetails
                                   join emp in cmpDBContext.Employee on user.RoleId equals emp.EmployeeId
                                   join empctrl in cmpDBContext.EmpCtrl on user.RoleId equals empctrl.RoleId 
                                   join submod in cmpDBContext.SubModMaster on user.SubModIdAccessed equals submod.SubModId 
                                   where (user.LoginDate >= fromDt && user.LogOutDate <= toDt)
                                   select new
                                   {
                                       emp.EmployeeName,
                                       empctrl.UserID,
                                       submod.SubModName,
                                       user.LoginDate,
                                       user.LoginTime,
                                       user.LogOutDate,
                                       user.LogOutTime
                                   }).ToList();
                if (userDetails.Count > 0)
                {
                    GrdUserDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = userDetails;
                    GrdUserDetails.AutoGenerateColumns = false;
                    GrdUserDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmUserAccessReports_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnView_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                btnClear_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }
    }
}
