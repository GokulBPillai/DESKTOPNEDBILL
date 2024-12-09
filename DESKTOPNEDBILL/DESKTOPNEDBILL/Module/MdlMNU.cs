using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableDims.Data;
using TableDims.Models;

namespace DESKTOPNEDBILL.Module
{
    public class MdlMNU
    {

        static CMPDBContext cmpDBContext = new CMPDBContext();
        public static List<string> CtrlMnuTxt()
        {
            List<string> BttnCap = new List<string> { "Select Company", "New Company", "Delete Company", "E X I T" };
            return BttnCap;
        }
        public static void CtrlMnuAct(string MnuTxt)
        {
            try
            {
                switch (MnuTxt)
                {
                    //case "Select Company":
                    //    var frmCompanySelect = new FrmCompanySelect();
                    //    frmCompanySelect.Show();
                    //    break;
                    //case "New Company":
                    //    FrmLogin frmLoginpage = new FrmLogin();
                    //    frmLoginpage.Show();
                    //    break;
                    case "E X I T":
                        Application.Exit();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool IsCompaniesExist()
        {
            List<CompanyProfile> cmpProfiles = cmpDBContext.CompanyProfiles.ToList();
            if (cmpProfiles.Count > 0)
            {
                return true;
            }
            else
            {
                //MessageBox.Show("No Company details found !!!, Please click 'New Company' button to add a Company.", "NEDSOFT", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public static CompanyProfile AddDefaultCompany()
        {
            try
            {
                int currentYear = DateTime.Now.Year;
                int nextYear = DateTime.Now.AddYears(1).Year;
                DateTime FStart = new DateTime(currentYear, 4, 1);
                DateTime FEnd = new DateTime(nextYear, 3, 31);
                int FYDir1 = currentYear % 100;
                int FYDir2 = nextYear % 100;
                var cmpProfiles = new CompanyProfile()
                {
                    CompanyName = "MyCompany",
                    Address1 = "MyAdd1",
                    Address2 = "MyAdd2",
                    City = "Adoor",
                    PIN = "691523",
                    PhoneNo = "9847131135",
                    Country = "India",
                    GSTNo = "",
                    EmailId = "gokulb14@gmail.com"
                };
                cmpDBContext.CompanyProfiles.Add(cmpProfiles);
                cmpDBContext.SaveChanges();
                var fyearTrans = new FYearTrans
                {
                    CompanyId = 1,
                    FYStart = FStart,
                    FYEnd = FEnd,
                    FYear = FStart.ToShortDateString() + " - " + FEnd.ToShortDateString(),
                    FYearDir = FYDir1.ToString() + FYDir2.ToString(),
                    DatabaseName = "BIMS" + FYDir1.ToString() + FYDir2.ToString(),
                    ServerName = "SQL//2017",
                    YearEndStatus = "No"
                };
                cmpDBContext.FYearTranss.Add(fyearTrans);
                cmpDBContext.SaveChanges();
                return cmpProfiles;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
