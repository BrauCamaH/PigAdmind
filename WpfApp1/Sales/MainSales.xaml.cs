using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using WpfApp1.Managers;

namespace WpfApp1.Sales
{
	/// <summary>
	/// Interaction logic for MainSales.xaml
	/// </summary>
	public partial class MainSales : UserControl
	{
		public MainSales()
		{
			InitializeComponent();
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void NewSaleButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NewSaleDialogHost.IsOpen = true;
			NewSale.IsEnabled = false;
		}

		private void NewSaleDialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
		{
			NewSale.IsEnabled = true;
		}
	}
}
