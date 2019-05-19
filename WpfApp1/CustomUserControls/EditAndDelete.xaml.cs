using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para EditAndDelete.xaml
    /// </summary>
    public partial class EditAndDelete : UserControl
    {
        public static MainDialogHost DialogHost { get; set; }

        public UserControl EditControl { get; set; }
        public UserControl DeleteControl { get; set; }

        public EditAndDelete()
        {
            InitializeComponent();
        }


        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.SetNewUserControl(EditControl);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.SetNewUserControl(DeleteControl);
        }
    }
}
