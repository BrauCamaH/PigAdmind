using System.Windows.Controls;

namespace WpfApp1.Managers
{
	class MenuToolbarManager
	{
		public static Button Back { get; set; }
		public static Button Edit { get; set; }
		public static Button Delete { get; set; }

		public static void SetEnableEditAndDelete(bool isEnable)
		{
			Edit.IsEnabled = isEnable;
			Delete.IsEnabled = isEnable;
		}
	}
}
