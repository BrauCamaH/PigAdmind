using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
	/// <summary>
	/// Interaction logic for FemalePage.xaml
	/// </summary>
	public partial class FemalePage : UserControl
	{
		private DatabaseFirst.Females _female;

		public FemalePage()
		{
			InitializeComponent();
		}

		public FemalePage(DatabaseFirst.Females female)
		{
			_female = female;
			InitializeComponent();
			SetFemaleInfo(female);
			GetBirthsFromDatabase();
		}

		private void GetBirthsFromDatabase()
		{
			UnitOfWork unitOfWork = new UnitOfWork(new Entities());
			BirthsListView.ItemsSource = unitOfWork.Births.GetBirthsByFemale(_female.code);
		}
		private void SetFemaleInfo(DatabaseFirst.Females female)
		{
			CodeLabel.Content = female.code;
		}
		private void AddUserControlToEventDialog(UserControl userControl)
		{
			FemaleEventDialog.IsOpen = true;
			MainGridEvent.Children.Clear();
			MainGridEvent.Children.Add(userControl);
		}

		private void InseminationButtonClick(object sender, System.Windows.RoutedEventArgs e)
		{
			AddUserControlToEventDialog(new AddInsemination());
		}

		private void SickButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			AddUserControlToEventDialog(new AddSick());
		}

		private void WeaningButton_Click(object sender, RoutedEventArgs e)
		{
			AddUserControlToEventDialog(new AddWeaning());
		}

		private void BirthButtonClick(object sender, RoutedEventArgs e)
		{
			AddUserControlToEventDialog(new AddBirth(_female));
		}
	}
}
