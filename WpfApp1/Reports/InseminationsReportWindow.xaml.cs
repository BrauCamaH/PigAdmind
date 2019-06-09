using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Reports
{
    /// <summary>
    /// Interaction logic for InseminationsReportWindow.xaml
    /// </summary>
    public partial class InseminationsReportWindow : Window
    {
        public InseminationsReportWindow()
        {
            InitializeComponent();
        }

        private void InseminationsReportWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\InseminationsReport.rpt");

                var unitOfWork = new UnitOfWork(new Entities());
                report.SetDataSource(unitOfWork.Inseminations.GetAll());
                CrystalReportsViewer.ViewerCore.ReportSource = report;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }


}
