using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
    /// <summary>
    /// Interaction logic for EditFemale.xaml
    /// </summary>
    public partial class EditFemale : UserControl
    {
        private DatabaseFirst.Females _female;
        private UnitOfWork _unitOfWork;
        public EditFemale()
        {
            InitializeComponent();

        }
        public EditFemale(DatabaseFirst.Females female)
        {
            InitializeComponent();
            _female = female;
            GetFemale(female);
            NotifyUserAgree.AcceptButton = AcceptBtn;

        }

        private void EditFemaleFromDatabase(string newCode, string newDate)
        {
            _unitOfWork = new UnitOfWork(new Entities());
            _female.code = newCode;
            _female.birthday = newDate;
            _unitOfWork.Complete();
        }

        private void GetFemale(DatabaseFirst.Females female)
        {
            _unitOfWork = new UnitOfWork(new Entities());
            if (_unitOfWork.Females.GetAll().Count() != 0)
            {
                CodeTextBox.Text = female.code;
                DatePicker.Text = female.birthday;
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditFemaleFromDatabase(CodeTextBox.Text, DatePicker.Text);
        }
    }
}
