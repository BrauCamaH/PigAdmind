using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddWeaning.xaml
    /// </summary>
    public partial class AddWeaning : UserControl
    {
        private DatabaseFirst.Females _female;
        public AddWeaning(DatabaseFirst.Females female)
        {
            _female = female;
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var female = unitOfWork.Females.GetFemaleByCode(_female.code);

            female.status = "Destetada";
            unitOfWork.Complete();
        }
    }
}
