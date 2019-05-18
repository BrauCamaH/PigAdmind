using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para EditAndDelete.xaml
    /// </summary>
    public partial class EditAndDelete : UserControl
    {
        public static DialogHost DialogHost { get; set; }
        public static Grid DialogGrid { get; set; }

        public UserControl EditControl { get; set; }
        public UserControl DeleteControl { get; set; }

        public EditAndDelete()
        {
            InitializeComponent();
        }

        public void AddUserControlToDialogHost(UserControl userControl)
        {
            if (userControl != null)
            {
                DialogHost.IsOpen = true;
                DialogGrid.Children.Clear();
                DialogGrid.Children.Add(userControl);
            }

        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToDialogHost(EditControl);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToDialogHost(DeleteControl);
        }
    }
}
