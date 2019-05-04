using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Females.BirthViews
{
	/// <summary>
	/// Interaction logic for EditBirth.xaml
	/// </summary>
	public partial class EditBirth : UserControl
	{
		public EditBirth()
		{
			InitializeComponent();
		}
		private void CheckBox_checked(object sender, RoutedEventArgs e)
		{
			Accept_btn.IsEnabled = true;
		}

		private void CheckBox_unchecked(object sender, RoutedEventArgs e)
		{
			Accept_btn.IsEnabled = false;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Confirm_checkbox.IsChecked = false;
		}
	}
}
