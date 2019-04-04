using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Delete.xaml
	/// </summary>
	public partial class Delete : UserControl
	{
		public Delete()
		{
			InitializeComponent();
		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			Btn_Aceptar.IsEnabled = true;
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Btn_Aceptar.IsEnabled = true;
		}
	}
}
