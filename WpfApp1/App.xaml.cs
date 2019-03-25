using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.DatabaseFirst;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private bool IsAUserAtDatabase()
		{
			var ctx = new Entities();
			var user = ctx.Users.SqlQuery("Select * from Users ").FirstOrDefault<Users>();
			if (user != null)
			{
				return true;
			}

			return false;
		}
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			if (IsAUserAtDatabase())
			{
				new Login().Show();
			}
			else
			{
				new InitialConfig().Show();
			}
		}
	}
}
