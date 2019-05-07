using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Females.BirthViews;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for FemalePage.xaml
    /// </summary>
    public partial class FemalePage : UserControl
    {
        private readonly DatabaseFirst.Females _female;
        private ObservableCollection<Births> _birthsObservable;

        public FemalePage()
        {
            InitializeComponent();
        }

        public FemalePage(DatabaseFirst.Females female)
        {
            _female = female;
            _birthsObservable = new ObservableCollection<Births>();
            InitializeComponent();
            SetFemaleInfo(female);
            GetBirthsFromDatabase();
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
            AddRange(_birthsObservable, unitOfWork.Births.GetBirthsByFemale(_female.code) as IEnumerable<Births>);
            BirthsListView.ItemsSource = _birthsObservable;
        }
        private void SetFemaleInfo(DatabaseFirst.Females female)
        {
            CodeLabel.Content = female.code;
        }
        private void AddUserControlToEventDialog(UserControl userControl)
        {
            FemaleEventDialog.IsOpen = true;
            MainGridEvent.Children.Clear();
            MainGridEvent.Children.Add(userControl);
        }

        private void InseminationButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new AddInsemination());
        }

        private void SickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new AddSick());
        }

        private void WeaningButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new AddWeaning());
        }

        private void BirthButtonClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new AddBirth(_female, _birthsObservable));
        }

        private void EditBirthButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new EditBirth());
        }


        private void DeleteBirthButton_Onclick(object sender, RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new DeleteBirth(BirthsListView.SelectedIndex, _female.code, _birthsObservable));
        }
    }
}
