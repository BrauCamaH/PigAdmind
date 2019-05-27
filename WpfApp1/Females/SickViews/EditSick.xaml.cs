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
        }

        private void EditSickFromdatabase(string newName, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var sick = unitOfWork.Sicks.Get(_sick.id);
            sick.name = newName;
            sick.date = newDate;

            OnSickEdited(sick);

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
