using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddInsemination.xaml
    /// </summary>
    public partial class AddInsemination : UserControl
    {
        private DatabaseFirst.Females _female;

        public event EventHandler<InseminationsEventArgs> InseminationAdded;
        public AddInsemination()
        {
            InitializeComponent();
        }
        public virtual void OnBirthAdded(Inseminations insemination)
        {
            InseminationAdded?.Invoke(this, new InseminationsEventArgs { Insemination = insemination });

        }
        public AddInsemination(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;

        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new Entities();
            var insemination = new Inseminations()
            {
                fem_code = _female.code,
                male_code = TextBoxCode.Text,
                date = DatePicker.Text,
                status = "Actual"
            };
            unitOfWork.Inseminations.Add(insemination);
            unitOfWork.SaveChanges();

            OnBirthAdded(insemination);
        }
    }
}
