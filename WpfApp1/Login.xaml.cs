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
		public Login()
		{
			InitializeComponent();
			//The checbox is checked when the user is online
			CheckBox.IsChecked = App.User.isonline == 1;
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
				MessageBox.Show("La contraseña es incorrecta");
			}

		}
		private void SetIsOnline(CheckBox checkBox)
		{
			var ctx = new Entities();
			var user = ctx.Users.First<Users>();
			if (checkBox.IsChecked == true)
			{
				user.isonline = 1;
			}
			else
			{
				user.isonline = 0;
			}
			ctx.SaveChanges();
		}

	}
}
