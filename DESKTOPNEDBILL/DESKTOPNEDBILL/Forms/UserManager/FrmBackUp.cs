using DESKTOPNEDBILL.Forms.Main;
using DESKTOPNEDBILL.Module;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DESKTOPNEDBILL.Forms.UserManager
{
    public partial class FrmBackUp : Form
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
        public FrmBackUp()
        {
            InitializeComponent();
            btnClose.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClose.Width, btnClose.Height, 5, 5));
            btnBackup.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnBackup.Width, btnBackup.Height, 5, 5));
            btnRestore.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRestore.Width, btnRestore.Height, 5, 5));
            btnBrowseFolder.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnBrowseFolder.Width, btnBrowseFolder.Height, 5, 5));
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            FBD.ShowDialog();
            txtBackupFolderPath.Text = FBD.SelectedPath.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            string localDbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\";
            
            string localDbPath1 = Application.StartupPath;
            string localDbPath2 = System.IO.Directory.GetCurrentDirectory();
            label4.Text = localDbPath;
            BackupDatabase();
        }
        private bool BackupDatabase()
        {
            string SourceDBPATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Apps\\2.0\\";
            //string SourceDBPATH = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Microsoft\\Microsoft SQL Server Local DB\\Instances\\MSSQLLocalDB";

            if (txtBackupFolderPath.Text != "")
            {
                //string SourceDBPATH = AppDomain.CurrentDomain.BaseDirectory;
                string DBPATH = txtBackupFolderPath.Text;//AppDomain.CurrentDomain.BaseDirectory;
                string Nm = DateTime.Now.ToString("dd.MM.yyyy-HH.mm");
                string foldername = MdlMain.gCompanyName + "_BackUpDB"; // "BackUp"
                string fullpath = Path.Combine(DBPATH, foldername);
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
                        rtxtBackUpStatus.AppendText("BackUp Folder Created " + fullpath + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    rtxtBackUpStatus.AppendText(ex.Message + Environment.NewLine);
                    rtxtBackUpStatus.AppendText("Database backup failed" + Environment.NewLine);
                    return false;
                }
                File.Copy(SourceDBPATH.Trim() + "\\NEDBILLDT.mdf", string.Format(fullpath + "\\" + "NEDBILLDT.mdf", DateTime.Today));
                File.Copy(SourceDBPATH.Trim() + "\\NEDBILLDT_log.ldf", string.Format(fullpath + "\\" + "NEDBILLDT_log.ldf", DateTime.Today));
                rtxtBackUpStatus.AppendText("Database BackUp Successful!!" + Environment.NewLine);
                return true;
            }
            else
            {
                MessageBox.Show("Please select a folder to proceed backup.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBackUp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }

        private void FrmBackUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FrmMain)this.MdiParent).MakeSidePanelVisible();
            MdlMain.UpdateUserAccessDetails();
        }
    }
}
