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
    public partial class FrmEmployeePosition : Form
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
        public FrmEmployeePosition()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
           
        }
        public void GetEmpPositionList()
        {
            try
            {
                var posList = (from pos in cmpDBContext.EmployeePosition
                               select new
                               {
                                   pos.EmpPositionId,
                                   pos.EmpPositionName,
                                   pos.Status
                               }).ToList();
                if (posList.Count() != 0)
                {
                    GrdEmpPosition.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = posList;
                    GrdEmpPosition.AutoGenerateColumns = false;
                    GrdEmpPosition.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAddEditEmployeePosition frmEmployeePosition = new FrmAddEditEmployeePosition(this);
                frmEmployeePosition.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmEmployeePosition_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetEmpPositionList();
        }

        private void GrdEmpPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdEmpPosition.Columns[e.ColumnIndex].Name == "Edit")
                {
                    MdlMain.gEmpPositionId = Convert.ToInt32(GrdEmpPosition.CurrentRow.Cells[0].Value);
                    FrmAddEditEmployeePosition frmAddEditEmployeePosition = new FrmAddEditEmployeePosition(this);
                    frmAddEditEmployeePosition.ShowDialog();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmEmployeePosition_KeyDown(object sender, KeyEventArgs e)
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtSearch.Text;
                var posList = (from pos in cmpDBContext.EmployeePosition
                               where pos.EmpPositionName.Contains(searchValue)
                               select new
                               {
                                   pos.EmpPositionId,
                                   pos.EmpPositionName,
                                   pos.Status
                               }).ToList();
                if (posList.Count() != 0)
                {
                    GrdEmpPosition.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = posList;
                    GrdEmpPosition.AutoGenerateColumns = false;
                    GrdEmpPosition.DataSource = bindingSource;
                }
                else
                {
                    GrdEmpPosition.DataSource = null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void FrmEmployeePosition_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
