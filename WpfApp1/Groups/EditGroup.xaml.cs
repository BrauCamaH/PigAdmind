using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Groups
{
	/// <summary>
	/// Interaction logic for EditGroup.xaml
	/// </summary>
	public partial class EditGroup : UserControl
	{
		public EditGroup()
		{
			InitializeComponent();
		}

		public EditGroup(int group)
		{

		}

		private void CheckBox_checked(object sender, RoutedEventArgs e)
		{
			Accept_btn.IsEnabled = true;
		}

		private void CheckBox_unchecked(object sender, RoutedEventArgs e)
		{
			Accept_btn.IsEnabled = false;
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Confirm_checkbox.IsChecked = false;
		}
	}
}
