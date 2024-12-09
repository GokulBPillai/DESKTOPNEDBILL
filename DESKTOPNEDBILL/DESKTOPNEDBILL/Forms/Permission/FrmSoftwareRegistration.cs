using DESKTOPNEDBILL.Forms.UserManager;
using Microsoft.Win32;
using SoftwareLock;
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

namespace DESKTOPNEDBILL.Forms.Permission
{
    public partial class FrmSoftwareRegistration : Form
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
        string _AppName = "NedMultiAcc";
        string _Password = "nedmultiacc";
        string _DiskSerial = HDDSerial.SerialNumber();
        string _SerialKey = "";
        CMPDBContext cmpDBContext = new CMPDBContext();
        public FrmSoftwareRegistration()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            btnSubmit.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSubmit.Width, btnSubmit.Height, 5, 5));
        }
        private void FrmSoftwareRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnClose_Click(sender, e);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text != null && txtSerialKey.Text != null)
            {
                bool IsReg = RegisterSoftware(txtSerialKey.Text.Trim());
                if (IsReg)
                {
                    MessageBox.Show("Activation Successful!!!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    List<CompanyProfile> cmpProfiles = cmpDBContext.CompanyProfiles.ToList();
                    if (cmpProfiles.Count > 0)
                    {
                        FrmLogin frmLogin = new FrmLogin();
                        frmLogin.Closed += (s, args) => this.Close();
                        frmLogin.ShowDialog();
                    }
                    else
                    {
                        CompanyProfiles company = new CompanyProfiles();
                        company.Closed += (s, args) => this.Close();
                        company.ShowDialog();
                    }
                }
            }
        }
        private void FrmSoftwareRegistration_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            txtReferenceNo.Text = GetReferenceCode();
        }
        public string GetReferenceCode()
        {
            try
            {
                return SoftwareLock.SoftwareLock.GetReferenceKey(_AppName, _Password, _DiskSerial);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RegisterSoftware(string SerialKey)
        {
            try
            {
                _SerialKey = SoftwareLock.SoftwareLock.GetSerialKey(_AppName, _Password, _DiskSerial);
                if (SerialKey == _SerialKey)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
                    key?.CreateSubKey("Nedcom", true);
                    key = key?.OpenSubKey("Nedcom", true);
                    key?.SetValue("SerialNo", SerialKey);
                    key?.SetValue("AppVersion", "1.0.0");
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
