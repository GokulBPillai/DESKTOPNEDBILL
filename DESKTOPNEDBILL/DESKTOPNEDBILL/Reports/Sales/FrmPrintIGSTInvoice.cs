using DESKTOPNEDBILL.Module;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace DESKTOPNEDBILL.Reports.Sales
{
    public partial class FrmPrintIGSTInvoice : Form
    {
        public FrmPrintIGSTInvoice()
        {
            InitializeComponent();
        }

        private void FrmPrintIGSTInvoice_Load(object sender, EventArgs e)
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

        private void FrmPrintIGSTInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
