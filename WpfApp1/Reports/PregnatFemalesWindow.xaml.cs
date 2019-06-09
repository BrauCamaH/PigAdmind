using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Reports
{
    /// <summary>
    /// Interaction logic for PregnatFemalesWindow.xaml
    /// </summary>
    public partial class PregnatFemalesWindow : Window
    {
        public PregnatFemalesWindow()
        {
            InitializeComponent();
        }

        private void PregnatFemalesWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                report.Load("C:\\Users\\ACER\\Documents\\PigAdmind\\WpfApp1\\Reports\\FemalesReport.rpt");

                var unitOfWork = new UnitOfWork(new Entities());
                report.SetDataSource(unitOfWork.Females.Find(i => i.status == "Preñada"));
                CrystalReportsViewer.ViewerCore.ReportSource = report;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
