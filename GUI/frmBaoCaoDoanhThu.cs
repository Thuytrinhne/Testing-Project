using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using BLL;
using System.Globalization;



namespace GUI
{
    public partial class frmBaoCaoDoanhThu : Form
    {

        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

      

        private void BaoCaoDoanhThu_Load_1(object sender, EventArgs e)
        {
            var culture = CultureInfo.GetCultureInfo("vi-VN");

            //Culture for any thread
            CultureInfo.DefaultThreadCurrentCulture = culture;

            //Culture for UI in any thread
            CultureInfo.DefaultThreadCurrentUICulture = culture;


            int t = BillBLL.hienThiTongDanhThu(frmAdmin.start, frmAdmin.end);
            DataTable dataTable = BillBLL.HienThiDoanhThuForReport(frmAdmin.start, frmAdmin.end);
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataTable));
            reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Report1.rdlc";

            // para
            ReportParameterCollection p = new ReportParameterCollection();
            p.Add(new ReportParameter("ReportParameter1", t.ToString("c", culture)));
            reportViewer1.LocalReport.SetParameters(p);

           reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }
    }
}
