using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
			
			var pass = user.password.Replace(" ","");
			if (pass==inputPass)
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
	}
}
