using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Females;
using WpfApp1.Groups;
using WpfApp1.Interfaces;
using WpfApp1.Managers;
using WpfApp1.Sales;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainFemales mainFemales;
        private MainGroups mainGroups;
        private MainSales mainSales;
        public IRemovable CurrentRemovableUc { get; private set; }

        private UserControl _editFemale;
        private UserControl _editGroup;
        private UserControl _editSale;

        private readonly Thickness _initialMargin;
        private readonly Thickness _initialMarginMenuBar;

        public MainFemales MainFemales { get => new MainFemales(); set => mainFemales = value; }
        public MainGroups MainGroups { get => new MainGroups(); set => mainGroups = value; }
        public MainSales MainSales { get => new MainSales(); set => mainSales = value; }

        public MainWindow()
        {
            InitializeComponent();
            SetInitialPage();
            InitializeEditControls();
            MainGridManager.MainWindowGrid = GridMain;
            MenuToolbarManager.Back = BackBtn;
            MenuToolbarManager.Edit = EditBtn;
            MenuToolbarManager.Delete = DeleteBtn;
            ContextManager.Instance().CurrentEditControlContext = _editFemale;

            CurrentRemovableUc = MainFemales;
            _initialMarginMenuBar = MenuBar.Margin;
            _initialMargin = GridMain.Margin;
        }

        private void InitializeEditControls()
        {
            _editFemale = new EditFemale();
            _editGroup = new EditGroup();
            _editSale = new EditSale();
        }

        private void SetInitialPage()
        {
            var usc = new MainFemales();
            GridMain.Children.Add(usc);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            GridMain.Margin = _initialMargin;
            MenuBar.Margin = _initialMarginMenuBar;
            ToolSeparator.Width = 132;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            GridMain.Margin = new Thickness((-_initialMargin.Left) + 10, _initialMargin.Top, _initialMargin.Right, _initialMargin.Bottom);
            MenuBar.Margin = new Thickness((-_initialMarginMenuBar.Left) + 10, _initialMarginMenuBar.Top, _initialMargin.Right, _initialMarginMenuBar.Bottom);
            ToolSeparator.Width = 0;
        }
        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (150 + (60 * index)), 0, 0);
        }


        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;

            GridMain.Children.Clear();
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            MenuToolbarManager.SetEnableEditAndDelete(false);
            MenuToolbarManager.Back.IsEnabled = false;
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Females":
                    usc = MainFemales;
                    CurrentRemovableUc = MainFemales;
                    GridMain.Children.Add(usc);
                    ContextManager.Instance().CurrentEditControlContext = _editFemale;
                    break;
                case "Groups":
                    usc = MainGroups;
                    CurrentRemovableUc = MainGroups;
                    GridMain.Children.Add(usc);
                    ContextManager.Instance().CurrentEditControlContext = _editGroup;
                    break;
                case "Sales":

                    usc = MainSales;
                    //_currentRemovableUC = MainSales;
                    GridMain.Children.Add(usc);
                    ContextManager.Instance().CurrentEditControlContext = _editSale;
                    break;
            }
        }

        private void Females_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LogOut();
            new Login().Show();
            Close();
        }

        private void LogOut()
        {
            var ctx = new Entities();
            var user = ctx.Users.First();
            user.isOnline = 0;
            ctx.SaveChanges();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainGridManager.SetUserControl(ContextManager.Instance().CurrentContext);
            BackBtn.IsEnabled = false;
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            MainDialogGrid.Children.Clear();
            MainDialogGrid.Children.Add(new EditFemale());
            MainDialogHost.IsOpen = true;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            MainDialogGrid.Children.Clear();
            MainDialogGrid.Children.Add(new Delete(CurrentRemovableUc));
            MainDialogHost.IsOpen = true;
        }


        private void MainDialogHost_DialogOpened(object sender, DialogOpenedEventArgs eventargs)
        {
            Window.IsEnabled = false;
        }

        private void MainDialogHost_DialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            Window.IsEnabled = true;
        }
    }
}
