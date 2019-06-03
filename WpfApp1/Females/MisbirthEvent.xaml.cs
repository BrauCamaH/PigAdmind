using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Lógica de interacción para MisbirthEvent.xaml
    /// </summary>
    public partial class MisbirthEvent : UserControl
    {
        private DatabaseFirst.Females _female;
        public MisbirthEvent(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            UnitOfWork unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);

            unitOfWork.Inseminations.GetCurrentInsemination(_female).status = "Fallida";
            female.status = "Abortada";
            unitOfWork.Complete();
        }
    }
}
