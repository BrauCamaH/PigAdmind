using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para AddImprovementDay.xaml
    /// </summary>
    public partial class AddImprovementDay : UserControl
    {
        private Sicks _sick;

        public event EventHandler<SicksEventArgs> SickEdited;

        public AddImprovementDay(Sicks sick)
        {
            _sick = sick;
            InitializeComponent();
        }

        public virtual void OnSickEdited(Sicks sick)
        {
            var events = new SicksEventArgs() { Sick = sick };
            SickEdited?.Invoke(this, events);
        }

        private void SetImprovementDay(string date)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var sick = unitOfWork.Sicks.Get(_sick.id);

            sick.improvement_date = date;
            unitOfWork.Complete();

            OnSickEdited(sick);
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            SetImprovementDay(DatePicker.Text);
        }
    }
}
