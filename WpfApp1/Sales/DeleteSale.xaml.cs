using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.CustomEventArgs;
using WpfApp1.DatabaseFirst;
using WpfApp1.Persistance;

namespace WpfApp1.Sales
{
    /// <summary>
    /// Lógica de interacción para DeleteSale.xaml
    /// </summary>
    public partial class DeleteSale : UserControl
    {
        private DatabaseFirst.Sales _sale;

        public event EventHandler<SalesEventArgs> SaleDeleted;

        public DeleteSale(DatabaseFirst.Sales sale)
        {
            _sale = sale;
            InitializeComponent();
            NotifyUserAgree.AcceptButton = Accept_btn;
        }

        public virtual void OnSaleDeleted(DatabaseFirst.Sales sale)
        {
            SaleDeleted?.Invoke(this, new SalesEventArgs { Sales = sale });
        }

        private void Delete()
        {
            var unitOfWork = new UnitOfWork(new Entities());
            unitOfWork.Sales.RemoveSaleById(_sale.id);
        }
        private void Accept_Btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Delete();
                OnSaleDeleted(_sale);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }
    }
}
