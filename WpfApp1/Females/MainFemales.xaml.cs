using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.Females
{
	/// <summary>
	/// Interaction logic for MainFemales.xaml
	/// </summary>
	public partial class MainFemales : UserControl
	{
		public MainFemales()
		{
			InitializeComponent();
			GetFemalesFromDataBase();
		}
		private void AddNewFemale(string code, string birthday)
		{
			var ctx = new Entities();
			var female = new DatabaseFirst.Females()
			{
				code = code,
				birthday = birthday,
				status = "Normal",
				martenity = 0,
				user = 1,
				misbirths = 0

			};
			ctx.Females.Add(female);
			ctx.SaveChanges();
		}
		private bool IsDataComplete(string txtbox, string datePicker)
		{
			if (txtbox != "" && datePicker != "")
			{
				return true;
			}
			return false;
		}

		private void GetFemalesFromDataBase()
		{
			var ctx = new Entities();
			FemalesList.ItemsSource = ctx.Females.ToList();
		}
		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void AddNewFemale_Click(object sender, RoutedEventArgs e)
		{
			string code = CodeBox.Text;
			string birthday = DatePicker.Text;
			if (IsDataComplete(code, birthday))
			{
				AddNewFemale(code, birthday);
				GetFemalesFromDataBase();
			}

		}
	}
}
