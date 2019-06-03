using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para PregnatFemale.xaml
    /// </summary>
    public partial class PregnatFemale : UserControl
    {

        private ObservableCollection<Inseminations> _observableCollection;


        private DatabaseFirst.Females _female;
        public PregnatFemale(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();

            _observableCollection = new ObservableCollection<Inseminations>();
            GetActualInsemination();
        }

        private void GetActualInsemination()
        {
            try
            {
                var unitOfWork = new UnitOfWork(new Entities());
                Inseminations insemination = unitOfWork.Inseminations.SingleOrDefault(i => i.status == "Actual" && i.fem_code == _female.code);

                _observableCollection.Add(insemination);
                InseminationsListView.ItemsSource = _observableCollection;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);

            female.status = "Embarazada";
            unitOfWork.Complete();
        }
    }
}
