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
			MenuToolbarManager.SetEnableEditAndDelete(true);
		}
	}
}
