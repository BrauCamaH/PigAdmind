using System.Windows.Controls;

namespace WpfApp1.Managers
{
	class ContextManager
	{
		public static ContextManager Instance()
		{
			if (_instance == null)
			{
				lock (Padlock)
				{
					if (_instance == null)
					{
						_instance = new ContextManager();
					}
				}
			}
			return _instance;
		}


		private static ContextManager _instance = null;
		private static readonly object Padlock = new object();

		public UserControl CurrentContext { get; set; }
		public UserControl CurrentEditControlContext { get; set; }
		public int CurrentElementSelected { get; set; }
	}
}
