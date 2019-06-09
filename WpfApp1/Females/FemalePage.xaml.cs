using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;

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

            ValidateEventButtons(female);
        }
        private void SetFemaleInfo(DatabaseFirst.Females female)
        {
            CodeLabel.Content = female.code;
        }


        private void ValidateEventButtons(DatabaseFirst.Females female)
        {
            try
            {
                InseminationButton.IsEnabled = female.status == "Abortada" ||
                                               female.status == "Destetada" ||
                                               female.status == "Normal";

                PregnatButton.IsEnabled = female.status == "Inseminada";

                BirthButton.IsEnabled = female.status == "Preñada";

                MisbirthButton.IsEnabled = female.status == "Inseminada" ||
                                           female.status == "Preñada";

                WeaningButton.IsEnabled = female.status == "Madre";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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

            addInsemination.StatusModified += OnFemaleStatusChanged;
        }

        private void SickButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var addSick = new AddSick(_female);
            AddUserControlToEventDialog(addSick);
            addSick.SickAdded += SicksPage.OnItemAdded;

        }

        private void WeaningButton_Click(object sender, RoutedEventArgs e)
        {
            var usc = new AddWeaning(_female);
            AddUserControlToEventDialog(usc);

            usc.StatusModified += OnFemaleStatusChanged;
            usc.BirthModified += Births.OnBirthEdited;
        }

        private void BirthButtonClick(object sender, RoutedEventArgs e)
        {
            var usc = new AddBirth(_female);
            AddUserControlToEventDialog(usc);

            usc.BirthAdded += Births.OnBirthAdded;

            usc.StatusModified += OnFemaleStatusChanged;
        }


        private void PregnatFemale_OnClick(object sender, RoutedEventArgs e)
        {
            var usc = new PregnatFemale(_female);
            AddUserControlToEventDialog(usc);

            usc.StatusModified += OnFemaleStatusChanged;

            usc.InseminationModified += InseminationsPage.OnInseminationEdited;
        }

        private void Misbirth_OnClick(object sender, RoutedEventArgs e)
        {
            var usc = new MisbirthEvent(_female);

            AddUserControlToEventDialog(usc);

            usc.StatusModified += OnFemaleStatusChanged;

            usc.InseminationModified += InseminationsPage.OnInseminationEdited;
        }

        private void OnFemaleStatusChanged(object obj, FemalesEventArgs e)
        {
            ValidateEventButtons(e.Female);
        }
    }
}
