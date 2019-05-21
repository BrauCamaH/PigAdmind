using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for AddInsemination.xaml
    /// </summary>
    public partial class AddInsemination : UserControl
    {
        private DatabaseFirst.Females _female;
        private ObservableCollection<Inseminations> _observableCollection;

        public AddInsemination()
        {
            InitializeComponent();
        }

        public AddInsemination(DatabaseFirst.Females female, ObservableCollection<Inseminations> births)
        {
            InitializeComponent();
            _female = female;
            _observableCollection = births;

        }
        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            var unitOfWork = new Entities();
            var insemination = new Inseminations()
            {
                date = DatePicker.Text,
                male_code = TextBoxCode.Text
            };
            unitOfWork.Inseminations.Add(insemination);
            _observableCollection.Add(insemination);
            unitOfWork.SaveChanges();
        }
    }
}
