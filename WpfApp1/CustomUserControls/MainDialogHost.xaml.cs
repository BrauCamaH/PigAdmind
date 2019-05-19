using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para MainDialogHost.xaml
    /// </summary>
    public partial class MainDialogHost : UserControl
    {

        private MainWindow _mainWindow;

        private static MainDialogHost _instance = null;
        private static readonly object Padlock = new object();


        public static MainDialogHost Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Padlock)
                    {
                        if (_instance == null)
                            _instance = new MainDialogHost();
                    }
                }

                return _instance;
            }
            set => _instance = value;
        }

        public MainDialogHost()
        {
            InitializeComponent();
            EditAndDelete.DialogHost = this;
        }
        public MainDialogHost(MainWindow window)
        {
            InitializeComponent();
            _mainWindow = window;
            EditAndDelete.DialogHost = this;


            window.DialogGrid.Children.Add(this);

        }

        public void SetNewUserControl(UserControl userControl)
        {
            if (userControl != null)
            {
                DialogHost.IsOpen = true;
                DialogGrid.Children.Clear();
                DialogGrid.Children.Add(userControl);
            }
            else
            {
                MessageBox.Show("UserControl is null");
            }

        }

        private void MainDialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventargs)
        {
            _mainWindow.IsEnabled = false;
        }

        private void MainDialogHost_DialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            _mainWindow.IsEnabled = true;
        }
    }
}
