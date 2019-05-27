using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddSick.xaml
    /// </summary>
    public partial class AddSick : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<SicksEventArgs> SickAdded;

        public AddSick()
        {
            InitializeComponent();
        }

        public AddSick(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;
        }

        public virtual void OnSickAdded(Sicks sick)
        {
            SickAdded?.Invoke(this, new SicksEventArgs { Sick = sick });
        }

        private void AcceptButton_OnClick(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var sick = new Sicks
            {
                date = DatePicker.Text,
                name = SickNameTextBox.Text,
                improvement_date = "Ninguna"
            };

            unitOfWork.Sicks.Add(sick);

            unitOfWork.Complete();

            OnSickAdded(sick);

        }
    }
}
