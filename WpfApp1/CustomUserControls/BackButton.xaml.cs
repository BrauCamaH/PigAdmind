using System.Windows;
using System.Windows.Controls;
using WpfApp1.Managers;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para BackButton.xaml
    /// </summary>
    public partial class BackButton : UserControl
    {
        private UserControl _userControl;
        public BackButton()
        {
            InitializeComponent();
        }

        public void SetActualContext(UserControl userControl)
        {
            _userControl = userControl;
            IsEnabled = true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_userControl != null)
            {
                MainGridManager.SetUserControl(_userControl);
                IsEnabled = false;
            }
        }
    }
}
