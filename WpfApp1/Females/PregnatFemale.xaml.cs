using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
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

        public event EventHandler<FemalesEventArgs> StatusModified;

        public event EventHandler<InseminationsEventArgs> InseminationModified;

        private DatabaseFirst.Females _female;
        public PregnatFemale(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();

            _observableCollection = new ObservableCollection<Inseminations>();
            GetActualInsemination();
        }

        public virtual void OnStatusModified(DatabaseFirst.Females female)
        {
            StatusModified?.Invoke(this, new FemalesEventArgs { Female = female });
        }

        public virtual void OnInseminationModified(Inseminations insemination)
        {
            InseminationModified?.Invoke(this, new InseminationsEventArgs { Insemination = insemination });
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

            var insemination = unitOfWork.Inseminations.GetCurrentInsemination(_female);

            insemination.status = "Exitosa";

            female.status = "Preñada";

            unitOfWork.Complete();

            OnStatusModified(female);
            OnInseminationModified(insemination);
        }
    }
}
