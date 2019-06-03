using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

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
        public virtual void OnInseminationAdded(Inseminations insemination, Inseminations last)
        {
            InseminationAdded?.Invoke(this, new InseminationsEventArgs { Insemination = insemination, LastInsemination = last });

        }
        public AddInsemination(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;

        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);
            var insemination = new Inseminations()
            {
                fem_code = _female.code,
                male_code = TextBoxCode.Text,
                date = DatePicker.Text,
                status = "Actual"
            };


            if (unitOfWork.Inseminations.GetCurrentInsemination(_female) != null)
            {
                unitOfWork.Inseminations.GetCurrentInsemination(_female).status = "Fallido";
            }

            unitOfWork.Inseminations.Add(insemination);

            female.status = "Inseminada";

            OnInseminationAdded(insemination, unitOfWork.Inseminations.GetCurrentInsemination(_female));

            unitOfWork.Complete();
        }
    }
}
