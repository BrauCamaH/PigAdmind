using MaterialDesignThemes.Wpf;
using System.Windows.Controls;

namespace WpfApp1.Managers
{
    public class DialogHostManager
    {
        private static readonly object padlock = new object();
        private static DialogHostManager _instance = null;
        public static DialogHostManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DialogHostManager();
                    }
                    return _instance;
                }
            }
        }

        public DialogHost MainDialogHost { get; set; }
        public Grid MainDialogHostGrid { get; set; }


        public void SetUserControl(UserControl userControl)
        {
            MainDialogHost.IsOpen = true;
            MainDialogHostGrid.Children.Clear();
            MainDialogHostGrid.Children.Add(userControl);
        }


    }
}
