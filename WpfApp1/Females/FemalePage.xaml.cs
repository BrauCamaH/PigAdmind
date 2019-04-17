using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Females
{
	/// <summary>
	/// Interaction logic for FemalePage.xaml
	/// </summary>
	public partial class FemalePage : UserControl
	{
		public FemalePage()
		{
			InitializeComponent();
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
			AddUserControlToEventDialog(new AddBirth());
		}
	}
}
