using System.Windows;
using System.Windows.Controls;
using WpfApp1.Interfaces;
using WpfApp1.Managers;

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
			Accept_btn.IsEnabled = true;
		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			Accept_btn.IsEnabled = false;
		}

		private void Accept_Btn_Click(object sender, RoutedEventArgs e)
		{
			CheckBox.IsChecked = false;
			((IRemovable)ContextManager.Instance().CurrentContext).RemoveSelectedItem();
		}
	}
}
