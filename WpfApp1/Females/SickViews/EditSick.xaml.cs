using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.SickViews
{
    /// <summary>
    /// Interaction logic for EditSick.xaml
    /// </summary>
    public partial class EditSick : UserControl
    {
        private Sicks _sick;

        public event EventHandler<SicksEventArgs> SickEdited;
        public EditSick()
        {
            InitializeComponent();
        }
        public EditSick(Sicks sick)
        {
            InitializeComponent();
            _sick = sick;

            GetSick();
            IfHaveImprovementDay(sick);
            UserAgree.AcceptButton = AcceptBtn;
        }

        public virtual void OnSickEdited(Sicks sick)
        {
            var events = new SicksEventArgs() { Sick = sick };
            SickEdited?.Invoke(this, events);
        }
        public void GetSick()
        {
            SickNameTextBox.Text = _sick.name;
            SickDatePicker.Text = _sick.date;
            if (IfHaveImprovementDay(_sick))
            {
                ImprovementDatePicker.Text = _sick.improvement_date;
            }
        }

        private void EditSickFromdatabase(string newName, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var sick = unitOfWork.Sicks.Get(_sick.id);
            sick.name = newName;
            sick.date = newDate;

            if (IfHaveImprovementDay(sick))
            {
                sick.improvement_date = ImprovementDatePicker.Text;
            }

            OnSickEdited(sick);

        }

        private bool IfHaveImprovementDay(Sicks sick)
        {
            if (sick.improvement_date == "Ninguna")
            {
                ImprovementDatePicker.Visibility = Visibility.Collapsed;
                return false;
            }

            ImprovementDatePicker.Visibility = Visibility.Visible;

            return true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditSickFromdatabase(SickNameTextBox.Text, SickDatePicker.Text);
        }
    }
}
