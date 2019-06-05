using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.CustomUserControls
{
    /// <summary>
    /// Lógica de interacción para EditAndDelete.xaml
    /// </summary>
    public partial class EditAndDelete : UserControl
    {

        // public static MainDialogHost DialogHost { get; set; }

        public UserControl EditControl { get; set; }
        public UserControl DeleteControl { get; set; }


        public EditAndDelete()
        {
            InitializeComponent();
        }


        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                MainDialogHost.Instance.SetNewUserControl(EditControl);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainDialogHost.Instance.SetNewUserControl(DeleteControl);
        }
    }
}
