using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Managers;
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
			GetFemale();

		}

		private void EditFemaleFromDatabase(string newCode, string newDate)
		{
			_unitOfWork = new UnitOfWork(new Entities());
			_female = _unitOfWork.Females.GetAll().ToList()[ContextManager.Instance().CurrentSelectedItem];
			_female.code = newCode;
			_female.birthday = newDate;
			_unitOfWork.Complete();
		}

		private void GetFemale()
		{
			_unitOfWork = new UnitOfWork(new Entities());
			if (_unitOfWork.Females.GetAll().Count() != 0)
			{
				_female = _unitOfWork.Females.GetAll().ToList()[ContextManager.Instance().CurrentSelectedItem];
				CodeTextBox.Text = _female.code;
				DatePicker.Text = _female.birthday;
			}
		}
		private void CheckBox_checked(object sender, RoutedEventArgs e)
		{
			AcceptBtn.IsEnabled = true;
		}

		private void CheckBox_unchecked(object sender, RoutedEventArgs e)
		{
			AcceptBtn.IsEnabled = false;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Confirm_checkbox.IsChecked = false;
		}

		private void AcceptBtn_OnClick(object sender, RoutedEventArgs e)
		{
			EditFemaleFromDatabase(CodeTextBox.Text, DatePicker.Text);
		}
	}
}
