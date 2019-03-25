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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.DatabaseFirst;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for InitialConfig.xaml
	/// </summary>
	public partial class InitialConfig : Window
	{
		public InitialConfig()
		{
			InitializeComponent();
		}
		private void AddUser(string email, String pass)
		{
			var context = new Entities();
			var user = new Users()
			{
				email = email,
				password = pass
			};
			context.Users.Add(user);
			context.SaveChanges();
		}
		private bool ValidatePassword(String pass, string scnpass)
		{
			return pass.Equals(scnpass);
		}
		private void OpenLogIn()
		{
			var login = new Login();
			login.Show();
			Close();
		}
		private void Btn_Click(object sender, RoutedEventArgs e)
		{
			var email = EmailBox.Text;
			var pass = PasswordBox.Password;
			var scnpass = PasswordBox_Copy.Password;
			if (email != "" && pass != "" && ValidatePassword(pass, scnpass))
			{
				AddUser(email, pass);
				OpenLogIn();
			}
			else
			{
				MessageBox.Show("Ingrese todos los datos de forma correcta");
			}

		}
	}
}
