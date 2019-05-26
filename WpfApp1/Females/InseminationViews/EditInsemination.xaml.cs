using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.InseminationViews
{
    /// <summary>
    /// Interaction logic for EditInsemination.xaml
    /// </summary>
    public partial class EditInsemination : UserControl
    {
        public Inseminations Insemination { get; private set; }

        public event EventHandler<InseminationsEventArgs> InseminationEdited;

        public EditInsemination()
        {
            InitializeComponent();
        }
        public EditInsemination(Inseminations insemination)
        {
            InitializeComponent();
            Insemination = insemination;
            GetInsemination(insemination);

            UserAgree.AcceptButton = AcceptBtn;
        }

        public virtual void OnInseminationEdited(Inseminations insemination)
        {
            var events = new InseminationsEventArgs() { Insemination = insemination };
            InseminationEdited?.Invoke(this, events);
        }


        private void GetInsemination(Inseminations insemination)
        {
            MaleCodeTextBox.Text = insemination.male_code;
            DatePicker.Text = insemination.date;
        }

        private void EditBirthFromDatabase(string newCode, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var insemination = unitOfWork.Inseminations.Get(Insemination.id);
            insemination.male_code = newCode;
            insemination.date = newDate;

            unitOfWork.Complete();

            OnInseminationEdited(insemination);

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditBirthFromDatabase(MaleCodeTextBox.Text, DatePicker.Text);
        }
    }
}
