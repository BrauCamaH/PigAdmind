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
				var window = new MainWindow();
				window.Show();
				Close();
			}
			else
			{
				MessageBox.Show("La contraseña es incorrecta");
			}

		}

		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			var ctx = new Entities();
			var user = ctx.Users.SqlQuery("Select * from Users ").FirstOrDefault();
			user.isonline = 1;
			ctx.SaveChanges();
			MessageBox.Show("");
		}
		private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
		{

		}
	}
}
