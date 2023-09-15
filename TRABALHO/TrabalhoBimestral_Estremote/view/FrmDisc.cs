using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoBimestral_Estremote.view
{
    public partial class FrmDisc : Form
    {
        private object reportViewer1;
        DataTable dt;

        public FrmDisc(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void FrmDisc_Load(object sender, EventArgs e)
        {
            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt));

            this.reportViewer2.RefreshReport();
        }
    }
}
