using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

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

            ValidateEventButtons();
        }
        private void SetFemaleInfo(DatabaseFirst.Females female)
        {
            CodeLabel.Content = female.code;
        }


        private void ValidateEventButtons()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());

            PregnatButton.IsEnabled =
                unitOfWork.Inseminations.GetCurrentInsemination(_female) != null;

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
            addInsemination.InseminationAdded += OnInseminationAdded;
        }

        private void OnInseminationAdded(object sender, InseminationsEventArgs e)
        {
            PregnatButton.IsEnabled = true;
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


        private void PregnatFemale_OnClick(object sender, RoutedEventArgs e)
        {
            var us = new PregnatFemale(_female);
            AddUserControlToEventDialog(us);
        }

        private void Misbirth_OnClick(object sender, RoutedEventArgs e)
        {
            var us = new MisbirthEvent(_female);

            AddUserControlToEventDialog(us);
        }
    }
}
