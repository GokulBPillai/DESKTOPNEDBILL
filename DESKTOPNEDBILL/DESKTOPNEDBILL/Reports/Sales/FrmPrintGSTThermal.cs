using DESKTOPNEDBILL.Module;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DESKTOPNEDBILL.Reports.Sales
{
    public partial class FrmPrintGSTThermal : Form
    {
        public FrmPrintGSTThermal()
        {
            InitializeComponent();
        }

        private void FrmPrintGSTThermal_Load(object sender, EventArgs e)
        {

            ReportDataSource reportDataSource1 = new ReportDataSource("ds_InvStkDtls", MdlMain.gDs_SalesInv1.Tables[0]);
            ReportDataSource reportDataSource2 = new ReportDataSource("ds_CompDtls", MdlMain.gDs_SalesInv1.Tables[1]);
            ReportDataSource reportDataSource3 = new ReportDataSource("ds_TaxSummDtls", MdlMain.gDs_SalesInv1.Tables[2]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }

        private void FrmPrintGSTThermal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
