using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para NotifyUserAgree.xaml
    /// </summary>
    public partial class NotifyUserAgree : UserControl
    {
        public Button AcceptButton { get; set; }
        public NotifyUserAgree()
        {
            InitializeComponent();

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (AcceptButton != null)
            {
                AcceptButton.IsEnabled = true;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (AcceptButton != null)
            {
                AcceptButton.IsEnabled = false;
            }
        }
    }
}
