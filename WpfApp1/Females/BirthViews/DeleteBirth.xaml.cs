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
        private int _index;
        private string _femaleCode;
        private ObservableCollection<Births> _observableCollection;
        public DeleteBirth()
        {
            InitializeComponent();
        }
        public DeleteBirth(int index, string femaleCode, ObservableCollection<Births> observableC)
        {
            InitializeComponent();
            _index = index;
            _observableCollection = observableC;
            _femaleCode = femaleCode;
        }

        private void Delete()
        {
            if (_observableCollection != null && _observableCollection.Count > 0)
            {
                var unitOfWork = new UnitOfWork(new Entities());
                unitOfWork.Births.Remove(unitOfWork.Births.GetAll().ToList()[_index]);
                _observableCollection.RemoveAt(_index);
                unitOfWork.Complete();
            }

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Accept_btn.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Accept_btn.IsEnabled = false;
        }


        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            CheckBox.IsChecked = false;
            Delete();
        }

    }
}
