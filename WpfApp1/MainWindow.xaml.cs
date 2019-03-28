using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Females;
using WpfApp1.Groups;
using WpfApp1.Managers;
using WpfApp1.Sales;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			SetInitialPage();
			MainGridManager.MainWindowGrid = GridMain;
			MenuToolbarManager.Back = BackBtn;
			MenuToolbarManager.Edit = EditBtn;
			MenuToolbarManager.Delete = DeleteBtn;
		}

		private void SetInitialPage()
		{
			var usc = new MainFemales();
			GridMain.Children.Add(usc);
		}

		private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Visible;
			ButtonOpenMenu.Visibility = Visibility.Collapsed;
		}

		private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Collapsed;
			ButtonOpenMenu.Visibility = Visibility.Visible;
		}
		private void MoveCursorMenu(int index)
		{
			TrainsitionigContentSlide.OnApplyTemplate();
			GridCursor.Margin = new Thickness(0, (150 + (60 * index)), 0, 0);
		}


		private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UserControl usc = null;
			GridMain.Children.Clear();
			int index = ListViewMenu.SelectedIndex;
			MoveCursorMenu(index);
			switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
			{
				case "Females":
					usc = new MainFemales();
					GridMain.Children.Add(usc);
					break;
				case "Groups":
					usc = new MainGroups();
					GridMain.Children.Add(usc);
					break;
				case "Sales":
					usc = new MainSales();
					GridMain.Children.Add(usc);
					break;
			}
		}

		private void Females_Selected(object sender, RoutedEventArgs e)
		{

		}

		private void Exit(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}

		private void LogOut(object sender, RoutedEventArgs e)
		{
			LogOut();
			new Login().Show();
			Close();
		}

		private void LogOut()
		{
			var ctx = new Entities();
			var user = ctx.Users.First();
			user.isonline = 0;
			ctx.SaveChanges();
		}

		private void BackBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void EditBtn_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
