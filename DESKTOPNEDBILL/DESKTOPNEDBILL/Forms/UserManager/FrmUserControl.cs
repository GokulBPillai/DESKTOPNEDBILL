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
using TableDims.Models;

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmUserControl : Form
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
        int MainModuleID = 0;
        int SubModuleID = 0;
        int EmpID = 0;
        int roleID = 0;
        DataTable MianModTbl = new DataTable();
        DataTable SubModTbl = new DataTable();
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmUserControl()
        {
            InitializeComponent();           
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            
        }
        public void GetMainModuleList(int roleId)
        {
            try
            {
                MianModTbl.Clear();
                var ModuleList = (from modmst in cmpDBContext.ModMaster
                                  join rolemod in cmpDBContext.RoleModule on modmst.ModId equals rolemod.ModId
                                  where rolemod.RoleId == roleId
                                  select new
                                  {
                                      modmst.ModId,
                                      modmst.ModName,
                                      IsAccess = rolemod.Status,
                                  }).ToList();
                if (ModuleList.Count() != 0)
                {
                    foreach (var item in ModuleList)
                    {
                        DataRow stkRow = MianModTbl.NewRow();
                        stkRow["ModId"] = item.ModId;
                        stkRow["ModName"] = item.ModName;
                        stkRow["IsAccess"] = item.IsAccess;
                        //var acce = cmpDBContext.RoleModule.Where(m => m.RoleId == roleId).ToList();
                        //if (acce.Count() > 0)
                        //{
                        //    stkRow["IsAccess"] = acce.First().Status;
                        //}
                        //else { stkRow["IsAccess"] = false; }
                        MianModTbl.Rows.Add(stkRow);
                    }
                    GrdMainModuleDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = MianModTbl;// ModuleList;
                    GrdMainModuleDetails.AutoGenerateColumns = false;
                    GrdMainModuleDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void GetSubModuleList(int ModId, int roleId)
        {
            try
            {
                SubModTbl.Clear();
                var ModuleList = (from submodmst in cmpDBContext.SubModMaster
                                  join rolesubmod in cmpDBContext.RoleSubModule on submodmst.SubModId equals rolesubmod.SubModId
                                  where submodmst.ModId == ModId && rolesubmod.RoleId == roleId
                                  select new
                                  {
                                      submodmst.SubModId,
                                      submodmst.SubModName,
                                      IsAccess = rolesubmod.Status
                                  }).ToList();
                if (ModuleList.Count() != 0)
                {
                    foreach (var item in ModuleList)
                    {
                        DataRow stkRow = SubModTbl.NewRow();
                        stkRow["SubModId"] = item.SubModId;
                        stkRow["SubModName"] = item.SubModName;
                        stkRow["IsAccess"] = item.IsAccess;
                        //var acce = cmpDBContext.RoleSubModule.Where(m => m.RoleId == roleId && m.ModId == ModId).ToList();
                        //if (acce.Count() > 0)
                        //{
                        //    stkRow["IsAccess"] = acce.First().Status;
                        //}
                        //else { stkRow["IsAccess"] = false; }
                        SubModTbl.Rows.Add(stkRow);
                    }
                    GrdSubModuleDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = SubModTbl;//ModuleList;
                    GrdSubModuleDetails.AutoGenerateColumns = false;
                    GrdSubModuleDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmUserControl_Load(object sender, EventArgs e)
        {
            CreateMainModTbl();
            CreateSubModTbl();
            GetEmpList();
            //GetMainModuleList();
        }

        private void CreateSubModTbl()
        {
            MianModTbl.Columns.Add("ModId", typeof(int));
            MianModTbl.Columns.Add("ModName", typeof(string));
            MianModTbl.Columns.Add("RoleId", typeof(int));
            MianModTbl.Columns.Add("IsAccess", typeof(bool));
        }

        private void CreateMainModTbl()
        {
            SubModTbl.Columns.Add("SubModId", typeof(int));
            SubModTbl.Columns.Add("ModId", typeof(int));
            SubModTbl.Columns.Add("SubModName", typeof(string));
            SubModTbl.Columns.Add("RoleId", typeof(int));
            SubModTbl.Columns.Add("IsAccess", typeof(bool));
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GetEmpList()
        {
            try
            {
                this.CmbEmployee.SelectedIndexChanged -= new EventHandler(CmbEmployee_SelectedIndexChanged);
                List<EmpCtrl> emp = cmpDBContext.EmpCtrl.ToList();
                if (emp.Count > 0)
                {
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = emp;
                    CmbEmployee.DataSource = bindingSource;
                }
                CmbEmployee.DisplayMember = "EmpName";
                CmbEmployee.ValueMember = "EmpId";
                CmbEmployee.SelectedIndex = -1;
                this.CmbEmployee.SelectedIndexChanged += new EventHandler(CmbEmployee_SelectedIndexChanged);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpID = Convert.ToInt32(CmbEmployee.SelectedValue);
            var roleIdByEmpId = cmpDBContext.EmpCtrl.Where(m => m.EmpId == EmpID).ToList();
            roleID = roleIdByEmpId.FirstOrDefault().RoleId;
            GetMainModuleList(roleID);
            GrdSubModuleDetails.DataSource = null;
        }

        private void GrdMainModuleDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MainModuleID = (int)GrdMainModuleDetails.CurrentRow.Cells[0].Value;
            GetSubModuleList(MainModuleID, roleID);
        }

        private void UpdateAccessControl()
        {
            try
            {
                var acceSub = cmpDBContext.RoleSubModule.Where(m => m.RoleId == roleID && m.SubModId == SubModuleID).ToList();
                foreach (var item in acceSub)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdSubModuleDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool grdcheckBoxStatus = Convert.ToBoolean(GrdSubModuleDetails.CurrentCell.EditedFormattedValue);
            var currentRow = GrdSubModuleDetails.CurrentRow.Index;
            SubModuleID = (int)GrdSubModuleDetails.Rows[currentRow].Cells[0].Value;
            bool substat = grdcheckBoxStatus;
            var acceSub = cmpDBContext.RoleSubModule.Where(m => m.RoleId == roleID && m.SubModId == SubModuleID).ToList();
            foreach (var item in acceSub)
            {
                item.Status = substat;
            }
            //cmpDBContext.RoleSubModule.UpdateRange(acceSub);
            cmpDBContext.SaveChanges();
            var acceSubCnt = cmpDBContext.RoleSubModule.Where(m => m.RoleId == roleID && m.ModId == MainModuleID).ToList();
            bool mmStat = acceSubCnt.Count() > 0 ? true : false;
            var accMM = cmpDBContext.RoleModule.Where(m => m.RoleId == roleID && m.ModId == SubModuleID).ToList();
            foreach (var item in accMM)
            {
                item.Status = mmStat;
            }
            //cmpDBContext.RoleModule.UpdateRange(accMM);
            cmpDBContext.SaveChanges();
            GetMainModuleList(roleID);
        }

        private void FrmUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnClose_Click(sender, e);
            }
        }

        private void FrmUserControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
