using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public DatabaseFirst.Sales CurrentSale { get; set; }

        private ObservableCollection<DatabaseFirst.Sales> _salesObservableCollection;

        private EditAndDelete _editAndDelete;

        private EditSale _editSale;
        private DeleteSale _deleteSale;

        public MainSales(EditAndDelete editAndDelete)
        {
            _editAndDelete = editAndDelete;
            _salesObservableCollection = new ObservableCollection<DatabaseFirst.Sales>();
            InitializeComponent();
            GetItemsFromDatabase();

            SearchTextBox.SetView(SalesListView, CustomFilter);
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _editAndDelete.IsEnabled = SalesListView.SelectedItem != null;
            DatabaseFirst.Sales sale = (DatabaseFirst.Sales)SalesListView.SelectedItem;
            CurrentSale = sale;
            var unitOfWork = new UnitOfWork(new Entities());
            if (sale != null) InitializeCrudControls(unitOfWork.Sales.Get(sale.id));
        }

        private void NewSaleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                var addUC = new NewSale();

                MainDialogHost.Instance.SetNewUserControl(addUC);

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
            _deleteSale = new DeleteSale(entity);
            _editSale = new EditSale(entity);

            _editAndDelete.DeleteControl = _deleteSale;
            _editAndDelete.EditControl = _editSale;


            _deleteSale.SaleDeleted += OnItemDeleted;
            _editSale.SaleEdited += OnItemEdited;
        }

        public bool CustomFilter(object obj)
        {
            if (String.IsNullOrEmpty(SearchTextBox.TextBox.Text))
                return true;
            else
            {
                return (((DatabaseFirst.Sales)obj).date.IndexOf(SearchTextBox.TextBox.Text,
                            StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Sales)obj).price.ToString().IndexOf(SearchTextBox.TextBox.Text,
                            StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (((DatabaseFirst.Sales)obj).n_pigs.ToString().IndexOf(SearchTextBox.TextBox.Text,
                            StringComparison.OrdinalIgnoreCase) >= 0);
            }
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
            _salesObservableCollection.Remove(_salesObservableCollection.Single(i => i.id == CurrentSale.id));
        }

        public void OnItemEdited(object sender, SalesEventArgs e)
        {
            _salesObservableCollection[_salesObservableCollection.IndexOf(CurrentSale)] = e.Sales;
        }
    }
}
