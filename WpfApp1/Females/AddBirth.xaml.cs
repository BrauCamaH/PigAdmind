using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddBirth.xaml
    /// </summary>
    public partial class AddBirth : UserControl
    {
        private DatabaseFirst.Females _female;
        private ObservableCollection<Births> _observableCollection;

        public AddBirth()
        {
            InitializeComponent();
        }

        public AddBirth(DatabaseFirst.Females female, ObservableCollection<Births> births)
        {
            InitializeComponent();
            _female = female;
            _observableCollection = births;
        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var birth = new Births()
            {
                n_piglets = Int32.Parse(N_Pigs_TextBox.Text),
                date = Date.Text,
                fem_code = _female.code
            };
            unitOfWork.Births.Add(birth);
            _observableCollection.Add(birth);
            unitOfWork.Complete();
        }
    }
}
