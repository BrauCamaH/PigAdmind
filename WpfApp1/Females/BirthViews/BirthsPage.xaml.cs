using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Lógica de interacción para BirthsPage.xaml
    /// </summary>
    public partial class BirthsPage : UserControl
    {
        public ObservableCollection<Births> BirthsObservable { get; set; }
        public DatabaseFirst.Females Female { get; private set; }

        public Births CurrentBirth { get; private set; }

        public BirthsPage()
        {
            InitializeComponent();
            BirthsObservable = new ObservableCollection<Births>();
        }

        public void SetFemale(DatabaseFirst.Females female)
        {
            Female = female;
            GetBirthsFromDatabase();
        }
        private void EditBirthButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DeleteBirthButton_Onclick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public static void AddRange<T>(ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
        }

        private void GetBirthsFromDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            AddRange(BirthsObservable, unitOfWork.Births.GetBirthsByFemale(Female.code) as IEnumerable<Births>);
            BirthsListView.ItemsSource = BirthsObservable;
        }
    }
}
