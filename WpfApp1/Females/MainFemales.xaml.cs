using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.DatabaseFirst;
using WpfApp1.Managers;
using WpfApp1.Persistance;

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
			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(FemalesList.ItemsSource);
			view.Filter = CustomFilter;
		}
		private void AddNewFemale(string code, string birthday)
		{
			UnitOfWork unitOfWork = new UnitOfWork(new Entities());
			var female = new DatabaseFirst.Females()
			{
				code = code,
				birthday = birthday,
				status = "Normal",
				martenity = 0,
				user = 1,
				misbirths = 0

			};
			unitOfWork.Females.Add(female);
			unitOfWork.Complete();
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
			MenuToolbarManager.SetEnableEditAndDelete(true);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}

		private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
		{
			MainGridManager.SetUserControl(new FemalePage());
			MenuToolbarManager.SetEnableEditAndDelete(false);
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

		private bool CustomFilter(object obj)
		{
			if (String.IsNullOrEmpty(TextBox.Text))
				return true;
			else
				return (((DatabaseFirst.Females)obj).code.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
					   (((DatabaseFirst.Females)obj).birthday.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
					   (((DatabaseFirst.Females)obj).martenity.ToString().IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
					   (((DatabaseFirst.Females)obj).status.IndexOf(TextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			CollectionViewSource.GetDefaultView(FemalesList.ItemsSource).Refresh();
		}
	}
}
