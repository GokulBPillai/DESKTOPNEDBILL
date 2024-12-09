using System.ComponentModel;
using System.Windows.Forms;
using TableDims.Data;
using SoftwareLock;
using Microsoft.Win32;
using System.Threading;
using System;
using System.Collections.Generic;
using TableDims.Models;
using System.Linq;
using DESKTOPNEDBILL.Forms.UserManager;
using DESKTOPNEDBILL.Forms.Permission;
using DESKTOPNEDBILL.Module;
using System.Runtime.InteropServices;
namespace DESKTOPNEDBILL.Forms.Main
{
    public partial class FrmSplashScreen : Form
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
        static bool IsReg = true;
        public FrmSplashScreen()
        {
            InitializeComponent();            
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 80; i++)
            {
                IsReg = IsSoftwareRegistered();
                backgroundWorker1.ReportProgress(i);
            }
        }
        public bool IsSoftwareRegistered()
        {
            try
            {
                Thread.Sleep(100);
                string _AppName = "NedMultiAcc";
                string _Password = "nedmultiacc";
                string _DiskSerial = HDDSerial.SerialNumber();
                string _SerialKey = SoftwareLock.SoftwareLock.GetSerialKey(_AppName, _Password, _DiskSerial);
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Nedcom", true);
                string strSerialNo = (string)(key.GetValue("SerialNo"));
                if (strSerialNo == _SerialKey)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            ProgressbarPerLabel.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (IsReg)
            {
                List<CompanyProfile> cmpProfiles = cmpDBContext.CompanyProfiles.ToList();
                if (cmpProfiles.Count > 0)
                {
                    this.Hide();
                    FrmLogin frmLogin = new FrmLogin();
                    frmLogin.Closed += (s, args) => this.Close();
                    frmLogin.ShowDialog();
                }
                else
                {
                    SetInitalValues();
                    this.Hide();
                    CompanyProfiles company = new CompanyProfiles();
                    company.Closed += (s, args) => this.Close();
                    company.ShowDialog();
                }
            }
            else
            {
                this.Hide();
                FrmSoftwareRegistration frmSoftwareRegistration = new FrmSoftwareRegistration();
                frmSoftwareRegistration.Closed += (s, args) => this.Close();
                frmSoftwareRegistration.ShowDialog();
            }
        }
        private void SetInitalValues()
        {
            MdlMain.AddInitialBankCash();
            MdlMain.AddInitialCompanyGstType();
            MdlMain.AddInitialCustomer();
            MdlMain.AddInitialVendor();
            MdlMain.AddInitialDepartment();            
            MdlMain.AddInitialEmployeePosition();
            MdlMain.AddInitialEmployee();
            MdlMain.AddInitialRoleMaster();
            MdlMain.AddInitialUnitMeasures();            
            MdlMain.AddStateCode();
            MdlMain.AddInitialGlAccount();
            MdlMain.AddInitialInventoryType();
            MdlMain.AddInitialInventoryCategory();
        }
    }
}
