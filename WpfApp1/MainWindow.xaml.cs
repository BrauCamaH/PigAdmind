using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Females;
using WpfApp1.Groups;
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

        private UserControl _editFemale;
        private UserControl _editGroup;
        private UserControl _editSale;

        private readonly Thickness _initialMargin;
        private readonly Thickness _initialMarginMenuBar;

        public MainFemales MainFemales { get => new MainFemales(MainBackButton, MainEditAndDelete); set => mainFemales = value; }
        public MainGroups MainGroups { get => new MainGroups(MainBackButton, MainEditAndDelete); set => mainGroups = value; }
        public MainSales MainSales { get => new MainSales(); set => mainSales = value; }

        public MainWindow()
        {
            InitializeComponent();
            SetInitialPage();
            InitializeEditControls();
            MainGridManager.MainWindowGrid = GridMain;

            _initialMarginMenuBar = MenuBar.Margin;
            _initialMargin = GridMain.Margin;

            var mainDialogHost = new MainDialogHost(this);
        }

        private void InitializeEditControls()
        {
            _editFemale = new EditFemale();
            _editGroup = new EditGroup();
            _editSale = new EditSale();
        }

        private void SetInitialPage()
        {
            var usc = MainFemales;
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

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "Females":
                    usc = MainFemales;
                    MainFemales.FemalesList.SelectedItem = null;
                    break;
                case "Groups":
                    usc = MainGroups;
                    MainGroups.GroupList.SelectedItem = null;
                    break;
                case "Sales":
                    usc = MainSales;

                    break;
            }
            MainGridManager.SetUserControl(usc);
            MainEditAndDelete.IsEnabled = false;
            MainBackButton.IsEnabled = false;
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

    }
}
