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
        private DialogHost _dialogHost;
        private Grid _grid;
        private UserControl _editControl;
        private UserControl _deleteControl;
        public EditAndDelete()
        {
            InitializeComponent();
        }

        public void SetCompleteBehaviour(DialogHost dialog, Grid grid, UserControl editControl, UserControl deleteControl)
        {
            SetDialogHost(dialog, grid);
            SetEditControl(editControl);
            SetDeleteControl(deleteControl);
        }

        public void SetDialogHost(DialogHost dialog, Grid grid)
        {
            _dialogHost = dialog;
            _grid = grid;
        }
        public void SetEditControl(UserControl editControl)
        {
            _editControl = editControl;
        }

        public void SetDeleteControl(UserControl deleteControl)
        {
            _deleteControl = deleteControl;
        }


        public void AddUserControlToDialogHost(UserControl userControl)
        {
            if (userControl != null)
            {
                _dialogHost.IsOpen = true;
                _grid.Children.Clear();
                _grid.Children.Add(userControl);
            }

        }


        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToDialogHost(_editControl);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddUserControlToDialogHost(_deleteControl);
        }
    }
}
