using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Sales
{
    /// <summary>
    /// Interaction logic for EditSale.xaml
    /// </summary>
    public partial class EditSale : UserControl
    {

        private DatabaseFirst.Sales _sale;

        public event EventHandler<SalesEventArgs> SaleEdited;

        public EditSale()
        {
            InitializeComponent();
        }

        public EditSale(DatabaseFirst.Sales sale)
        {
            InitializeComponent();
            _sale = sale;

            UserAgree.AcceptButton = Accept_btn;

            GetSale();
        }
        public virtual void OnSaleEdited(DatabaseFirst.Sales sale)
        {
            SaleEdited?.Invoke(this, new SalesEventArgs { Sales = sale });
        }

        private void GetSale()
        {
            PriceTextBox.Text = _sale.price.ToString();
            DatePicker.Text = _sale.date;
        }

        private void EditSaleFromDatabase(string newPrice, string newDate)
        {
            var unitOfWork = new UnitOfWork(new Entities());
            var sale = unitOfWork.Sales.Get(_sale.id);
            sale.price = int.Parse(newPrice);
            sale.date = newDate;
            unitOfWork.Complete();

            OnSaleEdited(sale);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Accept_btn_OnClick(object sender, RoutedEventArgs e)
        {
            EditSaleFromDatabase(PriceTextBox.Text, DatePicker.Text);
        }
    }
}
