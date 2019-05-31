using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Forms;
using WpfApp1.CustomEventArgs;
using WpfApp1.CustomUserControls;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;
using UserControl = System.Windows.Controls.UserControl;

namespace WpfApp1.Sales
{
    /// <summary>
    /// Interaction logic for MainSales.xaml
    /// </summary>
    public partial class MainSales : UserControl, IPigAdmindPage<DatabaseFirst.Sales, SalesEventArgs>
    {

        public DatabaseFirst.Sales Sale { get; set; }

        private ObservableCollection<DatabaseFirst.Sales> _salesObservableCollection;

        private EditAndDelete _editAndDelete;

        public MainSales(EditAndDelete editAndDelete)
        {
            _editAndDelete = editAndDelete;
            _salesObservableCollection = new ObservableCollection<DatabaseFirst.Sales>();
            InitializeComponent();

            GetItemsFromDatabase();


        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NewSaleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var addUC = new NewSale();
                NewSaleDialogHost.IsOpen = true;
                DialogGrid.Children.Clear();
                DialogGrid.Children.Add(addUC);
                addUC.SaleAdded += OnItemAdded;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }


        }

        private void NewSaleDialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            NewSale.IsEnabled = true;
        }

        public void InitializeCrudControls(DatabaseFirst.Sales entity)
        {

        }

        public bool CustomFilter(object obj)
        {
            throw new System.NotImplementedException();
        }

        public void GetItemsFromDatabase()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            CrudOperations<DatabaseFirst.Sales>.AddRange(_salesObservableCollection, unitOfWork.Sales.GetAll());
            SalesListView.ItemsSource = _salesObservableCollection;
        }

        public void OnItemAdded(object sender, SalesEventArgs e)
        {
            _salesObservableCollection.Add(e.Sales);
        }

        public void OnItemDeleted(object sender, SalesEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void OnItemEdited(object sender, SalesEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
