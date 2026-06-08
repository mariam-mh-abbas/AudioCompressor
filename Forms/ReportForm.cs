using AudioCompressor.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace AudioCompressor
{
    public partial class ReportForm : Form
    {
        private CompressionReport _report;

        public ReportForm(CompressionReport report)
        {
            InitializeComponent();
            _report = report;
            DisplayReport();
        }

        private void DisplayReport()
        {
            txtReport.Text = _report.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Export Report";
                sfd.Filter = "Text Files|*.txt";
                sfd.FileName = "compression_report.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, _report.ToString());
                    MessageBox.Show($"Report exported to:\n{sfd.FileName}", "Export Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
