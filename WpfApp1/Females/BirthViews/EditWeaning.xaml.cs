using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females.BirthViews
{
    /// <summary>
    /// Lógica de interacción para EditWeaning.xaml
    /// </summary>
    public partial class EditWeaning : UserControl
    {

        private Births _birth;

        public event EventHandler<BirthsEventArgs> BirthEdited;
        public EditWeaning(Births birth)
        {
            _birth = birth;
            InitializeComponent();

            GetWeaning();
        }

        public virtual void OnBirthEdited(Births birth)
        {
            var events = new BirthsEventArgs { Birth = birth };
            BirthEdited?.Invoke(this, events);
        }

        private void GetWeaning()
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var weaning = unitOfWork.Births.GetWeaning(_birth);
            NPigsTextBox.Text = weaning.weaned_pigs.ToString();

            DatePicker.Text = weaning.date;
        }

        private void EditWeaningFromDatabase(string newDate, string newPigs)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var weaning = unitOfWork.Births.GetWeaning(_birth);

            weaning.weaned_pigs = Int32.Parse(newPigs);
            weaning.date = newDate;

            unitOfWork.Complete();

            OnBirthEdited(_birth);
        }
        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            EditWeaningFromDatabase(DatePicker.Text, NPigsTextBox.Text);

        }
    }
}
