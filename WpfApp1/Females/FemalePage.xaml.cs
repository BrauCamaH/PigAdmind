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

		private void InseminationButtonClick(object sender, System.Windows.RoutedEventArgs e)
		{
			InseminationDialog.IsOpen = true;
		}

		private void SickButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SickDialog.IsOpen = true;
		}

		private void WeaningButton_Click(object sender, RoutedEventArgs e)
		{
			WeaningDialog.IsOpen = true;
		}

		private void BirthButtonClick(object sender, RoutedEventArgs e)
		{
			BirthDialog.IsOpen = true;
		}
	}
}
