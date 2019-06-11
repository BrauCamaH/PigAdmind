using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Sales
{
    /// <summary>
    /// Interaction logic for NewSale.xaml
    /// </summary>
    public partial class NewSale : UserControl
    {
        public class NumberOfPigs
        {
            public int N { get; set; }
        }

        private ObservableCollection<NumberOfPigs> _numberOfObservableCollection;
        private ObservableCollection<PigGroups> _groupObservableCollection;


        public event EventHandler<SalesEventArgs> SaleAdded;
        public NewSale()
        {
            InitializeComponent();
            _groupObservableCollection = new ObservableCollection<PigGroups>();
            _numberOfObservableCollection = new ObservableCollection<NumberOfPigs>();

            GroupList.ItemsSource = _groupObservableCollection;
            DataGridNumber.ItemsSource = _numberOfObservableCollection;
        }

        public virtual void OnSaleAdded(DatabaseFirst.Sales sale)
        {
            SaleAdded?.Invoke(this, new SalesEventArgs { Sales = sale });
        }


        private void AddSaleButton_OnClick(object sender, RoutedEventArgs e)
        {
            var unitOfWord = new UnitOfWork(new Entities());

            int ntotal = 0;
            PigGroups group = null;
            NumberOfPigs n = null;

            int i = 0;
            foreach (var item in _groupObservableCollection)
            {
                group = unitOfWord.Groups.Get(item.id);
                n = (NumberOfPigs)DataGridNumber.Items[i];

                group.pig_count -= n.N;

                MessageBox.Show(n.N.ToString());
                ntotal += n.N;
                i++;
            }

            var sale = new DatabaseFirst.Sales
            {
                n_pigs = ntotal,
                price = int.Parse(PriceTextBox.Text),
                date = Date.Text
            };
            unitOfWord.Sales.Add(sale);

            unitOfWord.Complete();

            OnSaleAdded(sale);
        }

        private bool IsACorrectGroup(String text)
        {
            var unitOfWord = new UnitOfWork(new Entities());
            var group = unitOfWord.Groups.SingleOrDefault(g => g.name.Equals(text));
            return @group != null;
        }
        private void GroupTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsACorrectGroup(GroupTextBox.Text))
            {
                AddButton.IsEnabled = true;
            }
            else
            {
                AddButton.IsEnabled = false;
            }
        }
        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var unitOfWord = new UnitOfWork(new Entities());
            var group = unitOfWord.Groups.SingleOrDefault(g => g.name.Equals(GroupTextBox.Text));
            _groupObservableCollection.Add(group);
            _numberOfObservableCollection.Add(new NumberOfPigs { N = 0 });
            GroupTextBox.Text = "";
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            int index = GroupList.SelectedIndex;

            _groupObservableCollection.RemoveAt(index);
            _numberOfObservableCollection.RemoveAt(index);
        }

        private void GroupList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteButton.IsEnabled = GroupList.SelectedItem != null;
        }
    }
}
