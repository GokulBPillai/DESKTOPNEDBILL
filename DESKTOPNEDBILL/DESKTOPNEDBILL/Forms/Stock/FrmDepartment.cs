using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;

namespace DESKTOPNEDBILL.Forms.Stock
{
    public partial class FrmDepartment : Form
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
        public FrmDepartment()
        {
            InitializeComponent();
            BtnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnClose.Width, BtnClose.Height, 5, 5));
            BtnAddnew.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, BtnAddnew.Width, BtnAddnew.Height, 5, 5));
           
        }

        #region Get & Set methods
        public void GetDepartmentList()
        {
            try
            {
                var dptList = (from dpt in cmpDBContext.Department
                               select new
                               {
                                   dpt.DepartmentId,
                                   dpt.DepartmentName,
                                   dpt.Status
                               }).ToList();
                if (dptList.Count() != 0)
                {
                    GrdDepartment.DataSource = null;
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = dptList;
                    GrdDepartment.AutoGenerateColumns = false;
                    GrdDepartment.DataSource = bindingSource;
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
                FrmAddEditDepartment frmAddEditDepartment = new FrmAddEditDepartment(this);
                frmAddEditDepartment.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GrdDepartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GrdDepartment.Columns[e.ColumnIndex].Name == "Edit")
                {
                    MdlMain.gDepartmentId = Convert.ToInt32(GrdDepartment.CurrentRow.Cells[0].Value);
                    FrmAddEditDepartment frmAddEditDepartment = new FrmAddEditDepartment(this);
                    frmAddEditDepartment.ShowDialog();
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

        #endregion

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            GetDepartmentList();
        }

        private void FrmDepartment_KeyDown(object sender, KeyEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Autobackup();
        }
        private bool Autobackup()
        {
            string foldername;
            string fullpath;
            string DBPATH = AppDomain.CurrentDomain.BaseDirectory;
            string Nm = DateTime.Now.ToString("dd.MM.yyyy-HH.mm");
            foldername = MdlMain.gCompanyName+"_BackUpDB"; // "BackUp"
            fullpath = Path.Combine(DBPATH, foldername);            
            string fullpath1 = Path.Combine(fullpath, Nm + ".accde");

            try
            {
                if (Directory.Exists(fullpath))
                {
                    DirectoryInfo dir = new DirectoryInfo(fullpath);

                    foreach (FileInfo fi in dir.GetFiles())
                    {
                        if (fi.FullName == fullpath1)
                        {
                            fi.Delete();
                        }
                    }
                }
                else
                {
                    Directory.CreateDirectory(fullpath);
                    Console.WriteLine("BackUp Folder Created " + fullpath + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine);
                Console.WriteLine("Database backup failed" + Environment.NewLine);
                return false;
            }

            if (File.Exists(Path.Combine(fullpath, Nm + ".accde")))
            {
                File.Delete(Path.Combine(fullpath, Nm + ".accde"));
            }

            File.Copy(DBPATH.Trim() + "\\NEDBILLDT.mdf", string.Format(fullpath + "\\" + "NEDBILLDT.mdf", DateTime.Today));
            File.Copy(DBPATH.Trim() + "\\NEDBILLDT_log.ldf", string.Format(fullpath + "\\" + "NEDBILLDT_log.ldf", DateTime.Today));
            Console.WriteLine("Database BackUp Successful!! ");

            return true;
        }

        private void FrmDepartment_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
