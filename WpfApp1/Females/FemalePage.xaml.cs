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
            InseminationsPage.SetFemale(_female);
            SicksPage.SetFemale(_female);
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
            var addInsemination = new AddInsemination(_female);
            AddUserControlToEventDialog(addInsemination);

            addInsemination.InseminationAdded += InseminationsPage.OnInseminationAdded;
        }

        private void SickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var addSick = new AddSick(_female);
            AddUserControlToEventDialog(addSick);

            addSick.SickAdded += SicksPage.OnItemAdded;
        }

        private void WeaningButton_Click(object sender, RoutedEventArgs e)
        {
            AddUserControlToEventDialog(new AddWeaning());
        }

        private void BirthButtonClick(object sender, RoutedEventArgs e)
        {
            var addBirth = new AddBirth(_female);
            AddUserControlToEventDialog(addBirth);

            addBirth.BirthAdded += Births.OnBirthAdded;
        }


    }
}
