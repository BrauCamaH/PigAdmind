using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Females
{
	/// <summary>
	/// Interaction logic for AddBirth.xaml
	/// </summary>
	public partial class AddBirth : UserControl
	{
		private DatabaseFirst.Females _female;
		public AddBirth()
		{
			InitializeComponent();
		}

		public AddBirth(DatabaseFirst.Females female)
		{
			InitializeComponent();
			_female = female;
		}
		private void Accept_Button_Click(object sender, RoutedEventArgs e)
		{
			var unitOfWork = new UnitOfWork(new Entities());
			var birth = new Births()
			{
				n_piglets = Int32.Parse(N_Pigs_TextBox.Text),
				date = Date.Text,
				fem_code = _female.code
			};
			unitOfWork.Births.Add(birth);
			unitOfWork.Complete();
		}
	}
}
