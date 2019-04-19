using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Managers;

namespace WpfApp1.Groups
{
	/// <summary>
	/// Interaction logic for MainGroups.xaml
	/// </summary>
	public partial class MainGroups : UserControl
	{
		public MainGroups()
		{
			InitializeComponent();
			GetGroupsFromDataBase();
		}
		private bool IsDataComplete(string txtbox, string datePicker)
		{
			if (txtbox != "" && datePicker != "")
			{
				return true;
			}
			return false;
		}
		private void GetGroupsFromDataBase()
		{
			var ctx = new Entities();
			GroupList.ItemsSource = ctx.PigGroups.ToList();
		}

		private void AddNewGroup(string id, float weigth, int n, string date)
		{
			var ctx = new Entities();
			var pigGroup = new DatabaseFirst.PigGroups()
			{
				weaning_date = date,
				weigth_avg = weigth,
				second_avg = 0,
				lastWeigth_avg = 0,
				pig_count = n,
				died_pigs = 0,
				user = 1,
				name = id
			};
			ctx.PigGroups.Add(pigGroup);
			ctx.SaveChanges();
		}

		private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			MenuToolbarManager.SetEnableEditAndDelete(true);
		}
		private void OnListMouseDoubleClick(object sender, RoutedEventArgs e)
		{
			MainGridManager.SetUserControl(new GroupPage());
			MenuToolbarManager.SetEnableEditAndDelete(false);
			MenuToolbarManager.Back.IsEnabled = true;
		}

		private void ClearFields()
		{
			NPigsBox.Text = "";
			WeigthAVGBox.Text = "";
			DatePicker.Text = "";
			IdBox.Text = "";
		}

		private void AddNewGroup_OnClick(object sender, RoutedEventArgs e)
		{
			int nPigs = Int32.Parse(NPigsBox.Text);
			float weigth = float.Parse(WeigthAVGBox.Text);
			string weanigDate = DatePicker.Text;
			string id = IdBox.Text;
			if (IsDataComplete(NPigsBox.Text, weanigDate))
			{
				AddNewGroup(id, weigth, nPigs, weanigDate);
				ClearFields();
				GetGroupsFromDataBase();
			}

		}


	}
}
