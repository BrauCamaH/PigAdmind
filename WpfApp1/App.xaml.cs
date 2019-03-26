using System.Linq;
using System.Windows;
using WpfApp1.DatabaseFirst;

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public Users User { get; set; }
		private bool IsAUserAtDatabase()
		{
			var ctx = new Entities();
			User = ctx.Users.SqlQuery("Select * from Users ").FirstOrDefault<Users>();
			if (User != null)
			{
				return true;
			}

			return false;
		}

		private bool IsUserOnline(Users user)
		{
			return (user.isonline == 1);
		}
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			if (IsAUserAtDatabase())
			{
				if (IsUserOnline(User))
				{
					new MainWindow().Show();
				}
				else
				{
					new Login().Show();
				}
			}
			else
			{
				new InitialConfig().Show();
			}
		}
	}
}
