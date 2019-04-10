using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Sales
{
	/// <summary>
	/// Interaction logic for EditSale.xaml
	/// </summary>
	public partial class EditSale : UserControl
	{
		public EditSale()
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
