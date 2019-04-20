using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		private readonly SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(1000));
		public Login()
		{
			InitializeComponent();
			//The checbox is checked when the user is online
			CheckBox.IsChecked = App.User.isOnline == 1;
		}

		private void ShowSnackbar(string message)
		{
			messageQueue.Enqueue(message);
			LoginSnackbar.MessageQueue = messageQueue;
		}

		private bool IsTheCorrectPassword(string inputPass)
		{
			var ctx = new Entities();
			var user = ctx.Users.SqlQuery("select * from users").First<Users>();

			var pass = user.password.Replace(" ", "");
			if (pass == inputPass)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (IsTheCorrectPassword(PasswordBox.Password))
			{
				SetIsOnline(CheckBox);
				var window = new MainWindow();
				window.Show();
				Close();
			}
			else
			{
				ShowSnackbar("La contraseña es incorrecta");
			}

		}
		private void SetIsOnline(CheckBox checkBox)
		{
			var ctx = new Entities();
			var user = ctx.Users.First<Users>();
			if (checkBox.IsChecked == true)
			{
				user.isOnline = 1;
			}
			else
			{
				user.isOnline = 0;
			}
			ctx.SaveChanges();
		}

	}
}
