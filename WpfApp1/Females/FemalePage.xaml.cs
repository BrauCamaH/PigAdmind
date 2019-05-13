using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for FemalePage.xaml
    /// </summary>
    public partial class FemalePage : UserControl
    {
        private readonly DatabaseFirst.Females _female;

        public FemalePage()
        {
            InitializeComponent();
        }

        public FemalePage(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
            SetFemaleInfo(female);
            Births.SetFemale(_female);
        }

        public static void AddRange<T>(ObservableCollection<T> coll, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                coll.Add(item);
            }
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
            AddUserControlToEventDialog(new AddBirth(_female, Births.BirthsObservable));
        }
    }
}
