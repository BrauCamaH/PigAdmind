using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Interaction logic for DeleteSelectedBirth.xaml
    /// </summary>
    public partial class DeleteBirth : UserControl
    {
        private Births _birth;
        private ObservableCollection<Births> _observableCollection;
        public DeleteBirth()
        {
            InitializeComponent();

        }
        public DeleteBirth(Births births, ObservableCollection<Births> observableC)
        {
            InitializeComponent();
            NotifyUserAgree.AcceptButton = Accept_btn;
            _observableCollection = observableC;
            _birth = births;
        }
        private void RemoveItemFromList(ObservableCollection<Births> collection, Births currentBirths)
        {
            collection.Remove(collection.Single(i => i.id == _birth.id));
        }
        private void Delete()
        {
            if (_observableCollection != null && _observableCollection.Count > 0)
            {
                var unitOfWork = new UnitOfWork(new Entities());
                unitOfWork.Births.RemoveByID(_birth.id);
                RemoveItemFromList(_observableCollection, _birth);
                unitOfWork.Complete();
            }

        }

        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

    }
}
