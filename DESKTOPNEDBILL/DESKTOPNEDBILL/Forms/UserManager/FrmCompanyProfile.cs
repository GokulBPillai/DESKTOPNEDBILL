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

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmCompanyProfile : Form
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
        public FrmCompanyProfile()
        {
            InitializeComponent();
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));            
        }
        #region Get & Post Methods
        public void GetCompanyList()
        {
            try
            {
                var compList = (from cmp in cmpDBContext.CompanyProfiles
                                select new
                                {
                                    cmp.CompanyId,
                                    cmp.CompanyCode,
                                    cmp.CompanyName,
                                    cmp.Address1,
                                    cmp.Address2,
                                    cmp.City,
                                    cmp.EmailId,
                                    cmp.Country,
                                    cmp.GSTNo,
                                    cmp.GSTType,
                                    cmp.PhoneNo
                                }).ToList();
                if (compList.Count() != 0)
                {
                    GrdCompanyDetails.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = compList;
                    GrdCompanyDetails.AutoGenerateColumns = false;
                    GrdCompanyDetails.DataSource = bindingSource;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Event Handling Methods
        private void GrdCompanyDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GrdCompanyDetails.Columns[e.ColumnIndex].Name == "Edit")
            {
                MdlMain.gCompanyId = Convert.ToInt32(GrdCompanyDetails.CurrentRow.Cells[0].Value);
                FrmAddEditCompanyProfile addEditCompanyProfile = new FrmAddEditCompanyProfile(this);
                addEditCompanyProfile.ShowDialog();
            }
        }
        private void FrmCompanyProfile_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetCompanyList();
        }
        private void BtnAddnew_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Sorry, Unable to access", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //FrmAddEditCompanyProfile frmAddEditCompProfile = new FrmAddEditCompanyProfile(this);
                //frmAddEditCompProfile.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void FrmCompanyProfile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnAddnew_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private void FrmCompanyProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
