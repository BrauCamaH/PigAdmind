using System.Windows.Controls;

namespace WpfApp1.Managers
{
	class MainGridManager
	{
		public static Grid MainWindowGrid { get; set; }
		public static void SetUserControl(UserControl usc)
		{
			MainWindowGrid.Children.Clear();
			MainWindowGrid.Children.Add(usc);
		}
	}
}
