using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Females.InseminationViews
{
	/// <summary>
	/// Interaction logic for EditInsemination.xaml
	/// </summary>
	public partial class EditInsemination : UserControl
	{
		public EditInsemination()
		{
			InitializeComponent();
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
			ConfirmCheckbox.IsChecked = false;
		}
	}
}
